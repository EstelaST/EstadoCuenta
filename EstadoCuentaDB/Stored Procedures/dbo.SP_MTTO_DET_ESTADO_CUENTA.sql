SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE [dbo].[SP_MTTO_DET_ESTADO_CUENTA]
(
	@TIPO_ACTUALIZA INT,
	@IDMAEESTADOCUENTA INT ,
	@IDCUENTA INT,
	@IDDETESTADOCUENTA INT OUTPUT,
	@FECHA_TRANSACCION DATETIME = NULL,
	@CONCEPTO NVARCHAR(500) = NULL,
	@CARGOS DECIMAL(18,4) = NULL,
	@ABONOS DECIMAL(18,4) = NULL,
	@FILAS_AFECTADAS INT OUTPUT,
	@NUMERO_ERROR NUMERIC(38,0) OUTPUT,
	@MENSAJE_ERROR NVARCHAR(4000) OUTPUT
)
AS 
BEGIN
	
	SET NOCOUNT ON

	SELECT @NUMERO_ERROR=0,@MENSAJE_ERROR='',@FILAS_AFECTADAS=0

	BEGIN TRY 
		IF @TIPO_ACTUALIZA=1 --Adicionar
		BEGIN
			SELECT @IDDETESTADOCUENTA =ISNULL(MAX(IdDetEstadoCuenta),0)+1
			FROM DetEstadoCuenta WITH(NOLOCK)

			IF ISNULL(@IDMAEESTADOCUENTA,0)=0 
			BEGIN 
				DECLARE @IDMAE_ESTADO_CUENTA INT, @FECHA_CORTE DATE  
			
				SELECT @IDMAE_ESTADO_CUENTA =ISNULL(MAX(IdMaeEstadoCuenta),0)+1
				FROM MaeEstadoCuenta WITH(NOLOCK)

				SELECT @FECHA_CORTE = DATEFROMPARTS( YEAR(GETDATE()), MONTH(GETDATE()), DiaCorte)
				FROM Cuenta WITH(NOLOCK)
				WHERE IdCuenta = @IDCUENTA

				INSERT INTO MaeEstadoCuenta(IdMaeEstadoCuenta, IdCuenta, Mes, Anio, FechaCorte, InteresBonificable, PagoContado, SaldoTotal, Fecha_Crea, Fecha_Modifica)
				VALUES(@IDMAE_ESTADO_CUENTA,@IDCUENTA,MONTH(GETDATE()),YEAR(GETDATE()),@FECHA_CORTE,0,0,0, GETDATE(), GETDATE())

				SELECT @IDMAEESTADOCUENTA = @IDMAE_ESTADO_CUENTA
			END 

			INSERT INTO dbo.DetEstadoCuenta(IdMaeEstadoCuenta,IdDetEstadoCuenta,FechaTransaccion,Concepto,Cargos,Abonos, Fecha_Crea, Fecha_Modifica)
			VALUES(@IDMAEESTADOCUENTA,@IDDETESTADOCUENTA,@FECHA_TRANSACCION,@CONCEPTO,@CARGOS,@ABONOS, GETDATE(), GETDATE())

		END ELSE
		IF @TIPO_ACTUALIZA=2 --Actualizar
		BEGIN
			UPDATE DetEstadoCuenta SET 
			FechaTransaccion = @FECHA_TRANSACCION,
			Concepto = @CONCEPTO,
			Cargos = @CARGOS,
			Abonos = @ABONOS,
			Fecha_Modifica = GETDATE()
			WHERE IdMaeEstadoCuenta = @IDMAEESTADOCUENTA
			AND IdDetEstadoCuenta = @IDDETESTADOCUENTA

		END ELSE
		IF @TIPO_ACTUALIZA=3 --Eliminar
		BEGIN

				--DECLARE @INTERES_BONIFICABLE DECIMAL(18,4), @PAGO_CONTADO DECIMAL(14,4), @SALDO_TOTAL DECIMAL(18,4)

				--SELECT 
				--		@PAGO_CONTADO =	 SUM(A.Cargos) - SUM(A.Abonos),
				--		@INTERES_BONIFICABLE = ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres), 
				--		@SALDO_TOTAL = 	(SUM(A.Cargos) - SUM(A.Abonos)) + ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres)
				--FROM dbo.DetEstadoCuenta A
				--INNER JOIN MaeEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				--INNER JOIN Cuenta C WITH(NOLOCK) ON B.IdCuenta = C.IdCuenta
				--WHERE B.IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				--AND IdDetEstadoCuenta <> @IDDETESTADOCUENTA
				--GROUP BY	A.IdMaeEstadoCuenta,
    --                        C.IdCuenta,
    --                        C.PorcentajeInteres

				--UPDATE MaeEstadoCuenta SET 
				--InteresBonificable = @INTERES_BONIFICABLE,
				--PagoContado = @PAGO_CONTADO,
				--SaldoTotal = @SALDO_TOTAL,
				--Fecha_Modifica = GETDATE()
				--WHERE IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				--AND  IdCuenta =  @IDCUENTA

				--SELECT 
				--@SALDO_TOTAL =  SUM(SaldoTotal),
				--@PAGO_CONTADO = SUM(PagoContado)
				--FROM MaeEstadoCuenta 	WITH(NOLOCK)			
				--WHERE  IdCuenta = @IDCUENTA

				--UPDATE Cuenta  SET Saldo = @SALDO_TOTAL, PagoContado = @PAGO_CONTADO,Fecha_Modifica = GETDATE()
				--WHERE IdCuenta = @IDCUENTA

				DELETE FROM DetEstadoCuenta
			    WHERE  IdMaeEstadoCuenta = @IDMAEESTADOCUENTA
			    AND IdDetEstadoCuenta = @IDDETESTADOCUENTA

		END
		
		SELECT @FILAS_AFECTADAS=@@ROWCOUNT
	
	END TRY

	BEGIN CATCH

		SELECT @NUMERO_ERROR=ERROR_NUMBER(), @MENSAJE_ERROR=ERROR_MESSAGE(), @FILAS_AFECTADAS = 0
	END CATCH;
FINA:
END
GO
