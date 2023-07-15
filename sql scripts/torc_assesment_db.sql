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