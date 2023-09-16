USE [master]
GO
/****** Object:  Database [BLW_FOR_KID]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE DATABASE [BLW_FOR_KID]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BLW_FOR_KID', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS_0001\MSSQL\DATA\BLW_FOR_KID.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BLW_FOR_KID_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS_0001\MSSQL\DATA\BLW_FOR_KID_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [BLW_FOR_KID] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BLW_FOR_KID].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BLW_FOR_KID] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET ARITHABORT OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BLW_FOR_KID] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BLW_FOR_KID] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BLW_FOR_KID] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BLW_FOR_KID] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BLW_FOR_KID] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BLW_FOR_KID] SET  MULTI_USER 
GO
ALTER DATABASE [BLW_FOR_KID] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BLW_FOR_KID] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BLW_FOR_KID] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BLW_FOR_KID] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BLW_FOR_KID] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BLW_FOR_KID] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BLW_FOR_KID] SET QUERY_STORE = ON
GO
ALTER DATABASE [BLW_FOR_KID] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [BLW_FOR_KID]
GO
/****** Object:  Table [dbo].[Age]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Age](
	[AgeId] [nvarchar](20) NOT NULL,
	[AgeName] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AgeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Baby]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Baby](
	[BabyId] [nvarchar](20) NOT NULL,
	[CustomerId] [nvarchar](20) NOT NULL,
	[Fullname] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Avatar] [text] NULL,
	[Gender] [int] NULL,
	[Age] [int] NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[BMI] [float] NULL,
	[HealthType] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[BabyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[ChatId] [nvarchar](20) NOT NULL,
	[ExpertId] [nvarchar](20) NOT NULL,
	[CustomerId] [nvarchar](20) NOT NULL,
	[StartChat] [datetime] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatHistory]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatHistory](
	[ChatHistoryId] [nvarchar](20) NOT NULL,
	[ChatId] [nvarchar](20) NOT NULL,
	[SendTime] [datetime] NOT NULL,
	[SendPerson] [nvarchar](20) NOT NULL,
	[Image] [text] NULL,
	[Message] [text] NULL,
	[IsRemove] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAccount]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerAccount](
	[CustomerId] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[GoogleId] [nvarchar](255) NULL,
	[FacebookId] [nvarchar](255) NULL,
	[Fullname] [nvarchar](255) NULL,
	[Avatar] [text] NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [int] NULL,
	[PhoneNum] [nvarchar](10) NULL,
	[Password] [nvarchar](255) NULL,
	[IsPremium] [bit] NULL,
	[SigupDate] [datetime] NULL,
	[LastLogin] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[LastPurchaseDate] [datetime] NULL,
	[IsTried] [bit] NULL,
	[NumOfTried] [int] NULL,
	[WasTried] [bit] NULL,
	[StartTriedDate] [datetime] NULL,
	[EndTriedDate] [datetime] NULL,
	[NumOfPremiumMonths] [int] NULL,
	[LastStartPremiumDate] [datetime] NULL,
	[LastEndPremiumDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direction]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Direction](
	[DirectionId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[DirectionNum] [int] NULL,
	[DirectionDesc] [text] NULL,
	[DirectionImage] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[DirectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expert]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expert](
	[ExpertId] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[GoogleId] [nvarchar](255) NULL,
	[FacebookId] [nvarchar](255) NULL,
	[PhoneNum] [nvarchar](10) NULL,
	[Avatar] [text] NULL,
	[DateOfBirth] [datetime] NULL,
	[Gender] [int] NULL,
	[Username] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[Title] [nvarchar](255) NULL,
	[Position] [nvarchar](255) NULL,
	[WorkUnit] [nvarchar](255) NULL,
	[Description] [text] NULL,
	[ProfessionalQualification] [text] NULL,
	[WorkProgress] [text] NULL,
	[Achievements] [text] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ExpertId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorite]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorite](
	[CustomerId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[FavoriteTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowHistory]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowHistory](
	[GrowId] [nvarchar](20) NOT NULL,
	[BabyId] [nvarchar](20) NOT NULL,
	[Age] [int] NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[BMI] [float] NULL,
	[HealthType] [nvarchar](255) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[GrowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowImage]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GrowImage](
	[ImageId] [nvarchar](20) NOT NULL,
	[GrowId] [nvarchar](20) NULL,
	[ImageLink] [text] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[IngredientId] [nvarchar](20) NOT NULL,
	[IngredientName] [text] NULL,
	[IngredientImage] [text] NULL,
	[Measure] [nvarchar](255) NULL,
	[Protein] [float] NULL,
	[Carbohydrate] [float] NULL,
	[Fat] [float] NULL,
	[Calories] [float] NULL,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL,
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientOfRecipe]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientOfRecipe](
	[IngredientId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[Quantity] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[IngredientId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meal]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meal](
	[MealId] [nvarchar](20) NOT NULL,
	[MealName] [text] NULL,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL,
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[MealId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentHistory](
	[PaymentId] [nvarchar](20) NOT NULL,
	[CustomerId] [nvarchar](20) NOT NULL,
	[PackageId] [nvarchar](20) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
	[PaymentChannel] [nvarchar](255) NOT NULL,
	[MomoRequestId] [nvarchar](255) NULL,
	[MomoOrderId] [nvarchar](255) NULL,
	[MomoOrderInfo] [text] NULL,
	[MomoResponseMsg] [text] NULL,
	[MomoResultCode] [nvarchar](255) NULL,
	[MomoPayType] [nvarchar](255) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Plan](
	[PlanId] [nvarchar](20) NOT NULL,
	[PlanName] [nvarchar](150) NULL,
	[AgeId] [nvarchar](20) NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanDetail]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanDetail](
	[PlanDetailId] [nvarchar](20) NOT NULL,
	[PlanId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[Date] [int] NULL,
	[MealOfDate] [int] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[PlanDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PremiumPackage]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PremiumPackage](
	[PackageId] [nvarchar](20) NOT NULL,
	[PackageName] [nvarchar](255) NOT NULL,
	[PackageAmount] [money] NOT NULL,
	[PackageDiscount] [float] NULL,
	[PackageMonth] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [nvarchar](20) NOT NULL,
	[CustomerId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[Rate] [int] NULL,
	[Comment] [text] NULL,
	[Date] [datetime] NULL,
	[RatingImage] [text] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL,
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL,
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL,
	[RecipeName] [text] NULL,
	[MealId] [nvarchar](20) NULL,
	[RecipeImage] [text] NULL,
	[Protein] [float] NULL,
	[Carbohydrate] [float] NULL,
	[Fat] [float] NULL,
	[Calories] [float] NULL,
	[AgeId] [nvarchar](20) NULL,
	[ForPremium] [bit] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffAccount]    Script Date: 9/14/2023 7:58:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffAccount](
	[StaffId] [nvarchar](20) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[GoogleId] [nvarchar](255) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [nvarchar](255) NULL,
	[Role] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[StaffId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_email]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_email] ON [dbo].[CustomerAccount]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_facebookid]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_facebookid] ON [dbo].[CustomerAccount]
(
	[FacebookId] ASC
)
WHERE ([FacebookId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_googleid]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_googleid] ON [dbo].[CustomerAccount]
(
	[GoogleId] ASC
)
WHERE ([GoogleId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_phone]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_phone] ON [dbo].[CustomerAccount]
(
	[PhoneNum] ASC
)
WHERE ([PhoneNum] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_email]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_email] ON [dbo].[Expert]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_facebookid]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_facebookid] ON [dbo].[Expert]
(
	[FacebookId] ASC
)
WHERE ([FacebookId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_googleid]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_googleid] ON [dbo].[Expert]
(
	[GoogleId] ASC
)
WHERE ([GoogleId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_phone]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_phone] ON [dbo].[Expert]
(
	[PhoneNum] ASC
)
WHERE ([PhoneNum] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_email]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_email] ON [dbo].[StaffAccount]
(
	[Email] ASC
)
WHERE ([Email] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idx_unique_googleid]    Script Date: 9/14/2023 7:58:25 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idx_unique_googleid] ON [dbo].[StaffAccount]
(
	[GoogleId] ASC
)
WHERE ([GoogleId] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Age] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Baby] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Chat] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[GrowHistory] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[GrowImage] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Ingredient] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Meal] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Plan] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[PlanDetail] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Rating] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Recipe] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Baby]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Expert] ([ExpertId])
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD FOREIGN KEY([ExpertId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[ChatHistory]  WITH CHECK ADD FOREIGN KEY([ChatId])
REFERENCES [dbo].[Chat] ([ChatId])
GO
ALTER TABLE [dbo].[Direction]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[Favorite]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[GrowHistory]  WITH CHECK ADD FOREIGN KEY([BabyId])
REFERENCES [dbo].[Baby] ([BabyId])
GO
ALTER TABLE [dbo].[GrowImage]  WITH CHECK ADD FOREIGN KEY([GrowId])
REFERENCES [dbo].[GrowHistory] ([GrowId])
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD FOREIGN KEY([StaffCreate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD FOREIGN KEY([StaffUpdate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD FOREIGN KEY([StaffDelete])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[IngredientOfRecipe]  WITH CHECK ADD FOREIGN KEY([IngredientId])
REFERENCES [dbo].[Ingredient] ([IngredientId])
GO
ALTER TABLE [dbo].[IngredientOfRecipe]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[Meal]  WITH CHECK ADD FOREIGN KEY([StaffCreate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Meal]  WITH CHECK ADD FOREIGN KEY([StaffDelete])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Meal]  WITH CHECK ADD FOREIGN KEY([StaffUpdate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[PaymentHistory]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[PaymentHistory]  WITH CHECK ADD FOREIGN KEY([PackageId])
REFERENCES [dbo].[PremiumPackage] ([PackageId])
GO
ALTER TABLE [dbo].[Plan]  WITH CHECK ADD FOREIGN KEY([AgeId])
REFERENCES [dbo].[Age] ([AgeId])
GO
ALTER TABLE [dbo].[PlanDetail]  WITH CHECK ADD FOREIGN KEY([PlanId])
REFERENCES [dbo].[Plan] ([PlanId])
GO
ALTER TABLE [dbo].[PlanDetail]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[Rating]  WITH CHECK ADD FOREIGN KEY([RecipeId])
REFERENCES [dbo].[Recipe] ([RecipeId])
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([AgeId])
REFERENCES [dbo].[Age] ([AgeId])
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([MealId])
REFERENCES [dbo].[Meal] ([MealId])
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([StaffCreate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([StaffDelete])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
ALTER TABLE [dbo].[Recipe]  WITH CHECK ADD FOREIGN KEY([StaffUpdate])
REFERENCES [dbo].[StaffAccount] ([StaffId])
GO
USE [master]
GO
ALTER DATABASE [BLW_FOR_KID] SET  READ_WRITE 
GO
