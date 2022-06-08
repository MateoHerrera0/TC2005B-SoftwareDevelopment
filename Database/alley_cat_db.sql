
SET NAMES utf8mb4;
SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL';

-- DROP only available from user with privilege 
DROP SCHEMA IF EXISTS alley_cat_db;
CREATE SCHEMA alley_cat_db;
USE alley_cat_db;

--
-- TABLES
--

--
-- Table users 
--

CREATE TABLE users (
	usernameID INT NOT NULL AUTO_INCREMENT,
	username VARCHAR(45) NOT NULL UNIQUE,
    pwd VARCHAR(45) NOT NULL,
	email VARCHAR(45) NOT NULL,
    PRIMARY KEY (usernameID)
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
	usernameID INT NOT NULL,
    gActivate BOOL,
    averageTime FLOAT NOT NULL DEFAULT 0,
    averagePoints FLOAT NOT NULL DEFAULT 0,
    gamesPlayed INT NOT NULL DEFAULT 0,
    totalTimePlayed FLOAT NOT NULL DEFAULT 0,
    totalPoints INT NOT NULL DEFAULT 0,
    highScore INT NOT NULL DEFAULT 0,
	KEY idx_fk_usernameID (usernameID),
	CONSTRAINT `fk_rating_usernameID2` FOREIGN KEY (usernameID) REFERENCES users(usernameID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
 
--
-- Table builderStatistics
--

CREATE TABLE builderStatistics (
	usernameID INT NOT NULL,
	bActivate BOOL,
    #mostUsedElementVARCHAR(45),
    demonEnemy INT DEFAULT 0,
    regularEnemy INT DEFAULT 0,
    dragonEnemy INT DEFAULT 0,
    goblinEnemy INT DEFAULT 0,
    muddyEnemy INT DEFAULT 0,
    zombieEnemy INT DEFAULT 0,
    #leastUsedElementVARCHAR(45),
    boxObstacle INT DEFAULT 0,
    floorSpikesObstacle INT DEFAULT 0,
    holeObject INT DEFAULT 0,
    ogreBoss INT DEFAULT 0,
    zombieBoss INT DEFAULT 0,
    totalBuiltLevels INT DEFAULT 0,
	KEY idx_fk_usernameID (usernameID),
	CONSTRAINT `fk_rating_usernameID3` FOREIGN KEY (usernameID) REFERENCES users(usernameID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Table playerStatistics
--

CREATE TABLE playerStatistics (
	usernameID INT NOT NULL,
	activity BOOLEAN,
	lastActive TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	KEY idx_fk_usernameID (usernameID),
	CONSTRAINT `fk_rating_usernameID4` FOREIGN KEY (usernameID) REFERENCES users(usernameID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- VIEWS
--

--
-- user view
CREATE VIEW user_display
AS
SELECT username, activity, lastActive
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.playerStatistics USING (usernameID);

--
-- user points view
CREATE VIEW user_point_stats AS SELECT username, usernameID, highScore AS `HighScore`, 
averagePoints AS `AveragePoints`, gamesPlayed AS `GamesPlayed`, totalPoints AS `TotalPoints`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.gameStatistics USING (usernameID);

--
-- user builder stats
CREATE VIEW user_builder_stats AS SELECT username, usernameID, totalBuiltLevels AS `LevelsCreated`, 
demonEnemy AS `Demon`, dragonEnemy AS `Dragon`, goblinEnemy AS `Goblin`, muddyEnemy AS `Muddy`,
zombieEnemy AS `Zombie`, boxObstacle AS `Box`, floorSpikesObstacle AS `FloorSpikes`, holeObject AS `Hole`,
ogreBoss AS `OgreBoss`, zombieBoss AS `ZombieBoss`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.builderStatistics USING (usernameID);
    
--
-- user time played view
CREATE VIEW user_time_played AS SELECT username, usernameID, averageTime AS `AverageTime`, totalTimePlayed AS `TotalTimePlayed`
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.gamestatistics USING (usernameID);

--
-- TRIGGERS
--

-- 
-- INSERT usernameID key on gameStatistics
-- 
DELIMITER $$
-- DROP TRIGGER IF EXISTS completeUserTable;
CREATE TRIGGER completeUserTable
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
	    INSERT INTO gameStatistics (usernameID, gActivate) VALUE (new.usernameID, true);
END$$
DELIMITER ;

--
-- INSERT usernameID key on builderStatistics
--
DELIMITER $$
-- DROP TRIGGER IF EXISTS completeUserTable2;
CREATE TRIGGER completeUserTable2
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
	    INSERT INTO builderStatistics (usernameID, bActivate) VALUE (new.usernameID, true);
END$$
DELIMITER ;


--
-- INSERT usernameID key on playerStatistics
--
DELIMITER $$
-- DROP TRIGGER IF EXISTS completePlayerStats;
CREATE TRIGGER completePlayerStats
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
		INSERT INTO playerStatistics (usernameID, activity) VALUE(new.usernameID, true);
END$$
DELIMITER ;


-- 
-- UPDATE levels to admin when deleting user
DELIMITER $$
-- DROP TRIGGER IF EXISTS deleteUserLevels
CREATE TRIGGER deleteUserLevels
BEFORE DELETE ON alley_cat_db.users
FOR EACH ROW
BEGIN
		UPDATE levels SET usernameID = 1 WHERE usernameID = old.usernameID;
END$$
DELIMITER ;

--
-- PROCEDURES
--

--
-- PROCEDURE to UPDATE last time a user was active
DElIMITER $$

CREATE PROCEDURE userActivity (IN activeBool BOOL, IN userID INT)
BEGIN
        UPDATE playerStatistics SET activity = activeBool, lastActive = DEFAULT
        WHERE usernameID = userID;
END $$

DELIMITER ;

--
-- PROCEDURE to delete user
DELIMITER $$

CREATE PROCEDURE deleteUser (IN deleteUserID VARCHAR(45))
BEGIN
		DELETE usernameID, username, pwd, email FROM alley_cat_db.users WHERE usernameID = deleteUserID;
        DELETE  usernameID, gActivate, averageTime, averagePoints, gamesPlayed, totalTimePlayed, totalPoints,
        highScore FROM alley_cat_db.gameStatistics WHERE usernameID = deleteUserID;
        DELETE usernameID, bActivate, demonEnemy, regularEnemy, dragonEnemy, goblinEnemy, muddyEnemy, zombieEnemy, 
        boxObstacle, floorSpikesObstacle, holeObject, ogreBoss, zombieBoss, totalBuiltLevels
        FROM alley_cat_db.builderStatistics WHERE usernameID = deleteUserID;
        DELETE usernameID, activity, lastActive FROM alley_cat_db.playerStatistics WHERE usernameID = deleteUserID;
END $$

DELIMITER ;

--
-- PROCEDURE to create admin
-- DELIMITER $$

-- CREATE PROCEDURE admincreate (IN 

