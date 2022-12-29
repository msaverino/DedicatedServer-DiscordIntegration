CREATE TABLE `updater` (
	`updater_key` INT(11) NOT NULL AUTO_INCREMENT,
	`game_key` INT(11) NOT NULL DEFAULT '0',
	`enable_updater` INT(11) NOT NULL DEFAULT '0' COMMENT 'Boolean - Decision if we should enable the updater.',
	`updater_path` VARCHAR(256) NOT NULL DEFAULT '' COMMENT 'Path to the updater executable' COLLATE 'latin1_swedish_ci',
	`root_path` VARCHAR(50) NOT NULL COLLATE 'latin1_swedish_ci',
	`application_path` VARCHAR(50) NOT NULL COLLATE 'latin1_swedish_ci',
	`updater_arguments` VARCHAR(256) NOT NULL DEFAULT '' COMMENT 'Arguments to run the updater.' COLLATE 'latin1_swedish_ci',
	`steam_game` INT(11) NOT NULL DEFAULT '0' COMMENT 'Is the game a steam game (SteamCMD)',
	`server_version` VARCHAR(256) NOT NULL DEFAULT '' COMMENT 'Version running on the dedicated server.' COLLATE 'latin1_swedish_ci',
	`latest_version` VARCHAR(256) NOT NULL DEFAULT '' COMMENT 'Latest version from the official source.' COLLATE 'latin1_swedish_ci',
	`update_lock` INT(11) NOT NULL DEFAULT '0' COMMENT 'Boolean - Dedicated Server is actively updating. - Can be stuck at 1 if something goes wrong.',
	`force_update` INT(11) NOT NULL DEFAULT '0' COMMENT 'Boolean - Decision if we should ignore that players are currently on the server.',
	`dedicated_acknowledgement` INT(11) NOT NULL DEFAULT '0' COMMENT 'Boolean - Dedicated server has acknowledged there is an update.',
	`dedicated_acknowledgement_date` DATETIME NOT NULL,
	`dedicated_completed_update` DATETIME NOT NULL,
	INDEX `Index 1` (`updater_key`) USING BTREE,
	INDEX `FK_games` (`game_key`) USING BTREE,
	CONSTRAINT `FK_games` FOREIGN KEY (`game_key`) REFERENCES `games` (`game_key`) ON UPDATE NO ACTION ON DELETE CASCADE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=1
;
