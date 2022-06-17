USE alley_cat_db;

-- Insert dummy data into users
INSERT INTO alley_cat_db.users (username, pwd, email) VALUES ('user1', '12341', 'jaja@elbromas.mx');

-- Insert dummy data into levels
INSERT INTO levels (roomLayout, enemyLayout, objectLayout, levelName, usernameID) 
VALUES ('1', 'Start,0,0_Empty,0,1_End,0,2_', '0,0,1,-4.25926,10.49074_2,0,1,-2.444444,10.80556_2,0,1,-0.009259119,10.67593_2,0,1,-3.185185,7.601851_2,0,1,4.157408,9.101851_2,0,1,-0.1759259,8.111111_2,0,1,4.25,10.30556_2,0,1,-3.37037,11.4537_2,0,1,-5.75926,11.41667_2,0,1,-5.314815,7.944445_2,0,1,-3.675926,9.685185_2,0,1,-1.018518,10.01852_2,0,1,-1.361111,11.82407_2,0,1,-4.453704,11.87963_2,0,1,-5.694445,9.796296_9,0,2,-2.564815,19.18518_9,0,2,1.601852,20.80556_10,0,2,-1.203704,20.68518_', '', 'level 1', '2'),
('2', 'Start,0,0_Empty,0,1_Empty,1,1_Empty,1,2_End,1,3_', '0,0,1,-5.851852,11.59259_0,0,1,-4.768519,11.57407_1,0,1,-3.62037,11.85185_1,0,1,-2.333333,11.80556_1,0,1,-0.8518521,11.89815_1,0,1,-3.342593,10.52778_1,0,1,-1.962963,10.34259_1,0,1,-0.009259119,10.32407_0,0,1,-5.148148,9.462963_0,0,1,-3.574074,8.703704_0,1,1,11.55556,10.61111_0,1,1,12.9537,11.36111_0,1,1,14.06481,10.98148_1,1,1,16.66667,9.935184_4,1,1,21.55556,11.92593_4,1,1,21.32407,9.407408_4,1,1,23.42593,10.18519_5,1,1,16.46296,11.89815_3,1,1,15.49074,11.82407_3,1,1,15.68519,12.23148_1,1,1,19.71296,11.83333_0,1,2,14.61111,20.08333_0,1,2,17.06481,19.39815_2,1,2,17.77778,20.2963_4,1,2,17.62037,20.0463_5,1,2,16.33333,20.40741_3,1,2,17.87037,15.91667_4,1,2,15.92593,17.12963_3,1,2,16.2037,18.36111_9,1,3,15.35185,29.5_9,1,3,17.63889,28.78704_9,1,3,20.03704,28.81482_10,1,3,14.07407,27.40741_10,1,3,15.98148,27.05556_', '6,0,1,-0.1944447,5.814815_6,0,1,-0.2962966,7.064815_6,0,1,7.027778,8.87037_6,0,1,7.064816,7.833333_', 'level 2', '2');



UPDATE builderStatistics SET demonEnemy = 15, regularEnemy = 10, dragonEnemy = 8, 
 goblinEnemy = 4, muddyEnemy = 5, zombieEnemy = 2, boxObstacle = 0, floorSpikesObstacle = 4, holeObject = 0,
 ogreBoss = 5, zombieBoss = 3, totalBuiltLevels = 2 WHERE usernameID = 2;

UPDATE gameStatistics SET averageTime = 100.89, averagePoints = 4999, gamesPlayed = 2, totalTimePlayed = 201.78, totalPoints = 9998, highScore = 5378
 WHERE usernameID = 2;


--  CALL userActivity(true, 9);

-- CALL deleteUser(6);
-- CALL deleteUser(11);

#SELECT * FROM users;
#SELECT * FROM levels;
#SELECT * FROM gameStatistics;
#SELECT * FROM builderStatistics;
#SELECT * FROM playerStatistics;
