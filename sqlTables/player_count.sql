CREATE TABLE `player_count` (
	`player_count_id` INT(11) NOT NULL AUTO_INCREMENT,
	`game_key` INT(11) NULL DEFAULT NULL,
	`online_players` INT(11) NULL DEFAULT NULL,
	`last_updated` DATETIME NULL DEFAULT NULL,
	`most_online` INT(11) NULL DEFAULT NULL,
	`most_online_date` DATETIME NULL DEFAULT NULL,
	INDEX `Index 1` (`player_count_id`) USING BTREE,
	INDEX `FK_player_count_games` (`game_key`) USING BTREE,
	CONSTRAINT `FK_player_count_games` FOREIGN KEY (`game_key`) REFERENCES `games` (`game_key`) ON UPDATE NO ACTION ON DELETE CASCADE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=9
;
