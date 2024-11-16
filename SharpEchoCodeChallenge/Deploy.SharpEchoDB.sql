-- Drop the database if it exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SharpEchoDB')
BEGIN
    DROP DATABASE SharpEchoDB;
END
GO

-- Create the database with custom file paths
CREATE DATABASE SharpEchoDB
ON PRIMARY (
    NAME = SharpEchoDB_Data,
    FILENAME = 'C:\Databases\SharpEchoDB.mdf'
)
LOG ON (
    NAME = SharpEchoDB_Log,
    FILENAME = 'C:\Databases\SharpEchoDB_Log.ldf'
);
GO

-- Use the database
USE SharpEchoDB;
GO

-- Create the Team table
CREATE TABLE [dbo].[Team]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
);
GO

-- Create a unique index on the Team Name
CREATE UNIQUE INDEX [IX_Team_Name] ON [dbo].[Team] ([Name]);
GO

-- Create the Game table
CREATE TABLE [dbo].[Game]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [HomeTeamId] BIGINT NOT NULL,
    [AwayTeamId] BIGINT NOT NULL,
    [Date] DATETIME NOT NULL,
    [WinningTeamId] BIGINT NOT NULL,
    FOREIGN KEY (HomeTeamId) REFERENCES Team(Id),
    FOREIGN KEY (AwayTeamId) REFERENCES Team(Id),
    FOREIGN KEY (WinningTeamId) REFERENCES Team(Id)
);
GO

-- Create indexes on the Game table for better performance
CREATE INDEX [IX_Game_HomeTeamId] ON [dbo].[Game] ([HomeTeamId]);
CREATE INDEX [IX_Game_AwayTeamId] ON [dbo].[Game] ([AwayTeamId]);
CREATE INDEX [IX_Game_WinningTeamId] ON [dbo].[Game] ([WinningTeamId]);
GO