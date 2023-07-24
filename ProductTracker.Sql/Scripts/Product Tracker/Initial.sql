USE [master]
GO
/****** Object:  Database [ProductTracker]    Script Date: 7/24/2023 11:17:06 AM ******/
CREATE DATABASE [ProductTracker]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProductTracker', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProductTracker.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProductTracker_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ProductTracker_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ProductTracker] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProductTracker].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProductTracker] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProductTracker] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProductTracker] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProductTracker] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProductTracker] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProductTracker] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ProductTracker] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProductTracker] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProductTracker] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProductTracker] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProductTracker] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProductTracker] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProductTracker] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProductTracker] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProductTracker] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ProductTracker] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProductTracker] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProductTracker] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProductTracker] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProductTracker] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProductTracker] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProductTracker] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProductTracker] SET RECOVERY FULL 
GO
ALTER DATABASE [ProductTracker] SET  MULTI_USER 
GO
ALTER DATABASE [ProductTracker] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProductTracker] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProductTracker] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProductTracker] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProductTracker] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ProductTracker', N'ON'
GO
ALTER DATABASE [ProductTracker] SET QUERY_STORE = OFF
GO
USE [ProductTracker]
GO
/****** Object:  User [TestSC]    Script Date: 7/24/2023 11:17:06 AM ******/
CREATE USER [TestSC] FOR LOGIN [TestSC] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [anil]    Script Date: 7/24/2023 11:17:06 AM ******/
CREATE USER [anil] FOR LOGIN [anil] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 7/24/2023 11:17:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 7/24/2023 11:17:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[FSSICode] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[Price] [decimal](18, 0) NULL,
 CONSTRAINT [PK_ProductDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Contact] ([ContactId], [FirstName], [LastName], [Email], [PhoneNumber]) VALUES (1, N'Test', N'TestLast', N'Test@Test.Com', N'987866467')
SET IDENTITY_INSERT [dbo].[ProductDetail] ON 

INSERT [dbo].[ProductDetail] ([Id], [ProductId], [Quantity], [FSSICode], [Description], [Price]) VALUES (1, N'TRDFER654RD654GF', 25, N'10022XXX000000', N'Product One', CAST(100 AS Decimal(18, 0)))
INSERT [dbo].[ProductDetail] ([Id], [ProductId], [Quantity], [FSSICode], [Description], [Price]) VALUES (2, N'TRDFER654RD6AFDSE', 50, N'10022ZZZ000000', N'Product Two', CAST(250 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[ProductDetail] OFF
USE [master]
GO
ALTER DATABASE [ProductTracker] SET  READ_WRITE 
GO
