/*
Post-Deployment Script Template       
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.  
 Use SQLCMD syntax to include a file in the post-deployment script.   
 Example:      :r .\myfile.sql        
 Use SQLCMD syntax to reference a variable in the post-deployment script.  
 Example:      :setvar TableName MyTable       
               SELECT * FROM [$(TableName)]     
--------------------------------------------------------------------------------------
*/
IF NOT EXISTS(SELECT 1 FROM Role)
BEGIN
    INSERT INTO Role
        (RoleName)
    VALUES
        ('Admin')
END

IF NOT EXISTS(SELECT 1 FROM [User])
BEGIN
    DECLARE @roleId INT = (SELECT TOP 1 RoleId FROM Role WHERE RoleName = 'Admin')
    INSERT INTO [User]
        (Username,
        Password,
        RoleId)
    VALUES
        ('admin',
        '$2b$10$BMH23FkUuDMDtMsLBEbb7u7AJ/eYnV5MbJ.or6hYSnRtfDAuAzTpS',
        @roleId)
END