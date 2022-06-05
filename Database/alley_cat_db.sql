
SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

-- DROP only available from user with privilege 
DROP SCHEMA IF EXISTS alley_cat_db;
CREATE SCHEMA alley_cat_db;
USE alley_cat_db;

--
-- Table users 
--

CREATE TABLE users (
	usernameID INT NOT NULL AUTO_INCREMENT,
	username VARCHAR(45) NOT NULL UNIQUE,
    pwd VARCHAR(45) NOT NULL,
	email VARCHAR(45) NOT NULL,
    gameStatisticsID INT,
    builderStatisticsID INT,
    PRIMARY KEY (usernameID),
    KEY idx_fk_gameStatisticsID (gameStatisticsID),
    CONSTRAINT `fk_rating_gameStatisticsID` FOREIGN KEY (gameStatisticsID) REFERENCES gameStatistics(gameStatisticsID),
	KEY idx_fk_builderStatisticsID (builderStatisticsID),
    CONSTRAINT `fk_rating_builderStatisticsID` FOREIGN KEY (builderStatisticsID) REFERENCES builderStatistics(builderStatisticsID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table level
--

CREATE TABLE levels (
	levelID INT NOT NULL AUTO_INCREMENT,
    roomLayout TEXT,
    enemyLayout TEXT,
    objectLayout TEXT,
    levelName TEXT,
    usernameID INT NOT NULL,
    PRIMARY KEY (levelID),
	KEY idx_fk_usernameID (usernameID),
	CONSTRAINT `fk_rating_usernameID` FOREIGN KEY (usernameID) REFERENCES users(usernameID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
    
--
-- Table gameStatistics
--

CREATE TABLE gameStatistics (
	gameStatisticsID INT NOT NULL AUTO_INCREMENT,
    averageTime FLOAT,
    averagePoints FLOAT,
    gamesPlayed INT,
    totalTimePlayed FLOAT,
    totalPoints INT,
    highScore INT,
    PRIMARY KEY (gameStatisticsID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
 
--
-- Table builderStatistics
--

CREATE TABLE builderStatistics (
	builderStatisticsID INT NOT NULL AUTO_INCREMENT,
    #mostUsedElementVARCHAR(45),
    demonEnemy INT,
    eyeEnemy INT,
    regularEnemy INT,
    dragonEnemy INT,
    goblinEnemy INT,
    muddyEnemy INT,
    zombieEnemy INT,
    #leastUsedElementVARCHAR(45),
    boxObstacle INT,
    floorSpikesObstacle INT,
    holeObject INT,
    totalBuiltLevels INT,
	PRIMARY KEY (builderStatisticsID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table playerStatistics
--

CREATE TABLE playerStatistics (
	usernameID INT NOT NULL,
	activity BOOLEAN,
	lastActive TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	KEY idx_fk_usernameID (usernameID),
	CONSTRAINT `fk_rating_usernameID2` FOREIGN KEY (usernameID) REFERENCES users(usernameID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- user view
CREATE VIEW user_display
AS
SELECT username, activity, lastActive
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.playerStatistics USING (usernameID);

--
-- user points view
CREATE VIEW user_point_stats AS SELECT username, gameStatisticsID, highScore AS `HighScore`, 
averagePoints AS `AveragePoints`, gamesPlayed AS `GamesPlayed`, totalPoints AS `TotalPoints`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.gameStatistics USING (gameStatisticsID);

--
-- user builder stats
CREATE VIEW user_builder_stats AS SELECT username, builderStatisticsID, totalBuiltLevels AS `LevelsCreated`, 
demonEnemy AS `Demon`, eyeEnemy AS `Eye`, dragonEnemy AS `Dragon`, goblinEnemy AS `Goblin`, muddyEnemy AS `Muddy`,
zombieEnemy AS `Zombie`, boxObstacle AS `Box`, floorSpikesObstacle AS `FloorSpikes`, holeObject AS `Hole`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.builderStatistics USING (builderStatisticsID);
    
--
-- user time played view
CREATE VIEW user_time_played AS SELECT username, gameStatisticsID, averageTime AS `AverageTime`, totalTimePlayed AS `TotalTimePlayed`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.gamestatistics USING (gameStatisticsID);
