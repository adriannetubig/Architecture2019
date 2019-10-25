CREATE TABLE [dbo].[ExceptionLog]
(
	   [ExceptionLogId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    [ApplicationName] VARCHAR(MAX) NULL, 
    [StackTraceString] VARCHAR(MAX) NULL, 
    [Source] VARCHAR(MAX) NULL, 
    [Message] VARCHAR(MAX) NULL, 
    [HResult] INT NULL,
    [CreatedDateUtc] DATETIME NOT NULL
)
