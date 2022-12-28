CREATE TABLE `discord_roles` (
	`discord_role_key` INT(11) NOT NULL AUTO_INCREMENT,
	`game_key` INT(11) NULL DEFAULT '0',
	`role` VARCHAR(50) NULL DEFAULT NULL COLLATE 'latin1_swedish_ci',
	INDEX `FK_admin_roles_games` (`game_key`) USING BTREE,
	INDEX `Index 1` (`discord_role_key`) USING BTREE,
	CONSTRAINT `FK_admin_roles_games` FOREIGN KEY (`game_key`) REFERENCES `games` (`game_key`) ON UPDATE SET NULL ON DELETE CASCADE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=16
;
