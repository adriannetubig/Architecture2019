CREATE TABLE [dbo].[Anagram]
(
	AnagramId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FirstWord VARCHAR(50),
	SecondWord VARCHAR(50),
	IsAnagram BIT
)