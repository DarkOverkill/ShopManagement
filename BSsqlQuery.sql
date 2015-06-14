create database TestShop_2

use TestShop_2

CREATE TABLE Producers(
	Id int not null identity(1, 1),
	Name nvarchar(64) default 'unknown',
	[Address] nvarchar(128) default 'unkown',
	Phone int

	constraint pk_producer_id primary key(Id)
);

CREATE TABLE Categoryes(
	Id int not null identity(1, 1),
	Name nvarchar(128) default 'unknown',
	ParentId int

	constraint pk_category_id primary key(Id),

	constraint fk_categoryes_parentId_id 
	foreign key (ParentId) references Categoryes(Id)
);

CREATE TABLE  Products(
	Id int not null identity(1, 1),
	Name nvarchar(64) default 'unknown',
	CategoryId int,
	ProducerId int

	constraint pk_product_id primary key(Id),

	constraint fk_product_categoryId_categoryes_id
	foreign key (CategoryId) references Categoryes(Id),

	constraint fk_product_producerId_droducers_id
	foreign key (ProducerId) references Producers(Id)
);

CREATE TABLE Measures(
	Id int not null identity(1, 1),
	ShortName nvarchar(32) default 'unknown',
	Name nvarchar(128) default 'unknown'

	constraint fk_measure_id primary key(Id)
);

CREATE TABLE Packeges(
	Id int not null identity(1, 1),
	ProductId int,
	VolumeMeasureId int,
	MeasureId int,
	Volume numeric

	constraint pk_packege_id primary key(Id),

	constraint fk_packege_productId_product_id
	foreign key (ProductId) references Products(Id),

	constraint fk_packege_volumeMeasureId_measure_id
	foreign key (VolumeMeasureId) references Measures(Id),
	
	constraint fk_packege_measureId_measure_id
	foreign key (MeasureId) references Measures(Id),
);
--drop table Price
CREATE TABLE Price(
	Id int not null identity(1, 1),
	PackegeId int,
	Cost numeric,
	[Date] datetime

	constraint pk_price_id primary key(Id),

	constraint fk_price_packegeId_packege_id
	foreign key (PackegeId) references Packeges(Id)
);

--select * from Packeges

CREATE TABLE Customers(
	Id int not null identity(1, 1),
	Name nvarchar(128) not null,
	[Address] nvarchar(128) default 'unknown',
	Phone nvarchar(32) default 'unknown'

	constraint pk_customer_id primary key(Id),
);

CREATE TABLE InputOrders(
	Id int not null identity(1, 1),
	CustomerId int,
	CreationTime datetime,
	Cost numeric not null,
	[status] int not null

	constraint pk_inputOrder_id primary key(Id),

	constraint fk_inputOrder_customerId_customer_id
	foreign key (CustomerId) references Customers(Id)
);

CREATE TABLE OutputOrders(
	Id int not null identity(1, 1),
	CustomerId int,
	CreationTime datetime,
	Cost numeric not null,
	[status] int not null

	constraint pk_outputOrder_id primary key(Id),

	constraint fk_outputOrder_customerId_customer_id
	foreign key (CustomerId) references Customers(Id)
);

CREATE TABLE InputOrderItems(
	Id int not null identity(1, 1),
	InputOrderId int,
	PackegeId int,
	Quantity int not null

	constraint pk_inputOrderItem_id primary key(Id),

	constraint fk_inputOrderItem_inputOrderId_inputOrder_Id
	foreign key (InputOrderId) references InputOrders(Id),

	constraint fk_inputOrderItem_packegeId_packege_id
	foreign key (PackegeId) references Packeges(Id)
);

CREATE TABLE OutputOrderItems(
	Id int not null identity(1 ,1),
	OutputOrderId int,
	PackegeId int,
	Quantity int not null

	constraint pk_outputOrderItem_id primary key(Id),

	constraint fk_outputOrderItem_outputOrdeId_outputOrder_id
	foreign key (OutputOrderId) references OutputOrders(Id),

	constraint fk_outputOrderItem_packegeId_packege_id
	foreign key (PackegeId) references Packeges(Id)
);

CREATE TABLE Warehouses(
	Id int not null identity(1, 1),
	[Address] nvarchar(128)

	constraint pk_warehouse_id primary key(Id)
);

CREATE TABLE WarehouseItems(
	Id int not null identity(1, 1),
	WarehouseId int,
	PackegeId int,
	Quantity int not null

	constraint pk_warehouseItem_id primary key(id),

	constraint fk_warehouseItem_warehouseId_warehouse_id
	foreign key (WarehouseId) references Warehouses(Id),

	constraint fk_warehouseItem_packegeId_packege_id
	foreign key (PackegeId) references Packeges(Id)
);

--select * from Producers
--select * from Categoryes

alter table Producers
alter column Phone nvarchar(64)

--delete from Producers
--DBCC CHECKIDENT ('Producers', RESEED, 0)

insert into Producers values
	('ООО Металл', 'Киев, ул. Космонавтов 1', '095-111-11-11'),
	('ООО Дерево', 'Львов, ул.Шевченка 23', '097-222-22-22'),
	('ООО Камень', 'Харьков, ул.Гагарина 5', '099-333-33-33'),
	('ООО ВсеОстальное', 'Днепропетровск, ул.Победы 2','050-444-44-44')
	INSERT INTO Producers VALUES
('ЧП Деревокорпорация',null,'066-708-88-99')

--select * from Categoryes
--delete from Categoryes
--DBCC CHECKIDENT ('Categoryes', RESEED, 0)

insert into Categoryes values
	('Изделия из дерева', null),
	('Доска', 1),
	('Фанера', 1),
	('Брус', 1)

insert into Categoryes values
	('Изделия из металла', null),
	('Швеллер', 2),
	('Двутавр', 2),
	('Уголок', 2),
	('Лист стальной', 2),
	('Труба круглая', 2)

insert into Categoryes values
	('Беттонные изделия', null),
	('Кирпич', 3),
	('Плита перекрытия', 3),
	('Фундаментный блок', 3),
	('Перемычка бетонная', 3),
	('Цемент', 3)

insert into Categoryes values
	('Разное', null),
	('Гипсокартон', 4),
	('Обои', 4),
	('Краска', 4),
	('Гвозди', 4),
	('Саморезы', 4)

--select * from Products

insert into Products values
	('Доска 20х100х6000 мм', 2, 2),
	('Доска 30х200х6000 мм', 2, 2),
	('Фанера 18х1200х2500 мм', 3, 2),
	('Фанера финская 22х1800х2400 мм', 3, 2),
	('Брус 100х100х6000 мм', 4, 2),
	('Брус 150х150х6000 мм', 4, 2)

insert into Products values
	('Швеллер 10', 6, 1),
	('Швеллер 12', 6, 1),
	('Швеллер 14', 6, 1),
	('Двутавр 18', 7, 1),
	('Двутавр 20', 7, 1),
	('Уголок равнополочный 50х4', 8, 1),
	('Уголок равнополочный 63х4', 8, 1),
	('Уголок равнополочный 63х5', 8, 1),
	('Уголок равнополочный 75х5', 8, 1),
	('Сталь листовая t = 4 мм', 9, 1),
	('Сталь листовая t = 10 мм', 9, 1),
	('Труба, диаметром 159мм t=4мм', 10, 1),
	('Труба, диаметром 159мм t=5мм', 10, 1)

insert into Products values
	('Кирпич силикатный', 12, 3),
	('Кирпич кервмический', 12, 3),
	('Плита ребристая 1,5х12м', 13, 3),
	('Плита ребристая 1,8х12м', 13, 3),
	('Плита пустотная 1,2х6м', 13, 3),
	('Блок фундаментный 400х500х2000мм', 14, 3),
	('Блок фундаментный 600х700х1500мм', 14, 3),
	('Перемычка дверная 100х120х1200', 15, 3),
	('Перемычка дверная 100х120х1500', 15, 3),
	('Портланд цемент 50кг', 16, 3),
	('Портланд цемент 25кг', 16, 3)

insert into Products values
	('Гипсокартон потолочный 12мм, 2000х1500мм', 18, 4),
	('Гипсокартон стеновой 10мм, 2000х1500мм', 18, 4),
	('Обои 1, 1х20м', 19, 4),
	('Обои 2, 1х20м', 19, 4),
	('Краска красная', 20, 4),
	('Краска белая', 20, 4),
	('Краска черная', 20, 4),
	('Грунтовка', 20, 4),
	('Гвозди 70мм', 21, 4),
	('Гвозди 100мм', 21, 4),
	('Саморезы по дереву 70мм', 22, 4),
	('Саморезы по дереву 50мм', 22, 4),
	('Саморезы по металу 30мм', 22, 4)
INSERT INTO Products VALUES
	('Гвозди 50мм', 21, null),
	('Краска водостойкая серая', 20, null)


 select * from Measures
 delete from Measures
 DBCC CHECKIDENT('Measures', RESEED, 0)


 insert into Measures values
 ('л', 'литров'),
 ('кг', 'килограмм'),
 ('шт', 'штук'),
 ('м.п.', 'метров погонных'),
 ('м.куб.', 'кубических метров')
 INSERT INTO Measures VALUES
 ('т', 'тонн'),
 ('м.кв.','метров квадратных'),
 ('уп.', 'упаковка'),
 ('м-к','мешок')


 --SELECT * FROM Measures
 --SELECT * FROM Products
 --SELECT * FROM Packeges
 
 ALTER TABLE Packeges
 ALTER COLUMN Volume Numeric(18,2)
 DELETE FROM Packeges
 DBCC CHECKIDENT('Packeges', RESEED, 0)
 INSERT INTO Packeges VALUES
 (1, 3, 5, 1),
 (2, 3, 5, 1),
 (3, 3, 7 , 1),
 (4, 3, 7, 1),
 (5, 3, 5, 1),
 (6, 3, 5, 1),
 (7, 4, 6, 1),
 (8, 4, 6, 1),
 (9, 4, 6, 1),
 (10, 4, 6, 1),
 (11, 4, 6, 1),
 (12, 4, 6, 1),
 (13, 4, 6, 1),
 (14, 4, 6, 1),
 (15, 4, 6, 1),
 (16, 7, 6, 1),
 (17, 7, 6, 1),
 (18, 4, 6, 1),
 (19, 4, 6, 1),
 (20, 3, 5, 1000),
 (21, 3, 5, 1000),
 (22, 3, 3, 1),
 (23, 3, 3, 1),
 (24, 3, 3, 1),
 (25, 3, 3, 1),
 (26, 3, 3, 1),
 (27, 3, 3, 1),
 (28, 3, 3, 1),
 (29, 2, 9, 50),
 (30, 2, 9, 25),
 (31, 7, 1, 3),
 (32, 7, 1, 3),
 (33, 7, 3, 20),
 (34, 7, 3, 20),
 (35, 1, 3, 2.5),
 (36, 1, 3, 2.5),
 (37, 1, 3, 2.5),
 (38, 1, 3, 3.0),
 (39, 2, 2, 1),
 (40, 2, 2, 1),
 (41, 3, 8, 100),
 (42, 3, 8, 100),
 (43, 3, 8, 100)
 INSERT INTO Packeges VALUES
(44, 2, 2, 1),
(45, 1, 3, 2.7)

 --SELECT * FROM Products
 --SELECT * FROM Packeges
-- SELECT * FROM Price

-- DELETE FROM Price
 --DBCC CHECKIDENT('Price', RESEED, 0)

 ALTER TABLE Price
 ALTER COLUMN Cost Numeric(18,2)
 ALTER TABLE Price
 ALTER COLUMN [Date] date

 INSERT INTO Price Values
 (1, 23.3, '1.03.2015'),
 (2, 33.0, '1.03.2015'),
 (3, 73.1, '1.03.2015'),
 (4, 120.0, '1.03.2015'),
 (5, 32.5, '1.03.2015'),
 (6, 45.0, '1.03.2015'),
 (7, 88.2, '1.03.2015'),
 (8, 98.2, '1.03.2015'),
 (9, 108.2, '1.03.2015'),
 (10, 161.0, '1.03.2015'),
 (11, 180.0, '1.03.2015'),
 (12, 28.0, '1.03.2015'),
 (13, 35.5, '1.03.2015'),
 (14, 50.0, '1.03.2015'),
 (15, 65.7, '1.03.2015'),
 (16, 300.0, '1.03.2015'),
 (17, 550.0, '1.03.2015'),
 (18, 110.8, '1.03.2015'),
 (19, 130.8, '1.03.2015'),
 (20, 3500.0, '1.03.2015'),
 (21, 2600.0, '1.03.2015'),
 (22, 1280.0, '1.03.2015'),
 (23, 1480.0, '1.03.2015'),
 (24, 770.0, '1.03.2015'),
 (25, 470.0, '1.03.2015'),
 (26, 450.0, '1.03.2015'),
 (27, 170.0, '1.03.2015'),
 (28, 210.0, '1.03.2015'),
 (29, 59.9, '1.03.2015'),
 (30, 35.0, '1.03.2015'),
 (31, 127.3, '1.03.2015'),
 (32, 117.6, '1.03.2015'),
 (33, 101.5, '1.03.2015'),
 (34, 150.0, '1.03.2015'),
 (35, 77.5, '1.03.2015'),
 (36, 77.5, '1.03.2015'),
 (37, 77.5, '1.03.2015'),
 (38, 90.5, '1.03.2015'),
 (39, 53.5, '1.03.2015'),
 (40, 67.5, '1.03.2015'),
 (41, 39.9, '1.03.2015'),
 (42, 32.9, '1.03.2015'),
 (43, 29.9, '1.03.2015')

 --SELECT * FROM Customers

 INSERT INTO Customers VALUES
 ('Иван Иванович', 'г.Киев', '111-222-333'),
 ('Дмитрий Дмитриевич', 'г.Харьков', '222-333-444'),
 ('Василий Василеевич', 'г.Киев', '333-444-555'),
 ('Гадя Петрович', 'г.Сочи', '0-444-555-666')
 INSERT INTO Customers VALUES
 ('ООО Днепр', 'г.Днепропетровск', '99-787')

 INSERT INTO Warehouses VALUES
 ('г.Киев, Склад №1'),
 ('г.Киев, Склад №2'),
 ('г.Киев, Склад №3'),
 ('г.Львов, Склад №1'),
 ('г.Одесса, Склад №1')
 
 --SELECT * FROM Warehouses
 --SELECT * FROM WarehouseItems


 INSERT INTO WarehouseItems VALUES
 (1, 1, 90),
 (1, 2, 190),
 (1, 3, 70),
 (1, 4, 90),
 (1, 5, 100),
 (1, 6, 200),
 (1, 7, 30),
 (1, 8, 120),
 (1, 9, 75),
 (1, 10, 95),
 (2, 11, 90),
 (2, 12, 190),
 (2, 13, 70),
 (2, 14, 90),
 (2, 15, 100),
 (2, 16, 200),
 (2, 17, 30),
 (2, 18, 120),
 (2, 19, 75),
 (2, 20, 95),
 (3, 21, 90),
 (3, 22, 190),
 (3, 23, 70),
 (3, 24, 90),
 (3, 25, 100),
 (3, 26, 200),
 (4, 27, 30),
 (4, 28, 120),
 (4, 29, 75),
 (4, 30, 95),
 (4, 31, 90),
 (4, 32, 190),
 (4, 33, 70),
 (4, 34, 90),
 (4, 35, 100),
 (4, 36, 200),
 (5, 37, 30),
 (5, 38, 120),
 (5, 39, 75),
 (5, 40, 95),
 (5, 41, 120),
 (5, 42, 75),
 (5, 43, 95),
 (5, 1, 95),
 (5, 11, 120),
 (5, 21, 75),
 (5, 31, 95)

-- SELECT * FROM Customers

 --SELECT * FROM OutputOrders
 --SELECT * FROM InputOrders

 ALTER TABLE OutputOrders
 ALTER COLUMN CreationTime date
 ALTER TABLE OutputOrders
 ALTER COLUMN Cost Numeric(18, 2)

 ALTER TABLE InputOrders
 ALTER COLUMN CreationTime date
 ALTER TABLE InputOrders
 ALTER COLUMN Cost Numeric(18, 2)


 INSERT INTO InputOrders VALUES
 (5, '5.03.2015', 7000, 1),
 (5, '5.03.2015', 3000, 1),
 (5, '5.03.2015', 4500, 1),
 (4, '4.03.2015', 50000, 1)

 --SELECT * FROM InputOrders
 --SELECT * FROM Packeges
 --SELECT * FROM Products
 --SELECT * FROM Measures

 INSERT INTO InputOrderItems VALUES
 (1, 20, 3),
 (1, 21, 2),
 (2, 3, 100),
 (2, 4, 20),
 (3, 11, 120),
 (4, 35, 450),
 (4, 36, 450),
 (4, 37, 420)

 --SELECT * FROM InputOrderItems

 --DELETE FROM OutputOrders
 --DBCC CHECKIDENT('OutputOrders', RESEED, 0)

 SELECT * FROM OutputOrders
 INSERT INTO OutputOrders VALUES
 (1, '7.12.2015', 100, 0),
 (1, '2.21.2015', 350.5, 0),
 (1, '2.23.2015', 290, 0),
 (2, '2.28.2015', 1500, 0),
 (2, '3.15.2015', 210, 0),
 (3, '1.23.2015', 600, 0)

 --DELETE FROM OutputOrderItems
 --DBCC CHECKIDENT('OutputOrderItems', RESEED, 0)

 ALTER TABLE OutputOrderItems
 ALTER COLUMN Quantity Numeric(9, 1)

 --SELECT * FROM OutputOrderItems
 INSERT INTO OutputOrderItems VALUES
 (1, 1 , 0.1)
 INSERT INTO OutputOrderItems VALUES
 (2, 35, 2),
 (2, 36, 5),
 (3, 4, 4),
 (4, 24, 2),
 (5, 19, 3),
 (6, 39, 4),
 (6, 1, 0.5),
 (6, 5, 0.2)

UPDATE Categoryes
SET ParentId = 5 WHERE ParentId =2
UPDATE Categoryes
SET ParentId = 11 WHERE ParentId =3
UPDATE Categoryes
SET ParentId = 17 WHERE ParentId =4

