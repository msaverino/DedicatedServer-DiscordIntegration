CREATE TABLE `discord_notification` (
	`notification_key` INT(11) NOT NULL AUTO_INCREMENT,
	`game_key` INT(11) NULL DEFAULT NULL,
	`admin_channel` VARCHAR(50) NULL DEFAULT '' COMMENT 'Used to notify admins in the event there is an error.' COLLATE 'latin1_swedish_ci',
	`notify_admins` INT(11) NULL DEFAULT '0',
	`user_notification_channel` VARCHAR(50) NULL DEFAULT '' COLLATE 'latin1_swedish_ci',
	`notify_users` INT(11) NULL DEFAULT '0',
	`player_count_channel` VARCHAR(50) NULL DEFAULT '' COLLATE 'latin1_swedish_ci',
	`update_player_count` INT(11) NOT NULL DEFAULT '0',
	INDEX `Index 1` (`notification_key`) USING BTREE,
	INDEX `FK_discord_notification_games` (`game_key`) USING BTREE,
	CONSTRAINT `FK_discord_notification_games` FOREIGN KEY (`game_key`) REFERENCES `games` (`game_key`) ON UPDATE SET NULL ON DELETE CASCADE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=1
;
