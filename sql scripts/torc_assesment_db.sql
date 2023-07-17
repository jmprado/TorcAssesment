create table Product (
   Id                   int			         not null, -- Identity not set for simplify assesment completion
   [Name]               varchar(255)         not null,
   Price                decimal(18, 2)           not null,
   constraint PK_PRODUCT primary key (Id)
)
go

create table Customer (
   Id                   integer			     not null, -- Identity not set for simplify assesment completion
   [Name]               varchar(255)         not null,
   constraint PK_CUSTOMER primary key (Id)
)
go

create table [Order] (
   Id                   integer identity     not null,
   CustomerId           integer              not null,
   ProductId            integer              not null,
   TotalPrice           decimal(18, 2)           not null,
   constraint PK_ORDER primary key (Id)
)
go

alter table [Order]
   add constraint FK_CUSTOMER foreign key (CustomerId)
      references Customer (Id)
	  on update cascade
go

alter table [Order]
   add constraint FK_PRODUCT foreign key (ProductId)
      references Product (Id)
	  on update cascade
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Joao Prado
-- Create date: 2023-06-22
-- Description:	Create a new order 
-- =============================================
CREATE PROCEDURE CreateOrder
	-- Add the parameters for the stored procedure here
	@productId int,
	@customerId int,
	@quantity int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @productPrice NUMERIC(18, 2);
	DECLARE @orderTotal NUMERIC(18, 2);

    -- Insert statements for procedure here
	SELECT @productPrice = Price FROM Product WHERE Id = @productId;

	SET @orderTotal = CONVERT(decimal(18,2), @productPrice * @quantity);

	INSERT INTO [Order] (CustomerId, ProductId, TotalPrice) 
	VALUES(@customerId, @productId, @orderTotal);

END
GO



insert into Customer(Id, [Name]) values(1, 'Joao Prado')
go

insert into Customer(Id, [Name]) values(2, 'Ramiro')
go

insert into Customer(Id, [Name]) values(3, 'Maria')
go

insert into Product(Id, [Name], Price) values(1, 'Torc Screwdriver', 15.5)
go

insert into Product(Id, [Name], Price) values(2, 'Torc Hammer', 9.25)
go

insert into Product(Id, [Name], Price) values(3, 'Torc Chainsaw', 51.0)
go

insert into Product(Id, [Name], Price) values(4, 'Torc Eletric Drill', 51.0)