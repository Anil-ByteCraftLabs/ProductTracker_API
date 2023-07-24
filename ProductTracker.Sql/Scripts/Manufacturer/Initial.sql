USE [master]
GO
/****** Object:  Database [Manufacturer]    Script Date: 7/24/2023 11:20:56 AM ******/
CREATE DATABASE [Manufacturer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Manufacturer', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Manufacturer.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Manufacturer_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\Manufacturer_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Manufacturer] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Manufacturer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Manufacturer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Manufacturer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Manufacturer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Manufacturer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Manufacturer] SET ARITHABORT OFF 
GO
ALTER DATABASE [Manufacturer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Manufacturer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Manufacturer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Manufacturer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Manufacturer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Manufacturer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Manufacturer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Manufacturer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Manufacturer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Manufacturer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Manufacturer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Manufacturer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Manufacturer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Manufacturer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Manufacturer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Manufacturer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Manufacturer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Manufacturer] SET RECOVERY FULL 
GO
ALTER DATABASE [Manufacturer] SET  MULTI_USER 
GO
ALTER DATABASE [Manufacturer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Manufacturer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Manufacturer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Manufacturer] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Manufacturer] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Manufacturer', N'ON'
GO
ALTER DATABASE [Manufacturer] SET QUERY_STORE = OFF
GO
USE [Manufacturer]
GO
/****** Object:  User [TestSC]    Script Date: 7/24/2023 11:20:56 AM ******/
CREATE USER [TestSC] FOR LOGIN [TestSC] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [anil]    Script Date: 7/24/2023 11:20:57 AM ******/
CREATE USER [anil] FOR LOGIN [anil] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[BatchData]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BatchData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[BatchNo] [varchar](50) NULL,
	[NoOfCoupons] [int] NULL,
	[NoOfPrintedCoupons] [nchar](10) NULL,
	[Status] [int] NULL,
	[IsActive] [bit] NULL,
	[OrgId] [int] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_BatchData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CouponsData]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CouponsData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UniqueId] [nvarchar](200) NULL,
	[BatchId] [int] NULL,
	[OrgAliasName] [varchar](4) NULL,
	[ParentCouponId] [int] NULL,
	[IsScanned] [bit] NULL,
	[ScannedDate] [datetime] NULL,
	[ScannedBy] [int] NULL,
	[ScannedLocation] [geography] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_CouponsData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductData]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](150) NULL,
	[ProductType] [int] NULL,
	[ProductWeight] [int] NULL,
	[FssaiCode] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductData] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductPrice]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductPrice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_ProductPrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductType]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varbinary](150) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_ProductType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductWeight]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductWeight](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varbinary](150) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_ProductWeight] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GenerateUniqueCode]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GenerateUniqueCode]
    @CodeLength INT,
    @GeneratedCode VARCHAR(50) OUT
AS
BEGIN
    DECLARE @Characters VARCHAR(50) = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789';
    DECLARE @Code VARCHAR(50);
    DECLARE @IsUnique BIT = 0;

    WHILE @IsUnique = 0
    BEGIN
        SET @Code = '';
        WHILE LEN(@Code) < @CodeLength
        BEGIN
            SET @Code = @Code + SUBSTRING(@Characters, (ABS(CHECKSUM(NEWID())) % LEN(@Characters)) + 1, 1);
        END

        --IF NOT EXISTS (1=1)
        --BEGIN
            SET @IsUnique = 1;
        --END
    END

    SET @GeneratedCode = @Code;
END
GO
/****** Object:  StoredProcedure [dbo].[usp_SaveBatchData]    Script Date: 7/24/2023 11:20:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[usp_SaveBatchData]
(
	@Name Varchar(100)
	,@BatchNo Varchar(50)
	,@NoOfCoupons Int
	,@OrgId Int
	,@CreatedBy Int

)
As
Begin

INSERT INTO [dbo].[BatchData]
           ([Name]
           ,[BatchNo]
           ,[NoOfCoupons]
           ,[NoOfPrintedCoupons]
           ,[Status]
           ,[IsActive]
           ,[OrgId]
           ,[CreatedBy]
           ,[CreatedOn])
     VALUES
           (
			@Name,
			@BatchNo,
			@NoOfCoupons,
			0,
			1,
			1,
			@OrgId,
			@CreatedBy,
			GetDate()
		   )

End



GO
USE [master]
GO
ALTER DATABASE [Manufacturer] SET  READ_WRITE 
GO
