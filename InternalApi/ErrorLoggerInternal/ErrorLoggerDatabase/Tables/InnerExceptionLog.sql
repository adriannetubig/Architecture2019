CREATE TABLE [dbo].[InnerExceptionLog]
(
	   [InnerExceptionLogId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
	   [ExceptionLogId] INT NOT NULL, 
    [StackTrace] VARCHAR(MAX) NULL, 
    [Source] VARCHAR(MAX) NULL, 
    [Message] VARCHAR(MAX) NULL, 
    [HResult] INT NULL,
    [CreatedDateUtc] DATETIME NOT NULL
)