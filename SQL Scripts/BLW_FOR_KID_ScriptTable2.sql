USE [master]
GO
/****** Object:  Database [BLW_FOR_KID]    Script Date: 9/14/2023 10:55:07 AM ******/
CREATE DATABASE [BLW_FOR_KID]
GO

USE [BLW_FOR_KID]
GO

CREATE TABLE [dbo].[Age](
	[AgeId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[AgeName] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[Baby](
	[BabyId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[CustomerId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.CustomerAccount(CustomerId),
	[Fullname] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[Avatar] [text] NULL,
	[Gender] [int] NULL,
	[Age] [int] NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[BMI] [float] NULL,
	[HealthType] [nvarchar](255) NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[Chat](
	[ChatId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[ExpertId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.CustomerAccount(CustomerId),
	[CustomerId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.Expert(ExpertId),
	[StartChat] [datetime] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[ChatHistory](
	[ChatHistoryId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[ChatId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.Chat(ChatId),
	[SendTime] [datetime] NOT NULL,
	[SendPerson] [nvarchar](20) NOT NULL,
	[Image] [text] NULL,
	[Message] [text] NULL,
	[IsRemove] [bit] NOT NULL
)
GO
CREATE TABLE [dbo].[CustomerAccount](
	[CustomerId] [nvarchar](20) NOT NULL PRIMARY KEY,
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
	[IsDelete] [bit] NULL
)
GO

CREATE TABLE [dbo].[Direction](
	[DirectionId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[RecipeId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.Recipe(RecipeId),
	[DirectionNum] [int] NULL,
	[DirectionDesc] [text] NULL,
	[DirectionImage] [text] NULL,
)
GO
CREATE TABLE [dbo].[Expert](
	[ExpertId] [nvarchar](20) NOT NULL PRIMARY KEY,
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
	[IsDelete] [bit] NULL
)
GO

CREATE TABLE [dbo].[Favorite](
	[CustomerId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.CustomerAccount(CustomerId),
	[RecipeId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.Recipe(RecipeId),
	[FavoriteTime] [datetime] NOT NULL,
	PRIMARY KEY(CustomerId,RecipeId)
)
GO

CREATE TABLE [dbo].[GrowHistory](
	[GrowId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[BabyId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.Baby(BabyId),
	[Age] [int] NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[BMI] [float] NULL,
	[HealthType] [nvarchar](255) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO

CREATE TABLE [dbo].[GrowImage](
	[ImageId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[GrowId] [nvarchar](20) NULL  FOREIGN KEY REFERENCES dbo.GrowHistory(GrowId),
	[ImageLink] [text] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO

CREATE TABLE [dbo].[Ingredient](
	[IngredientId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[IngredientName] [text] NULL,
	[IngredientImage] [text] NULL,
	[Measure] [nvarchar](255) NULL,
	[Protein] [float] NULL,
	[Carbohydrate] [float] NULL,
	[Fat] [float] NULL,
	[Calories] [float] NULL,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL  FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[IngredientOfRecipe](
	[IngredientId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.Ingredient(IngredientId),
	[RecipeId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.Recipe(RecipeId),
	[Quantity] [float] NULL,
	PRIMARY KEY(IngredientId,RecipeId)
)
GO
CREATE TABLE [dbo].[Meal](
	[MealId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[MealName] [text] NULL,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL  FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[PaymentHistory](
	[PaymentId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[CustomerId] [nvarchar](20) NOT NULL FOREIGN KEY REFERENCES dbo.CustomerAccount(CustomerId),
	[PackageId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.PremiumPackage(PackageId),
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
)
GO
CREATE TABLE [dbo].[Plan](
	[PlanId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[PlanName] [nvarchar](150) NULL,
	[AgeId] [nvarchar](20) NULL  FOREIGN KEY REFERENCES dbo.Age(AgeId),
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[PlanDetail](
	[PlanDetailId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[PlanId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.[Plan](PlanId),
	[RecipeId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.Recipe(RecipeId),
	[Date] [int] NULL,
	[MealOfDate] [int] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[PremiumPackage](
	[PackageId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[PackageName] [nvarchar](255) NOT NULL,
	[PackageAmount] [money] NOT NULL,
	[PackageDiscount] [float] NULL,
	[PackageMonth] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
)
GO
CREATE TABLE [dbo].[Rating](
	[RatingId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[CustomerId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.CustomerAccount(CustomerId),
	[RecipeId] [nvarchar](20) NOT NULL  FOREIGN KEY REFERENCES dbo.Recipe(RecipeId),
	[Rate] [int] NULL,
	[Comment] [text] NULL,
	[Date] [datetime] NULL,
	[RatingImage] [text] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO
CREATE TABLE [dbo].[Recipe](
	[RecipeId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[CreateTime] [datetime] NULL,
	[StaffCreate] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[UpdateTime] [datetime] NULL,
	[StaffUpdate] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[DeleteDate] [datetime] NULL,
	[StaffDelete] [nvarchar](20) NULL FOREIGN KEY REFERENCES dbo.StaffAccount(StaffId),
	[RecipeName] [text] NULL,
	[MealId] [nvarchar](20) NULL  FOREIGN KEY REFERENCES dbo.Meal(MealId),
	[RecipeImage] [text] NULL,
	[Protein] [float] NULL,
	[Carbohydrate] [float] NULL,
	[Fat] [float] NULL,
	[Calories] [float] NULL,
	[AgeId] [nvarchar](20) NULL   FOREIGN KEY REFERENCES dbo.Age(AgeId),
	[ForPremium] [bit] NULL,
	[IsDelete] [bit] NULL DEFAULT 0
)
GO

CREATE TABLE [dbo].[StaffAccount](
	[StaffId] [nvarchar](20) NOT NULL PRIMARY KEY,
	[Email] [nvarchar](255) NULL,
	[GoogleId] [nvarchar](255) NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [nvarchar](255) NULL,
	[Role] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDelete] [bit] NULL
)
GO
CREATE UNIQUE INDEX idx_unique_email ON dbo.[StaffAccount] (Email) WHERE Email IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_googleid ON dbo.[StaffAccount] (GoogleId) WHERE GoogleId IS NOT NULL
GO

CREATE UNIQUE INDEX idx_unique_email ON dbo.CustomerAccount (Email) WHERE Email IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_googleid ON dbo.CustomerAccount (GoogleId) WHERE GoogleId IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_facebookid ON dbo.CustomerAccount (FacebookId) WHERE FacebookId IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_phone ON dbo.CustomerAccount (PhoneNum) WHERE PhoneNum IS NOT NULL
GO

CREATE UNIQUE INDEX idx_unique_email ON dbo.Expert (Email) WHERE Email IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_googleid ON dbo.Expert (GoogleId) WHERE GoogleId IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_facebookid ON dbo.Expert (FacebookId) WHERE FacebookId IS NOT NULL
GO
CREATE UNIQUE INDEX idx_unique_phone ON dbo.Expert (PhoneNum) WHERE PhoneNum IS NOT NULL
GO