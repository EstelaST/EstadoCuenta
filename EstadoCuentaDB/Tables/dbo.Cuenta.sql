CREATE TABLE [dbo].[Cuenta]
(
[IdCuenta] [int] NOT NULL,
[IdUsuario] [int] NOT NULL,
[NumeroTarjeta] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[PorcentajeInteres] [decimal] (10, 4) NOT NULL,
[PorcentajePagoMin] [decimal] (10, 4) NOT NULL,
[LimiteCredito] [decimal] (18, 4) NOT NULL,
[DiaCorte] [int] NOT NULL,
[PagoContado] [decimal] (18, 4) NULL,
[Saldo] [decimal] (18, 4) NOT NULL,
[Fecha_Crea] [datetime] NULL,
[Fecha_Modifica] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cuenta] ADD CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED ([IdCuenta]) ON [PRIMARY]
GO
