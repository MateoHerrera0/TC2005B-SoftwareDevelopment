USE heroku_29b718f30c1a210;
USE alley_cat_db;

CREATE VIEW users_display
AS
SELECT username, activity, lastActive
	FROM heroku_29b718f30c1a210.users LEFT JOIN heroku_29b718f30c1a210.playerStatistics USING (usernameID);
    
#DESCRIBE users_display;
SELECT * FROM user_display;

SELECT * FROM user_point_stats;

SELECT * FROM user_builder_stats;

SELECT * FROM user_time_played;