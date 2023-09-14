USE [master]
DROP DATABASE BLW_FOR_KID
GO
/****** Object:  Database [BLW_FOR_KID]    Script Date: 9/8/2023 11:29:10 AM ******/
CREATE DATABASE [BLW_FOR_KID]
GO
USE [BLW_FOR_KID]
GO
/****** Object:  Table [dbo].[Age]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Baby]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatHistory]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatHistory](
	[ChatId] [nvarchar](20) NOT NULL,
	[SendTime] [datetime] NOT NULL,
	[SendPerson] [nvarchar](20) NOT NULL,
	[Image] [text] NULL,
	[Message] [text] NULL,
	[IsRemove] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC,
	[SendTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerAccount]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Direction]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expert]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
INSERT INTO Expert VALUES('E11','tuongdcse151377@fpt.edu.vn','G11','F11','0911919991','Hinh vuong',
'2023-10-01',0,'caotuongg','12','tuong','tuong1','tuong2','tuong3','tuong4','tuong5','tuong6','tuong7','false','false');
INSERT INTO Expert VALUES('E12','truongdcse151142@fpt.edu.vn','G12','F12','0362341929','Hinh chu nhat',
'2023-11-01',0,'huutruong','123','truong','truong1','truong2','truong3','truong4','truong5','truong6','truong7','false','false');
INSERT INTO Expert VALUES('E13','tungdcse151166@fpt.edu.vn','G13','F13','0919883839','Hinh thoi',
'2023-12-01',0,'thanhtung','1234','tung','tung1','tung2','tung3','tung4','tung5','tung6','tung7','false','false');
select * from Expert
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorite]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorite](
	[CustomerId] [nvarchar](20) NOT NULL,
	[RecipeId] [nvarchar](20) NOT NULL,
	[FavoriteTime] [DATETIME] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC,
	[RecipeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowHistory]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GrowImage]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
select * from Ingredient
INSERT INTO Ingredient VALUES('S111','Chao an dam nuoc ham rau cu','https://sakukostore.com.vn/Data/Sites/1/Product/7380/49840452.jpg',
'tinhbothamraucu',60,30,70,40,'2023-09-02','S13','2023-09-12','S11','2023-09-20','S11','true');
INSERT INTO Ingredient VALUES('S112','Sua Aptakid New Zealand','https://media.bibomart.com.vn/resize.460x-/media/catalog/product/s/u/sua-aptamil-nz-so-3-900gr-24m-126084.jpg',
'cong thuc Synbiotic',50,20,60,80,'2023-10-02','S13','2023-10-12','S13','2023-10-20','S13','true');
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientOfRecipe]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meal]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentHistory]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentHistory](
	[PaymentId] [nvarchar](20) NOT NULL,
	[CustomerId] [nvarchar](20) NOT NULL,
	[PurchaseTime] [datetime] NOT NULL,
	[NumOfMonth] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Plan]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanDetail]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rating]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipe]    Script Date: 9/8/2023 11:29:10 AM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffAccount]    Script Date: 9/8/2023 11:29:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
INSERT INTO StaffAccount VALUES ('S11','tuongdcse151377@fpt.edu.vn','G11','caotuongg','12','docaotuong',1,'true','true');
INSERT INTO StaffAccount VALUES ('S13','truongdcse151142@fpt.edu.vn','G12','huutruong','123','trinhhuutruong',1,'true','true');
select * from StaffAccount
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__4D6564654B7EDC80]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[CustomerAccount] ADD UNIQUE NONCLUSTERED 
(
	[FacebookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,IGNORE_DUP_KEY = OFF,ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A6FBF2FBBCAB3317]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[CustomerAccount] ADD UNIQUE NONCLUSTERED 
(
	[GoogleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A9D10534B6C8937D]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[CustomerAccount] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__DF8F1A02227D342A]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[CustomerAccount] ADD UNIQUE NONCLUSTERED 
(
	[PhoneNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Expert__4D656465B3C0B35A]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[Expert] ADD UNIQUE NONCLUSTERED 
(
	[FacebookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Expert__A6FBF2FB5D9C9B57]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[Expert] ADD UNIQUE NONCLUSTERED 
(
	[GoogleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Expert__A9D10534BB17B2C0]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[Expert] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Expert__DF8F1A02B03674F0]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[Expert] ADD UNIQUE NONCLUSTERED 
(
	[PhoneNum] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__StaffAcc__A6FBF2FB28CAB0F7]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[StaffAccount] ADD UNIQUE NONCLUSTERED 
(
	[GoogleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__StaffAcc__A9D1053404403CD9]    Script Date: 9/8/2023 11:29:11 AM ******/
ALTER TABLE [dbo].[StaffAccount] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Age] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Baby] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Chat] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[ChatHistory] ADD  DEFAULT ((0)) FOR [IsRemove]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT ((0)) FOR [IsTried]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT ((0)) FOR [WasTried]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CustomerAccount] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Expert] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Expert] ADD  DEFAULT ((0)) FOR [IsDelete]
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
ALTER TABLE [dbo].[StaffAccount] ADD  DEFAULT ((1)) FOR [Role]
GO
ALTER TABLE [dbo].[StaffAccount] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[StaffAccount] ADD  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Baby]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[CustomerAccount] ([CustomerId])
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD FOREIGN KEY([ExpertId])
REFERENCES [dbo].[Expert] ([ExpertId])
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
