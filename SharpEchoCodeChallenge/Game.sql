-- Use the new database
USE [SharpEchoCodeChallenge];
GO

-- If exists drop the Game table
IF OBJECT_ID('dbo.Game', 'U') IS NOT NULL
DROP TABLE [dbo].[Game];

-- Create the Game table
CREATE TABLE [dbo].[Game]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    -- Add team names to the Game table char or varchar
    [WinningTeamName] NVARCHAR(50) NOT NULL,
    [LosingTeamName] NVARCHAR(50) NOT NULL,
    [Date] DATETIME NOT NULL,
    [WinningTeamId] BIGINT NOT NULL,
    FOREIGN KEY (LosingTeamId) REFERENCES Team(Id),
    FOREIGN KEY (WinningTeamId) REFERENCES Team(Id),
);

GO
