-- Use the new database
USE [SharpEchoCodeChallenge];
GO

-- Seed data into the Team table
INSERT INTO [dbo].[Team] ([Name]) VALUES ('Dallas Cowboys');
INSERT INTO [dbo].[Team] ([Name]) VALUES ('Atlanta Falcons');
GO

-- Seed data into the Game table
INSERT INTO [dbo].[Game] ([TeamAId], [TeamBId], [WinnerId], [GameDate]) VALUES 
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2020-09-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2018-11-18'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), '2017-11-12'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2015-09-27'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2015-09-27'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2012-11-04'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), '2009-10-25'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2006-12-16'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2003-11-09'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '2001-10-15'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1999-09-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1998-09-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1997-09-21'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1995-11-12'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1993-11-21'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1992-09-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1990-11-11'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1988-11-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1986-11-23'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1985-11-10'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1984-11-11'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1983-11-20'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1982-11-21'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1981-11-15'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1980-11-23'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1979-11-25'),
((SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Atlanta Falcons'), (SELECT Id FROM [dbo].[Team] WHERE [Name] = 'Dallas Cowboys'), '1978-11-12');
GO

-- Verify data in the Team table
SELECT * FROM [dbo].[Team];
GO

-- Verify data in the Game table
SELECT * FROM [dbo].[Game];
GO