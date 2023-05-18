--Procedimientos Almacenados
use master;
go
use BibliotecaBD
GO
select * from usuario
GO
--PROCEDIMIENTOS PARA USUARIO
create procedure sp_RegistrarUsuario(
    @Nombres varchar(100),
    @Apellidos varchar(100),
    @Correo varchar(100),
    @Clave varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo)
    begin 
        insert into USUARIO(Nombres, Apellidos, Correo, Clave, Activo) values 
        (@Nombres, @Apellidos, @Correo, @Clave, @Activo)
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = scope_identity()
    end 
    else 
     SET @Mensaje = 'El correo del usuario ya existe'
end 
go 
create proc sp_EditarUsuario(
    @IdUsuario int,
    @Nombres varchar(100),
    @Apellidos varchar(100),
    @Correo varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado bit output
)
as
begin 
    SET @Resultado = 0
    IF NOT EXISTS (SELECT * FROM USUARIO WHERE Correo = @Correo and IdUsuario != @IdUsuario)
    begin 
        update top(1) USUARIO set 
        Nombres = @Nombres,
        Apellidos  = @Apellidos,
        Correo = @Correo,
        Activo = @Activo
        where IdUsuario = @IdUsuario

        set @Resultado = 1
    end 
    else 
        set @Mensaje = 'El correo del usuario ya existe'
end
GO
--PROCEDIMIENTOS PARA CATEGORIA
create procedure sp_RegistrarCategoria(
    @Descripcion varchar(100), 
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (SELECT * FROM Categoria WHERE Descripcion = @Descripcion)
    begin 
        insert into CATEGORIA(Descripcion, Activo) values 
        (@Descripcion, @Activo)
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = scope_identity()
    end 
    else 
     SET @Mensaje = 'La categoria ya existe'
end 
go
create proc sp_EditarCategoria( --Trabajo como un booleano
    @IdCategoria int,
    @Descripcion varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado bit output
)
as
begin 
    SET @Resultado = 0 --false
    IF NOT EXISTS (SELECT * FROM CATEGORIA WHERE Descripcion = @Descripcion and IDCategoria != @IdCategoria)
    begin 
        update top(1) CATEGORIA set 
        Descripcion = @Descripcion,
        Activo = @Activo
        where IDCategoria = @IdCategoria

        set @Resultado = 1 --true
    end 
    else 
        set @Mensaje = 'La categoria ya existe'
end
go
create proc sp_EliminarCategoria    ( --Trabajo como un booleano
    @IdCategoria int,
    @Mensaje varchar(500) output,
    @Resultado bit output
)
as
begin 
    SET @Resultado = 0 --false
    IF NOT EXISTS (SELECT * FROM Libro p --validacion de que la categoria no este relacionada con un producto
    inner join CATEGORIA c on c.IDCategoria = p.IdCategoria WHERE p.IdCategoria= @IdCategoria)
    begin 
        delete top(1) from CATEGORIA where IDCategoria = @IdCategoria
        set @Resultado = 1 --true
    end 
    else 
        set @Mensaje = 'La categoria se encuentra relacionada con un producto'
end
GO
select * from autor;
go
--PROCEDIMIENTOS PARA Autor
create procedure sp_RegistrarAutor(
    @Nombre varchar(100), 
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (SELECT * FROM Autor WHERE Nombre = @Nombre)
    begin 
        insert into Autor(Nombre, Activo) values 
        (@Nombre, @Activo)
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = scope_identity()
    end 
    else 
     SET @Mensaje = 'El autor ya existe'
end 
go
create proc sp_EditarAutor( --Trabajo como un booleano
    @IdAutor int,
    @Nombre varchar(100),
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado bit output
)
as
begin 
    SET @Resultado = 0 --false
    IF NOT EXISTS (SELECT * FROM Autor WHERE Nombre = @Nombre and IDAutor != @IdAutor)
    begin 
        update top(1) Autor set 
        Nombre = @Nombre,
        Activo = @Activo
        where IDAutor = @IdAutor

        set @Resultado = 1 --true
    end 
    else 
        set @Mensaje = 'El Autor ya existe'
end
go
create proc sp_EliminarAutor( --Trabajo como un booleano
    @IdAutor int,
    @Mensaje varchar(500) output,
    @Resultado bit output
)
as
begin 
    SET @Resultado = 0 --false
    IF NOT EXISTS (SELECT * FROM Libro p --validacion de que la categoria no este relacionada con un producto
    inner join Autor c on c.IDAutor = p.IdLibro WHERE p.IdAutor= @IdAutor)
    begin 
        delete top(1) from Autor where IDAutor = @IdAutor
        set @Resultado = 1 --true
    end 
    else 
        set @Mensaje = 'El Autor se encuentra relacionado con un libro'
end
GO
select * from Autor 

GO
exec sp_RegistrarLibro 'Consola de PS4 Pro 1TB Negro', 'Tipo: PS4 Procesador: AMD Entradas USB: 3 Entrada...Tipo: PS4 Procesador: AMD Entradas USB: 3 Entrada...Tipo: PS4 Procesador: AMD Entradas USB: 3 Entrada...',
1,1,150,49,1, 'Mensaje',1
go
--PROCEDIMIENTOS PARA PRODUCTO
create procedure sp_RegistrarLibro(
    @Titulo varchar(100), 
    @Descripcion varchar(100),
    @IDAutor varchar(100),
    @IDCategoria varchar(100),
    @Paginas decimal (10,2),
    @Stock int,
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (SELECT * FROM Libro WHERE Titulo = @Titulo)
    begin 
        insert into Libro(Titulo,Descripcion,IdAutor, IdCategoria, Paginas, Stock, Activo) values 
        (@Titulo, @Descripcion,@IdAutor, @IdCategoria, @Paginas, @Stock, @Activo)
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = scope_identity()
    end 
    else 
     SET @Mensaje = 'El producto ya existe'
end 
go
create procedure sp_EditarLibro(
    @IdLibro int,
    @Titulo varchar(100), 
    @Descripcion varchar(100),
    @IDAutor varchar(100),
    @IDCategoria varchar(100),
    @Paginas decimal (10,2),
    @Stock int,
    @Activo bit,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (SELECT * FROM Libro WHERE Titulo = @Titulo and IdLibro != @IdLibro)
    begin 
        update Libro set
        Titulo = @Titulo,
        Descripcion = @Descripcion,
        IdAutor = @IDAutor, 
        IdCategoria = @IDCategoria, 
        Paginas = @Paginas, 
        Stock = @Stock, 
        Activo = @Activo 
        where IdLibro = @IdLibro
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = 1 --true
    end 
    else 
     SET @Mensaje = 'El producto ya existe'
end 

go

create procedure sp_EliminarLibro(
    @IdLibro int,
    @Mensaje varchar(500) output,
    @Resultado int output
    )
as
begin
    SET @Resultado = 0 --No permite repetir un mismo correo, ni al insertar ni al actualizar
    IF NOT EXISTS (select * from DetalleVenta dv 
    inner join Libro p on p.IdLibro = dv.IdLibro
    where p.IdLibro = @IdLibro)--No podemos eliminar un producto si ya esta incluido en una venta
    begin 
        delete top(1) from Libro where IdLibro= @IdLibro
        --La funci�n SCOPE_IDENTITY() devuelve el �ltimo ID generado para cualquier tabla de la sesi�n activa y en el �mbito actual.
        SET @Resultado = 1 --true
    end 
    else 
     SET @Mensaje = 'El producto se encuentra relacionado a una venta'
end 
go
use DB_SistemaCarrito
go
create proc sp_ReporteDashboard --Para el reporte de dashboard
as 
begin
select
    (select count(*) from Usuario) [TotalUsuario],
    (select isnull(sum(cantidad),0) from DetalleVenta) [TotalVenta],
    (select count(*) from Libro)[TotalLibro]
end
go
select * from libro;
select * from venta;
select * from DetalleVenta
--insercion prueba de detalle venta
insert into DetalleVenta(IdVenta,IdLibro, Cantidad, Total)
values (9,1,1,4000);


select * from Producto
select * from Cliente
select * from DetalleVenta
select * from Venta

insert into cliente(Nombres,Apellidos,Correo,Clave) 
VALUES
('Oscar','Marcos','oscar@gmail.com','123'),
('Ruben','Salazar','ruben@gmail.com','123'),
('Osvaldo','Sandoval','osvaldo@gmail.com','123'),
('Raul','Ortega','raul@gmail.com','123')
go
select * from Venta
insert into Venta(IdCliente, TotalProducto, MontoTotal, Contacto, IdLocalidad, Telefono, Direccion, IdTransaccion)
values
(2,89,4000,'deiv2','020101','7641298792','calle nueva2','13'),
(3,89,4000,'osar','020102','76412234792','calle nueva3','14'),
(4,89,4000,'rben','030101','764123492','calle nueva4','15'),
(5,89,4000,'osvdo','020201','7641292352','calle nueva5','16')

GO
go
insert into DetalleVenta(IdVenta, IdLibro, Cantidad, Total)
select * from DetalleVenta
go
insert into DetalleVenta (IdVenta, IdLibro, Cantidad, Total)
values(2,1,2,5000),
(3,2,1,3000),
(4,3,3,4000),
(5,3,1,6000),
(6,4,2,1500)



select * from venta
GO
select * from Producto
select * from Distrito
SELECT * from Venta
select * from DetalleVenta
select * from libro
go
use BibliotecaBD;
go
create proc sp_ReporteVentas(
    @fechaInicio varchar(10),
    @fechaFin varchar(10),
    @idLibro varchar(50)
)
as
begin
    set dateformat dmy; /*Indicamos el formato que queremos si o si*/
    --el formato 103, muestra solo la fecha
        select CONVERT(char(10), l.FechaRegistro,103)[FechaRegistro],
        l.Titulo[Libro],a.Nombre[Autor], c.Descripcion[Categoria], l.Paginas, l.Stock,l.IdLibro
        from Libro l
        --select * from libro l
        inner join Autor a on a.IdAutor = l.IdAutor 
        inner join Categoria c on c.IdCategoria = l.IdCategoria
    where CONVERT(date,l.FechaRegistro) BETWEEN @fechaInicio and @fechaFin 
    /*Si el usuario no esta indicando ningun id de transaccion, le decimos que use ese mismo id transaccion del where, pero
    si lo esta indicando, lo use con el @idTransaccion*/
    and l.IdLibro = iif(@idLibro = '', l.IdLibro, @idLibro)
end
go
go
sp_ReporteVentas '14-05-2023','16-05-2023','';
go
select * from Categoria
select * from autor
select * from libro
select * from venta;
select * from detalle venta;


go
insert into Libro(Titulo, Descripcion, IdAutor, IdCategoria, Paginas, Stock)
values
('Calculo I','Mate', 3,1,422,20)

insert into Libro(Titulo, Descripcion, IdAutor, IdCategoria, Paginas, Stock)
values
('Las mil y una noches','Prestado', 5,1,800,8)

insert into Libro(Titulo, Descripcion, IdAutor, IdCategoria, Paginas, Stock)
values
('Las mil y una noches','Prestado', 5,1,800,8)

insert into Libro(Titulo, Descripcion, IdAutor, IdCategoria, Paginas, Stock)
values
('ODISEA','Maltratado en las orillas', 2,1,200,15)

insert into Libro(Titulo, Descripcion, IdAutor, IdCategoria, Paginas, Stock)
values
('El principito','Nuevo', 3,1,89,10)

select * from venta
select * from DetalleVenta
select * from libro
select * from cliente

sp_ReporteVentas '14/05/2023','15/05/2023','';


select * from autor
SELECT * FROM LIBRO

