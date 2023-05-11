CREATE PROC SP_Delete_Customer(@id int)
as
begin try
begin transaction
	delete from LineItem
	where LineItem.Order_id=(select	Ordr.order_id						
							from Orders Ordr
							where Ordr.customer_id=@id)
	delete from Orders where customer_id = @id
	delete from Customer where customer_id = @id
commit
end try
begin catch
	if(@@TRANCOUNT>0)
		rollback tran
end catch
go