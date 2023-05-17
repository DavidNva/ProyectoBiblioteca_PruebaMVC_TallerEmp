use master
go
create database BibliotecaBD
go
use BibliotecaBD
go
select * from autor
--Datos de UBI-GEO
go
CREATE TABLE Estado(--Estado
	IdEstado varchar(5) not null primary key,
	Descripcion varchar(45) not null
)
go
CREATE TABLE Municipio(--Municipio
	IdMunicipio varchar(5) not null primary key,
	Descripcion varchar(45) not null,
	IdEstado varchar(5) not null references Estado(IdEstado),
)
go
CREATE TABLE Localidad(--Pueblo
	IdLocalidad varchar(6) NOT NULL primary key,
	Descripcion varchar(45) NOT NULL,
	IdMunicipio varchar(5) references Municipio(IdMunicipio),
	IdEstado varchar(5) references Estado(IdEstado),
)
go

CREATE TABLE Categoria(
	IdCategoria int primary key identity,
	Descripcion varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
go
CREATE TABLE Autor(
	IdAutor int primary key identity,
	Nombre varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
go
CREATE TABLE Libro(
	IdLibro int primary key identity,
	Titulo varchar(500),
	Descripcion varchar(500),
	IdAutor int references Autor(IdAutor),
	IdCategoria int references Categoria(IdCategoria),
	Paginas decimal(10,2) default 0,--10 es la longitud maxima
	Stock int,
	RutaImagen varchar(100),
	NombreImagen varchar(100),
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)
go
CREATE TABLE Cliente(
	IdCliente int primary key identity,
	Nombres varchar(100),
	Apellidos varchar(100),
	Correo varchar(100),
	Clave varchar(150),
	Reestablecer bit default 0,
	FechaRegistro datetime default getdate()
)
go
CREATE TABLE Carrito(
	IdCarrito int primary key identity,
	IdCliente int references Cliente(IdCliente),
	IdLibro int references Libro(IdLibro),
	Cantidad int--Cuantas unidades para este producto esta seleccionando el cliente
)
go
CREATE TABLE Venta(
	IdVenta int primary key identity,
	IdCliente int references Cliente(IdCliente),
	TotalProducto int, --El cliente pudo haber comprado 3 productos
	MontoTotal decimal(10,2),--La suma total del precio de todos los productos
	Contacto varchar(50), --alguien de contacto que pueda usar de referencia como contacto con una persona
	IdLocalidad varchar(6) references Localidad(IdLocalidad),
	Telefono varchar(50),
	Direccion varchar(500),
	IdTransaccion varchar(50),
	FechaVenta datetime default getdate()
)
go
select * from venta
CREATE TABLE DetalleVenta(
	IdDetalleVenta int primary key identity,
	IdVenta int references Venta(IdVenta),
	IdLibro int references Libro(IdLibro),
	Cantidad int,
	Total decimal(10,2)--total del precio del producto
)
go
CREATE TABLE Usuario(
	IdUsuario int primary key identity,
	Nombres varchar(100),
	Apellidos varchar(100),
	Correo varchar(100),
	Clave varchar(150), --Contrase�as encriptadas
	Reestablecer bit default 1, -- Por default 1
	Activo bit default 1,
	FechaRegistro datetime default getdate()
)

select * from usuario; 
go
--inserciones 
--insercion a usuario
insert into Usuario(Nombres,Apellidos,Correo,Clave) --david123
values ('David','Nava G','david@example.com','0f14089313b20c1723ec1d660b0aaa4f473cf5b321cd370f2d48b7bcf9a7b234');

insert into Usuario(Nombres,Apellidos,Correo,Clave) --david123
values ('Test 02','User 02','user02@example.com','0f14089313b20c1723ec1d660b0aaa4f473cf5b321cd370f2d48b7bcf9a7b234');

go 
insert into Categoria(Descripcion) values 
('Literatura'),
('Historia'),
('Ciencias Puras'),
('Ingles');
go
select * from Categoria

go
insert into Autor(Nombre) values 
('David Nava'),
('Jonathan Juarez'),
('Angel Jair Luna'),
('Juan Adolfo Lopez'),
('Juan Manuel Lopez')
go
select * from Autor
go
--departamento = estado
insert into Estado(IdEstado,Descripcion) --En mexico podemos decir que departamento es un estado
values 
('01','Mexico'),
('02','Veracruz'),
('03','Puebla');

select * from Estado
select * from Municipio
go 
--provincia = municipio
insert into Municipio(IdMunicipio,Descripcion,IdEstado) --y que provincia en mexico es un municipio
values --los dos primero digitos hacen referencia a que departamento(estado) se hace referencia, y los dos ultimos
--digitos hacen referencia al codigo de la provincia perteneciente a ese departamento
--municipios (provincia segun el video) del Estado de Mexico
--municipios del estado de Mexico
('0101','Cuautitlan','01'),--cuautitlan es un municipio del estado de México
('0102','Texcoco','01'),--texcoco es un municipio del estado de Mexico

--municipios de veracruz
('0201','Xalapa','02'),--xalapa veracruz es un municipio de Veracruz
('0202','Córdoba','02'),--xalapa veracruz es un municipio de Veracruz

--municipios de Puebla
('0301','Zacatlan','03'),--zacatlan es un municipio de Puebla
('0302','Chignahuapan','03');--chignahuapan es un municipio de Puebla
go
select * from Municipio
go


--En mexico el distrito seria como un pueblo dentro de un municipio
--Distrito = pueblo
insert into Localidad(IdLocalidad, Descripcion,IdMunicipio,IdEstado) values 
--los dos primero digitos hacen referencia a que departamento(estado) se hace referencia, 
--los siguientes dos digitos hacen referencia al codigo de la provincia perteneciente a ese departamento
--los primero cuatro como tal estarian referenciando a la provincia(municipio) y ya esta tiene sus propias relaciones
--los ultimos dos digitios es el codigo para el distrito (pueblo) que pertenece a ese municipio
--municipios (provincia segun el video) del Estado de Mexico
/*Distritos del Estado de Mexico*/
--Distritos (pueblos) de cuautitlan
('010101','Barrio Tlatenco','0101','01'),--Barrio Tlatenco es un pueblo del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('010102','Cuautitlan','0101','01'),
--Distritos (pueblos) de texcoco
('010201','Texcoco de Mora','0102','01'),--Barrio Tlatenco es un pueblo del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('010202','San Bernardino','0102','01'),

/*Distritos (pueblos) de Veracruz*/
--Distritos (pueblos) de xalapa
('020101','Lomas Verdes','0201','02'),--Lomas verdes y el castillo son pueblos del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('020102','El castillo','0201','02'),
--Distritos (pueblos) de cordoba
('020201','El pueblito','0202','02'),--Barrio Tlatenco es un pueblo del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('020202','Cordoba','0202','02'),

/*Distritos (pueblos) de Puebla*/
--Distritos (pueblos) de zacatlan
('030101','Tepeixco','0301','03'),--Lomas verdes y el castillo son pueblos del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('030102','Jicolapa','0301','03'),
--Distritos (pueblos) de cordoba
('030201','Ciudad de Chignahuapan','0302','03'),--Barrio Tlatenco es un pueblo del municipio Cuautitlan y cuautitlan es un municipio de Mexico
('030202','Acoculco','0302','03');

select * from Localidad;



select P.IdLibro, p.Titulo, p.Descripcion,
                    m.IDAutor, m.Nombre,
                   c.IDCategoria, c.Descripcion [DesCategoria],
                   p.Paginas, p.Stock, p.RutaImagen, p.NombreImagen, p.Activo
                    from Libro p
                    inner join Autor m on m.IDAutor = p.IdAutor
					                   inner join Categoria c on c.IDCategoria = p.IdCategoria;


				 select * from libro
				 select * from autor