USE alley_cat_db;

#CREATE VIEW users_display
AS
SELECT username, activity, lastActive
	FROM alley_cat_db.users LEFT JOIN alley_cat_db.playerStatistics USING (usernameID);
    
#DESCRIBE users_display;
#SELECT * FROM users_display;

