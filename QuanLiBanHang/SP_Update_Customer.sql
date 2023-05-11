CREATE PROC SP_Update_Customer(@id int, @name nvarchar(100))
as
update Customer
set customer_name=@name
where customer_id=@id
go