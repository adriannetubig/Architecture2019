CREATE TABLE [dbo].[RefreshToken]
(
	UserId INT,
	RefreshTokenId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,

	Token VARCHAR(250),

    FOREIGN KEY (UserId) REFERENCES [User](UserId)
)