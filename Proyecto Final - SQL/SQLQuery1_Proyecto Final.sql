-- Tabla Cliente
CREATE TABLE Cliente (
    id INT IDENTITY(1,1) PRIMARY KEY,
    cedula VARCHAR(15) NOT NULL UNIQUE,
    nombre VARCHAR(30) NOT NULL,
    apellido VARCHAR(30) NOT NULL,
    direccion VARCHAR(50),
    celular VARCHAR(9) CHECK (celular LIKE '[0-9][0-9][0-9][0-9]-[0-9][0-9][0-9][0-9]'),
    correo VARCHAR(50),
    sexo CHAR(1) CHECK (sexo IN ('M', 'F')),
    estatus CHAR(1) DEFAULT 'A' CHECK (estatus IN ('A', 'I'))
);

-- Tabla Producto
CREATE TABLE Producto (
    id INT IDENTITY(1,1) PRIMARY KEY,
    codigoBarra VARCHAR(12) NOT NULL UNIQUE,
    descripcion VARCHAR(50) NOT NULL,
    costo DECIMAL(7,2) NOT NULL CHECK (costo >= 0),
    precio DECIMAL(7,2) NOT NULL CHECK (precio >= 0),
    itbms INT NOT NULL,
    existencia INT NOT NULL CHECK (existencia >= 0),
    puntoReorden INT NOT NULL CHECK (puntoReorden >= 0),
    estatus CHAR(1) DEFAULT 'A' CHECK (estatus IN ('A', 'I'))
);

CREATE PROCEDURE ListarClientes
    @orden NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    IF @orden = 'cedula'
        SELECT * FROM CLIENTE ORDER BY cedula;
    ELSE IF @orden = 'nombre'
        SELECT * FROM CLIENTE ORDER BY nombre;
    ELSE
        SELECT * FROM CLIENTE;
END;

CREATE PROCEDURE AgregarCliente
    @cedula NVARCHAR(15),
    @nombre NVARCHAR(30),
    @apellido NVARCHAR(30),
    @direccion NVARCHAR(50),
    @celular NVARCHAR(9),
    @correo NVARCHAR(50),
    @sexo CHAR(1),
    @estatus CHAR(1)
AS
BEGIN
    INSERT INTO CLIENTE (cedula, nombre, apellido, direccion, celular, correo, sexo, estatus)
    VALUES (@cedula, @nombre, @apellido, @direccion, @celular, @correo, @sexo, @estatus)
END;

CREATE PROCEDURE AgregarProducto
    @codigobarra NVARCHAR(12),
    @descripcion NVARCHAR(50),
    @costo DECIMAL(7,2),
    @precio DECIMAL(7,2),
    @itbms INT,
    @existencia INT,
    @puntodereorden INT,
    @estatus CHAR(1)
AS
BEGIN
    INSERT INTO PRODUCTO (codigobarra, descripcion, costo, precio, itbms, existencia, puntoReorden, estatus)
    VALUES (@codigobarra, @descripcion, @costo, @precio, @itbms, @existencia, @puntodereorden, @estatus)
END;

CREATE PROCEDURE ListarClientes1
AS
BEGIN
    SELECT cedula, nombre, apellido, direccion, celular, correo, sexo, estatus
    FROM Cliente
    ORDER BY cedula;
END;

CREATE PROCEDURE BuscarCliente
    @cedula NVARCHAR(15)
AS
BEGIN
    SELECT * 
    FROM Cliente
    WHERE cedula = @cedula
END;

CREATE PROCEDURE ListarClientes2
    @orden NVARCHAR(20)
AS
BEGIN
    DECLARE @query NVARCHAR(MAX)
    SET @query = 'SELECT * FROM Cliente ORDER BY ' + @orden
    EXEC sp_executesql @query
END;

CREATE PROCEDURE ModificarCliente
    @cedula NVARCHAR(15),
    @nombre NVARCHAR(30),
    @apellido NVARCHAR(30),
    @direccion NVARCHAR(50),
    @celular NVARCHAR(9),
    @correo NVARCHAR(50),
    @sexo NCHAR(1),
    @estatus NCHAR(1)
AS
BEGIN
    UPDATE Cliente
    SET nombre = @nombre,
        apellido = @apellido,
        direccion = @direccion,
        celular = @celular,
        correo = @correo,
        sexo = @sexo,
        estatus = @estatus
    WHERE cedula = @cedula
END;

CREATE PROCEDURE EliminarCliente
    @cedula NVARCHAR(15)
AS
BEGIN
    DELETE FROM Cliente
    WHERE cedula = @cedula
END;

CREATE PROCEDURE EliminarCliente1
    @cedula NVARCHAR(15)
AS
BEGIN
    UPDATE Cliente
    SET estatus = 'I'
    WHERE cedula = @cedula
END;

CREATE PROCEDURE AgregarProducto1
    @codigoBarra NVARCHAR(12),
    @descripcion NVARCHAR(50),
    @costo DECIMAL(7, 2),
    @precio DECIMAL(7, 2),
    @itbms INT,
    @existencia INT,
    @puntoReorden INT,
    @estatus NVARCHAR(1)
AS
BEGIN
    INSERT INTO Producto (codigoBarra, descripcion, costo, precio, itbms, existencia, puntoReorden, estatus)
    VALUES (@codigoBarra, @descripcion, @costo, @precio, @itbms, @existencia, @puntoReorden, @estatus)
END;

CREATE PROCEDURE BuscarProducto
    @codigoBarra NVARCHAR(12)
AS
BEGIN
    SELECT *
    FROM Producto
    WHERE codigoBarra = @codigoBarra
END;

CREATE PROCEDURE AgregarProducto2
    @codigoBarra NVARCHAR(12),
    @descripcion NVARCHAR(50),
    @costo DECIMAL(7, 2),
    @precio DECIMAL(7, 2),
    @itbms INT,
    @existencia INT,
    @puntoReorden INT,
    @estatus CHAR(1)
AS
BEGIN
    -- Validar si ya existe un producto con el mismo código de barra
    IF EXISTS (SELECT 1 FROM Producto WHERE codigoBarra = @codigoBarra)
    BEGIN
        THROW 50001, 'Ya existe un producto con este código de barra.', 1
    END

    -- Insertar el nuevo producto
    INSERT INTO Producto (codigoBarra, descripcion, costo, precio, itbms, existencia, puntoReorden, estatus)
    VALUES (@codigoBarra, @descripcion, @costo, @precio, @itbms, @existencia, @puntoReorden, @estatus)
END;

CREATE PROCEDURE ModificarProducto
    @codigoBarra NVARCHAR(12),
    @descripcion NVARCHAR(50),
    @costo DECIMAL(7, 2),
    @precio DECIMAL(7, 2),
    @itbms INT,
    @existencia INT,
    @puntodereorden INT,
    @estatus CHAR(1)
AS
BEGIN
    -- Validar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE codigoBarra = @codigoBarra)
    BEGIN
        THROW 50002, 'El producto no existe.', 1
    END

    -- Actualizar el producto
    UPDATE Producto
    SET descripcion = @descripcion,
        costo = @costo,
        precio = @precio,
        itbms = @itbms,
        existencia = @existencia,
        puntoReorden = @puntodereorden,
        estatus = @estatus
    WHERE codigoBarra = @codigoBarra
END;

CREATE PROCEDURE EliminarProducto
    @codigoBarra NVARCHAR(12)
AS
BEGIN
    -- Validar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE codigoBarra = @codigoBarra)
    BEGIN
        THROW 50002, 'El producto no existe.', 1
    END

    -- Eliminar el producto
    DELETE FROM Producto
    WHERE codigoBarra = @codigoBarra
END;

CREATE PROCEDURE EliminarProducto1
    @codigoBarra NVARCHAR(12)
AS
BEGIN
    -- Validar si el producto existe
    IF NOT EXISTS (SELECT 1 FROM Producto WHERE codigoBarra = @codigoBarra)
    BEGIN
        THROW 50002, 'El producto no existe.', 1
    END

    -- Actualizar el estatus del producto a inactivo
    UPDATE Producto
    SET estatus = 'I'
    WHERE codigoBarra = @codigoBarra
END;

CREATE PROCEDURE ListarProductos
AS
BEGIN
    SELECT codigoBarra,
           descripcion,
           costo,
           precio,
           itbms,
           existencia,
           puntoReorden,
           estatus
    FROM Producto
    ORDER BY descripcion -- Ordenar por descripción de forma predeterminada
END;










