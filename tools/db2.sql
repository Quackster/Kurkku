/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE TABLE IF NOT EXISTS `authentication_ticket` (
  `user_id` int(11) NOT NULL,
  `sso_ticket` varchar(255) NOT NULL,
  `expires_at` datetime DEFAULT NULL,
  PRIMARY KEY (`user_id`,`sso_ticket`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `authentication_ticket` DISABLE KEYS */;
/*!40000 ALTER TABLE `authentication_ticket` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `catalogue_discounts` (
  `page_id` int(11) NOT NULL AUTO_INCREMENT,
  `purchase_limit` int(11) DEFAULT NULL,
  `item_count_required` decimal(19,5) DEFAULT NULL,
  `item_count_free` decimal(19,5) DEFAULT NULL,
  `expire_at` datetime DEFAULT NULL,
  PRIMARY KEY (`page_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `catalogue_discounts` DISABLE KEYS */;
/*!40000 ALTER TABLE `catalogue_discounts` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `catalogue_items` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sale_code` varchar(255) DEFAULT NULL,
  `page_id` varchar(255) DEFAULT NULL,
  `order_id` int(11) DEFAULT NULL,
  `price_coins` int(11) DEFAULT NULL,
  `price_seasonal` int(11) DEFAULT NULL,
  `seasonal_type` varchar(255) DEFAULT NULL,
  `hidden` tinyint(1) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  `definition_id` int(11) DEFAULT NULL,
  `item_specialspriteid` varchar(255) DEFAULT NULL,
  `is_package` tinyint(1) DEFAULT NULL,
  `allow_bulk_purchase` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `catalogue_items` DISABLE KEYS */;
/*!40000 ALTER TABLE `catalogue_items` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `catalogue_packages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `salecode` varchar(255) DEFAULT NULL,
  `definition_id` int(11) DEFAULT NULL,
  `special_sprite_id` varchar(255) DEFAULT NULL,
  `amount` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `catalogue_packages` DISABLE KEYS */;
/*!40000 ALTER TABLE `catalogue_packages` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `catalogue_pages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) DEFAULT NULL,
  `order_id` int(11) DEFAULT NULL,
  `caption` varchar(255) DEFAULT NULL,
  `page_link` varchar(255) DEFAULT NULL,
  `min_rank` int(11) DEFAULT NULL,
  `icon_colour` int(11) DEFAULT NULL,
  `icon_image` int(11) DEFAULT NULL,
  `is_navigatable` tinyint(1) DEFAULT NULL,
  `is_enabled` tinyint(1) DEFAULT NULL,
  `is_club_only` tinyint(1) DEFAULT NULL,
  `layout` varchar(255) DEFAULT NULL,
  `images` varchar(255) DEFAULT NULL,
  `texts` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `catalogue_pages` DISABLE KEYS */;
/*!40000 ALTER TABLE `catalogue_pages` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `catalogue_subscriptions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `page_id` int(11) DEFAULT NULL,
  `price_coins` int(11) DEFAULT NULL,
  `price_seasonal` int(11) DEFAULT NULL,
  `seasonal_type` varchar(255) DEFAULT NULL,
  `months` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `catalogue_subscriptions` DISABLE KEYS */;
/*!40000 ALTER TABLE `catalogue_subscriptions` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `item` (
  `id` varchar(255) NOT NULL,
  `order_id` int(11) DEFAULT NULL,
  `user_id` int(11) DEFAULT NULL,
  `room_id` int(11) DEFAULT NULL,
  `definition_id` int(11) DEFAULT NULL,
  `x` int(11) DEFAULT NULL,
  `y` int(11) DEFAULT NULL,
  `z` double DEFAULT NULL,
  `wall_position` varchar(255) DEFAULT NULL,
  `rotation` int(11) DEFAULT NULL,
  `custom_data` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `item` DISABLE KEYS */;
/*!40000 ALTER TABLE `item` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `item_definitions` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `sprite` varchar(255) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `sprite_id` int(11) DEFAULT NULL,
  `length` int(11) DEFAULT NULL,
  `width` int(11) DEFAULT NULL,
  `top_height` double DEFAULT NULL,
  `max_status` int(11) DEFAULT NULL,
  `behaviour` varchar(255) DEFAULT NULL,
  `interactor` varchar(255) DEFAULT NULL,
  `is_tradable` tinyint(1) DEFAULT NULL,
  `is_recyclable` tinyint(1) DEFAULT NULL,
  `is_stackable` tinyint(1) DEFAULT NULL,
  `is_sellable` tinyint(1) DEFAULT NULL,
  `drink_ids` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `item_definitions` DISABLE KEYS */;
/*!40000 ALTER TABLE `item_definitions` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_category` (
  `user_id` int(11) NOT NULL,
  `label` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`,`label`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_chat_history` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  `message` varchar(255) DEFAULT NULL,
  `has_read` tinyint(1) DEFAULT NULL,
  `messaged_at` datetime DEFAULT NULL,
  PRIMARY KEY (`user_id`,`friend_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_chat_history` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_chat_history` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_friend` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  PRIMARY KEY (`user_id`,`friend_id`),
  KEY `friend_id` (`friend_id`),
  CONSTRAINT `FK8FCB46E2E7EFF0E0` FOREIGN KEY (`friend_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_friend` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_friend` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_request` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  PRIMARY KEY (`user_id`,`friend_id`),
  KEY `friend_id` (`friend_id`),
  CONSTRAINT `FK4294C9D7E7EFF0E0` FOREIGN KEY (`friend_id`) REFERENCES `user` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_request` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_request` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `navigator_official_rooms` (
  `banner_id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) DEFAULT NULL,
  `banner_type` varchar(255) DEFAULT NULL,
  `room_id` int(11) DEFAULT NULL,
  `image_type` varchar(255) DEFAULT NULL,
  `label` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `description_entry` int(11) DEFAULT NULL,
  `image_url` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`banner_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `navigator_official_rooms` DISABLE KEYS */;
/*!40000 ALTER TABLE `navigator_official_rooms` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `owner_id` int(11) DEFAULT NULL,
  `name` varchar(255) DEFAULT NULL,
  `description` varchar(255) DEFAULT NULL,
  `category_id` int(11) DEFAULT NULL,
  `visitors_now` int(11) DEFAULT NULL,
  `visitors_max` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL,
  `password` varchar(255) DEFAULT NULL,
  `model` varchar(255) DEFAULT NULL,
  `ccts` varchar(255) DEFAULT NULL,
  `wallpaper` varchar(255) DEFAULT NULL,
  `floor` varchar(255) DEFAULT NULL,
  `landscape` varchar(255) DEFAULT NULL,
  `allow_pets` tinyint(1) DEFAULT NULL,
  `allow_pets_eat` tinyint(1) DEFAULT NULL,
  `allow_walkthrough` tinyint(1) DEFAULT NULL,
  `hidewall` tinyint(1) DEFAULT NULL,
  `wall_thickness` int(11) DEFAULT NULL,
  `floor_thickness` int(11) DEFAULT NULL,
  `rating` int(11) DEFAULT NULL,
  `is_owner_hidden` tinyint(1) DEFAULT NULL,
  `trade_setting` int(11) DEFAULT NULL,
  `is_muted` tinyint(1) DEFAULT NULL,
  `who_can_ban` varchar(255) DEFAULT NULL,
  `who_can_kick` varchar(255) DEFAULT NULL,
  `who_can_mute` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room` DISABLE KEYS */;
/*!40000 ALTER TABLE `room` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `caption` varchar(255) DEFAULT NULL,
  `enabled` tinyint(1) DEFAULT NULL,
  `min_rank` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `room_category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room_models` (
  `model` varchar(255) NOT NULL,
  `door_x` int(11) DEFAULT NULL,
  `door_y` int(11) DEFAULT NULL,
  `door_z` int(11) DEFAULT NULL,
  `door_dir` int(11) DEFAULT NULL,
  `heightmap` varchar(255) DEFAULT NULL,
  `club_only` tinyint(1) DEFAULT NULL,
  PRIMARY KEY (`model`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room_models` DISABLE KEYS */;
/*!40000 ALTER TABLE `room_models` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `server_settings` (
  `setting` varchar(255) NOT NULL,
  `value` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`setting`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `server_settings` DISABLE KEYS */;
INSERT INTO `server_settings` (`setting`, `value`) VALUES
	('catalogue.subscription.page', '63'),
	('club.gift.interval', '1'),
	('club.gift.interval.type', 'MONTH'),
	('inventory.items.per.page', '500'),
	('max.friends.hc', '600'),
	('max.friends.normal', '300'),
	('max.friends.vip', '1100'),
	('max.rooms.allowed', '100'),
	('max.rooms.allowed.subscribed', '200'),
	('timer.speech.bubble', '15');
/*!40000 ALTER TABLE `server_settings` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `subscription_gifts` (
  `sale_code` varchar(255) NOT NULL,
  `duration_requirement` int(11) DEFAULT NULL,
  PRIMARY KEY (`sale_code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `subscription_gifts` DISABLE KEYS */;
/*!40000 ALTER TABLE `subscription_gifts` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `tags` (
  `user_id` int(11) NOT NULL,
  `room_id` int(11) NOT NULL,
  `text` varchar(255) NOT NULL,
  PRIMARY KEY (`user_id`,`room_id`,`text`),
  KEY `room_id` (`room_id`),
  CONSTRAINT `FK1F58E933DB33DBB` FOREIGN KEY (`room_id`) REFERENCES `room` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `tags` DISABLE KEYS */;
/*!40000 ALTER TABLE `tags` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(255) DEFAULT NULL,
  `figure` varchar(255) DEFAULT NULL,
  `sex` varchar(255) DEFAULT NULL,
  `rank` int(11) DEFAULT NULL,
  `credits` int(11) DEFAULT NULL,
  `join_date` datetime DEFAULT NULL,
  `last_online` datetime DEFAULT NULL,
  `motto` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_seasonal_currencies` (
  `user_id` int(11) NOT NULL,
  `seasonal_type` varchar(255) NOT NULL,
  `balance` int(11) DEFAULT NULL,
  PRIMARY KEY (`user_id`,`seasonal_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_seasonal_currencies` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_seasonal_currencies` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_settings` (
  `user_id` int(11) NOT NULL,
  `respect_points` int(11) DEFAULT NULL,
  `daily_respect_points` int(11) DEFAULT NULL,
  `daily_respect_pet_points` int(11) DEFAULT NULL,
  `friend_requests_enabled` tinyint(1) DEFAULT NULL,
  `following_enabled` tinyint(1) DEFAULT NULL,
  `online_time` bigint(20) DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_settings` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_settings` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_subscriptions` (
  `user_id` int(11) NOT NULL,
  `subscribed_at` datetime DEFAULT NULL,
  `expire_at` datetime DEFAULT NULL,
  `gift_at` datetime DEFAULT NULL,
  `gifts_redeemable` int(11) DEFAULT NULL,
  `subscription_age` bigint(20) DEFAULT NULL,
  `subscription_age_last_updated` datetime DEFAULT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_subscriptions` DISABLE KEYS */;
/*!40000 ALTER TABLE `user_subscriptions` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
