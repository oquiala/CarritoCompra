# CarritoCompra
Web Carrito de Compra / .Net Framework MVC / SQL Server / Stored Procedure
-- DataBase Scripts
CREATE DATABASE Carrito
GO
-- Tables
CREATE TABLE [Categoria](
	[IdCategoria] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Activo] [bit] DEFAULT 1 NOT NULL,
	[Fecha] [datetime] DEFAULT GETDATE() NOT NULL
	)
GO
CREATE TABLE [Marca](
	[IdMarca] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Activo] [bit] DEFAULT 1 NOT NULL,
	[Fecha] [datetime] DEFAULT GETDATE() NOT NULL
	)
GO
CREATE TABLE [Producto](
	[IdProducto] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[IdMarca] [int] REFERENCES [Marca] ([IdMarca]) NOT NULL,
	[IdCategoria] [int] REFERENCES [Categoria] ([IdCategoria]) NOT NULL,
	[Precio] [decimal](10,2) DEFAULT 0 NOT NULL,
	[Stock] [int] DEFAULT 0 NOT NULL,
	[RutaImagen] [varchar](100) NULL,
	[NombreImagen] [varchar](100) NULL,
	[Activo] [bit] DEFAULT 1 NOT NULL,
	[Fecha] [datetime] DEFAULT GETDATE() NOT NULL
	)
GO
CREATE TABLE [Usuario](
	[IdUsuario] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Usuario] [varchar](100) NOT NULL,
	[Nombre] [varchar](100) NULL,
	[Apellidos] [varchar](100) NULL,
	[Clave] [varchar](150) NOT NULL,
	[Correo] [varchar](100) NULL,
	[Reestablecer] [bit] DEFAULT 0 NOT NULL,
	[Activo] [bit] DEFAULT 1 NOT NULL,
	[Fecha] [datetime] DEFAULT GETDATE() NOT NULL
	)
GO
CREATE TABLE [Carrito](
	[IdCarrito] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[IdUsuario] [int] REFERENCES [Usuario] ([IdUsuario]) NOT NULL,
	[IdProducto] [int] REFERENCES [Producto] ([IdProducto]) NOT NULL,
	[Cantidad] [int] NULL
	)
GO
CREATE TABLE [Venta](
	[IdVenta] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[IdUsuario] [int] REFERENCES [Usuario] ([IdUsuario]) NOT NULL,
	[TotalProducto] [int] NULL,
	[MontoTotal] [decimal](10,2) NULL,
	[Contacto] [varchar](50) NULL,
	[IdDistrito] [varchar](10) NULL,
	[Telefono] [varchar](50) NULL,
	[Direccion] [varchar](500) NULL,
	[IdTransaccion] [varchar](50) NULL,
	[Fecha] [datetime] DEFAULT GETDATE() NOT NULL
	)
GO
CREATE TABLE [DetalleVenta](
	[IdDetalleVenta] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[IdVenta] [int] REFERENCES [Venta] ([IdVenta]) NOT NULL,
	[IdProducto] [int] REFERENCES [Producto] ([IdProducto]) NOT NULL,
	[Cantidad] [int] NULL,	
	[Total] [decimal](10,2) NULL	
	)
GO
-- Stored Procedure
CREATE PROCEDURE sp_RegistrarUsuario
( 
	@Usuario varchar(100),
    @Nombre varchar(100),	
    @Apellidos varchar(100),	
    @Correo varchar(100),	  
	@Clave varchar(100),	  
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuario WHERE Correo = @Correo)
		BEGIN 
			INSERT INTO Usuario (Usuario, Nombre, Apellidos, Correo, Clave, Activo)
					     VALUES (@Usuario, @Nombre, @Apellidos, @Correo, @Clave, @Activo)
			SET @Resultado = SCOPE_IDENTITY()
		END
		ELSE 
			SET @Mensaje = 'El correo del usuario ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_EditarUsuario
( 
	@IdUsuario int,
	@Usuario varchar(100),
    @Nombre varchar(100),	
    @Apellidos varchar(100),	
    @Correo varchar(100),	  	  
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Usuario WHERE Correo = @Correo AND IdUsuario != @IdUsuario)
		BEGIN 
			UPDATE TOP(1) Usuario
			SET 
			Usuario = @Usuario,
			Nombre = @Nombre,
			Apellidos = @Apellidos,
			Correo = @Correo,
			Activo = @Activo
			WHERE IdUsuario = @IdUsuario			
			SET @Resultado = 1
		END
		ELSE 
			SET @Mensaje = 'El correo del usuario ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_RegistrarCategoria
( 
	@Descripcion varchar(100),   
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria WHERE Descripcion = @Descripcion)
		BEGIN 
			INSERT INTO Categoria (Descripcion, Activo) VALUES (@Descripcion, @Activo)
			SET @Resultado = SCOPE_IDENTITY()
		END
		ELSE 
			SET @Mensaje = 'La categoría ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_EditarCategoria
( 
	@IdCategoria int,
	@Descripcion varchar(100),   
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Categoria WHERE Descripcion = @Descripcion AND IdCategoria != @IdCategoria)
		BEGIN 
			UPDATE TOP(1) Categoria	SET Descripcion = @Descripcion, Activo = @Activo WHERE IdCategoria = @IdCategoria			
			SET @Resultado = 1
		END
		ELSE 
			SET @Mensaje = 'La categoría ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_RegistrarMarca
( 
	@Descripcion varchar(100),   
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Marca WHERE Descripcion = @Descripcion)
		BEGIN 
			INSERT INTO Marca (Descripcion, Activo) VALUES (@Descripcion, @Activo)
			SET @Resultado = SCOPE_IDENTITY()
		END
		ELSE 
			SET @Mensaje = 'La marca ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_EditarMarca
( 
	@IdMarca int,
	@Descripcion varchar(100),   
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Marca WHERE Descripcion = @Descripcion AND IdMarca != @IdMarca)
		BEGIN 
			UPDATE TOP(1) Marca	SET Descripcion = @Descripcion,	Activo = @Activo WHERE IdMarca = @IdMarca			
			SET @Resultado = 1
		END
		ELSE 
			SET @Mensaje = 'La marca ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_RegistrarProducto
( 	
	@Nombre varchar(100), 
	@Descripcion varchar(100), 
	@IdMarca int, 
	@IdCategoria int, 
	@Precio decimal (10,2), 
	@Stock int,
    @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Producto WHERE Nombre = @Nombre)
		BEGIN 
			INSERT INTO Producto (Nombre, Descripcion, IdMarca, IdCategoria, Precio, Stock, Activo) VALUES (@Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Stock, @Activo)
			SET @Resultado = SCOPE_IDENTITY()
		END
		ELSE 
			SET @Mensaje = 'El producto ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_EditarProducto
( 
	@IdProducto int,
	@Nombre varchar(100), 
	@Descripcion varchar(100), 
	@IdMarca int, 
	@IdCategoria int, 
	@Precio decimal (10,2), 
	@Stock int,
  @Activo bit,
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM Producto WHERE Nombre = @Nombre AND IdProducto != @IdProducto)
		BEGIN 
			UPDATE TOP(1) Producto
			SET 
			Nombre = @Nombre,
			Descripcion = @Descripcion, 
			IdMarca = @IdMarca, 
			IdCategoria = @IdCategoria, 
			Precio = @Precio, 			
			Stock = @Stock, 
			Activo = @Activo
			WHERE IdProducto = @IdProducto			
			SET @Resultado = 1
		END
		ELSE 
			SET @Mensaje = 'El producto ya existe' 
END
-----------------------------------------
CREATE PROCEDURE sp_EliminarProducto
( 
	@IdProducto int,	
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 0
	IF NOT EXISTS (SELECT * FROM DetalleVenta dv INNER JOIN Producto p ON p.IdProducto = dv.IdProducto WHERE p.IdProducto = @IdProducto)
		BEGIN 
			DELETE TOP(1) FROM Producto WHERE IdProducto = @IdProducto
			SET @Resultado = 1
		END
		ELSE 
			SET @Mensaje = 'El producto está en uso' 
END
-----------------------------------------
CREATE PROCEDURE sp_ReporteDashboard
AS
BEGIN
	SELECT
		(SELECT COUNT(*) FROM Producto) [TotalProducto],
		(SELECT COUNT(*) FROM Usuario) [TotalCliente],
		(SELECT ISNULL(SUM(cantidad),0) FROM DetalleVenta) [TotalVenta]
END
GO
-----------------------------------------
CREATE PROCEDURE sp_ReporteVentas
(
	@FechaIni varchar(10),
	@FechaFin varchar(10),
	@IdTrans varchar(50)
)
AS
BEGIN
	SET dateFormat dmy;
	SELECT CONVERT(char(10),v.Fecha,103) Fecha, CONCAT(u.Nombre, ' ', u.Apellidos) Usuario, p.Nombre Producto, p.Precio, dv.Cantidad, dv.Total, v.IdTransaccion
	FROM DetalleVenta dv
	INNER JOIN Producto p ON(p.IdProducto = dv.IdProducto)
	INNER JOIN Venta v ON(v.IdVenta = dv.IdVenta)
	INNER JOIN Usuario u ON(u.IdUsuario = v.IdUsuario)
	WHERE CONVERT(date, v.Fecha) BETWEEN @FechaIni AND @FechaFin
	AND v.IdTransaccion = IIF(@IdTrans = '', v.IdTransaccion, @IdTrans)	
END
GO
-----------------------------------------
CREATE PROCEDURE sp_ExisteCarrito
( 
	@IdUsuario int,	
    @IdProducto int,
	@Resultado bit output
)
AS
BEGIN 	
	IF EXISTS (SELECT * FROM Carrito WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto)
		SET @Resultado = 1
	ELSE 
		SET @Resultado = 0
END
-----------------------------------------
CREATE PROCEDURE sp_OperacionCarrito
( 
	@IdUsuario int,	
    @IdProducto int,
	@Sumar bit,
	@Mensaje varchar(500) output,
	@Resultado bit output
)
AS
BEGIN 	
	SET @Resultado = 1
	SET @Mensaje = ''	
	DECLARE @existeCarrito bit = IIF(EXISTS (SELECT * FROM Carrito WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto),1,0)
	DECLARE @stockProducto int = (SELECT Stock FROM Producto WHERE IdProducto = @IdProducto)
	BEGIN TRY
		BEGIN TRANSACTION Operacion
			IF(@Sumar = 1)
			BEGIN
				if(@stockProducto > 0)
				BEGIN
					IF(@existeCarrito = 1)
						UPDATE Carrito SET Cantidad = Cantidad + 1 WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto
					ELSE
						INSERT INTO Carrito(IdUsuario, IdProducto, Cantidad) VALUES (@IdUsuario, @IdProducto, 1)
					UPDATE Producto SET Stock = Stock - 1 WHERE IdProducto = @IdProducto
				END
				ELSE
				BEGIN
					SET @Resultado = 0
					SET @Mensaje = 'El producto no cuenta con stock disponible'
				END
			END
			ELSE
			BEGIN
				UPDATE Carrito SET Cantidad = Cantidad - 1 WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto
				UPDATE Producto SET Stock = Stock + 1 WHERE IdProducto = @IdProducto
			END
		COMMIT TRANSACTION Operacion
	END TRY
	BEGIN CATCH
		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION Operacion
	END CATCH
END
-----------------------------------------
CREATE FUNCTION fn_ObtenerCarrito
(
	@IdUsuario int
)
RETURNS TABLE AS RETURN
(
	SELECT p.IdProducto, p.Nombre, P.Precio, p.Stock, p.RutaImagen, p.NombreImagen, m.Descripcion DesMarca, c.Cantidad
	FROM Carrito c 
	INNER JOIN Producto p ON p.IdProducto = c.IdProducto
	INNER JOIN Marca m ON m.IdMarca = p.IdMarca
	WHERE c.IdUsuario = @IdUsuario
)
-----------------------------------------
CREATE PROCEDURE sp_EliminarCarrito
( 
	@IdProducto int,	
	@IdUsuario int,	
	@Resultado int output
)
AS
BEGIN 
	SET @Resultado = 1
	DECLARE @cantidad int = (SELECT Cantidad FROM Carrito WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto)
	BEGIN TRY
		BEGIN TRANSACTION Operacion
			UPDATE Producto SET Stock = Stock + @cantidad WHERE IdProducto = @IdProducto
			DELETE TOP(1) FROM Carrito WHERE IdUsuario = @IdUsuario AND IdProducto = @IdProducto
	COMMIT TRANSACTION Operacion
	END TRY
	BEGIN CATCH
		SET @Resultado = 0		
		ROLLBACK TRANSACTION Operacion
	END CATCH 
END
-----------------------------------------
CREATE TYPE EDetalleVenta AS TABLE(
IdProducto int NULL,
Cantidad int NULL,
Total decimal(18,2) NULL
)
CREATE PROCEDURE sp_RegistrarVenta
( 
	@IdUsuario int,
    @TotalProducto int,	
    @MontoTotal decimal(18,2),	
    @Contacto varchar(100),	  
	@IdDistrito varchar(6),	  
    @Telefono varchar(10),
	@Direccion varchar(100),	  
	@IdTransaccion varchar(50),
	@DetalleVenta [EDetalleVenta] READONLY,	 
	@Mensaje varchar(500) output,
	@Resultado int output
)
AS
BEGIN 
	BEGIN TRY
		DECLARE @IdVenta int = 0
		SET @Resultado = 1
		SET @Mensaje = ''
		BEGIN TRANSACTION Registro
			INSERT INTO Venta (IdUsuario, TotalProducto, MontoTotal, Contacto, IdDistrito, Telefono, Direccion, IdTransaccion)
					   VALUES (@IdUsuario, @TotalProducto, @MontoTotal, @Contacto, @IdDistrito, @Telefono, @Direccion, @IdTransaccion)
				SET @IdVenta = SCOPE_IDENTITY()
			INSERT INTO DetalleVenta (IdVenta, IdProducto, Cantidad, Total)
			SELECT @IdVenta, IdProducto, Cantidad, Total FROM @DetalleVenta
			DELETE FROM Carrito WHERE IdUsuario = @IdUsuario
		COMMIT TRANSACTION Registro
	END TRY
	BEGIN CATCH
		SET @Resultado = 0
		SET @Mensaje = ERROR_MESSAGE()
		ROLLBACK TRANSACTION Registro
	END CATCH
END
