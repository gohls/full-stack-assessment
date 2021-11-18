using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var users = new List<Client>();
            try
            {
                string query = @"SELECT * FROM Clients";
                using (var connection = new MySqlConnection(connString))
                {
                    var result = await connection.QueryAsync<Client>(query, CommandType.Text);
                    users = result.ToList();
                }
                
				return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unable To Process Request: " + e.ToString());
            }
        }
    }
}