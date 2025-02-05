-- Highscore.dbo.Highscores definition

-- Drop table

-- DROP TABLE Highscore.dbo.Highscores;

CREATE TABLE Highscore.dbo.Highscores (
	CurrentStreak int NOT NULL,
	HighScore int NOT NULL,
	TotalDays int NOT NULL,
	ID int IDENTITY(1,1) NOT NULL,
	CONSTRAINT Highscores_PK PRIMARY KEY (ID)
);
