/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

CREATE TABLE IF NOT EXISTS `authentication_ticket` (
  `user_id` int(11) NOT NULL,
  `sso_ticket` varchar(250) NOT NULL DEFAULT '',
  `expires_at` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `authentication_ticket` DISABLE KEYS */;
INSERT INTO `authentication_ticket` (`user_id`, `sso_ticket`, `expires_at`) VALUES
	(1, '123', NULL),
	(2, 'kek', NULL);
/*!40000 ALTER TABLE `authentication_ticket` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `label` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_friend` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_friend` DISABLE KEYS */;
INSERT INTO `messenger_friend` (`user_id`, `friend_id`) VALUES
	(1, 2),
	(2, 1);
/*!40000 ALTER TABLE `messenger_friend` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_request` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_request` DISABLE KEYS */;
INSERT INTO `messenger_request` (`user_id`, `friend_id`) VALUES
	(2, 1),
	(3, 1),
	(1, 1),
	(1, 4);
/*!40000 ALTER TABLE `messenger_request` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(50) DEFAULT NULL,
  `figure` varchar(250) NOT NULL DEFAULT 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80',
  `sex` varchar(1) NOT NULL DEFAULT 'M',
  `rank` int(11) NOT NULL DEFAULT 1,
  `credits` int(11) NOT NULL DEFAULT 0,
  `pixels` int(11) NOT NULL DEFAULT 0,
  `motto` text NOT NULL DEFAULT '',
  `join_date` datetime NOT NULL DEFAULT current_timestamp(),
  `last_online` datetime NOT NULL DEFAULT current_timestamp(),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `username`, `figure`, `sex`, `rank`, `credits`, `pixels`, `motto`, `join_date`, `last_online`) VALUES
	(1, 'Alex', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 1, 2000, 0, '123', '2020-03-29 18:20:03', '2020-04-08 21:43:54'),
	(2, 'Test', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 1, 0, 0, '456', '2020-03-29 20:47:31', '2020-04-08 17:34:58'),
	(3, 'Test123', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 1, 0, 0, '789', '2020-03-29 20:47:31', '2020-03-29 20:47:31');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_settings` (
  `user_id` int(11) NOT NULL,
  `daily_respect_points` int(11) NOT NULL DEFAULT 0,
  `daily_respect_pet_points` int(11) NOT NULL DEFAULT 0,
  `respect_points` int(11) NOT NULL DEFAULT 0,
  `friend_requests_enabled` tinyint(1) NOT NULL DEFAULT 1,
  `following_enabled` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_settings` DISABLE KEYS */;
INSERT INTO `user_settings` (`user_id`, `daily_respect_points`, `daily_respect_pet_points`, `respect_points`, `friend_requests_enabled`, `following_enabled`) VALUES
	(1, 0, 0, 0, 1, 1);
/*!40000 ALTER TABLE `user_settings` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_subscriptions` (
  `user_id` int(11) NOT NULL,
  `subscription_type` enum('HC','VIP') NOT NULL,
  `subscribed_at` datetime NOT NULL,
  `expire_at` datetime NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_subscriptions` DISABLE KEYS */;
INSERT INTO `user_subscriptions` (`user_id`, `subscription_type`, `subscribed_at`, `expire_at`) VALUES
	(1, 'VIP', '2020-04-10 04:05:48', '2020-04-10 04:05:48');
/*!40000 ALTER TABLE `user_subscriptions` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
