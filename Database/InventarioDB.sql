create database InventarioDB
go
use InventarioDB
go
create table Categorias (
IdCategoria int identity(1,1) primary key,
Nombre varchar(100)not null,
Activo BIT not null default 1
);
go
create table Productos (
IdProducto int identity(1,1) primary key,
Nombre varchar(150) not null,
Precio decimal(10,2) not null,
Stock int not null,
StockMinimo int not null,
IdCategoria int not null,
Activo BIT null default 1,

Constraint FK_Productos_Categorias
Foreign key (IdCategoria) References Categorias(IdCategoria)
);
go

Create table Ventas (
IdVenta int identity(1,1) primary key,
Fecha DATETIME not null default getdate(),
Total Decimal(10,2) not null,
DineroRecibido Decimal(10,2) not null,
Cambio decimal(10,2) not null
);
go

create table DetalleVenta (
IdDetalle int identity(1,1) primary key,
IdVenta int not null,
IdProducto int not null,
Cantidad int not null,
PrecioUnitario decimal(10,2) not null,
SubTotal decimal(10,2) not null,

Constraint FK_DetalleVenta_Ventas
Foreign key (IdVenta) References Ventas(IdVenta),

Constraint FK_DetalleVenta_Productos
Foreign key (IdProducto) References Productos(IdProducto)
);

SET IDENTITY_INSERT Categorias ON;

INSERT INTO Categorias
(IdCategoria, Nombre, Activo)
VALUES
(0, 'Sistema', 0);

SET IDENTITY_INSERT Categorias OFF;

go

SET IDENTITY_INSERT Productos ON;

INSERT INTO Productos
(IdProducto, Nombre, Precio, Stock, StockMinimo, IdCategoria, Activo)
VALUES
(0, 'Producto Manual', 0, 0, 0, 0, 0);

SET IDENTITY_INSERT Productos OFF;

go


ALTER TABLE DetalleVenta
ADD NombreProductoManual NVARCHAR(150) NULL

go

ALTER TABLE DetalleVenta
ALTER COLUMN IdProducto INT NULL;

