CREATE TABLE [dbo].[DetEstadoCuenta]
(
[IdMaeEstadoCuenta] [int] NOT NULL,
[IdDetEstadoCuenta] [int] NOT NULL,
[FechaTransaccion] [datetime] NOT NULL,
[Concepto] [nvarchar] (500) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Cargos] [decimal] (18, 4) NULL,
[Abonos] [decimal] (18, 4) NULL,
[Fecha_Crea] [datetime] NULL,
[Fecha_Modifica] [datetime] NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE TRIGGER [dbo].[ADD_ESTADO_CUENTA] ON [dbo].[DetEstadoCuenta] AFTER INSERT
AS 
	DECLARE  @IDMAE_ESTADO_CUENTA INT, @IDCUENTA   INT, @INTERES_BONIFICABLE DECIMAL(18,4), @PAGO_CONTADO DECIMAL(14,4), @SALDO_TOTAL DECIMAL(18,4)
	BEGIN
				SELECT @IDMAE_ESTADO_CUENTA = IdMaeEstadoCuenta FROM Inserted
				SELECT 
						@IDCUENTA = C.IdCuenta,
						@PAGO_CONTADO =	SUM(A.Cargos) - SUM(A.Abonos), -- Sub total sin intereses del mes / Pago al contado
						@INTERES_BONIFICABLE = ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres), -- Interes Bonificable 
						@SALDO_TOTAL = 	(SUM(A.Cargos) - SUM(A.Abonos)) + ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres)
				FROM dbo.DetEstadoCuenta A
				INNER JOIN MaeEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				INNER JOIN Cuenta C WITH(NOLOCK) ON B.IdCuenta = C.IdCuenta
				WHERE B.IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				GROUP BY	A.IdMaeEstadoCuenta,
                            C.IdCuenta,
                            C.PorcentajeInteres

				UPDATE MaeEstadoCuenta SET 
				InteresBonificable = @INTERES_BONIFICABLE,
				PagoContado = @PAGO_CONTADO,
				SaldoTotal = @SALDO_TOTAL,
				Fecha_Modifica = GETDATE()
				WHERE IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				AND  IdCuenta =  @IDCUENTA

				SELECT 
				@SALDO_TOTAL =  SUM(SaldoTotal),
				@PAGO_CONTADO = SUM(PagoContado)
				FROM MaeEstadoCuenta 	WITH(NOLOCK)			
				WHERE  IdCuenta = @IDCUENTA

				UPDATE Cuenta  SET Saldo = @SALDO_TOTAL, PagoContado = @PAGO_CONTADO ,Fecha_Modifica = GETDATE()
				WHERE IdCuenta = @IDCUENTA
	END
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[DELETE_ESTADO_CUENTA] ON [dbo].[DetEstadoCuenta] AFTER DELETE
AS 
	DECLARE  @IDMAE_ESTADO_CUENTA INT, @IDCUENTA   INT, @INTERES_BONIFICABLE DECIMAL(18,4), @PAGO_CONTADO DECIMAL(14,4),
					@SALDO_TOTAL DECIMAL(18,4), @IDDETESTADOCUENTA  INT = 0
	BEGIN
				SELECT @IDMAE_ESTADO_CUENTA = IdMaeEstadoCuenta, @IDDETESTADOCUENTA = IdDetEstadoCuenta FROM Deleted
				SELECT 
						@IDCUENTA = C.IdCuenta,
						@PAGO_CONTADO = ISNULL(SUM(A.Cargos) - SUM(A.Abonos),0), -- Sub total sin intereses del mes / Pago al contado
						@INTERES_BONIFICABLE = ISNULL(((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres),0), -- Interes Bonificable 
						@SALDO_TOTAL = 	ISNULL((SUM(A.Cargos) - SUM(A.Abonos)) + ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres),0)
				FROM dbo.DetEstadoCuenta A
				INNER JOIN MaeEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				INNER JOIN Cuenta C WITH(NOLOCK) ON B.IdCuenta = C.IdCuenta
				WHERE B.IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				AND IdDetEstadoCuenta <> @IDDETESTADOCUENTA
				GROUP BY	A.IdMaeEstadoCuenta,
                            C.IdCuenta,
                            C.PorcentajeInteres

				UPDATE MaeEstadoCuenta SET 
				InteresBonificable = @INTERES_BONIFICABLE,
				PagoContado = @PAGO_CONTADO,
				SaldoTotal = @SALDO_TOTAL,
				Fecha_Modifica = GETDATE()
				WHERE IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				AND  IdCuenta =  @IDCUENTA

				SELECT 
				@SALDO_TOTAL =  SUM(SaldoTotal),
				@PAGO_CONTADO = SUM(PagoContado)
				FROM MaeEstadoCuenta 	WITH(NOLOCK)			
				WHERE  IdCuenta = @IDCUENTA

				UPDATE Cuenta  SET Saldo = @SALDO_TOTAL, PagoContado = @PAGO_CONTADO,Fecha_Modifica = GETDATE()
				WHERE IdCuenta = @IDCUENTA
	END
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

CREATE TRIGGER [dbo].[UPDATE_ESTADO_CUENTA] ON [dbo].[DetEstadoCuenta] AFTER UPDATE
AS 
	DECLARE  @IDMAE_ESTADO_CUENTA INT, @IDCUENTA   INT, @INTERES_BONIFICABLE DECIMAL(18,4), @PAGO_CONTADO DECIMAL(14,4), @SALDO_TOTAL DECIMAL(18,4)
	BEGIN
				SELECT @IDMAE_ESTADO_CUENTA = IdMaeEstadoCuenta FROM Inserted
				SELECT 
						@IDCUENTA = C.IdCuenta,
						@PAGO_CONTADO =	SUM(A.Cargos) - SUM(A.Abonos), -- Sub total sin intereses del mes / Pago al contado
						@INTERES_BONIFICABLE = ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres), -- Interes Bonificable 
						@SALDO_TOTAL = 	(SUM(A.Cargos) - SUM(A.Abonos)) + ((SUM(A.Cargos) - SUM(A.Abonos)) * C.PorcentajeInteres)
				FROM dbo.DetEstadoCuenta A
				INNER JOIN MaeEstadoCuenta B WITH(NOLOCK) ON A.IdMaeEstadoCuenta = B.IdMaeEstadoCuenta
				INNER JOIN Cuenta C WITH(NOLOCK) ON B.IdCuenta = C.IdCuenta
				WHERE B.IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				GROUP BY	A.IdMaeEstadoCuenta,
                            C.IdCuenta,
                            C.PorcentajeInteres

				UPDATE MaeEstadoCuenta SET 
				InteresBonificable = @INTERES_BONIFICABLE,
				PagoContado = @PAGO_CONTADO,
				SaldoTotal = @SALDO_TOTAL,
				Fecha_Modifica = GETDATE()
				WHERE IdMaeEstadoCuenta = @IDMAE_ESTADO_CUENTA
				AND  IdCuenta =  @IDCUENTA

				SELECT 
				@SALDO_TOTAL =  SUM(SaldoTotal),
				@PAGO_CONTADO = SUM(PagoContado)
				FROM MaeEstadoCuenta 	WITH(NOLOCK)			
				WHERE IdCuenta = @IDCUENTA

				UPDATE Cuenta  SET Saldo = @SALDO_TOTAL, PagoContado = @PAGO_CONTADO ,Fecha_Modifica = GETDATE()
				WHERE IdCuenta = @IDCUENTA
	END
GO
ALTER TABLE [dbo].[DetEstadoCuenta] ADD CONSTRAINT [PK_DetEstadoCuenta] PRIMARY KEY CLUSTERED ([IdDetEstadoCuenta]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetEstadoCuenta] ADD CONSTRAINT [FK_DetEstadoCuenta_MaeEstadoCuenta] FOREIGN KEY ([IdMaeEstadoCuenta]) REFERENCES [dbo].[MaeEstadoCuenta] ([IdMaeEstadoCuenta])
GO
