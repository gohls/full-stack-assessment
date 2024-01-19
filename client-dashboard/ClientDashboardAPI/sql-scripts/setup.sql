CREATE TABLE ClientDashboardDB.Clients (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    Email VARCHAR(255),
    LastUpdated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

CREATE TABLE ClientDashboardDB.AlternativePhoneNumbers (
    id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
    PhoneNumber VARCHAR(20) NOT NULL
);

CREATE TABLE ClientDashboardDB.ClientAlternativePhoneNumbers (
    ClientID INT UNSIGNED,
    AlternativePhoneNumberID INT UNSIGNED,
    PRIMARY KEY (ClientID, AlternativePhoneNumberID),
    FOREIGN KEY (ClientID) REFERENCES ClientDashboardDB.Clients(id),
    FOREIGN KEY (AlternativePhoneNumberID) REFERENCES ClientDashboardDB.AlternativePhoneNumbers(id)
);
