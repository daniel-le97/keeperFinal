-- Active: 1666715470309@@SG-flat-record-3002-6839-mysql-master.servers.mongodirector.com@3306@garbageDemo

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg VARCHAR(500),
    ) default charset utf8 COMMENT '';
ALTER TABLE accounts ADD coverImg VARCHAR(500);
CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL primary key AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) NOT NULL,
        description varchar(255) NOT NULL,
        coverImg varchar(500) NOT NULL,
        isPrivate BOOLEAN DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE 
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL primary key AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255),
        description varchar(500),
        img varchar(500),
        views INT,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId varchar(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';


    DROP Table accounts;
    DROP Table vaults;
    DROP Table keeps;
    DROP Table vaultKeeps;