
create database AppLogin 
go
use AppLogin  
CREATE TABLE Usuarios(
	Id int IDENTITY(1,1) NOT NULL,
	Dni char(8) NULL,
	Nombres nvarchar(50) NULL,
	Apellidos nvarchar(50) NULL,
	Cargo nvarchar(50) NULL,
	Email nvarchar(50) NULL,
	Contraseña nvarchar(50) NULL
	)

insert into usuarios values ('73635171','Johnatan','Fernandez Selmon','Administrador','Roma@gmail.com','admin')

CREATE PROCEDURE SpLogin 
    @Usuario nvarchar(50), 
    @Password nvarchar(50) 
AS 
    SELECT * FROM Usuarios
    WHERE Dni = @Usuario AND Contraseña = @Password;
GO

execute SpLogin @Usuario='73635171',@Password='admin'