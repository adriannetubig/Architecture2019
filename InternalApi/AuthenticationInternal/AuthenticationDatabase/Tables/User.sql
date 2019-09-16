﻿CREATE TABLE [dbo].[User]
(
    UserId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    RoleId INT NOT NULL,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(250) NOT NULL,

    FOREIGN KEY (RoleId) REFERENCES Role(RoleId)
)