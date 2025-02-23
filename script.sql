USE [master]
GO
/****** Object:  Database [MetroDatabase]    Script Date: 29.07.2024 15:38:42 ******/
CREATE DATABASE [MetroDatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MetroDatabase', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MetroDatabase.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MetroDatabase_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MetroDatabase_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MetroDatabase] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MetroDatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MetroDatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MetroDatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MetroDatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MetroDatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MetroDatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [MetroDatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MetroDatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MetroDatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MetroDatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MetroDatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MetroDatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MetroDatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MetroDatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MetroDatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MetroDatabase] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MetroDatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MetroDatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MetroDatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MetroDatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MetroDatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MetroDatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MetroDatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MetroDatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [MetroDatabase] SET  MULTI_USER 
GO
ALTER DATABASE [MetroDatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MetroDatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MetroDatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MetroDatabase] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MetroDatabase] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MetroDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MetroDatabase', N'ON'
GO
ALTER DATABASE [MetroDatabase] SET QUERY_STORE = ON
GO
ALTER DATABASE [MetroDatabase] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MetroDatabase]
GO
/****** Object:  Table [dbo].[Branch]    Script Date: 29.07.2024 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branch](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[StationStartId] [int] NULL,
	[StationEndId] [int] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StartToEnd]    Script Date: 29.07.2024 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StartToEnd](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationStartId] [int] NULL,
	[StationEndId] [int] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_StartToEnd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Station]    Script Date: 29.07.2024 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Station](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
 CONSTRAINT [PK_Station] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Train]    Script Date: 29.07.2024 15:38:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Train](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationId] [int] NULL,
	[IsUp] [bit] NULL,
	[IsRun] [bit] NULL,
	[BranchId] [int] NULL,
 CONSTRAINT [PK_Train] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Branch] ON 

INSERT [dbo].[Branch] ([Id], [Name], [StationStartId], [StationEndId]) VALUES (1, N'Казанская линия', 1, 5)
INSERT [dbo].[Branch] ([Id], [Name], [StationStartId], [StationEndId]) VALUES (2, N'Московская линия', 6, 11)
INSERT [dbo].[Branch] ([Id], [Name], [StationStartId], [StationEndId]) VALUES (3, N'Ленинградская линия', 12, 17)
SET IDENTITY_INSERT [dbo].[Branch] OFF
GO
SET IDENTITY_INSERT [dbo].[StartToEnd] ON 

INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (1, 1, 2, 1)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (2, 2, 3, 1)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (3, 3, 4, 1)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (4, 4, 5, 1)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (5, 6, 7, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (6, 7, 8, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (7, 8, 9, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (8, 9, 4, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (9, 4, 10, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (10, 10, 11, 2)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (11, 12, 13, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (12, 13, 2, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (13, 2, 14, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (14, 14, 15, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (15, 15, 4, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (16, 4, 16, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (17, 16, 8, 3)
INSERT [dbo].[StartToEnd] ([Id], [StationStartId], [StationEndId], [BranchId]) VALUES (18, 8, 17, 3)
SET IDENTITY_INSERT [dbo].[StartToEnd] OFF
GO
SET IDENTITY_INSERT [dbo].[Station] ON 

INSERT [dbo].[Station] ([Id], [Name]) VALUES (1, N'Дубравная')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (2, N'Проспект победы')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (3, N'Горки')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (4, N'Аметьево')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (5, N'Суконная слобода')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (6, N'Северноатланчическая')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (7, N'Проспект Щаранского 2')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (8, N'Институт Демократии')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (9, N'Кавказкий хребет')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (10, N'Свидетельская')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (11, N'Боровая')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (12, N'Приморская')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (13, N'Гостинный двор')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (14, N'Маяковская')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (15, N'Лиговская')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (16, N'Красногвардейская')
INSERT [dbo].[Station] ([Id], [Name]) VALUES (17, N'Технологический институт')
SET IDENTITY_INSERT [dbo].[Station] OFF
GO
SET IDENTITY_INSERT [dbo].[Train] ON 

INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (1, 2, 0, 1, 1)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (2, 1, 0, 1, 1)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (3, 3, 0, 0, 1)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (4, 4, 1, 0, 1)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (5, 7, 1, 1, 2)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (6, 8, 0, 1, 3)
INSERT [dbo].[Train] ([Id], [StationId], [IsUp], [IsRun], [BranchId]) VALUES (7, 14, 1, 1, 3)
SET IDENTITY_INSERT [dbo].[Train] OFF
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Station] FOREIGN KEY([StationStartId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Station]
GO
ALTER TABLE [dbo].[Branch]  WITH CHECK ADD  CONSTRAINT [FK_Branch_Station1] FOREIGN KEY([StationEndId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Branch] CHECK CONSTRAINT [FK_Branch_Station1]
GO
ALTER TABLE [dbo].[StartToEnd]  WITH CHECK ADD  CONSTRAINT [FK_StartToEnd_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[StartToEnd] CHECK CONSTRAINT [FK_StartToEnd_Branch]
GO
ALTER TABLE [dbo].[StartToEnd]  WITH CHECK ADD  CONSTRAINT [FK_StartToEnd_Station] FOREIGN KEY([StationStartId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[StartToEnd] CHECK CONSTRAINT [FK_StartToEnd_Station]
GO
ALTER TABLE [dbo].[StartToEnd]  WITH CHECK ADD  CONSTRAINT [FK_StartToEnd_Station1] FOREIGN KEY([StationEndId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[StartToEnd] CHECK CONSTRAINT [FK_StartToEnd_Station1]
GO
ALTER TABLE [dbo].[Train]  WITH CHECK ADD  CONSTRAINT [FK_Train_Branch] FOREIGN KEY([BranchId])
REFERENCES [dbo].[Branch] ([Id])
GO
ALTER TABLE [dbo].[Train] CHECK CONSTRAINT [FK_Train_Branch]
GO
ALTER TABLE [dbo].[Train]  WITH CHECK ADD  CONSTRAINT [FK_Train_Station] FOREIGN KEY([StationId])
REFERENCES [dbo].[Station] ([Id])
GO
ALTER TABLE [dbo].[Train] CHECK CONSTRAINT [FK_Train_Station]
GO
USE [master]
GO
ALTER DATABASE [MetroDatabase] SET  READ_WRITE 
GO
