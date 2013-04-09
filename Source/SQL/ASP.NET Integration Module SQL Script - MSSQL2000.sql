USE [master]
GO

CREATE DATABASE [ClickTale] ON  PRIMARY 
( NAME = N'ClickTale', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\ClickTale.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ClickTale_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\DATA\ClickTale_log.ldf' , SIZE = 1024KB , MAXSIZE = 200MB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ClickTale] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClickTale] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClickTale] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClickTale] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClickTale] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClickTale] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ClickTale] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ClickTale] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClickTale] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClickTale] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClickTale] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClickTale] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClickTale] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClickTale] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClickTale] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClickTale] SET RECOVERY SIMPLE 
GO
 
USE [ClickTale]
GO
CREATE TABLE [dbo].[CachedPages](
	Token binary(20) NOT NULL,
	Page Image NOT NULL,
	DateCreated datetime NOT NULL,
 CONSTRAINT [PK_CachedPages] PRIMARY KEY NONCLUSTERED (	Token ASC )
) 

CREATE CLUSTERED INDEX [IX_DateCreated] ON [dbo].[CachedPages] 
(
	[DateCreated] ASC
)
GO

CREATE PROCEDURE [dbo].[CacheAddCachedPage] 
	-- Add the parameters for the stored procedure here
	@Token BINARY(20), 
	@Page Image,
	@MaxCachedSeconds INT = 60
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE FROM CachedPages 
	WHERE DateCreated < DATEADD(ss, -@MaxCachedSeconds, GETDATE())
	
	INSERT INTO CachedPages (Token, Page, DateCreated) 
	VALUES (@Token, @Page, GETDATE())
END
GO

CREATE PROCEDURE [dbo].[CachePullCachedPage] 
	-- Add the parameters for the stored procedure here
	@Token BINARY(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRAN
		SELECT Page 
		FROM CachedPages 
		WHERE Token = @Token
		
		DELETE FROM CachedPages 
		WHERE Token = @Token
	COMMIT TRAN

END
GO
/*
Maintenance index rebuilds once in a while
ALTER INDEX [IX_DateCreated] ON [dbo].[CachedPages] REBUILD
ALTER INDEX [PK_CachedPages] ON [dbo].[CachedPages] REBUILD
*/
