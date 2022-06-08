
--
-- TRIGGERS
--

-- 
-- INSERT usernameID key on gameStatistics
-- 
DELIMITER $$
DROP TRIGGER IF EXISTS completeUserTable$$
CREATE TRIGGER completeUserTable
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
	    INSERT INTO gameStatistics (usernameID, gActivate) VALUE (new.usernameID, true);
END$$


--
-- INSERT usernameID key on builderStatistics
--
DROP TRIGGER IF EXISTS completeUserTable2$$
CREATE TRIGGER completeUserTable2
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
	    INSERT INTO builderStatistics (usernameID, bActivate) VALUE (new.usernameID, true);
END$$

--
-- -- INSERT usernameID key on playerStatistics
DROP TRIGGER IF EXISTS completePlayerStats$$
CREATE TRIGGER completePlayerStats
AFTER INSERT ON alley_cat_db.users
FOR EACH ROW
BEGIN
		INSERT INTO playerStatistics (usernameID, activity) VALUE(new.usernameID, true);
END$$

DELIMITER ;