CREATE TABLE `discord` (
	`discord_key` INT(11) NOT NULL AUTO_INCREMENT,
	`token` VARCHAR(128) NULL DEFAULT '' COMMENT 'Token to Authenticate' COLLATE 'latin1_swedish_ci',
	`status` VARCHAR(128) NULL DEFAULT '' COMMENT 'Status of the Bot' COLLATE 'latin1_swedish_ci',
	`game` VARCHAR(128) NULL DEFAULT '' COMMENT 'Game the bot is playing' COLLATE 'latin1_swedish_ci',
	`display_name` VARCHAR(128) NULL DEFAULT '' COMMENT 'Display Name of the Bot' COLLATE 'latin1_swedish_ci',
	INDEX `Index 1` (`discord_key`) USING BTREE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=1
;
