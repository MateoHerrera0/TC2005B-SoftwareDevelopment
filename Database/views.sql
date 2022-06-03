USE heroku_29b718f30c1a210;

CREATE VIEW users_display
AS
SELECT username, activity, lastActive
	FROM heroku_29b718f30c1a210.users LEFT JOIN heroku_29b718f30c1a210.playerStatistics USING (usernameID);
    
#DESCRIBE users_display;
SELECT * FROM users_display;

