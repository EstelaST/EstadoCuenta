SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[SP_DATA_ESTADO_CUENTA]
(
	@TIPO_CONSULTA INT,
	@IDUSUARIO INT,
	@IDCUENTA INT OUTPUT,
	@MES INT = NULL
)
AS 
BEGIN

		IF @TIPO_CONSULTA=1 -- Estado de Cuentas Del Usuario
		BEGIN
				SELECT DISTINCT
							A.Nombre +' ' + A.Apellido AS Nombre,
							A.Usuario, 
							A.Correo,
							B.Fecha_Crea AS FechaApertura,
							B.NumeroTarjeta AS NumeroTarjeta,
							B.LimiteCredito,
							B.PagoContado * B.PorcentajePagoMin AS PagoMinimo,
							B.LimiteCredito - B.PagoContado AS SaldoDisponible,
							B.PagoContado * B.PorcentajeInteres AS InteresBonificable,
							B.PagoContado,
							B.Saldo AS SaldoActual
				FROM Usuario A WITH(NOLOCK)
				INNER JOIN Cuenta B WITH(NOLOCK) ON B.IdUsuario = A.IdUsuario
				LEFT JOIN MaeEstadoCuenta C WITH(NOLOCK) ON C.IdCuenta = B.IdCuenta
				LEFT JOIN DetEstadoCuenta D WITH(NOLOCK) ON D.IdMaeEstadoCuenta = C.IdMaeEstadoCuenta
				WHERE A.IdUsuario = @IDUSUARIO

		END ELSE
		IF @TIPO_CONSULTA=2 -- Encabezado de estado de cuenta por mes 
		BEGIN
				SELECT DISTINCT
							D.Nombre +' ' + D.Apellido AS Nombre,
							D.Usuario, 
							D.Correo,
							C.Fecha_Crea AS FechaApertura,
							A.FechaCorte,
							C.NumeroTarjeta AS NumeroTarjeta,
							C.LimiteCredito,
							C.PagoContado * C.PorcentajePagoMin AS PagoMinimo,
							C.LimiteCredito - C.PagoContado AS SaldoDisponible,
							A.InteresBonificable, -- MES
							A.PagoContado,
							A.SaldoTotal AS SaldoActual -- MES
				FROM MaeEstadoCuenta A WITH(NOLOCK) 
				INNER JOIN DetEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				INNER	JOIN Cuenta C WITH(NOLOCK) ON A.IdCuenta = C.IdCuenta
				INNER JOIN Usuario D WITH(NOLOCK) ON C.IdUsuario = D.IdUsuario
				WHERE  C.IdCuenta = @IDCUENTA
				AND A.Mes = @MES

		END ELSE
		IF @TIPO_CONSULTA=3 -- Detalle de compras de la cuenta segun el mes 
		BEGIN
				SELECT 
							B.FechaTransaccion,
							B.Concepto,
							ISNULL(B.Cargos,0) Cargos,
							ISNULL(B.Abonos,0) Abonos
				FROM MaeEstadoCuenta A WITH(NOLOCK) 
				INNER JOIN DetEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				INNER	JOIN Cuenta C WITH(NOLOCK) ON A.IdCuenta = C.IdCuenta
				WHERE  C.IdCuenta = @IDCUENTA
				AND A.Mes = @IDCUENTA
		END ELSE
		SELECT 'No existe la Consulta'

END
GO
