USE alley_cat_db;

-- Insert dummy data into users
INSERT INTO users (username, pwd, email) VALUES ('user1', '12341', 'jaja@elbromas.mx'),
('user2', '12342', 'jaja2@elbromas.mx'),
('user3', '12343', 'jaja3@elbromas.mx'),
('user4', '12344', 'jaja4@elbromas.mx'),
('user5', '12345', 'jaja5@elbromas.mx'),
('user6', '12346', 'jaja6@elbromas.mx'),
('user7', '12347', 'jaja7@elbromas.mx'),
('user8', '12348', 'jaja8@elbromas.mx'),
('user9', '12349', 'jaja9@elbromas.mx'),
('user10', '123410', 'jaja10@elbromas.mx'),
('user11', '123411', 'jaja11@elbromas.mx'),
('user12', '123412', 'jaja12@elbromas.mx');
COMMIT;

-- Insert dummy data into levels
INSERT INTO levels (roomLayout, enemyLayout, objectLayout, levelName, usernameID) 
VALUES ('', '', '', 'level1', '1'),
('', '', '', 'level2', '2'),
('', '', '', 'level3', '3'),
('', '', '', 'level4', '4'),
('', '', '', 'level5', '5'),
('', '', '', 'level6', '6'),
('', '', '', 'level7', '7'),
('', '', '', 'level8', '8'),
('', '', '', 'level9', '9'),
('', '', '', 'level10', '10'),
('', '', '', 'level11', '11'),
('', '', '', 'level12', '12');
COMMIT;

-- insert dummy data into gameStatistics
INSERT INTO gameStatistics (averageTime, averagePoints, gamesPlayed, totalTimePlayed, totalPoints, highScore) 
VALUES (900, 1200, 9, 8100,10800, 3000),
(1200, 1800, 12, 14400,21600, 5000),
(200, 1300, 6, 1200,7800, 500),
(500, 1100, 12, 6000,13200, 600),
(900, 1400, 11, 9900,15400, 3000),
(1000, 1000, 4, 4000,4000, 3000),
(900, 900, 17, 15300,15300, 1200),
(1000, 1250, 10, 10000,12500, 3000),
(800, 1400, 9, 7200,12600, 3050),
(1200, 1100, 7, 8400,7700, 1600),
(900, 700, 8, 7200,5600, 1000),
(900, 1700, 14, 12600,23800, 2000);
COMMIT;

-- insert dummy data into builderStatistics
INSERT INTO builderStatistics (mostUsedElement, leastUsedElement, totalBuiltLevels) 
VALUES ('enemy1', 'obstacle2', 1),
('enemy2', 'obstacle2', 1),
('enemy1', 'obstacle3', 1),
('enemy1', 'obstacle2', 1),
('enemy2', 'obstacle2', 1),
('enemy2', 'obstacle3', 1),
('enemy2', 'obstacle1', 1),
('enemy1', 'obstacle2', 1),
('enemy2', 'obstacle3', 1),
('enemy3', 'obstacle1', 1),
('enemy1', 'obstacle3', 1),
('enemy1', 'obstacle2', 1);
COMMIT;

-- insert dummy data into playerStatistics
INSERT INTO  playerStatistics (usernameID, activity) 
VALUES (1, true),
(2, false),
(3, true),
(4, true),
(5, true),
(6, true),
(7, true),
(8, true),
(9, true),
(10, true),
(11, true),
(12, true);
COMMIT;


UPDATE users SET gameStatisticsID = 1, builderStatisticsID = 1 WHERE usernameID=1;
UPDATE users SET gameStatisticsID = 2, builderStatisticsID = 2 WHERE usernameID=2;
UPDATE users SET gameStatisticsID = 3, builderStatisticsID = 3 WHERE usernameID=3;
UPDATE users SET gameStatisticsID = 4, builderStatisticsID = 4 WHERE usernameID=4;
UPDATE users SET gameStatisticsID = 5, builderStatisticsID = 5 WHERE usernameID=5;
UPDATE users SET gameStatisticsID = 6, builderStatisticsID = 6 WHERE usernameID=6;
UPDATE users SET gameStatisticsID = 7, builderStatisticsID = 7 WHERE usernameID=7;
UPDATE users SET gameStatisticsID = 8, builderStatisticsID = 8 WHERE usernameID=8;
UPDATE users SET gameStatisticsID = 9, builderStatisticsID = 9 WHERE usernameID=9;
UPDATE users SET gameStatisticsID = 10, builderStatisticsID = 10 WHERE usernameID=10;
UPDATE users SET gameStatisticsID = 11, builderStatisticsID = 11 WHERE usernameID=11;
UPDATE users SET gameStatisticsID = 12, builderStatisticsID = 12 WHERE usernameID=12;


SELECT * FROM users;
SELECT * FROM levels;
SELECT * FROM gameStatistics;
SELECT * FROM builderStatistics;
SELECT * FROM playerStatistics;


