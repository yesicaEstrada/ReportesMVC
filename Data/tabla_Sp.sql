use [BDReportes]

CREATE TABLE Usuario(
	idUsuario int primary key identity,
	correo varchar(50) not null,
	clave varchar(100) not null,
)


create procedure Sp_RegistrarUsuario(
	@correo varchar(100),
	@clave varchar(500),
	@registrado bit output,
	@mensaje varchar(100) output
)
as
begin

	if(not exists(select * from Usuario where correo = @correo))
	begin 
		insert into Usuario(correo, clave) values (@correo, @clave)
		set @registrado = 1
		set @Mensaje = 'Usuario registrado correctamente'
	end
	else 
	begin
		set @registrado = 0
		set @mensaje = 'El correo ya se encuentra registrado, valide nuevamente o inicie sesion'
	end
end




create proc sp_ValidarUsuario(
	@correo varchar(100),
	@clave varchar(500)
)
as 
begin
	if(exists(select * from Usuario where correo = @correo and clave = @clave))
		select idusuario from Usuario where correo = @correo and clave = @clave
	else
		select '0'
end



-- procedimientos de la tabla personas
CREATE PROCEDURE Sp_ConsultaPersona
AS
BEGIN
SET NOCOUNT ON;
	SELECT IIDPERSONA, APPATERNO, APMATERNO, s.nombre as NOMBRE_SEXO, CORREO, TELEFONOOCELULAR1, t.NOMBRE as TIPODOC, NUMEROIDENTIFICACION 
	FROM [dbo].[Persona] AS p WITH (NOLOCK)
	INNER JOIN [dbo].[Sexo] as s
	on p.IIDSEXO = S.IIDSEXO
	inner join [dbo].[TipoDocumentoIdentificacion] as t
	on p.IIDTIPODOCUMENTO = t.IIDTIPODOCUMENTO
END

EXEC Sp_ConsultaPersona

-- procedimientos de la tabla personas
CREATE PROCEDURE Sp_ConsultaXIdPersona
(
	@IIDPERSONA INT
)
AS
BEGIN
SET NOCOUNT ON;
	SELECT IIDPERSONA, APPATERNO, APMATERNO, IIDSEXO, CORREO, TELEFONOOCELULAR1, IIDTIPODOCUMENTO, NUMEROIDENTIFICACION 
	FROM [dbo].[Persona] AS p WITH (NOLOCK)
	WHERE IIDPERSONA = @IIDPERSONA
END

EXEC Sp_ConsultaXIdPersona 2

CREATE PROCEDURE Sp_ActualizarPersona
(
	@IIDPERSONA INT,
	@APPATERNO varchar(100),
	@APMATERNO varchar(100),
	@IIDSEXO int,
	@CORREO varchar(100),
	@TELEFONOOCELULAR1 varchar(15),
	@IIDTIPODOCUMENTO int,
	@NUMEROIDENTIFICACION varchar(20)
)
AS 
BEGIN
	UPDATE [dbo].[Persona] SET 
	APPATERNO = @APPATERNO,
	APMATERNO = @APMATERNO,
	IIDSEXO = @IIDSEXO,
	CORREO = @CORREO,
	TELEFONOOCELULAR1 = @TELEFONOOCELULAR1,
	IIDTIPODOCUMENTO = @IIDTIPODOCUMENTO,
	NUMEROIDENTIFICACION = @NUMEROIDENTIFICACION
	WHERE IIDPERSONA = @IIDPERSONA
END
