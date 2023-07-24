﻿USE [master]
GO
/****** Object:  Database [Admin]    Script Date: 7/24/2023 11:20:13 AM ******/
CREATE DATABASE [Admin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Admin', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Admin.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Admin_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Admin_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Admin] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Admin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Admin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Admin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Admin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Admin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Admin] SET ARITHABORT OFF 
GO
ALTER DATABASE [Admin] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Admin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Admin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Admin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Admin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Admin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Admin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Admin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Admin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Admin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Admin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Admin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Admin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Admin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Admin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Admin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Admin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Admin] SET RECOVERY FULL 
GO
ALTER DATABASE [Admin] SET  MULTI_USER 
GO
ALTER DATABASE [Admin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Admin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Admin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Admin] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Admin] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Admin', N'ON'
GO
ALTER DATABASE [Admin] SET QUERY_STORE = OFF
GO
USE [Admin]
GO
/****** Object:  User [TestSC]    Script Date: 7/24/2023 11:20:14 AM ******/
CREATE USER [TestSC] FOR LOGIN [TestSC] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [anil]    Script Date: 7/24/2023 11:20:14 AM ******/
CREATE USER [anil] FOR LOGIN [anil] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Organization]    Script Date: 7/24/2023 11:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgName] [varchar](150) NULL,
	[AliasName] [varchar](4) NULL,
	[Logo] [image] NULL,
	[DBPath] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[DeActivationDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveOrganization]    Script Date: 7/24/2023 11:20:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[usp_SaveOrganization]
(
	@OrgName varchar(200), 
	@AliasName Varchar(4),
	@DBPath Varchar(200),
	@DeActivationDate Datetime,
	@CreatedBy int
)
AS
Begin
	
	Insert into [dbo].[Organization](OrgName,AliasName, DBPath,IsActive, DeActivationDate,CreatedBy, CreatedOn)
	Values (@OrgName,@AliasName,@DBPath,1,@DeActivationDate, @CreatedBy, GetDate())

End
GO
USE [master]
GO
ALTER DATABASE [Admin] SET  READ_WRITE 
GO
