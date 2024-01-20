using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ClientDashboardAPI.DTOs;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Microsoft.Extensions.Logging;

namespace ClientDashboardAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string connString;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IConfiguration configuration, ILogger<ClientController> logger)
        {
            _configuration = configuration;
            _logger = logger;
            var host = _configuration["DBHOST"] ?? "localhost";
            var port = _configuration["DBPORT"] ?? "3306";
            var password = _configuration["MYSQL_PASSWORD"] ?? _configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = _configuration["MYSQL_USER"] ?? _configuration.GetConnectionString("MYSQL_USER");
            var clientDashboardDatabase = _configuration["MYSQL_DATABASE"] ?? _configuration.GetConnectionString("MYSQL_DATABASE");

            connString = $"server={host}; userid={userid};pwd={password};port={port};database={clientDashboardDatabase}";
        }


		/*******************************************************************************************
		* Dapper is used for our ORM here. Check it out at https://dapper-tutorial.net/dapper
		********************************************************************************************/
		
        [HttpGet("GetAllClients")]
        public async Task<ActionResult<List<Client>>> GetAllClients()
        {
            var clients = new List<Client>();
            try
            {
                string query = @"SELECT 
                                    c.*,
                                    GROUP_CONCAT(apn.PhoneNumber) AS AlternativeNumbers
                                FROM 
                                    Clients c
                                LEFT JOIN 
                                    ClientAlternativePhoneNumbers capn ON c.Id = capn.ClientID
                                LEFT JOIN 
                                    AlternativePhoneNumbers apn ON capn.AlternativePhoneNumberID = apn.Id
                                WHERE 
                                    c.Archive = false
                                GROUP BY 
                                    c.Id;";

                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<Client, string, Client>(
                        query,
                        (client, alternativeNumbers) =>
                        {
                            if (alternativeNumbers != null)
                            {
                                client.AlternativeNumbers = alternativeNumbers.Split(',').ToList();
                            }
                            return client;
                        },
                        splitOn: "AlternativeNumbers",
                        commandType: CommandType.Text
                    );

                    clients = result.ToList();
                }

                return Ok(clients);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unable To Process Request: " + e.ToString());
            }
        }


        [HttpPost("CreateClient")]
        public async Task<ActionResult<Client>> CreateClient(Client newClient)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    // insert the client information
                    string insertClientQuery = @"INSERT INTO Clients (FirstName, LastName, PrimaryPhoneNumber, Email, Archive) 
                                                VALUES (@FirstName, @LastName, @PrimaryPhoneNumber, @Email, @Archive);
                                                SELECT LAST_INSERT_ID();";

                    using (var connection = new MySqlConnection(connString))
                    {
                        var clientId = await connection.ExecuteScalarAsync<int>(insertClientQuery, newClient, null);
                        newClient.Id = clientId;
                    }

                    // insert the alternative phone numbers and associate them with the client
                    string insertAlternativePhoneNumbersQuery = @"INSERT INTO AlternativePhoneNumbers (PhoneNumber) 
                                                                VALUES (@PhoneNumber);
                                                                SELECT LAST_INSERT_ID();";

                    string insertClientAlternativeNumbersQuery = @"INSERT INTO ClientAlternativePhoneNumbers (ClientID, AlternativePhoneNumberID) 
                                                                VALUES (@ClientID, @AlternativePhoneNumberID);";

                    if (newClient.AlternativeNumbers != null && newClient.AlternativeNumbers.Any())
                    {
                        foreach (var phoneNumber in newClient.AlternativeNumbers)
                        {
                            int alternativePhoneNumberId;

                            // insert alternative phone number
                            using (var connection = new MySqlConnection(connString))
                            {
                                alternativePhoneNumberId = await connection.ExecuteScalarAsync<int>(insertAlternativePhoneNumbersQuery, new { PhoneNumber = phoneNumber }, null);
                            }

                            // associate alternative phone number with the client
                            using (var connection = new MySqlConnection(connString))
                            {
                                await connection.ExecuteAsync(insertClientAlternativeNumbersQuery, new { ClientID = newClient.Id, AlternativePhoneNumberID = alternativePhoneNumberId }, null);
                            }
                        }
                    }

                    // commit the transaction
                    transactionScope.Complete();

                    return Ok(newClient);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Unable To Process Request: " + e.ToString());
                }
            }
        }

        [HttpPut("UpdateClient")]
        public async Task<ActionResult<Client>> UpdateClient(Client updatedClient)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    // update the client information
                    string updateClientQuery = @"UPDATE Clients 
                                                SET FirstName = @FirstName, 
                                                    LastName = @LastName, 
                                                    PrimaryPhoneNumber = @PrimaryPhoneNumber, 
                                                    Email = @Email, 
                                                    Archive = @Archive
                                                WHERE Id = @Id;";

                    using (var connection = new MySqlConnection(connString))
                    {
                        await connection.ExecuteAsync(updateClientQuery, updatedClient, null);
                    }

                    // remove existing alternative phone numbers association
                    string deleteClientAlternativeNumbersQuery = @"DELETE FROM ClientAlternativePhoneNumbers 
                                                                WHERE ClientID = @ClientID;";

                    using (var connection = new MySqlConnection(connString))
                    {
                        await connection.ExecuteAsync(deleteClientAlternativeNumbersQuery, new { ClientID = updatedClient.Id }, null);
                    }

                    // insert the updated alternative phone numbers and associate them with the client
                    string insertAlternativePhoneNumbersQuery = @"INSERT INTO AlternativePhoneNumbers (PhoneNumber) 
                                                                VALUES (@PhoneNumber);
                                                                SELECT LAST_INSERT_ID();";

                    string insertClientAlternativeNumbersQuery = @"INSERT INTO ClientAlternativePhoneNumbers (ClientID, AlternativePhoneNumberID) 
                                                                VALUES (@ClientID, @AlternativePhoneNumberID);";

                    if (updatedClient.AlternativeNumbers != null && updatedClient.AlternativeNumbers.Any())
                    {
                        foreach (var phoneNumber in updatedClient.AlternativeNumbers)
                        {
                            int alternativePhoneNumberId;

                            // insert alternative phone number
                            using (var connection = new MySqlConnection(connString))
                            {
                                alternativePhoneNumberId = await connection.ExecuteScalarAsync<int>(insertAlternativePhoneNumbersQuery, new { PhoneNumber = phoneNumber }, null);
                            }

                            // associate alternative phone number with the client
                            using (var connection = new MySqlConnection(connString))
                            {
                                await connection.ExecuteAsync(insertClientAlternativeNumbersQuery, new { ClientID = updatedClient.Id, AlternativePhoneNumberID = alternativePhoneNumberId }, null);
                            }
                        }
                    }

                    // commit the transaction
                    transactionScope.Complete();

                    return Ok(updatedClient);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Unable To Process Request: " + e.ToString());
                }
            }
        }

        [HttpPut("ArchiveClient/{clientId}")]
        public async Task<ActionResult> ArchiveClient(int clientId)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    // archive the client
                    string archiveClientQuery = @"UPDATE Clients 
                                                SET Archive = true 
                                                WHERE Id = @ClientId;";

                    using (var connection = new MySqlConnection(connString))
                    {
                        await connection.ExecuteAsync(archiveClientQuery, new { ClientId = clientId }, null);
                    }

                    // remove existing alternative phone numbers association
                    string deleteClientAlternativeNumbersQuery = @"DELETE FROM ClientAlternativePhoneNumbers 
                                                                WHERE ClientID = @ClientID;";

                    using (var connection = new MySqlConnection(connString))
                    {
                        await connection.ExecuteAsync(deleteClientAlternativeNumbersQuery, new { ClientID = clientId }, null);
                    }

                    // commit the transaction
                    transactionScope.Complete();

                    return Ok();
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Unable To Process Request: " + e.ToString());
                }
            }
        }
    }
}