CREATE TABLE `games` (
	`game_key` INT(11) NOT NULL AUTO_INCREMENT,
	`game_name` VARCHAR(50) NOT NULL DEFAULT '' COLLATE 'latin1_swedish_ci',
	INDEX `Index 1` (`game_key`) USING BTREE
)
COLLATE='latin1_swedish_ci'
ENGINE=InnoDB
AUTO_INCREMENT=17
;
