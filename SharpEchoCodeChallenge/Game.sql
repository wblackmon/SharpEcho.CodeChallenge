-- Use the new database
USE [SharpEchoCodeChallenge];
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
