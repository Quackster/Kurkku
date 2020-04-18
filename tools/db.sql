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

CREATE TABLE IF NOT EXISTS `catalogue_pages` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) NOT NULL DEFAULT -1,
  `order_id` int(11) NOT NULL DEFAULT 1,
  `min_rank` int(11) NOT NULL DEFAULT 1,
  `is_navigatable` tinyint(1) NOT NULL DEFAULT 0,
  `is_enabled` tinyint(1) NOT NULL DEFAULT 1,
  `is_club_only` tinyint(11) NOT NULL DEFAULT 0,
  `caption` text NOT NULL,
  `icon_image` int(11) NOT NULL DEFAULT 0,
  `icon_colour` int(11) NOT NULL DEFAULT 0,
  `layout` text NOT NULL DEFAULT '',
  `images` text NOT NULL,
  `texts` text NOT NULL,
  PRIMARY KEY (`id`),
  KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=139 DEFAULT CHARSET=utf8mb4 ROW_FORMAT=COMPACT;

/*!40000 ALTER TABLE `catalogue_pages` DISABLE KEYS */;
INSERT INTO `catalogue_pages` (`id`, `parent_id`, `order_id`, `min_rank`, `is_navigatable`, `is_enabled`, `is_club_only`, `caption`, `icon_image`, `icon_colour`, `layout`, `images`, `texts`) VALUES
	(1, -1, 0, 1, 1, 1, 0, 'Frontpage', 1, 0, 'frontpage3', '["catalog_frontpage_headline2_en","topstory_ecotron_03"]', '["Kurkku Development","Enjoy the catalogue for the Kurkku server","","How do I get Credits easily?","1. Always ask permission from the bill payer first.\\r\\n2. Send HABBO in a UK SMS to 78881. You\'\'ll get an SMS back with a voucher code and will be charged £3 plus your standard UK SMS rate, normally 10p.\\r\\n3. Enter the code below to redeem 35 Credits. For Habbo Credit options or to redeem a Wallie Voucher Card, simply click \\"Get Credits >>\\" below.","Redeem a Habbo Voucher code here:","","#FAF8CC","#FAF8CC","Click here for more information..."]'),
	(2, -1, 1000, 1, 0, 1, 0, 'Classic Furni', 2, 2, '', '[]', '[]'),
	(3, 2, 1001, 1, 1, 1, 0, 'Spaces', 55, 1, 'spaces', '["catalog_spaces_headline1"]', '["Floors, wallpapers, landscapes - get a groovy combination to your room. Use our virtual room preview below to test out the combinations before you buy. Select the design and color you like and click Buy."]'),
	(4, -1, 8000, 1, 1, 1, 0, 'Exchange', 6, 3, 'default_3x3', '["catalog_bank_headline1","catalog_bank_teaser","catalog_special_txtbg1"]', '["The Habbo Exchange is where you can convert your Habbo Credits into a tradable currency. You can use this tradable currency to exchange Habbo Credits for Furni!","Click on the item you want for more information","Gold! Glorious Gold!"]'),
	(5, 2, 1019, 1, 1, 1, 0, 'Rollers', 96, 0, 'default_3x3', '["catalog_roller_headline1","","catalog_special_txtbg1"]', '["Move your imagination, while you move your Habbo!  Perfect for mazes, games, for keeping your queue moving or making your pet go round in circles for hours.  Available in multi-packs - the more you buy the cheaper the Roller! Pink Rollers out now!","Click on a Roller to see more information!","You can fit 30 Rollers in a user flat!"]'),
	(6, 2, 1004, 1, 1, 1, 0, 'Teleporters', 58, 0, 'default_3x3', '["catalog_doors_headline1","catalog_teleports_teaser2_en","catalog_special_txtbg1"]', '["Beam your user from one room to another with one of our cunningly disguised, space age teleports. Now you can link any two rooms together! Teleports are sold in pairs, so if you trade for them, check you\'re getting a linked pair.","Click on the item you want for more information","New Door Teleport!"]'),
	(7, 54, 0, 1, 1, 1, 0, 'Pets', 0, 0, 'pets', '["catalog_pet_headline1"]', '["Fluff and whiskers, meows and woofs! You\'\'re about to enter the world of small creatures with furry features. Find a new friend from our ever-changing selection. From faithful servants to playful playmates - here\'s where you\'\'ll find them all.","Find your own pet!"]'),
	(8, 54, 0, 1, 1, 1, 0, 'Pet Accessories', 43, 0, 'default_3x3', '["catalog_pet_headline2","ctlg_pet_teaser1","catalog_special_txtbg2"]', '["You\'\'ll need to take care of your pet to keep it happy and healthy. This section of the Catalogue has EVERYTHING you\'ll need to satisfy your pet\'s needs.","Click on the item you want for more information","You\'ll have to share it!"]'),
	(9, 2, 1008, 1, 1, 1, 0, 'Area', 14, 0, 'default_3x3', '["catalog_area_headline1","catalog_area_teaser1","catalog_special_txtbg2"]', '["Introducing the Area Collection...  Clean, chunky lines set this collection apart as a preserve of the down-to-earth person. It\'s beautiful in its simplicity, and welcoming to everyone.","Click on the item you want for more information","Beautiful in it\'s simplicity!"]'),
	(10, 138, 0, 1, 1, 1, 0, 'Gothic', 30, 0, 'default_3x3', '["catalog_gothic_headline1","catalog_gothic_teaser1"]', '["The Gothic section is full of medieval looking items. Create your own Gothic castle!","Click on the item you want for more information",""]'),
	(11, -1, 6000, 1, 0, 1, 0, 'Trax', 4, 4, 'default_3x3', '[]', '[]'),
	(12, 2, 1006, 1, 1, 1, 0, 'Candy', 19, 0, 'default_3x3', '["catalog_candy_headline1","catalog_candy_teaser1","catalog_special_txtbg2"]', '["Candy combines the cool, clean lines of the Mode collection with a softer, more soothing style. It\'\'s urban sharpness with a hint of the feminine.","Click on the item you want for more information","Relax! It\'s faux-fur."]'),
	(13, 137, 2002, 1, 1, 1, 0, 'Asian', 15, 0, 'default_3x3', '["catalog_asian_headline1","catalog_asian_teaser1"]', '["Introducing the Asian collection... These handcrafted items are the result of years of child slavery, some mixture of Ying and Yang and a mass-shipping from China. These authentic items fit in every oriental themed user flat.","Click on the item you want for more information",""]'),
	(14, 2, 1018, 1, 1, 1, 0, 'Iced', 13, 0, 'default_3x3', '["catalog_iced_headline1","catalog_iced_teaser1","catalog_special_txtbg2"]', '["Introducing the Iced Collection...  For the person who needs no introduction. It\'s so chic, it says everything and nothing. It\'s a blank canvas, let your imagination to run wild!","Click on the item you want for more information"," These chairs are so comfy."]'),
	(15, 2, 1009, 1, 1, 1, 0, 'Lodge', 37, 0, 'default_3x3', '["catalog_lodge_headline1","catalog_lodge_teaser1","catalog_special_txtbg2"]', '["Introducing the Lodge Collection...  Do you appreciate the beauty of wood?  For that ski lodge effect, or to match that open fire... Lodge is the Furni of choice for people with that no frills approach to decorating.","Click on the item you want for more information"," I LOVE this wood Furni!"]'),
	(16, 2, 1010, 1, 1, 1, 0, 'Plasto', 46, 0, 'plasto', '["catalog_plasto_headline1",""]', '["Introducing The Plasto Collection...  Can you feel that 1970s vibe?  Decorate with Plasto and add some colour to your life. Choose a colour that reflect your mood, or just pick your favourite shade.","Select an item and a colour and buy!"]'),
	(17, 2, 1007, 1, 1, 1, 0, 'Pura', 48, 0, 'default_3x3', '["catalog_pura_headline1","catalog_pura_teaser1"]', '["Introducing the Pura Collection...  This collection breathes fresh, clean air and cool tranquillity. Use it to create a special haven away from the hullabaloo of life outside the Hotel.","Click on the item you want for more information",""]'),
	(18, 2, 1005, 1, 1, 1, 0, 'Mode', 39, 0, 'default_3x3', '["catalog_mode_headline1","catalog_mode_teaser1","catalog_special_txtbg2"]', '["Introducing the Mode Collection...  Steely grey functionality combined with sleek designer upholstery. The person that chooses this furniture is a cool urban cat - streetwise, sassy and so slightly untouchable.","Click on the item you want for more information","So shiny and new..."]'),
	(19, 2, 1017, 1, 1, 1, 0, 'Accessories', 11, 0, 'default_3x3', '["catalog_extra_headline1","catalog_extra_teaser1","catalog_special_txtbg2"]', '["Is your room missing something?  Well, now you can add the finishing touches that express your true personality. And don\'t forget, like everything else, these accessories can be moved about to suit your mood.","Click on the item you want for more information","I love my rabbit..."]'),
	(20, 2, 1011, 1, 1, 1, 0, 'Bathroom', 17, 0, 'default_3x3', '["catalog_bath_headline1","catalog_bath_teaser1","catalog_special_txtbg2"]', '["Introducing the Bathroom Collection...  Have some fun with the cheerful bathroom collection. Give yourself and your guests somewhere to freshen up - vital if you want to avoid nasty niffs. Put your loo in a corner though...","Click on the item you want for more information","  Every Habbo needs one!"]'),
	(21, 2, 1012, 1, 1, 1, 0, 'Plants', 45, 0, 'default_3x3', '["catalog_plants_headline1","catalog_plants_teaser1"]', '["Introducing the Plant Collection...  Every room needs a plant! Not only do they bring a bit of the outside inside, they also enhance the air quality! Do we give a fuck? Up to you!","Click on the item you want for more information",""]'),
	(22, 138, 0, 1, 1, 1, 0, 'Sports', 56, 0, 'default_3x3', '["catalog_sports_headline1","catalog_sports_teaser1"]', '["For the sporty people, here is the Sports section! Create your own hockey stadium!","Click on the item you want for more information",""]'),
	(23, 2, 1013, 1, 1, 1, 0, 'Rugs', 52, 0, 'default_3x3', '["catalog_rugs_headline1","catalog_rugs_teaser1","catalog_special_txtbg2"]', '["We have rugs for all occasions. All rugs are non-slip and washable.","Click on the item you want for more information","We have rugs for ANY room!"]'),
	(24, 2, 1014, 1, 1, 1, 0, 'Gallery', 47, 0, 'default_3x3', '["catalog_gallery_headline1","catalog_posters_teaser1","catalog_special_txtbg2"]', '["Adorn your walls with wondrous works of art, posters, plaques and wall hangings. We have items to suit all tastes, from kitsch to cool, traditional to modern.","Click on the item you want for more information","Brighten up your walls!"]'),
	(25, 2, 1015, 1, 1, 1, 0, 'Flags', 55, 0, 'default_3x3', '["catalog_flags_headline1","catalog_flags_teaser1","catalog_special_txtbg2"]', '["If you\'re feeling patriotic, get a flag to prove it. Our finest cloth flags will brighten up the dullest walls.","Click on the item you want for more information","  Flag this     section for later!"]'),
	(26, 2, 1016, 1, 1, 1, 0, 'Trophies', 60, 0, 'trophies', '["catalog_trophies_headline1",""]', '["Reward your friends, or yourself with one of our fabulous glittering array of bronze, silver and gold trophies.\r\nFirst choose the trophy model (click on the arrows to see all the different styles) and then the metal (click on the seal below the trop",""]'),
	(27, 63, 0, 6, 1, 1, 0, 'Club Gifts', 172, 0, 'default_3x3', '["catalog_club_headline1","catalog_hc_teaser"]', '["Welcome to the Club Shop! All \'Habbo Club membership gifts\' are available here, use them wisely you greedy cunt! We have sofas, butlers and all the happy stuff.","Click on the item you want for more information",""]'),
	(28, 62, 0, 6, 1, 1, 0, 'Dragons', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["The Dragon page contains all of the Dragon Lamps.","Click on the item you want for more information",""]'),
	(29, 62, 0, 6, 1, 1, 0, 'Sci-fi Doors', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(30, 62, 0, 6, 1, 1, 0, 'Parasols', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(31, 62, 0, 6, 1, 1, 0, 'Screens', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(32, 62, 0, 6, 1, 1, 0, 'Marquees', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(33, 62, 0, 6, 1, 1, 0, 'Pillows', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(34, 62, 0, 6, 1, 1, 0, 'Icecream', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(35, 62, 0, 6, 1, 1, 0, 'Smoke machines', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(36, 62, 0, 6, 1, 1, 0, 'Sci-Fi Ports', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(37, 62, 0, 6, 1, 1, 0, 'Amber Lamp', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(38, 62, 0, 6, 1, 1, 0, 'Fountains', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(39, 62, 0, 6, 1, 1, 0, 'Elephants', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(40, 62, 0, 6, 1, 1, 0, 'Fans', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(43, 62, 0, 6, 1, 1, 0, 'Inflatable Chairs', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(44, 62, 0, 6, 1, 1, 0, 'Rares Mixed', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(45, 138, 0, 1, 1, 1, 0, 'Executive', 27, 0, 'default_3x3', '["catalog_exe_headline1_en","catalog_exe_teaser_en"]', '["The Executive Furni is ideal for creating a sophisticated working environment, whether it be an office, a mafia headquarters or study!","Click on the item you want for more information",""]'),
	(46, 137, 2001, 1, 1, 1, 0, 'Alhambra', 12, 0, 'soundmachine', '["catalog_alh_headline2","catalog_alh_teaser2","catalog_special_txtbg1"]', '["The Palace of Alhambra has appeared and with it this exotic and beautifully crafted range of Arabian Furni. Luxury seating and gourmet food combine to make your room sparkle with riches.","Click on the item you want for more information","Get your Alhambrian goodies now!"]'),
	(50, -1, 10000, 1, 1, 1, 0, 'Limited Rares', 145, 9, 'cars', '["catalog_limited_headline1","catalog_limited_teaser",""]', '["Here for one week only, it\'s the purple umbrella! Grab it while you can!","",""]'),
	(53, 2, 1002, 1, 1, 1, 0, 'Windows', 63, 0, 'default_3x3', '["ctlg_windows_headline1_en","ctlg_windows_teaser1_en","catalog_special_txtbg2"]', '["Let some sunshine in! Our windows come in many styles to give a unique look to your room. Who said your room can\'t have a view?","Click on the item you want for more information","Ooh, new view!"]'),
	(54, -1, 9000, 1, 0, 1, 0, 'Pets Shop', 8, 2, '', '[]', '[]'),
	(55, 54, 0, 1, 1, 1, 0, 'Pets Info', 42, 0, 'pets2', '["catalog_pet_headline1","ctlg_pet_note"]', '["Pets are inhabitants of Habbo Hotel too, so each pet owner needs to know a bit about them. Look after your pet by looking through our key points below.","A few things you should know:","- You can only put it down in one of your rooms.\\r\\n- There can be 3 pets in each room.\\r\\n- The basket is your pet\'s home. If you pick it up, the pet will go back in your hand.\\r\\n- Every time you visit this page, you will go back to a different selection of pets.\\r\\n- Your pet will sleep more when it gets old\\r\\n- You cannot trade your pet"]'),
	(56, -1, 4000, 1, 0, 1, 0, 'Duckets Shop', 178, 8, '', '[]', '[]'),
	(57, 56, 0, 1, 1, 1, 0, 'Hello Furni', 35, 0, 'pixelrent', '["catalog_hello_header1_en","catalog_hello_teaser1_en"]', '["Hello Furni is available with Pixels and is perfect if you are decorating your room for the very first time. The Furni is yours to keep and therefore cannot be traded.","Click on the item you want for more information"," "]'),
	(58, -1, 7000, 1, 0, 1, 0, 'Ecotron', 7, 3, '', '[]', '[]'),
	(59, 138, 0, 1, 1, 1, 0, 'Country', 21, 0, 'default_3x3', '["catalog_country_header1_en","catalog_country_teaser1_en","catalog_special_txtbg2"]', '["Let\'s leave the busy city streets and head over to the wide abyss of golden wheat, emerald fields and home grown, organic vegetables. Everything you need to create a farm!","Click on the item you want for more information","Who\'d be a crow, eh?"]'),
	(60, 56, 0, 1, 1, 1, 0, 'Special Effects', 61, 0, 'pixeleffects', '["catalog_pixeleffects_headline1_en","catalog_pxl_teaser1_en"]', '["Tune your character with cool effects that fit the occasion. Do you want to fly away with the red carpet or be in the spotlight? Now is your chance!\\r\\n\\r\\nThe effects can be activated in your badge dialog under effects tab.",""," "]'),
	(61, 56, 0, 1, 1, 1, 0, 'Automobile', 16, 0, 'cars', '["catalog_automobile_header1_en","catalog_automobile_teaser1_en"]', '["Every Habbo needs a car effect! Not only do they bring a bit of the outside inside, they also enhance the air quality! And what better gift for a friend than a beautiful traffic sign or elegant pile of tires...","Click on the item you want for more information."," "]'),
	(62, -1, 16000, 6, 0, 1, 0, 'Admin Shop', 1, 6, '', '[]', '[]'),
	(63, -1, 5000, 1, 1, 1, 0, 'Habbo Club', 172, 0, 'club1', '["catalog_club_headline1","clubcat_pic"]', '["Welcome to Habbo Club - the members only club that all the best Habbos belong to!","Every member of Habbo Club gets priority access to the hotel. So, if the hotel\'s full up, you\'ll get to the front of the queue automatically - no waiting around! And you\'ll get exclusive clothes, hair colours, furni, special guest room layouts and more besides. Normal Habbos will not have any of these.","How do I join? Use the Navigator to go to \'Hotel View\' and click on the Habbo Club icon. Habbo Club costs 20 Credits a month. We\'ll remind you when your membership is about to run out.","Well, what are you waiting for? Join Habbo Club today!"]'),
	(64, 63, 0, 1, 1, 1, 1, 'Club Shop', 172, 0, 'default_3x3', '["catalog_club_headline1","catalog_hc_teaser","catalog_special_txtbg1"]', '["NEW Habbo Club Furni range. Allow these elegant delights to make your room sophisticated and humble. They look great placed with your monthly gifts!","Click on the item you want for more information","For Habbo Club members only!"]'),
	(65, 63, 0, 1, 1, 1, 0, 'Club Info', 172, 0, 'club2', '["catalog_club_headline1","club_pos","","club_neg"]', '["What happens when my Habbo Club runs out?","If your Habbo Club runs out, you WILL be able to keep any rooms you made with a Club layout and the Habbo Club Furni is yours to keep.","If your Habbo Club runs out you WON\'T be able to wander around with a cool HC badge, the funky clothes and your hair will vanish from your Habbo, you won\'t be able to do chose HC rooms layouts in the Room-O-Matics, you\'ll receive no new HC Furni and worst all, you won\'t be able to jump the queue if the Hotel\'s Full!","Stay in Habbo Club for more than a year and you\'ll get a special sparkly BADGE!"]'),
	(66, 138, 0, 1, 1, 1, 0, 'Glass', 29, 0, 'default_3x3', '["catalog_glass_headline1","catalog_glass_teaser1"]', '["You can really open up a space with this stylish glass furniture, just don\'t walk into it!","Click on the item you want for more information",""]'),
	(67, 137, 2006, 1, 1, 1, 0, 'Greek', 31, 0, 'default_3x3', '["catalog_greek_header1","catalog_greek_teaser1"]', '["Be transported back to ancient Greece with a couple of thousand pounds and British Airways. Until then, build your own panthenon with our realist Greek range!","Click on the item you want for more information",""]'),
	(68, 138, 0, 1, 1, 1, 0, 'Romantique', 50, 0, 'default_3x3', '["catalog_romantique_headline1","catalog_romantique_teaser1"]', '["The Romantique range features Grand Pianos, old antique lamps and tables. It is the ideal range for setting a warm and loving mood in your room. Spruce up your room and invite that special someone over. Now featuring the extra special COLOUR edition.","Click on the item you want for more information",""]'),
	(69, 138, 0, 1, 1, 1, 0, 'Arctic', 13, 0, 'soundmachine', '["catalog_arc_header1_en","catalog_arc_teaser1_en"]', '["Stay cool (or warm with our campfire!) and create your own Winter Wonderland or Humble Homeland for your penguins.","Click on the item you want for more information",""]'),
	(70, 137, 2003, 1, 1, 1, 0, 'Bensalem', 18, 0, 'default_3x3', '["catalog_header_bensalem","catalog_teaser_bensalem"]', '["The Lost City of Bensalem has been located beneath the sea. We have worked hard to salvage all kinds of fantastic furni, which is now all available below.","Click on the item you want for more information",""]'),
	(71, 138, 0, 1, 1, 1, 0, 'Neon', 41, 0, 'default_3x3', '["catalog_neon_header1_en","catalog_neon_teaser1_en"]', '["New years eve, birthdays and every other day of the year, there\'s always an excuse for a party! So, why don\'t you buy some Neon furni!?","Click on the item you want for more information",""]'),
	(72, 137, 2005, 1, 1, 1, 0, 'Lost Tribe', 38, 0, 'default_3x3', '["catalog_header_lost_tribe","LT_teaser_en"]', '["Start your own tribal village with our ancient furniture, all carved from hard wearing stone. NOTE: Lava is hot, get an adult to help you.","Click on the item you want for more information",""]'),
	(73, 138, 0, 1, 1, 1, 0, 'Virus', 42, 0, 'default_3x3', '["catalog_vir_header1_en","catalog_vir_teaser_en","catalog_special_txtbg1"]', '["A virus is spreading through Habbo Hotel. Many casualties reported and it could get much worse! Whether you are hoping to help infected Habbos or look after number one, get your clean hands on our terrifying Infection Furni.","Click on the item you want for more information","Latest virus news on TV now!"]'),
	(74, 58, 0, 1, 1, 1, 0, 'Ecotron', 7, 3, 'recycler', '["catalog_recycler_headline3_en"]', '["Become an Eco-warrior\\\\r\\\\nRecycle your worthless stuff and be rewarded with a random prize. Check out the prizes and the instructions for recycling.\\\\r\\\\nDrag 5 items to the boxes below and click recycle!"]'),
	(75, 58, 0, 1, 1, 1, 0, 'Rewards', 26, 1, 'recycler_prizes', '["catalog_recycler_headline3_en","",""]', '["What are the prizes? Ecotron box may contain one of these:","","What are the prizes? Ecotron box may contain one of these:"]'),
	(76, 58, 0, 6, 1, 1, 0, 'Ecotron Shop', 7, 0, 'default_3x3', '["catalog_recycler_headline4_en",""]', '["Yet another Ecotron page!","",""]'),
	(77, 58, 0, 1, 1, 1, 0, 'Instructions', 42, 1, 'pets2', '["catalog_recycler_headline5_en","ctlg_ecotron_box2"]', '["The Ecotron is a furni recycler. Get rid of old furni... what will you get in return? It\'s a surprise! Become a Habbo eco-warrior. No refunds!","How to use the Ecotron?","1. Just drag 5 items from your hand to the Ecotron. One item / square. Recyclable items are marked in your inventory with an image. When you have 5 items in the boxes, click the \\"Recycle\\" button. You can now find the Ecotron prize box from your hand.\\r\\n\\r\\n2. Click the box to see its tag. Open the box, or trade it unopened. The timer shows you how long you have to wait before you can recycle more items. Check the prizes before you recycle- don\'t be surprised by the surprise!"]'),
	(78, 138, 0, 1, 1, 1, 0, 'Kitchen', 217, 1, 'default_3x3', '["catalog_header_kitchen","catalog_teaser_kitchen"]', '["Create your dream kitchen with this exquisite range of matured pine and marble furniture.","Click on the item you want for more information",""]'),
	(79, 138, 0, 1, 1, 1, 0, 'Christmas 09', 64, 0, 'default_3x3', '["catalog_xmas_headline1","catalog_xmas_teaser"]', '["Get yourself into the Christmas spirit with our selection of festive furni! From baubles to reindeer poo, we\'ve got it all!","Click on the item you want for more information",""]'),
	(80, 138, 0, 1, 1, 1, 0, 'Urban', 26, 0, 'default_3x3', '["urban_header_en","urban_teaser_en"]', '["New York City styled furni range, Urban is perfect for any street, alleyway or road. Rubbish bins, street lights and benches, all the Urban furniture you need!","Click on the item you want for more information",""]'),
	(81, 138, 0, 1, 1, 1, 0, 'Grunge', 32, 0, 'default_3x3', '["catalog_grunge_headline1","catalog_grunge_teaser"]', '["The Grunge range will get your bedroom looking just the way you like it - organised, neat and tidy!","Click on the item you want for more information",""]'),
	(82, 137, 2005, 1, 1, 1, 0, 'Shalimar', 54, 0, 'default_3x3', '["catalog_shal_header1_en","catalog_shal_teaser_en"]', '["Everyone loves Bollywood! Watch out for rose petals!","Click on the item you want for more information",""]'),
	(84, 11, 0, 1, 1, 1, 0, 'How to make music?', 0, 0, 'default_3x3', '["catalog_djshop_headline1","catalog_djshop_teaser1"]', '["You must own a trax machine and at least one trax pax. Place the trax machine in your room and the trax pax in your hand. Double click the trax machine, click \\"Trax editor\\" and start editing music. When ready, save, select the tune, turn on the trax machine and enjoy.","Cool, my own music!",""]'),
	(85, 11, 0, 1, 1, 1, 0, 'Ambient', 0, 0, 'soundmachine', '["catalog_trx_header1_en","catalog_trx_teaser1"]', '["Welcome to the Ambient Trax Store! With groovy beats and chilled out melodies, this is the section for some cool and relaxing tunes.",""]'),
	(86, 11, 0, 1, 1, 1, 0, 'Dance', 0, 0, 'soundmachine', '["catalog_trx_header2_en","catalog_trx_teaser2"]', '["Welcome to the Dance Trax Store! With funky beats and catchy melodies, this is the section for every clubber  to indulge in.",""]'),
	(87, 11, 0, 1, 1, 1, 0, 'Rock', 0, 0, 'soundmachine', '["catalog_trx_header3_en","catalog_trx_teaser3"]', '["Welcome to the Rock Trax Store! With heavy beats and rockin\' riffs, this is the section for every rock fan to experiment with.",""]'),
	(88, 11, 0, 1, 1, 1, 0, 'SFX', 0, 0, 'soundmachine', '["catalog_trx_header4_en","catalog_trx_teaser4"]', '["Welcome to the SFX Trax Store! With crazy sounds and weird noises, this is the section for every creative room builder  to indulge in.",""]'),
	(89, 11, 0, 1, 1, 1, 0, 'Urban', 26, 0, 'soundmachine', '["catalog_trx_header5_en","catalog_trx_teaser5"]', '["Welcome to the Urban Trax Store! With hip hop beats and RnB vocals, this is the section for every city bopper  to indulge in.",""]'),
	(90, 138, 0, 1, 1, 1, 0, 'Science Fiction', 53, 0, 'default_3x3', '["sf_header_en","sf_teaser_en"]', '["Blipblop blip blip blip.. Oooh.. what\\\'s this button do?.. You can find out exactly what it does with our new Scifi range, batteries included!","Click on the item you want for more information",""]'),
	(91, 138, 0, 1, 1, 1, 0, 'American Idol', 92, 0, 'default_3x3', '["catalog_idol_header1","catalog_idol_teaser1"]', '["Create your own American Idol world in Habbo with this exclusive AI furniture set.","Click on the item you want for more information",""]'),
	(92, 56, 0, 1, 1, 1, 0, 'Rentals', 44, 0, 'pixelrent', '["catalog_pixelrentals_header_en","catalog_pxl_teaser3_en"]', '["Crate a cool room, with these rocking room effects you can expand your friends experience.","",""]'),
	(93, 56, 0, 1, 1, 1, 0, 'Pixel Collectable', 44, 0, 'cars', '["catalog_pixeldeals_headline1_en","catalog_pxl_teaser2_en"]', '["The Pixel Collectable is the ultimate collectors item requiring a mammoth 2000 pixels to purchase.","",""]'),
	(94, 62, 0, 6, 1, 1, 0, 'Pixel Collectables', 0, 0, 'cars', '["catalog_pixeldeals_headline1_en","catalog_pxl_teaser2_en"]', '["The admin page for all the pixel collectables.","",""]'),
	(95, 63, 0, 1, 1, 1, 1, 'One Way Gates', 0, 0, 'cars', '["catalog_onewaygates_en",""]', '["As a token of gratitude for Habbo Club members, you can now purchase One Way Gates without having to wait for them to appear in the catalogue!","Click on the item you want for more information",""]'),
	(96, 137, 2007, 1, 1, 1, 0, 'Tiki', 59, 0, 'soundmachine', '["catalog_tiki_header1_en","tiki_teaser"]', '["Go a little bit exotic with your food choices with these items from our much-loved Tiki range!","Click on the item you want for more information",""]'),
	(97, 138, 0, 1, 1, 1, 0, 'Twilight', 64, 0, 'default_3x3', '["catalog_twilight_header_en","catalog_teaser_twilight"]', '["The Twilight Saga - New Moon is here! To celebrate the arrival of the Cullens, some special furniture has been made so you can create your own Twilight rooms.","Click on the item you want for more information",""]'),
	(98, 138, 0, 1, 1, 1, 0, 'Habbowood', 33, 0, 'default_3x3', '["ctlg_habbowood_headline1_en","ctlg_habbowood_teaser1_en"]', '["Presenting the all new Habbowood Furni range! Whether it\'s a boulevard of stars, a cinema, a theatre, a dressing room or an entire film studio - the Habbowood Furni ticks all the stage exit right boxes!","Click on the item you want for more information",""]'),
	(99, 138, 0, 1, 1, 1, 0, 'Love', 62, 0, 'default_3x3', '["catalog_love_headline1","catalog_love_teaser1"]', '["It is Valentine\'s Day and time to express your love and affection for your friends. Go wild and leave anonymous Heart Stickies all over the hotel!","Click on the item you want for more information",""]'),
	(100, 138, 0, 1, 1, 1, 0, 'Valentines', 0, 0, 'default_3x3', '["catalog_va2_headline1_en","catalog_va2_teaser_en","catalog_special_txtbg2"]', '["Valentine\'s Love Furni will set the right mood in your room this week. Avaliable until Monday so don\'t miss out on the Heart Sofa - one of the most popular items in Habbo - and Heart Stickies","Mood Light - Turn the lights down low this Valentine\'s","I prefer White and Purple Roses             :o"]'),
	(101, 138, 0, 1, 1, 1, 0, 'Habboween', 34, 0, 'default_3x3', '["catalog_halloween_headline1","catalog_halloween_teaser","catalog_special_txtbg2"]', '["Yes, it\'\'s a spookfest! Get your boney hands on our creepy collection of ghoulish goodies. But be quick - they\'\'\'ll be gone from the Catalogue after two weeks!","Click an item for more information","Halloween is My day!"]'),
	(102, 138, 0, 1, 1, 1, 0, 'Relax', 49, 0, 'default_3x3', '["catalog_relax_headline1_en","catalog_relax_teaser1_en",""]', '["Relax after a busy day in the Welcome Lounge. Light a few candles, and chill out with a good read in a wicker chair. We understand the needs of a Habbo with a hectic lifestyle!","Click an item for more information",""]'),
	(103, 137, 2004, 1, 1, 1, 0, 'Japan', 36, 0, 'default_3x3', '["catalog_jap_headline2_en","catalog_jap_teaser3_en",""]', '["We have sushi, tatami and katana\\\'s! I have no idea what the difference is, but I sure know its Japanese! Fulfil your fantasies and buy some today!","Click an item for more information",""]'),
	(104, 138, 0, 1, 1, 1, 0, 'Haunted House', 34, 0, 'default_3x3', '["catalog_halloween_headline2","catalog_halloween_teaser2_en",""]', '["The creepy house on top of the hill has swung open its haunted doors to let you inside. With creaky floors and even creakier doors, you better watch your step in this eerie haunted mansion.","Click an item for more information",""]'),
	(105, 138, 0, 1, 1, 1, 0, 'Igor', 239, 0, 'default_3x3', '["catalog_igor_headine2_en","catalog_igor_teaser1_en",""]', '["Igor\'s back and he means business. Celebrating the release of IGOR on DVD, he\'s Introducing FOUR new additions to the IGOR furni line. These include a Flask, Science Desk, Wall Poster and Evil Bone!","Click an item for more information",""]'),
	(106, 138, 0, 1, 1, 1, 0, 'Spiderwick', 0, 0, 'default_3x3', '["catalog_spw_header1_en","catalog_spw_teaser2_en",""]', '["The Spiderwick Exhibition has arrived at the \\"Museum of Invention\\" in Habbo Hotel. Grab yourself a limited edition souvenir item of Furni below before it\'s too late!","Click an item for more information",""]'),
	(107, 138, 0, 1, 1, 1, 0, 'Summer', 57, 0, 'default_3x3', '["catalog_sum_headline1_en","catalog_sum_teaser1_en",""]', '["Phwoar! Start up the barbie! This range has everything you need for the perfect summer garden!","Click an item for more information",""]'),
	(108, 2, 1003, 1, 1, 1, 0, 'Moodlights', 40, 0, 'default_3x3', '["catalog_dimmers_header1_en","catalog_dimmer_teaser_en",""]', '["Our range of moodlights allow you to control the atmosphere and transform your room in just a click. What will your room look like? Click the switch and find out now!","Click an item for more information",""]'),
	(109, 62, 0, 6, 1, 1, 0, 'Various Ads', 0, 0, 'default_3x3', '["catalog_rares_headline1","",""]', '["Miscelaneous advertisement furniture","Click an item for more information",""]'),
	(110, 138, 0, 1, 1, 1, 0, 'Memorial', 0, 0, 'default_3x3', '["catalog_limited_headline1_en","catalog_limited_teaser_en","catalog_special_txtbg2"]', '["Available this week only and NEVER to be sold again, special Memorial Furni. As we have a fond farewell to Old Habbo and welcome New Habbo, bag yourself a highly collectible momento.","Click an item for more details","Habbo Memorial"]'),
	(111, 62, 0, 6, 1, 1, 0, 'Extra Rollers', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(112, 138, 0, 1, 1, 1, 0, 'Diner', 51, 0, 'default_3x3', '["catalog_diner_header_en","catalog_diner_teaser_en",""]', '["Get cookin\' with Diner Furni! Serve up your eggs \'n\' grits on new Yellow or Aquamarine Diner Furni, avaliable for a limited time only!","Click an item for more information",""]'),
	(113, 62, 0, 6, 1, 1, 0, 'Sleeping Bags', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(114, 62, 0, 6, 1, 1, 0, 'Pillars', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(116, 62, 0, 6, 1, 1, 0, 'Penguins', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(117, 138, 0, 1, 1, 1, 0, 'Christmas 06', 64, 0, 'default_3x3', '["catalog_xmas_headline1","catalog_xmas_teaser"]', '["Get yourself into the Christmas spirit with our selection of festive furni! From baubles to reindeer poo, we\'ve got it all!","Click on the item you want for more information",""]'),
	(118, 62, 0, 6, 1, 1, 0, 'Various Misc', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(119, 138, 0, 1, 1, 1, 0, 'Christmas 07', 64, 0, 'default_3x3', '["catalog_xmas_headline1","catalog_xmas_teaser"]', '["Get yourself into the Christmas spirit with our selection of festive furni! From baubles to reindeer poo, we\'ve got it all!","Click on the item you want for more information",""]'),
	(120, 62, 0, 6, 1, 1, 0, 'Recycler (Old)', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(121, 62, 0, 6, 1, 1, 0, 'Super Rares', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(122, 62, 0, 6, 1, 1, 0, 'Expensive Rares', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(123, 124, 0, 6, 1, 1, 0, 'Child Line', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(124, -1, 15000, 6, 0, 1, 0, 'Advertisements', 6, 3, '', '[]', '[]'),
	(125, 124, 0, 6, 1, 1, 0, 'Calippo', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(126, 124, 0, 6, 1, 1, 0, 'Habbo Mall', 7165, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(127, 62, 0, 6, 1, 1, 0, 'StrayPixels', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(128, 62, 0, 6, 1, 1, 0, 'Super Trophies', 0, 0, 'trophies', '["catalog_trophies_headline1",""]', '["Reward your friends, or yourself with one of our fabulous glittering array of bronze, silver and gold trophies.\r\nFirst choose the trophy model (click on the arrows to see all the different styles) and then the metal (click on the seal below the trop",""]'),
	(129, 138, 0, 1, 1, 1, 0, 'Waasa', 103, 1, 'default_3x3', '["waasa_catalogue_header","waasa_teaser"]', '["Waasa that? With Bunk Beds, Wooden Desks, Computer Chairs and Sailing Ships there is everything you need to take your study room to the next sophisticated level.","Click on the item you want for more information",""]'),
	(130, 138, 0, 1, 1, 1, 0, 'Coco', 127, 0, 'default_3x3', '["catalog_coco2","coco_teaser_thumb2"]', '["The Coco range will send you coconuts with the ultimate in resort relaxation! Laze by the pool and unwind with earthy tones and smooth edges.","Click on the item you want for more information",""]'),
	(131, 138, 0, 1, 1, 1, 0, 'Bling', 42, 0, 'default_3x3', '["catalog_header_bling_en","catalog_teaser_bling11"]', '["Bling things up with some serious glitz and glamour! We hope you brought your lucky charm, cause you might just need it...","Click on the item you want for more information",""]'),
	(132, 62, 0, 6, 1, 1, 0, 'Premium Rares', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(133, 124, 0, 6, 1, 1, 0, 'Mountain Dew', 0, 0, 'default_3x3', '["catalog_rares_headline1",""]', '["Yet another rares page.","Click on the item you want for more information",""]'),
	(134, 138, 0, 6, 1, 1, 0, 'Flower Power', 73, 0, 'default_3x3', '["catalog_header_bling_en","catalog_teaser_bling11"]', '["Bling things up with some serious glitz and glamour! We hope you brought your lucky charm, cause you might just need it...","Click on the item you want for more information",""]'),
	(135, 63, 0, 1, 1, 1, 1, 'VIP Shop', 0, 0, 'default_3x3', '["ctlg_buy_vip_header","vippremium2",""]', '["NEW VIP Furni range. Allow these elegant delights to make your room sophisticated and humble. They look great placed with your monthly gifts!","Click on the item you want for more information",""]'),
	(136, 138, 0, 1, 1, 1, 0, 'Easter', 25, 3, 'default_3x3', '["catalog_easter_headline1","catalog_easter_teaser1","catalog_special_txtbg2"]', '["\'Egg\'cellent furni - Bouncing bunnies, fluffy chicks, choccy eggs... Yep, it\'s Easter!\\rCelebrate with something \'eggs\'tra special from our Easter range. But hurry - it\'s not here for long this year!","Click on the item you want for more information","\'Egg\'-Tastic!"]'),
	(137, -1, 2000, 1, 0, 1, 0, 'Furni By Line', 121, 2, '', '[]', '[]'),
	(138, -1, 3000, 1, 0, 1, 0, 'Furni By Theme', 64, 2, '', '[]', '[]');
/*!40000 ALTER TABLE `catalogue_pages` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` int(11) NOT NULL,
  `label` varchar(100) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_category` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_chat_history` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL,
  `message` text NOT NULL DEFAULT '',
  `has_read` tinyint(1) NOT NULL,
  `messaged_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_chat_history` DISABLE KEYS */;
INSERT INTO `messenger_chat_history` (`user_id`, `friend_id`, `message`, `has_read`, `messaged_at`) VALUES
	(1, 2, 'test', 1, '2020-04-13 14:45:21'),
	(2, 1, 'hey bitch lol', 1, '2020-04-13 14:45:25'),
	(2, 1, 'sup bish', 1, '2020-04-13 23:19:57');
/*!40000 ALTER TABLE `messenger_chat_history` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_friend` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_friend` DISABLE KEYS */;
INSERT INTO `messenger_friend` (`user_id`, `friend_id`) VALUES
	(1, 3),
	(3, 1),
	(1, 4),
	(4, 1),
	(1, 2),
	(2, 1);
/*!40000 ALTER TABLE `messenger_friend` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `messenger_request` (
  `user_id` int(11) NOT NULL,
  `friend_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `messenger_request` DISABLE KEYS */;
/*!40000 ALTER TABLE `messenger_request` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `navigator_official_rooms` (
  `banner_id` int(11) NOT NULL AUTO_INCREMENT,
  `parent_id` int(11) NOT NULL,
  `banner_type` enum('NONE','TAG','FLAT','PUBLIC_FLAT','CATEGORY') NOT NULL,
  `room_id` int(11) NOT NULL,
  `image_type` enum('INTERNAL','EXTERNAL') NOT NULL DEFAULT 'INTERNAL',
  `label` text NOT NULL DEFAULT '',
  `description` text NOT NULL DEFAULT '',
  `description_entry` tinyint(1) NOT NULL DEFAULT 0,
  `image_url` text NOT NULL,
  PRIMARY KEY (`banner_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `navigator_official_rooms` DISABLE KEYS */;
INSERT INTO `navigator_official_rooms` (`banner_id`, `parent_id`, `banner_type`, `room_id`, `image_type`, `label`, `description`, `description_entry`, `image_url`) VALUES
	(1, 0, 'CATEGORY', 0, 'EXTERNAL', 'Classic Rooms', '', 0, 'officialrooms_hq/alhambra_official_rooms.gif'),
	(2, 1, 'PUBLIC_FLAT', 1, 'EXTERNAL', 'Welcome Lounge', 'welcome_lounge', 0, 'officialrooms_defaults/hh_room_nlobby.png');
/*!40000 ALTER TABLE `navigator_official_rooms` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room` (
  `id` int(100) NOT NULL AUTO_INCREMENT,
  `owner_id` int(11) NOT NULL,
  `name` mediumtext NOT NULL,
  `description` mediumtext NOT NULL,
  `category_id` int(11) NOT NULL DEFAULT 14,
  `visitors_now` int(11) NOT NULL DEFAULT 0,
  `visitors_max` int(11) NOT NULL DEFAULT 25,
  `status` enum('OPEN','CLOSED','PASSWORD') NOT NULL DEFAULT 'OPEN',
  `password` text NOT NULL DEFAULT '',
  `model` varchar(30) NOT NULL DEFAULT '',
  `ccts` text NOT NULL DEFAULT '',
  `wallpaper` int(11) NOT NULL DEFAULT 0,
  `floor` int(11) NOT NULL DEFAULT 0,
  `landscape` int(11) NOT NULL DEFAULT 0,
  `rating` int(11) NOT NULL DEFAULT 0,
  `allow_pets` tinyint(1) NOT NULL DEFAULT 1,
  `allow_pets_eat` tinyint(1) NOT NULL DEFAULT 1,
  `allow_walkthrough` tinyint(1) NOT NULL DEFAULT 0,
  `hidewall` tinyint(1) NOT NULL DEFAULT 0,
  `wall_thickness` tinyint(3) NOT NULL DEFAULT 0,
  `floor_thickness` tinyint(3) NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room` (`id`, `owner_id`, `name`, `description`, `category_id`, `visitors_now`, `visitors_max`, `status`, `password`, `model`, `ccts`, `wallpaper`, `floor`, `landscape`, `rating`, `allow_pets`, `allow_pets_eat`, `allow_walkthrough`, `hidewall`, `wall_thickness`, `floor_thickness`) VALUES
	(1, 0, 'Welcome Lobby', 'welcome_lounge', 15, 0, 25, 'OPEN', '', 'newbie_lobby', 'hh_room_nlobby', 0, 0, 0, 0, 1, 1, 1, 0, 0, 0),
	(2, 1, 'test creation lolz', '', 6, 0, 25, 'OPEN', '', 'model_t', '', 0, 0, 0, 0, 1, 1, 0, 0, 0, 0);
/*!40000 ALTER TABLE `room` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room_category` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `caption` varchar(100) NOT NULL,
  `enabled` tinyint(1) NOT NULL DEFAULT 1,
  `min_rank` int(11) NOT NULL DEFAULT 1,
  `trading_allowed` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room_category` DISABLE KEYS */;
INSERT INTO `room_category` (`id`, `caption`, `enabled`, `min_rank`, `trading_allowed`) VALUES
	(1, 'Staff Rooms', 1, 4, 1),
	(2, 'Competition Category', 1, 1, 1),
	(3, 'unused competition category', 1, 1, 1),
	(4, 'Themed & RPG Rooms', 1, 1, 0),
	(5, 'Restaurant, Bar & Night Club Rooms', 1, 1, 0),
	(6, 'Club & Group Rooms', 1, 1, 1),
	(7, 'Chat, Chill & Discussion Rooms', 1, 1, 1),
	(8, 'Maze & Theme Park Rooms', 1, 1, 1),
	(9, 'Trading & Shopping Rooms', 1, 1, 1),
	(10, 'Gaming & Race Rooms', 1, 1, 1),
	(11, 'Hair Salons & Modelling Rooms', 1, 1, 0),
	(12, 'Help Centre, Guide & Service Rooms', 1, 1, 0),
	(13, 'School, Daycare & Adoption Rooms', 1, 1, 0),
	(14, 'All Other Rooms', 1, 1, 1),
	(15, 'No Category', 1, 1, 1);
/*!40000 ALTER TABLE `room_category` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `room_models` (
  `model` varchar(100) NOT NULL,
  `door_x` int(11) NOT NULL,
  `door_y` int(11) NOT NULL,
  `door_z` double NOT NULL,
  `door_dir` int(4) NOT NULL DEFAULT 2,
  `heightmap` text NOT NULL,
  `club_only` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`model`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `room_models` DISABLE KEYS */;
INSERT INTO `room_models` (`model`, `door_x`, `door_y`, `door_z`, `door_dir`, `heightmap`, `club_only`) VALUES
	('model_a', 3, 5, 0, 2, 'xxxxxxxxxxxx|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxx00000000|xxxxxxxxxxxx|xxxxxxxxxxxx', 0),
	('model_b', 0, 5, 0, 2, 'xxxxxxxxxxxx|xxxxx0000000|xxxxx0000000|xxxxx0000000|xxxxx0000000|x00000000000|x00000000000|x00000000000|x00000000000|x00000000000|x00000000000|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 0),
	('model_c', 4, 7, 0, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 0),
	('model_d', 4, 7, 0, 2, 'xxxxxxxxxxxx|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxx000000x|xxxxxxxxxxxx', 0),
	('model_e', 1, 5, 0, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xx0000000000|xx0000000000|xx0000000000|xx0000000000|xx0000000000|xx0000000000|xx0000000000|xx0000000000|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 0),
	('model_f', 2, 5, 0, 2, 'xxxxxxxxxxxx|xxxxxxx0000x|xxxxxxx0000x|xxx00000000x|xxx00000000x|xxx00000000x|xxx00000000x|x0000000000x|x0000000000x|x0000000000x|x0000000000x|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 0),
	('model_g', 1, 7, 1, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxx00000|xxxxxxx00000|xxxxxxx00000|xx1111000000|xx1111000000|xx1111000000|xx1111000000|xx1111000000|xxxxxxx00000|xxxxxxx00000|xxxxxxx00000|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 1),
	('model_h', 4, 4, 1, 2, 'xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxx111111x|xxxxx111111x|xxxxx111111x|xxxxx111111x|xxxxx111111x|xxxxx000000x|xxxxx000000x|xxx00000000x|xxx00000000x|xxx00000000x|xxx00000000x|xxxxxxxxxxxx|xxxxxxxxxxxx|xxxxxxxxxxxx', 1),
	('model_i', 0, 10, 0, 2, 'xxxxxxxxxxxxxxxxx|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|x0000000000000000|xxxxxxxxxxxxxxxxx', 0),
	('model_j', 0, 10, 0, 2, 'xxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxx0000000000|xxxxxxxxxxx0000000000|xxxxxxxxxxx0000000000|xxxxxxxxxxx0000000000|xxxxxxxxxxx0000000000|xxxxxxxxxxx0000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x0000000000xxxxxxxxxx|x0000000000xxxxxxxxxx|x0000000000xxxxxxxxxx|x0000000000xxxxxxxxxx|x0000000000xxxxxxxxxx|x0000000000xxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxx', 0),
	('model_k', 0, 13, 0, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxxxxxx00000000|xxxxxxxxxxxxxxxxx00000000|xxxxxxxxxxxxxxxxx00000000|xxxxxxxxxxxxxxxxx00000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|x000000000000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxxxxxxxxxxxxxxxxxx', 0),
	('model_l', 0, 16, 0, 2, 'xxxxxxxxxxxxxxxxxxxxx|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|x00000000xxxx00000000|xxxxxxxxxxxxxxxxxxxxx', 0),
	('model_m', 0, 15, 0, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|x0000000000000000000000000000|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxx00000000xxxxxxxxxx|xxxxxxxxxxxxxxxxxxxxxxxxxxxxx', 0),
	('model_n', 0, 16, 0, 2, 'xxxxxxxxxxxxxxxxxxxxx|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x000000xxxxxxxx000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000x000000x000000|x000000xxxxxxxx000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|x00000000000000000000|xxxxxxxxxxxxxxxxxxxxx', 0),
	('model_o', 0, 18, 1, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx11111111xxxx|xxxxxxxxxxxxx00000000xxxx|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|x111111100000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxx0000000000000000|xxxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('model_p', 0, 23, 2, 2, 'xxxxxxxxxxxxxxxxxxx|xxxxxxx222222222222|xxxxxxx222222222222|xxxxxxx222222222222|xxxxxxx222222222222|xxxxxxx222222222222|xxxxxxx222222222222|xxxxxxx22222222xxxx|xxxxxxx11111111xxxx|x222221111111111111|x222221111111111111|x222221111111111111|x222221111111111111|x222221111111111111|x222221111111111111|x222221111111111111|x222221111111111111|x2222xx11111111xxxx|x2222xx00000000xxxx|x2222xx000000000000|x2222xx000000000000|x2222xx000000000000|x2222xx000000000000|22222xx000000000000|x2222xx000000000000|xxxxxxxxxxxxxxxxxxx', 1),
	('model_q', 10, 4, 2, 2, 'xxxxxxxxxxxxxxxxxxx|xxxxxxxxxxx22222222|xxxxxxxxxxx22222222|xxxxxxxxxxx22222222|xxxxxxxxxxx22222222|xxxxxxxxxxx22222222|xxxxxxxxxxx22222222|x222222222222222222|x222222222222222222|x222222222222222222|x222222222222222222|x222222222222222222|x222222222222222222|x2222xxxxxxxxxxxxxx|x2222xxxxxxxxxxxxxx|x2222211111xx000000|x222221111110000000|x222221111110000000|x2222211111xx000000|xx22xxx1111xxxxxxxx|xx11xxx1111xxxxxxxx|x1111xx1111xx000000|x1111xx111110000000|x1111xx111110000000|x1111xx1111xx000000|xxxxxxxxxxxxxxxxxxx', 1),
	('model_r', 10, 4, 3, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxx33333333333333|xxxxxxxxxxx33333333333333|xxxxxxxxxxx33333333333333|xxxxxxxxxx333333333333333|xxxxxxxxxxx33333333333333|xxxxxxxxxxx33333333333333|xxxxxxx333333333333333333|xxxxxxx333333333333333333|xxxxxxx333333333333333333|xxxxxxx333333333333333333|xxxxxxx333333333333333333|xxxxxxx333333333333333333|x4444433333xxxxxxxxxxxxxx|x4444433333xxxxxxxxxxxxxx|x44444333333222xx000000xx|x44444333333222xx000000xx|xxx44xxxxxxxx22xx000000xx|xxx33xxxxxxxx11xx000000xx|xxx33322222211110000000xx|xxx33322222211110000000xx|xxxxxxxxxxxxxxxxx000000xx|xxxxxxxxxxxxxxxxx000000xx|xxxxxxxxxxxxxxxxx000000xx|xxxxxxxxxxxxxxxxx000000xx|xxxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('model_t', 0, 3, 2, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxx|x222222222222222222222222222x|x222222222222222222222222222x|2222222222222222222222222222x|x222222222222222222222222222x|x2222xxxxxx222222xxxxxxx2222x|x2222xxxxxx111111xxxxxxx2222x|x2222xx111111111111111xx2222x|x2222xx111111111111111xx2222x|x2222xx11xxx1111xxxx11xx2222x|x2222xx11xxx0000xxxx11xx2222x|x22222111x00000000xx11xx2222x|x22222111x00000000xx11xx2222x|x22222111x00000000xx11xx2222x|x22222111x00000000xx11xx2222x|x22222111x00000000xx11xx2222x|x22222111x00000000xx11xx2222x|x2222xx11xxxxxxxxxxx11xx2222x|x2222xx11xxxxxxxxxxx11xx2222x|x2222xx111111111111111xx2222x|x2222xx111111111111111xx2222x|x2222xxxxxxxxxxxxxxxxxxx2222x|x2222xxxxxxxxxxxxxxxxxxx2222x|x222222222222222222222222222x|x222222222222222222222222222x|x222222222222222222222222222x|x222222222222222222222222222x|xxxxxxxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('model_u', 0, 17, 1, 2, 'xxxxxxxxxxxxxxxxxxxxxxxx|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|11111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|x1111100000000000000000x|xxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('model_v', 0, 3, 2, 2, 'xxxxxxxxxxxxxxxxxxxx|x222221111111111111x|x222221111111111111x|2222221111111111111x|x222221111111111111x|x222221111111111111x|x222221111111111111x|xxxxxxxx1111xxxxxxxx|xxxxxxxx0000xxxxxxxx|x000000x0000x000000x|x000000x0000x000000x|x00000000000x000000x|x00000000000x000000x|x000000000000000000x|x000000000000000000x|xxxxxxxx00000000000x|x000000x00000000000x|x000000x0000xxxxxxxx|x00000000000x000000x|x00000000000x000000x|x00000000000x000000x|x00000000000x000000x|xxxxxxxx0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|xxxxxxxxxxxxxxxxxxxx', 1),
	('model_w', 0, 3, 2, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxxxx|x2222xx1111111111xx11111111|x2222xx1111111111xx11111111|222222111111111111111111111|x22222111111111111111111111|x22222111111111111111111111|x22222111111111111111111111|x2222xx1111111111xx11111111|x2222xx1111111111xx11111111|x2222xx1111111111xxxx1111xx|x2222xx1111111111xxxx0000xx|xxxxxxx1111111111xx00000000|xxxxxxx1111111111xx00000000|x22222111111111111000000000|x22222111111111111000000000|x22222111111111111000000000|x22222111111111111000000000|x2222xx1111111111xx00000000|x2222xx1111111111xx00000000|x2222xxxx1111xxxxxxxxxxxxxx|x2222xxxx0000xxxxxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx|x2222x0000000000xxxxxxxxxxx', 1),
	('model_x', 0, 12, 0, 2, 'xxxxxxxxxxxxxxxxxxxx|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|xxx00xxx0000xxx00xxx|x000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|0000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000x0000x000000x|x000000xxxxxx000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|x000000000000000000x|xxxxxxxxxxxxxxxxxxxx', 1),
	('model_y', 0, 3, 0, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxxxxx|x00000000xx0000000000xx0000x|x00000000xx0000000000xx0000x|000000000xx0000000000xx0000x|x00000000xx0000000000xx0000x|x00000000xx0000xx0000xx0000x|x00000000xx0000xx0000xx0000x|x00000000xx0000xx0000000000x|x00000000xx0000xx0000000000x|xxxxx0000xx0000xx0000000000x|xxxxx0000xx0000xx0000000000x|xxxxx0000xx0000xxxxxxxxxxxxx|xxxxx0000xx0000xxxxxxxxxxxxx|x00000000xx0000000000000000x|x00000000xx0000000000000000x|x00000000xx0000000000000000x|x00000000xx0000000000000000x|x0000xxxxxxxxxxxxxxxxxx0000x|x0000xxxxxxxxxxxxxxxxxx0000x|x00000000000000000000000000x|x00000000000000000000000000x|x00000000000000000000000000x|x00000000000000000000000000x|xxxxxxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('model_z', 0, 9, 0, 2, 'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx|xxxxxxxxxxx00000000000000000000|xxxxxxxxxxx00000000000000000000|xxxxxxxxxxx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|000000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|x00000000xx00000000000000000000|xxxxxxxxxxx00000000000000000000|xxxxxxxxxxx00000000000000000000|xxxxxxxxxxx00000000000000000000|xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx', 1),
	('newbie_lobby', 2, 11, 0, 2, 'xxxxxxxxxxxxxxxx000000|xxxxx0xxxxxxxxxx000000|xxxxx00000000xxx000000|xxxxx000000000xx000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|0000000000000000000000|xxxxx000000000000000xx|xxxxx000000000000000xx|x0000000000000000000xx|x0000000000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxxx0000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxxxx000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxxx00000000000xxxxx|xxxxx000000000000xxxxx|xxxxx000000000000xxxxx', 0);
/*!40000 ALTER TABLE `room_models` ENABLE KEYS */;

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
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`id`, `username`, `figure`, `sex`, `rank`, `credits`, `pixels`, `motto`, `join_date`, `last_online`) VALUES
	(1, 'Alex', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 7, 2000, 0, '123', '2020-03-29 18:20:03', '2020-04-18 15:29:25'),
	(2, 'Test', 'hd-180-1.hr-100-.ch-260-62.lg-275-64.ha-1008-.ea-1402-.ca-1806-73', 'M', 1, 0, 0, '456', '2020-03-29 20:47:31', '2020-04-15 20:48:07'),
	(3, 'Test123', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 1, 0, 0, '789', '2020-03-29 20:47:31', '2020-04-10 20:37:28'),
	(4, 'Test456', 'hd-180-1.hr-100-61.ch-210-66.lg-270-82.sh-290-80', 'M', 1, 0, 0, '789', '2020-03-29 20:47:31', '2020-04-10 20:37:28');
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
	(1, 0, 0, 0, 1, 1),
	(2, 0, 0, 0, 1, 1),
	(3, 0, 0, 0, 1, 1);
/*!40000 ALTER TABLE `user_settings` ENABLE KEYS */;

CREATE TABLE IF NOT EXISTS `user_subscriptions` (
  `user_id` int(11) NOT NULL,
  `subscribed_at` datetime NOT NULL,
  `expire_at` datetime NOT NULL,
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40000 ALTER TABLE `user_subscriptions` DISABLE KEYS */;
INSERT INTO `user_subscriptions` (`user_id`, `subscribed_at`, `expire_at`) VALUES
	(1, '2020-04-10 14:00:31', '2024-04-28 14:00:31');
/*!40000 ALTER TABLE `user_subscriptions` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
