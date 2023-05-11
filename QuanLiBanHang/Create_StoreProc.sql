
CREATE PROC SP_Delete_Customer(@id int)
as
begin try
begin transaction
	delete from LineItem
	where LineItem.Order_id=(select	Ordr.order_id						
							from Orders Ordr
							where Ordr.customer_id=@id)
	delete from Orders where Order.order_id =(select Ordr.order_id						
						from Orders Ordr
						where Ordr.customer_id=@id))
	delete from Customer where customer_id = @id
commit
end try
begin catch
	if(@@TRANCOUNT>0)
		rollback tran
end catch
go




CREATE PROC SP_Update_Customer(@id int, @name nvarchar(100))
as
update Customer
set customer_name=@name
where customer_id=@id
go
