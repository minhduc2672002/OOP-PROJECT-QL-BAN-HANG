CREATE DATABASE SMS
GO

USE SMS
GO

-- Tao table
CREATE TABLE Customer
(
	customer_id int identity(1,1),
	customer_name nvarchar(100)
)
GO

CREATE TABLE Employee
(
	employee_id int identity(1,1),
	employee_name nvarchar(100),
	salary float,
	supervisor_id int
)
GO

CREATE TABLE Product
(
	product_id int identity(1,1),
	product_name nvarchar(100),
	product_price float
)
GO

CREATE TABLE Orders
(
	order_id int identity(1, 1),
	order_date datetime,
	customer_id int not null,
	employee_id int not null,
	total float
)
GO

CREATE TABLE LineItem
(
	order_id int not null,
	product_id int not null,
	quantity int,
	price float
)
GO

-- Tao khoa cho database
ALTER TABLE Customer ADD PRIMARY KEY(customer_id)
ALTER TABLE Employee ADD PRIMARY KEY(employee_id)
ALTER TABLE Product ADD PRIMARY KEY(product_id)
ALTER TABLE Orders ADD PRIMARY KEY(order_id)
GO

ALTER TABLE Orders ADD FOREIGN KEY(customer_id) REFERENCES Customer(customer_id)
ALTER TABLE Orders ADD FOREIGN KEY(employee_id) REFERENCES Employee(employee_id)
ALTER TABLE LineItem ADD FOREIGN KEY(order_id) REFERENCES Orders(order_id)
ALTER TABLE LineItem ADD FOREIGN KEY(product_id) REFERENCES Product(product_id)
GO

INSERT INTO [Customer](customer_name) VALUES
(N'Chị Dậu'),
(N'Anh Phèo'),
(N'Lão Hạc'),
(N'Bá Kiến'),
(N'Ông Giáo')
Go

INSERT INTO [Employee](employee_name, salary, supervisor_id) VALUES
(N'Minh Phúc', 1000, 2),
(N'Đỗ Thịnh', 1700, 1),
(N'Hải Triều', 1200, 1)
GO

INSERT INTO [Product](product_name, product_price) VALUES
(N'Thế Giới Otome Game Thật Khắc Nghiệt Với Nhân Vật Quần Chúng - Tập 3', 179000),
(N'[Manga] Diệt Slime Suốt 300 Năm, Tôi Levelmax Lúc Nào Chẳng Hay (Tập 6)', 48000),
(N'Anh Hùng Diệt Thần & Bảy Thệ Ước - Tập 1', 139000),
(N'Ma Vương Kiến Tạo - Hầm Ngục Kiên Cố Nhất Chính Là Thành Phố Hiện Đại (Tập 7)',139000),
(N'Demian: Câu Chuyện Tuổi Trẻ Của Emil Sinclair',119000)
GO

INSERT INTO [Orders](order_date, customer_id, employee_id, total) VALUES
('20211122', 2, 1, 0),
('20211120', 4, 1, 0),
('20211125', 5, 2, 0)

INSERT INTO [LineItem](order_id ,product_id, quantity, price) VALUES
(1, 2, 1, 48000),
(1, 3, 2, 139000),
(1, 5, 1, 119000),
(2, 1, 1, 179000),
(2, 3, 1, 139000),
(3, 4, 3, 139000)
GO