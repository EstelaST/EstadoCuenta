CREATE TABLE [dbo].[MaeEstadoCuenta]
(
[IdMaeEstadoCuenta] [int] NOT NULL,
[IdCuenta] [int] NOT NULL,
[Mes] [int] NOT NULL,
[Anio] [int] NOT NULL,
[FechaCorte] [date] NOT NULL,
[InteresBonificable] [decimal] (18, 4) NOT NULL,
[PagoContado] [decimal] (18, 4) NOT NULL,
[SaldoTotal] [decimal] (18, 4) NOT NULL,
[Fecha_Crea] [datetime] NULL,
[Fecha_Modifica] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MaeEstadoCuenta] ADD CONSTRAINT [PK_MaeEstadoCuenta] PRIMARY KEY CLUSTERED ([IdMaeEstadoCuenta]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MaeEstadoCuenta] ADD CONSTRAINT [FK_MaeEstadoCuenta_Cuenta] FOREIGN KEY ([IdCuenta]) REFERENCES [dbo].[Cuenta] ([IdCuenta])
GO
