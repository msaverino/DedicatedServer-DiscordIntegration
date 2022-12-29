CREATE TABLE `networking` (
	`network_key` INT(11) NOT NULL AUTO_INCREMENT,
	`game_key` INT(11) NOT NULL DEFAULT '0',
	`server_ip` VARCHAR(50) NULL DEFAULT '' COMMENT 'Server IP' COLLATE 'latin1_swedish_ci',
	`server_port` INT(11) NULL DEFAULT '0',
	`query_port_send` INT(11) NULL DEFAULT NULL,
	`query_port_receive` INT(11) NULL DEFAULT NULL,
	`query_port_two_way` INT(11) NULL DEFAULT NULL,
	INDEX `Index 1` (`network_key`) USING BTREE,
	INDEX `FK__games` (`game_key`) USING BTREE,
	CONSTRAINT `FK__games` FOREIGN KEY (`game_key`) REFERENCES `games` (`game_key`) ON UPDATE NO ACTION ON DELETE CASCADE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=1
;
