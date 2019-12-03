CREATE TABLE [dbo].[Anagram]
(
	AnagramId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	AnagramWord VARCHAR(50),
	IsAnagram BIT
)