CREATE TABLE [dbo].[Fibonacci]
(
	FibonacciId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	Iterations INT,
	Total INT,

	Token VARCHAR(250)
)