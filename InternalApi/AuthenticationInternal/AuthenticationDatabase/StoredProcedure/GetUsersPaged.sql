create procedure [dbo].[GetUsersPaged]
    @Offset int,
    @Fetch int
as
begin
  select u.UserId,
         u.RoleId,
         u.Username,
         u.[Password],
         r.RoleName
    from [User] u
    join [Role] r
      on u.RoleId = r.RoleId
order by u.UserId
  offset @Offset rows
   fetch next @fetch rows only

   select count(0)
    from [User] u
    join [Role] r
      on u.RoleId = r.RoleId

END
