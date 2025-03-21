USE [master]
GO
/****** Object:  Database [Resume_Portal_Arivani]    Script Date: 7/9/2024 10:58:19 PM ******/
CREATE DATABASE [Resume_Portal_Arivani]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Resume_Portal_Arivani', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Resume_Portal_Arivani.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Resume_Portal_Arivani_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Resume_Portal_Arivani_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Resume_Portal_Arivani] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Resume_Portal_Arivani].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ARITHABORT OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET  MULTI_USER 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Resume_Portal_Arivani] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Resume_Portal_Arivani] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Resume_Portal_Arivani] SET QUERY_STORE = OFF
GO
USE [Resume_Portal_Arivani]
GO
/****** Object:  Table [dbo].[tbl_Designation]    Script Date: 7/9/2024 10:58:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Designation](
	[Designation] [varchar](200) NOT NULL,
	[Status] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Designation] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_resume]    Script Date: 7/9/2024 10:58:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_resume](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](120) NULL,
	[Designation] [varchar](200) NULL,
	[Resume] [varchar](250) NULL,
	[Title] [varchar](120) NULL,
	[KeyWords] [varchar](250) NULL,
	[Status] [bit] NULL,
	[UploadedBy] [varchar](120) NULL,
	[FilePath] [varchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbl_User]    Script Date: 7/9/2024 10:58:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_User](
	[Email] [varchar](120) NOT NULL,
	[Name] [varchar](100) NULL,
	[Mobile] [bigint] NULL,
	[Gender] [varchar](50) NULL,
	[Address] [varchar](250) NULL,
	[password] [varchar](20) NULL,
	[Role] [varchar](50) NULL,
	[Status] [bit] NULL,
	[Designation] [varchar](200) NULL,
	[Photo] [varchar](200) NULL,
	[Date_of_Joining] [date] NULL,
	[CreatedBy] [varchar](120) NULL,
PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[tbl_Designation] ([Designation], [Status]) VALUES (N'Frontend Developer', 1)
INSERT [dbo].[tbl_Designation] ([Designation], [Status]) VALUES (N'PHP Developer', 1)
INSERT [dbo].[tbl_Designation] ([Designation], [Status]) VALUES (N'Python Developer', 1)
INSERT [dbo].[tbl_Designation] ([Designation], [Status]) VALUES (N'React Developer', 1)
GO
SET IDENTITY_INSERT [dbo].[tbl_resume] ON 

INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (10, N'raiyan@gmail.com', N'Python Developer', N'Nilesh-Resume_my.pdf.pdf', N'Developer', N'Html,Css,Python', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\Nilesh-Resume_my.pdf.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (11, N'anm@gmail.com', N'React Developer', N'vivek-Resume.pdf (1).pdf', N'Asp.Net Developer', N'Html,Css,C#,Mvc', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\vivek-Resume.pdf (1).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (12, N'ashish@arivani.net', N'Python Developer', N'White simple student cv resume.pdf', N'DEveloper', N'Lucknow', 1, N'ashish@arivani.net', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\White simple student cv resume.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (13, N'raiyan@gmail.com', N'Python Developer', N'automationselenium.pdf', N'Developer', N'C#,HTML,FILE', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\automationselenium.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (14, N'raiyan@gmail.com', N'Python Developer', N'Vivek.pdf', N'Developer', N'HTML,Python', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\Vivek.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (15, N'raiyan@gmail.com', N'Python Developer', N'White simple student cv resume (5).pdf', N'Developer', N'HTML,Python', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\White simple student cv resume (5).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (16, N'raiyan@gmail.com', N'Python Developer', N'myresumenk.pdf', N'Developer', N'HTML,Python', 0, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\myresumenk.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (17, N'anm@gmail.com', N'React Developer', N'White Simple Student CV Resume (4).pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\White Simple Student CV Resume (4).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (18, N'anm@gmail.com', N'React Developer', N'White Simple Student CV Resume (3).pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\White Simple Student CV Resume (3).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (19, N'anm@gmail.com', N'React Developer', N'White Simple Student CV Resume (2).pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\White Simple Student CV Resume (2).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (20, N'anm@gmail.com', N'React Developer', N'myresumenk.pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\myresumenk.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (21, N'anm@gmail.com', N'React Developer', N'MVC.pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\MVC.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (22, N'anm@gmail.com', N'React Developer', N'Graphic Designer Resume (8).pdf', N'React DEveloper', N'PHP,MONGODB', 1, N'anm@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\Graphic Designer Resume (8).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (23, N'nkk3918@gmail.com', N'React Developer', N'Here is my resume.pdf', N'Developer', N'HTML,CSS,', 0, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\Here is my resume.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (24, N'nkk3918@gmail.com', N'React Developer', N'nileshresume.pdf', N'Developer', N'HTML,CSS,', 0, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/React Developer\nileshresume.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (25, N'nkk3918@gmail.com', N'PHP Developer', NULL, N'Developer', N'HTML,CSS,', 0, N'nkk3918@gmail.com', NULL)
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (26, N'nsrivastav081@gmail.com', N'Python Developer', N'myresumenk.pdf', N'Developer', N'Python,CSS,HTML', 1, N'nsrivastav081@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\myresumenk.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (27, N'nsrivastav081@gmail.com', N'Python Developer', N'vks.pdf', N'Developer', N'Python,CSS,HTML', 1, N'nsrivastav081@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\vks.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (28, N'nsrivastav081@gmail.com', N'Python Developer', N'myresumenk.pdf', N'Developer', N'Python,CSS,HTML', 1, N'nsrivastav081@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\myresumenk.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (29, N'nsrivastav081@gmail.com', N'Python Developer', N'vks.pdf', N'Developer', N'Python,CSS,HTML', 1, N'nsrivastav081@gmail.com', N'C:\Users\Dell\source\repos\Resum_Portal\Resum_Portal\Content/Python Developer\vks.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (30, N'raiyan@gmail.com', N'Frontend Developer', N'Graphic Designer Resume (3).pdf', N'Web Developer', N'Html,CSS', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Graphic Designer Resume (3).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (31, N'raiyan@gmail.com', N'Frontend Developer', N'Graphic Designer Resume (1).pdf', N'Web Developer', N'Html,CSS', 1, N'raiyan@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Graphic Designer Resume (1).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (32, N'nkk3918@gmail.com', N'Frontend Developer', N'White And Brown Vintage Resume.pdf', N'Designer', N'Html,Css,Python', 1, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\White And Brown Vintage Resume.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (33, N'nkk3918@gmail.com', N'React Developer', N'Graphic Designer Resume (3).pdf', N'Designer', N'Html,Css', 1, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Graphic Designer Resume (3).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (34, N'nkk3918@gmail.com', N'Frontend Developer', N'Graphic Designer Resume (1).pdf', N'Designer', N'Html,Css', 1, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Graphic Designer Resume (1).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (35, N'nkk3918@gmail.com', N'Frontend Developer', N'Graphic Designer Resume.pdf', N'Designer', N'Html,Css', 1, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Graphic Designer Resume.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (36, N'nkk3918@gmail.com', N'Frontend Developer', N'Nilesh-Resume_my.pdf.pdf', N'Developer', N'Html', 1, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\Nilesh-Resume_my.pdf.pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (37, N'nkk3918@gmail.com', N'Frontend Developer', N'vivek-Resume.pdf (1).pdf', N'Developer', N'Html', 0, N'nkk3918@gmail.com', N'C:\Users\Dell\source\repos\Resume_Portal\Resume_Portal\wwwroot\Frontend Developer\vivek-Resume.pdf (1).pdf')
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (38, N'nkk3918@gmail.com', N'React Developer', NULL, N'Developer', N'HTML,CSS,', 1, N'nkk3918@gmail.com', NULL)
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (39, N'nkk3918@gmail.com', N'PHP Developer', NULL, N'Developer', N'HTML,CSS,', 1, N'nkk3918@gmail.com', NULL)
INSERT [dbo].[tbl_resume] ([id], [Email], [Designation], [Resume], [Title], [KeyWords], [Status], [UploadedBy], [FilePath]) VALUES (40, N'nkk3918@gmail.com', N'PHP Developer', NULL, N'Developer', N'HTML,CSS,', 1, N'nkk3918@gmail.com', NULL)
SET IDENTITY_INSERT [dbo].[tbl_resume] OFF
GO
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'anm@gmail.com', N'Anmol Mishra', 9087654321, N'Male', N'Ayodhya', N'0987654321', N'User', 1, N'React Developer', N'm2.jpg', CAST(N'2024-06-04' AS Date), N'Nilesh')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'ashish@arivani.net', N'Ashish', 8978675647, N'Male', N'Lucknow', N'0987654321', N'User', 1, N'Python Developer', N'm.jpg', CAST(N'2024-06-04' AS Date), N'nilesh@gmail.com')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'nilesh@gmail.com', N'Nilesh Shrivastav', 8766573820, N'Male', N'Jaunpur', N'nilesh12345', N'Admin', 1, N'Python Developer', N'man2.jpg', CAST(N'2024-06-03' AS Date), N'nilesh@gmail.com')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'nkk3918@gmail.com', N'Vivek', 9087654321, N'Male', N'Lucknow', N'nks12345678', N'User', 1, N'PHP Developer', N'm4.jpg', CAST(N'2024-06-18' AS Date), N'nilesh@gmail.com')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'nsrivastav081@gmail.com', N'Akash', 8907654321, N'Male', N'Kanpur', N'nks@1234', N'User', 1, N'Python Developer', N'man.png', CAST(N'2024-07-04' AS Date), N'nilesh@gmail.com')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'raiyan@gmail.com', N'Raiyan', 9870654321, N'Male', N'Lucknow', N'123456789', N'User', 1, N'Python Developer', N'm2.jpg', CAST(N'2024-06-14' AS Date), N'nilesh@gmail.com')
INSERT [dbo].[tbl_User] ([Email], [Name], [Mobile], [Gender], [Address], [password], [Role], [Status], [Designation], [Photo], [Date_of_Joining], [CreatedBy]) VALUES (N'sahil@gmail.com', N'Sahil', 9089785645, N'Male', N'Lucknow', N'sahil9876543', N'User', 1, N'Frontend Developer', N'm3.jpg', CAST(N'2024-07-04' AS Date), N'nilesh@gmail.com')
GO
ALTER TABLE [dbo].[tbl_resume]  WITH CHECK ADD FOREIGN KEY([Designation])
REFERENCES [dbo].[tbl_Designation] ([Designation])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[tbl_resume]  WITH CHECK ADD FOREIGN KEY([Email])
REFERENCES [dbo].[tbl_User] ([Email])
ON UPDATE CASCADE
ON DELETE SET NULL
GO
USE [master]
GO
ALTER DATABASE [Resume_Portal_Arivani] SET  READ_WRITE 
GO
