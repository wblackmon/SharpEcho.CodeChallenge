-- Use the database
USE SharpEchoDB;
GO

-- Delete all data in the Game table
DELETE FROM [dbo].[Game];
GO

-- Delete all data in the Team table
DELETE FROM [dbo].[Team];
GO

-- Insert test data into the Team table
INSERT INTO [dbo].[Team] ([Name]) VALUES ('Dallas Cowboys');
INSERT INTO [dbo].[Team] ([Name]) VALUES ('Atlanta Falcons');
GO

-- Insert test data into the Game table
-- Assuming Dallas Cowboys won 17 out of 28 matches against Atlanta Falcons
DECLARE @DallasCowboysId BIGINT = (SELECT [Id] FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys');
DECLARE @AtlantaFalconsId BIGINT = (SELECT [Id] FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons');

-- Insert 17 wins for Dallas Cowboys
DECLARE @i INT = 1;
WHILE @i <= 17
BEGIN
    INSERT INTO [dbo].[Game] ([HomeTeamId], [AwayTeamId], [Date], [WinningTeamId])
    VALUES (@DallasCowboysId, @AtlantaFalconsId, GETDATE(), @DallasCowboysId);
    SET @i = @i + 1;
END

-- Insert 11 wins for Atlanta Falcons
SET @i = 1;
WHILE @i <= 11
BEGIN
    INSERT INTO [dbo].[Game] ([HomeTeamId], [AwayTeamId], [Date], [WinningTeamId])
    VALUES (@DallasCowboysId, @AtlantaFalconsId, GETDATE(), @AtlantaFalconsId);
    SET @i = @i + 1;
END
GO

-- Verify the data in the Team table
SELECT * FROM [dbo].[Team];
GO

-- Verify the data in the Game table
SELECT * FROM [dbo].[Game];
GO