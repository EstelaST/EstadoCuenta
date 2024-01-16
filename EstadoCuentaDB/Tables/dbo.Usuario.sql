CREATE TABLE [dbo].[Usuario]
(
[IdUsuario] [int] NOT NULL,
[Nombre] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Apellido] [varchar] (50) COLLATE Modern_Spanish_CI_AS NULL,
[Edad] [int] NOT NULL,
[Correo] [varchar] (50) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Usuario] [varchar] (20) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Contrasena] [varchar] (100) COLLATE Modern_Spanish_CI_AS NOT NULL,
[Fecha_Crea] [datetime] NOT NULL,
[Fecha_Actualiza] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Usuario] ADD CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED ([IdUsuario]) ON [PRIMARY]
GO
