-- Enable xp_cmdshell if it is not already enabled
EXEC sp_configure 'show advanced options', 1;
RECONFIGURE;
EXEC sp_configure 'xp_cmdshell', 1;
RECONFIGURE;
GO

-- Create the database if it does not exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'SharpEchoDB')
BEGIN
    CREATE DATABASE SharpEchoDB;
END
GO

-- Use the database
USE SharpEchoDB;
GO

-- Drop foreign key constraints if they exist
IF OBJECT_ID('dbo.Game', 'U') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Game DROP CONSTRAINT IF EXISTS FK_Game_WinningTeamId;
    ALTER TABLE dbo.Game DROP CONSTRAINT IF EXISTS FK_Game_LosingTeamId;
END
GO

-- Drop and recreate the Game table if it exists
IF OBJECT_ID('dbo.Game', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Game;
END
GO

-- Drop and recreate the Team table if it exists
IF OBJECT_ID('dbo.Team', 'U') IS NOT NULL
BEGIN
    DROP TABLE dbo.Team;
END
GO

CREATE TABLE [dbo].[Team]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
);
GO

-- Create a unique index on the Team Name
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Team_Name' AND object_id = OBJECT_ID('dbo.Team'))
BEGIN
    CREATE UNIQUE INDEX [IX_Team_Name] ON [dbo].[Team] ([Name]);
END
GO

CREATE TABLE [dbo].[Game]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [WinningTeamName] NVARCHAR(50) NOT NULL,
    [LosingTeamName] NVARCHAR(50) NOT NULL,
    [Date] DATETIME NOT NULL,
    [WinningTeamId] BIGINT NOT NULL,
    [LosingTeamId] BIGINT NOT NULL,
    FOREIGN KEY (WinningTeamId) REFERENCES Team(Id),
    FOREIGN KEY (LosingTeamId) REFERENCES Team(Id)
);
GO

-- Create indexes on the Game table for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Game_WinningTeamId' AND object_id = OBJECT_ID('dbo.Game'))
BEGIN
    CREATE INDEX [IX_Game_WinningTeamId] ON [dbo].[Game] ([WinningTeamId]);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Game_LosingTeamId' AND object_id = OBJECT_ID('dbo.Game'))
BEGIN
    CREATE INDEX [IX_Game_LosingTeamId] ON [dbo].[Game] ([LosingTeamId]);
END
GO