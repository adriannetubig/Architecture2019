CREATE TABLE [dbo].[Palindrome]
(
	PalindromeId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	PalindromeText VARCHAR(MAX),
	IsPalindrome BIT
)