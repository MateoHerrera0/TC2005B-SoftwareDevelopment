
SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

-- DROP only available from user with privilege 
#DROP SCHEMA IF EXISTS alley_cat_db;
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
    mostUsedElement VARCHAR(45),
    leastUsedElement VARCHAR(45),
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