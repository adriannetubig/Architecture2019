create procedure [dbo].[GetFibonaccisPaged]
    @Offset int,
    @Fetch int
as
begin
  select FibonacciId,
         Iterations,
         Total
    from [Fibonacci]
order by FibonacciId
  offset @Offset rows
   fetch next @fetch rows only

   select count(0)
    from [Fibonacci] u

END
