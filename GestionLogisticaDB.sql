USE [master]
GO
/****** Object:  Database [GestionLogistica]    Script Date: 8/12/2023 8:34:55 AM ******/
CREATE DATABASE [GestionLogistica]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GestionLogistica', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\GestionLogistica.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GestionLogistica_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\GestionLogistica_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [GestionLogistica] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GestionLogistica].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GestionLogistica] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestionLogistica] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestionLogistica] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestionLogistica] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestionLogistica] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestionLogistica] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GestionLogistica] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestionLogistica] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestionLogistica] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestionLogistica] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GestionLogistica] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestionLogistica] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestionLogistica] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestionLogistica] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestionLogistica] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GestionLogistica] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestionLogistica] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GestionLogistica] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GestionLogistica] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestionLogistica] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestionLogistica] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GestionLogistica] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GestionLogistica] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GestionLogistica] SET  MULTI_USER 
GO
ALTER DATABASE [GestionLogistica] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GestionLogistica] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GestionLogistica] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GestionLogistica] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GestionLogistica] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GestionLogistica] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [GestionLogistica] SET QUERY_STORE = OFF
GO
USE [GestionLogistica]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 8/12/2023 8:34:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Direccion] [varchar](150) NOT NULL,
	[Celular] [varchar](50) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[EmpresaId] [int] NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[empresa]    Script Date: 8/12/2023 8:34:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[empresa](
	[EmpresaId] [int] IDENTITY(1,1) NOT NULL,
	[NombreEmpresa] [varchar](50) NOT NULL,
	[ContactoEmpresa] [varchar](50) NOT NULL,
 CONSTRAINT [PK_empresa] PRIMARY KEY CLUSTERED 
(
	[EmpresaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[equipo]    Script Date: 8/12/2023 8:34:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[equipo](
	[EquipoId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [varchar](15) NOT NULL,
	[Marca] [varchar](15) NOT NULL,
	[Modelo] [varchar](15) NOT NULL,
	[Cpu] [varchar](20) NOT NULL,
	[RAM] [varchar](10) NOT NULL,
	[CargadorEquipo] [bit] NOT NULL,
 CONSTRAINT [PK_equipo] PRIMARY KEY CLUSTERED 
(
	[EquipoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[gestionenvio]    Script Date: 8/12/2023 8:34:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[gestionenvio](
	[GestionId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[EquipoId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[FechaGestion] [date] NOT NULL,
	[FechaLlegada] [date] NOT NULL,
	[Observaciones] [varchar](200) NULL,
	[MontoAsegurado] [float] NOT NULL,
	[Empaque] [bit] NOT NULL,
 CONSTRAINT [PK_gestionenvio] PRIMARY KEY CLUSTERED 
(
	[GestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 8/12/2023 8:34:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Rol] [varchar](50) NULL,
	[Contraseña] [varchar](256) NOT NULL,
	[Estado] [varchar](20) NULL,
 CONSTRAINT [PK_usuario] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([ClienteId], [Nombre], [Direccion], [Celular], [Correo], [EmpresaId]) VALUES (3, N'string', N'string', N'string', N'string', 1)
INSERT [dbo].[cliente] ([ClienteId], [Nombre], [Direccion], [Celular], [Correo], [EmpresaId]) VALUES (6, N'string', N'string', N'string', N'string', 1)
INSERT [dbo].[cliente] ([ClienteId], [Nombre], [Direccion], [Celular], [Correo], [EmpresaId]) VALUES (7, N'PaolaC', N'direccion', N'12312313', N'@info', 1)
INSERT [dbo].[cliente] ([ClienteId], [Nombre], [Direccion], [Celular], [Correo], [EmpresaId]) VALUES (9, N'string', N'string', N'string', N'string', 1)
INSERT [dbo].[cliente] ([ClienteId], [Nombre], [Direccion], [Celular], [Correo], [EmpresaId]) VALUES (11, N'Prueba11', N'string', N'string', N'string', 1)
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[empresa] ON 

INSERT [dbo].[empresa] ([EmpresaId], [NombreEmpresa], [ContactoEmpresa]) VALUES (1, N'YuxiGlobal', N'yuxi@info.com')
INSERT [dbo].[empresa] ([EmpresaId], [NombreEmpresa], [ContactoEmpresa]) VALUES (6, N'Zinerco S.A.S', N'zinerco@info.com')
INSERT [dbo].[empresa] ([EmpresaId], [NombreEmpresa], [ContactoEmpresa]) VALUES (7, N'Ultracom', N'3154487799')
INSERT [dbo].[empresa] ([EmpresaId], [NombreEmpresa], [ContactoEmpresa]) VALUES (9, N'Empresa9', N'@empresa9')
INSERT [dbo].[empresa] ([EmpresaId], [NombreEmpresa], [ContactoEmpresa]) VALUES (12, N'Empresa12', N'@prueba12')
SET IDENTITY_INSERT [dbo].[empresa] OFF
GO
SET IDENTITY_INSERT [dbo].[equipo] ON 

INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (1, N'PF656AS6D', N'Lenovo', N'E470', N'I5 6500', N'8 GB', 1)
INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (2, N'PF656A123D', N'Lenovo', N'E470', N'I5 6500', N'8 GB', 0)
INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (4, N'5CD80123123', N'Hp 4', N'430', N'i7 8500', N'8 Gb', 0)
INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (6, N'string', N'string', N'5CD6', N'string', N'string', 1)
INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (7, N'5CD80123123', N'Hp', N'430', N'i7 8500', N'8 Gb', 0)
INSERT [dbo].[equipo] ([EquipoId], [Serial], [Marca], [Modelo], [Cpu], [RAM], [CargadorEquipo]) VALUES (8, N'string', N'string', N'NUEVO', N'string', N'string', 1)
SET IDENTITY_INSERT [dbo].[equipo] OFF
GO
SET IDENTITY_INSERT [dbo].[gestionenvio] ON 

INSERT [dbo].[gestionenvio] ([GestionId], [ClienteId], [EquipoId], [UsuarioId], [FechaGestion], [FechaLlegada], [Observaciones], [MontoAsegurado], [Empaque]) VALUES (1, 7, 1, 3, CAST(N'2023-08-11' AS Date), CAST(N'2023-08-11' AS Date), N'Pantalla quebrada', 3000000, 1)
INSERT [dbo].[gestionenvio] ([GestionId], [ClienteId], [EquipoId], [UsuarioId], [FechaGestion], [FechaLlegada], [Observaciones], [MontoAsegurado], [Empaque]) VALUES (2, 7, 1, 3, CAST(N'2023-08-11' AS Date), CAST(N'2023-08-11' AS Date), N'string', 0, 1)
INSERT [dbo].[gestionenvio] ([GestionId], [ClienteId], [EquipoId], [UsuarioId], [FechaGestion], [FechaLlegada], [Observaciones], [MontoAsegurado], [Empaque]) VALUES (7, 7, 1, 3, CAST(N'2023-08-11' AS Date), CAST(N'2023-08-11' AS Date), N'string', 50002, 0)
INSERT [dbo].[gestionenvio] ([GestionId], [ClienteId], [EquipoId], [UsuarioId], [FechaGestion], [FechaLlegada], [Observaciones], [MontoAsegurado], [Empaque]) VALUES (8, 7, 1, 3, CAST(N'2023-08-11' AS Date), CAST(N'2023-08-11' AS Date), N'Pantalla quebrada y sin mouse', 200000, 1)
SET IDENTITY_INSERT [dbo].[gestionenvio] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([UsuarioId], [Nombre], [Email], [Telefono], [Rol], [Contraseña], [Estado]) VALUES (3, N'santiago', N'santiago@administrador', N'11231', N'Administrador', N'30362306091ef8d489fd8e92de49e5a40f3a2ffb6bda952f178fe394d8492fd9', N'Activo')
INSERT [dbo].[usuario] ([UsuarioId], [Nombre], [Email], [Telefono], [Rol], [Contraseña], [Estado]) VALUES (4, N'asdad', N'santiago', N'123', N'Administrador', N'30362306091ef8d489fd8e92de49e5a40f3a2ffb6bda952f178fe394d8492fd9', N'Activo')
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[cliente]  WITH CHECK ADD  CONSTRAINT [FK_cliente_empresa] FOREIGN KEY([EmpresaId])
REFERENCES [dbo].[empresa] ([EmpresaId])
GO
ALTER TABLE [dbo].[cliente] CHECK CONSTRAINT [FK_cliente_empresa]
GO
ALTER TABLE [dbo].[gestionenvio]  WITH CHECK ADD  CONSTRAINT [FK_gestionenvio_cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[gestionenvio] CHECK CONSTRAINT [FK_gestionenvio_cliente]
GO
ALTER TABLE [dbo].[gestionenvio]  WITH CHECK ADD  CONSTRAINT [FK_gestionenvio_equipo] FOREIGN KEY([EquipoId])
REFERENCES [dbo].[equipo] ([EquipoId])
GO
ALTER TABLE [dbo].[gestionenvio] CHECK CONSTRAINT [FK_gestionenvio_equipo]
GO
ALTER TABLE [dbo].[gestionenvio]  WITH CHECK ADD  CONSTRAINT [FK_gestionenvio_usuario] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[usuario] ([UsuarioId])
GO
ALTER TABLE [dbo].[gestionenvio] CHECK CONSTRAINT [FK_gestionenvio_usuario]
GO
USE [master]
GO
ALTER DATABASE [GestionLogistica] SET  READ_WRITE 
GO
