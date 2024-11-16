-- Use the new database
USE [SharpEchoCodeChallenge];
GO

-- Create the Game table
CREATE TABLE [dbo].[Game]
(
    [Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [TeamAId] BIGINT NOT NULL,
    [TeamBId] BIGINT NOT NULL,
    [WinnerId] BIGINT NOT NULL,
    [GameDate] DATE NOT NULL,
    FOREIGN KEY (TeamAId) REFERENCES [dbo].[Team](Id),
    FOREIGN KEY (TeamBId) REFERENCES [dbo].[Team](Id),
    FOREIGN KEY (WinnerId) REFERENCES [dbo].[Team](Id)
);
GO