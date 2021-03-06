USE [master]
GO
/****** Object:  Database [QQDB]    Script Date: 2016/9/28 10:22:52 ******/
CREATE DATABASE [QQDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QQDB', FILENAME = N'E:\GitHub\QQUserManageSystem\data\QQDB.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QQDB_log', FILENAME = N'E:\GitHub\QQUserManageSystem\data\QQDB_log.ldf' , SIZE = 3456KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QQDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QQDB].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [QQDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QQDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QQDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QQDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QQDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [QQDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QQDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QQDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QQDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QQDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QQDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QQDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QQDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QQDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QQDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QQDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QQDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QQDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QQDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QQDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QQDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QQDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QQDB] SET RECOVERY FULL 
GO
ALTER DATABASE [QQDB] SET  MULTI_USER 
GO
ALTER DATABASE [QQDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QQDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QQDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QQDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [QQDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QQDB', N'ON'
GO
USE [QQDB]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 2016/9/28 10:22:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[admin](
	[LoginId] [char](10) NOT NULL,
	[LoginPwd] [varchar](20) NOT NULL,
 CONSTRAINT [PK_admin] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Level]    Script Date: 2016/9/28 10:22:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Level](
	[LevelId] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserInfo]    Script Date: 2016/9/28 10:22:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserInfo](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[UserPwd] [varchar](50) NOT NULL,
	[LevelId] [int] NOT NULL,
	[Email] [varchar](50) NULL,
	[OnLineDay] [float] NOT NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[admin] ([LoginId], [LoginPwd]) VALUES (N'admin     ', N'8888')
INSERT [dbo].[admin] ([LoginId], [LoginPwd]) VALUES (N'guest     ', N'6666')
INSERT [dbo].[admin] ([LoginId], [LoginPwd]) VALUES (N'jbit      ', N'bdqn')
SET IDENTITY_INSERT [dbo].[Level] ON 

INSERT [dbo].[Level] ([LevelId], [LevelName]) VALUES (1, N'无等级')
INSERT [dbo].[Level] ([LevelId], [LevelName]) VALUES (2, N'星星')
INSERT [dbo].[Level] ([LevelId], [LevelName]) VALUES (3, N'月亮')
INSERT [dbo].[Level] ([LevelId], [LevelName]) VALUES (4, N'太阳')
SET IDENTITY_INSERT [dbo].[Level] OFF
SET IDENTITY_INSERT [dbo].[UserInfo] ON 

INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (1, N'东方不败', N'123456', 2, N'east@126.com', 888)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (2, N'花瓣鱼儿', N'123456', 2, N'fish@163.com', 5)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (3, N'彩云追月', N'123456', 2, N'cloud@sina.com', 9.3)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (4, N'水蜜桃香', N'123456', 2, N'peach@sohu.com', 15)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (5, N'孤单钓影', N'123456', 3, N'alone@yahoo.com', 33.5)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (6, N'柠檬小草', N'123456', 4, N'grass@126.com', 329)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (7, N'沉默的人', N'123456', 3, N'silent@163.com', 33)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (8, N'风暴来临', N'123456', 1, N'storm@sina.com', 4.8)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (9, N'永恒冰点', N'123456', 2, N'freeze@sina.com', 29)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (11, N'流浪的心', N'123456', 2, N'heart@sohu.com', 9.9)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (12, N'狂奔的蜗牛', N'123456', 3, N'snail@yahoo.com', 160)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (14, N'雨中漫步', N'123456', 2, N'rain@163.com', 6)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (15, N'秘密之约', N'123456', 1, N'little@126.com', 0)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (16, N'山上的树', N'123456', 1, N'tree@163.com', 0)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (21, N'雨中漫步', N'123456', 1, N'rain@126.com', 0)
INSERT [dbo].[UserInfo] ([UserId], [UserName], [UserPwd], [LevelId], [Email], [OnLineDay]) VALUES (23, N'niirr', N'333333', 1, N'22@123.cd', 0)
SET IDENTITY_INSERT [dbo].[UserInfo] OFF
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [CK_UserEmail] CHECK  (([Email] like '%@%'))
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [CK_UserEmail]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [CK_UserOnLineDay] CHECK  (([OnLineDay]>=(0)))
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [CK_UserOnLineDay]
GO
ALTER TABLE [dbo].[UserInfo]  WITH CHECK ADD  CONSTRAINT [CK_UserPwd] CHECK  ((len([UserPwd])>=(6)))
GO
ALTER TABLE [dbo].[UserInfo] CHECK CONSTRAINT [CK_UserPwd]
GO
USE [master]
GO
ALTER DATABASE [QQDB] SET  READ_WRITE 
GO
