/*
Navicat MySQL Data Transfer

Source Server         : 192.168.1.99
Source Server Version : 50556
Source Host           : 192.168.1.99:53306
Source Database       : UTH_Meeting

Target Server Type    : MYSQL
Target Server Version : 50556
File Encoding         : 65001

Date: 2018-10-26 17:23:48
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for BAS_App
-- ----------------------------
DROP TABLE IF EXISTS `BAS_App`;
CREATE TABLE `BAS_App` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `AppType` int(11) NOT NULL DEFAULT '0',
  `Code` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Key` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Secret` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of BAS_App
-- ----------------------------
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee100000', '接口中心', '4', '100000', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee100100', '更新程序', '4', '100100', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee200100', '授权工具', '4', '200100', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee900101', '会议系统微信端', '4', '900101', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee900102', '会议系统客户端', '4', '900102', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');

-- ----------------------------
-- Table structure for BAS_AppVersion
-- ----------------------------
DROP TABLE IF EXISTS `BAS_AppVersion`;
CREATE TABLE `BAS_AppVersion` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `AppId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `No` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Descript` varchar(600) COLLATE utf8_unicode_ci NOT NULL,
  `UpdateType` int(11) NOT NULL,
  `ReleaseDt` datetime NOT NULL,
  `FileSize` bigint(20) DEFAULT NULL,
  `FileAddress` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileMd5` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FileName` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of BAS_AppVersion
-- ----------------------------
INSERT INTO `BAS_AppVersion` VALUES ('00fedee6-ce95-4077-9c26-ad0b3bd8dde4', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.0.0.1', 'aaaa', '2', '2018-10-25 16:35:33', '0', null, null, null, '2018-10-26 16:35:45', '00000000-0000-0000-0000-000000000000', '2018-10-26 16:36:03', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810000000', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810010000', '79e775ec-c1f2-4865-883f-82d8ee100100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a820010000', '79e775ec-c1f2-4865-883f-82d8ee200100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010100', '79e775ec-c1f2-4865-883f-82d8ee900101', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010200', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010201', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.1.0.0', '当前版本在线更新被中止，请联系客服获取新版。', '0', '1900-01-01 00:00:00', '17666415', 'http://localhost:8101/api/app/download', '498783EBC25B416AAB7E12F8A4CC38E0', 'v1100.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');

-- ----------------------------
-- Table structure for MET_Meeting
-- ----------------------------
DROP TABLE IF EXISTS `MET_Meeting`;
CREATE TABLE `MET_Meeting` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Num` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Descript` varchar(600) COLLATE utf8_unicode_ci NOT NULL,
  `Setting` text COLLATE utf8_unicode_ci,
  `CreateDt` datetime DEFAULT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastDt` datetime DEFAULT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MET_Meeting
-- ----------------------------
INSERT INTO `MET_Meeting` VALUES ('072215ee-1281-4dc3-82bf-5607399a6bdd', '100032', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 18:30:06', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:06', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('0b64bb20-ceed-45c3-a815-7c5381c4e925', '100003', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:28:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:28:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('0e08283b-8cdf-441d-bb38-7519ed63ed5e', '100020', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":100,\"sentenceMilliseconds\":1500,\"byteLength\":0,\"profileId\":\"78df3ff0-157c-49db-b1cc-ed7294c62891\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":null}', '2018-10-25 13:40:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:49', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('1280bcc4-0102-4c0a-a477-733b5f012bfe', '100016', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"79b58d91-0c5a-44b7-ab7c-779ad0afb478\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:59:52', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:59:52', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('14a451ec-98cd-4bb6-9037-f7b7e4f647d6', '100018', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"9b2e3368-2135-44c8-8911-480591a7c368\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 18:13:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:13:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('174bd068-6f57-406d-9cc4-21acc7ca9267', '100013', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:22:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:22:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('219095a2-0bd6-44a7-9e6c-8078ae9e5399', '100028', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:48:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:48:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('24845e95-feff-4301-a484-7537ae90ff96', '100002', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:19:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('25fdb67b-5e26-48a9-846e-d4c42b9a3ce2', '100008', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:50:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:50:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('2d2ae484-a52d-4d7f-825a-2c28077b1207', '100006', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:44:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:44:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('33816c36-7e64-4674-93b1-b7cc4944a702', '100031', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3500,\"byteLength\":4800,\"profileId\":\"78df3ff0-157c-49db-b1cc-ed7294c62891\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 16:09:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 16:09:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('3545c281-061c-438c-8abc-a140c28ee9a6', '100009', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:52:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:52:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('43ffa003-942e-4939-b60c-4daf680d3feb', '100015', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:30:13', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:30:13', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('7072f4b8-b5ff-4a14-bb4b-527316fe667f', '100001', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:17:16', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:16', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('7e74b867-3437-43fe-8bec-723f6e60a396', '100024', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:35:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:35:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('89d2ecc4-79b5-4f4f-885a-ef507c5614a5', '100014', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:26:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('900beba3-4e16-4958-98fe-1513e87628f8', '100007', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:47:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:47:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('90116997-d320-499d-a82e-944559451345', '100030', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 15:02:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:02:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('9bd261d6-b405-4339-9acf-63500a0eb9cc', '100026', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:42:39', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:42:39', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('b4d9b9da-ec6b-440f-a904-b9ae19c56bb7', '100010', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:55:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:55:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('b6718ae9-ca4d-4038-8452-72030a5513fa', '100005', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:41:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:41:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('c4c9b11a-d741-4284-9824-a2f5472d7ee4', '100029', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:56:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:56:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('c756924f-4fe8-4a1c-9869-9fcf46155ff1', '100025', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:37:59', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:37:59', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('c86789e7-cba8-4805-a23d-f105277dfc16', '100017', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"bd36d50a-752f-4ce9-8924-8800d4fdeed5\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 18:03:35', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:03:35', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('c8c19145-a034-47ef-a15a-ba991387a2eb', '100027', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:44:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:44:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('cf475f40-cda5-463c-b007-b374b1d2540f', '100022', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:01:06', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:01:06', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('d0316cd4-ca93-4917-bcec-5c2daace2bb1', '100011', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:07:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:07:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('d4518961-07f1-43f7-8b9b-4f3e735406ee', '100019', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":100,\"sentenceMilliseconds\":1500,\"byteLength\":0,\"profileId\":\"78df3ff0-157c-49db-b1cc-ed7294c62891\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"en\",\"targetLangs\":\"de|ru|fr\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":null}', '2018-10-25 13:33:14', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:33:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('ee9bc8db-2914-4577-a211-4ec8518a083c', '100004', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 16:32:39', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:32:39', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('f0df55b2-c18d-48c0-a8d2-873c1388a53d', '100023', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 14:09:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:09:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('f147fb33-908c-4e6b-b37a-cedc1e313252', '100012', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"489b1b04-bd38-416c-b86b-39d5adabcd96\",\"accountId\":\"afba732c-474f-4475-8dd3-d05aaeaf860f\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-24 17:18:32', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:18:32', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Meeting` VALUES ('f9a9b342-db54-4a63-b18f-bd4e90751641', '100021', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":3000,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en|ru\",\"accent\":\"mandarin\",\"fontSize\":18,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-10-25 13:49:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:49:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');

-- ----------------------------
-- Table structure for MET_Profile
-- ----------------------------
DROP TABLE IF EXISTS `MET_Profile`;
CREATE TABLE `MET_Profile` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `AccountId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `SourceLang` varchar(600) COLLATE utf8_unicode_ci NOT NULL,
  `TargetLangs` text COLLATE utf8_unicode_ci,
  `Speed` int(11) DEFAULT NULL,
  `FontSize` int(11) DEFAULT NULL,
  `CreateDt` datetime DEFAULT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastDt` datetime DEFAULT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MET_Profile
-- ----------------------------
INSERT INTO `MET_Profile` VALUES ('78df3ff0-157c-49db-b1cc-ed7294c62891', 'afba732c-474f-4475-8dd3-d05aaeaf860f', 'zs', 'en|ru', '5', '18', '2018-10-25 10:55:15', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:49', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Profile` VALUES ('878f50f9-a9d4-41f5-af4d-22942c0db950', '310acdce-5bf7-4aca-8651-b054f71328b1', 'zs', 'en', '5', '16', '2018-10-25 19:03:31', '310acdce-5bf7-4aca-8651-b054f71328b1', '2018-10-25 19:03:31', '310acdce-5bf7-4aca-8651-b054f71328b1', '0');

-- ----------------------------
-- Table structure for MET_Record
-- ----------------------------
DROP TABLE IF EXISTS `MET_Record`;
CREATE TABLE `MET_Record` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `MeetingId` varchar(60) COLLATE utf8_unicode_ci NOT NULL COMMENT '会议Id',
  `Langue` varchar(60) COLLATE utf8_unicode_ci NOT NULL COMMENT '内容语言',
  `LangueTrs` varchar(200) COLLATE utf8_unicode_ci NOT NULL DEFAULT '' COMMENT '翻译目录语言',
  `Content` text COLLATE utf8_unicode_ci NOT NULL COMMENT '会议内容',
  `Sort` int(11) NOT NULL COMMENT '序号',
  `CreateDt` datetime DEFAULT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastDt` datetime DEFAULT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) DEFAULT NULL,
  `LocalId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MET_Record
-- ----------------------------
INSERT INTO `MET_Record` VALUES ('034ef513-dc95-45a3-a259-8dc48b67f0e7', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '它的内容才是很长的内容测试，很长的内容测试测试这是测试有没有这事很长的内容测试？', '0', '2018-10-24 17:27:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '134c4bdf-b56d-4dc9-9a92-5ff7ff09682e');
INSERT INTO `MET_Record` VALUES ('06279cd7-7707-4ec8-85ce-884365af321e', '90116997-d320-499d-a82e-944559451345', 'zs', 'en', '测试2', '-1', '2018-10-25 15:02:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:02:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'f57df8e5-9cc7-4b0b-8a0f-c2cc37b96d5f');
INSERT INTO `MET_Record` VALUES ('07c2338c-d22f-44fe-8a48-38ca88c07bc9', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '问题是啥。', '0', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'a75e0363-9c12-41ae-8a47-0af4997ec546');
INSERT INTO `MET_Record` VALUES ('0a38fbd6-a4c1-4dd1-bcd7-a96b539fcd2a', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '吃啥吃啥。', '0', '2018-10-24 16:17:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '25cddaec-1ea4-488e-801b-8ddd35da0e57');
INSERT INTO `MET_Record` VALUES ('0b0a1517-a3d2-45e5-8155-45006cd691ef', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '很长的那种姿势。', '0', '2018-10-24 17:27:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'e2f1eccb-5fe4-44d2-bf12-8c7bc5e28226');
INSERT INTO `MET_Record` VALUES ('149b6862-05c3-4736-8d69-94d74f444a2a', '072215ee-1281-4dc3-82bf-5607399a6bdd', 'zs', 'en|ru', '隔三秒。', '0', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '434d4746-6b1e-448a-9e9e-4fb7af785d9f');
INSERT INTO `MET_Record` VALUES ('17fc32c5-6cf9-483e-aaf4-cbd69c7ed765', '072215ee-1281-4dc3-82bf-5607399a6bdd', 'zs', 'en|ru', '这是。', '0', '2018-10-25 18:30:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '388f905f-b4c8-4dbb-b9bb-62b0f71c2713');
INSERT INTO `MET_Record` VALUES ('1bb41632-22eb-4d97-8f1a-c1b7dbb12a7f', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', 'word内。', '0', '2018-10-24 17:46:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:46:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '53527e6d-ada9-4f29-a948-b65b853606f6');
INSERT INTO `MET_Record` VALUES ('1ce90b26-bb7f-4624-823e-e75c18e4096b', 'd4518961-07f1-43f7-8b9b-4f3e735406ee', 'en', 'de|ru|fr', ' Take.', '0', '2018-10-25 13:33:53', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:33:53', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '94d1bb43-af29-4a46-88b9-efca097c9fa3');
INSERT INTO `MET_Record` VALUES ('26c7da47-05da-4eec-b584-ee3ed5321ceb', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '这是山。', '0', '2018-10-24 16:17:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '3b629564-fd60-4815-ba81-4016e706264c');
INSERT INTO `MET_Record` VALUES ('2fa0dd1d-a309-4864-a887-c0829414e98f', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', '我去内蒙节日快乐！', '0', '2018-10-24 17:43:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:43:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'd798d945-0bca-450f-ad9c-e2aeb8fe964e');
INSERT INTO `MET_Record` VALUES ('31e1cfb5-7933-4806-9359-de13a4a43107', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '这是我这16就是七。', '0', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'd6ad7943-472e-4834-9f7f-51b62d9de668');
INSERT INTO `MET_Record` VALUES ('338bded3-45c6-442b-9fc8-6f6fbcbb0d64', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '一本地测试一。', '0', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '36e171d4-56d2-4cdd-9bc8-290e3fb79939');
INSERT INTO `MET_Record` VALUES ('3410348d-3c27-4bd8-8ffe-a99b21a45704', '43ffa003-942e-4939-b60c-4daf680d3feb', 'zs', 'en', '很长的那种姿势，很长的那种姿势，很长的内容测试，很长的内容测试，南昌的内容测试。', '0', '2018-10-24 17:30:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:30:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '3559a206-4ced-450d-bd5b-679ee51e90db');
INSERT INTO `MET_Record` VALUES ('3f6a06f1-ef98-429e-bb96-420e0adf66af', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '很长的内容测试。', '0', '2018-10-24 17:26:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'ce4b6472-c99e-414d-925c-05aa6fe40e8e');
INSERT INTO `MET_Record` VALUES ('463a6707-e27b-4880-a5db-7b879e42602a', '90116997-d320-499d-a82e-944559451345', 'zs', 'en|ru', '测试四。', '0', '2018-10-25 15:04:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:04:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '28be6676-7b69-43cc-8015-dd4906fa24e1');
INSERT INTO `MET_Record` VALUES ('47673469-3d51-4482-97c5-7f61ba038cb5', '2d2ae484-a52d-4d7f-825a-2c28077b1207', 'zs', 'en', '很长的那种姿势，很长的那种姿势，很长的内容测试，很长的那种姿势，很长的那种姿势。', '0', '2018-10-24 16:44:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:44:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'b7daf0c3-909e-40cb-af8e-48903b63c9c2');
INSERT INTO `MET_Record` VALUES ('47f7de1a-1e19-4e9a-b6a2-1486b0f20b9c', '24845e95-feff-4301-a484-7537ae90ff96', 'zs', 'en', '这是一四十二四十三。', '0', '2018-10-24 16:19:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'c391aef3-d758-4ec9-b55a-60b949420bfa');
INSERT INTO `MET_Record` VALUES ('4854fe3e-d9ab-41a9-8239-6928c5138379', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'en', '', ' Can I help you.', '0', '2018-10-24 18:04:49', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:04:49', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '61828a9e-a637-41b4-be9e-6a0dcd4383d1');
INSERT INTO `MET_Record` VALUES ('4be15097-b048-4527-9b7c-2c75452c15ac', '24845e95-feff-4301-a484-7537ae90ff96', 'zs', 'en', '这次是失误。', '0', '2018-10-24 16:19:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '2cfda23a-3006-424b-9b37-6c5a5285c291');
INSERT INTO `MET_Record` VALUES ('4e28d342-7ccb-4d66-b5dd-a1e5d8e4b7da', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '这是事。', '0', '2018-10-25 15:00:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '1b72c27c-8450-44fd-b226-ede085c18835');
INSERT INTO `MET_Record` VALUES ('56b5d964-27cb-4f83-9997-7f2f787ee673', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en', '测试2', '-1', '2018-10-25 14:59:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:59:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'ba169ed9-fa99-402b-964b-d7c0dc0379df');
INSERT INTO `MET_Record` VALUES ('575168f0-4b2b-40fa-8c3d-16586ad2b71f', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', '嗨呀hello.', '0', '2018-10-24 17:43:52', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:43:52', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'f5614a55-66ab-4e2c-902d-ca93e5fc4f1e');
INSERT INTO `MET_Record` VALUES ('5edd6299-ccdd-4fe5-9bdc-6e45a16235f5', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', '又有啊谢啊.', '0', '2018-10-24 17:44:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:44:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '492112f1-644f-4e42-9313-5ec0c69c9697');
INSERT INTO `MET_Record` VALUES ('5f8853e0-0a83-41bb-b087-221467c44440', 'f147fb33-908c-4e6b-b37a-cedc1e313252', 'zs', 'en', '很长的内容测试。', '0', '2018-10-24 17:19:17', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:19:17', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '3d8bd94d-dc5b-4bd6-96dd-5a7af9656245');
INSERT INTO `MET_Record` VALUES ('6351e173-774d-4c41-b301-dc42f90cb0b5', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'en', '', ' What\'s your name?', '0', '2018-10-24 18:04:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:04:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '54769b2a-b72b-453e-8295-2b3650043384');
INSERT INTO `MET_Record` VALUES ('74e60dfd-87a9-4341-a540-56bb7e842376', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '这是一。', '0', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '2942a19b-3efe-429c-a67f-12266e13ee60');
INSERT INTO `MET_Record` VALUES ('78704ac5-cc47-4cc6-a39c-2ff88f1aca35', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '很长的内容测试。', '0', '2018-10-24 17:26:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '3750e51e-13bf-4fe6-8e0d-a8d667d507cc');
INSERT INTO `MET_Record` VALUES ('799f54e3-b6c5-459b-9bb8-7adfe625f8f4', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'en', '', ' Excuse me.', '0', '2018-10-24 18:04:40', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:04:40', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '58200aa2-6383-4044-860d-a5e5931b26ae');
INSERT INTO `MET_Record` VALUES ('79e682d7-524d-4127-a180-07cd3283d961', 'b6718ae9-ca4d-4038-8452-72030a5513fa', 'zs', 'en', '这是一段据称间连续内容，看看有多长。', '0', '2018-10-24 16:41:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:41:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'a0a4d17a-fc84-4604-901d-473ac01a3e91');
INSERT INTO `MET_Record` VALUES ('842de96f-870e-4a97-9467-7ccdab045508', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '这是啥。', '0', '2018-10-24 16:17:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:36', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'd665a1d3-dd04-453c-9c71-4e78fbaaab6d');
INSERT INTO `MET_Record` VALUES ('85e5dac7-308b-4efa-8c4e-c980c97d36d2', '072215ee-1281-4dc3-82bf-5607399a6bdd', 'zs', 'en|ru', '会议动态全部内容。', '0', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '5cea5841-e63f-4154-bcb8-0741f6f9fdbd');
INSERT INTO `MET_Record` VALUES ('86ca8e83-b45a-4ea3-935d-1d79da0118f0', '1280bcc4-0102-4c0a-a477-733b5f012bfe', 'en', '', 'word内.', '0', '2018-10-24 18:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '4c45841c-7cbf-49dc-a4c9-670a725ba65d');
INSERT INTO `MET_Record` VALUES ('8ade1826-f1e3-46c9-96f5-ad345f6d5702', 'f147fb33-908c-4e6b-b37a-cedc1e313252', 'zs', 'en', '很长的那种姿势。', '0', '2018-10-24 17:19:03', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:19:03', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '32083737-2c7a-4ac8-9db5-3d6fc5a11bd6');
INSERT INTO `MET_Record` VALUES ('90f7cdaf-39a4-4207-b42e-477fbcb81abc', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'en', '', ' Yeah.', '0', '2018-10-24 18:04:04', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:04:04', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '32b29fe7-0db1-4b78-893a-e1f78cebd2c3');
INSERT INTO `MET_Record` VALUES ('93f5e118-f79d-492a-be40-7dd0855e4db8', 'c8c19145-a034-47ef-a15a-ba991387a2eb', 'zs', 'en|ru', '测试一。', '0', '2018-10-25 14:45:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:45:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'cd791224-8087-4df9-9576-847694c5073a');
INSERT INTO `MET_Record` VALUES ('955f47f2-4148-4d59-b8df-83c12bb8d274', '072215ee-1281-4dc3-82bf-5607399a6bdd', 'zs', 'en|ru', '这是一下。', '0', '2018-10-25 18:30:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '2c18ba02-d884-44c7-ba79-34de9db8c8dc');
INSERT INTO `MET_Record` VALUES ('9bbceaa9-ed7d-40d3-8766-e31e948eeeee', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'zs', 'en|ru', ' Tension。', '0', '2018-10-25 13:40:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:24', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'b4c120f9-01d2-4933-b160-0ea6a56540b4');
INSERT INTO `MET_Record` VALUES ('a3913866-4a92-4714-b611-26466f86360a', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '不是色。', '0', '2018-10-24 16:17:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'c648e5ca-4460-4b2d-910c-4d48b44f3741');
INSERT INTO `MET_Record` VALUES ('a46e7a86-ae01-4987-94e4-5866131711ba', '43ffa003-942e-4939-b60c-4daf680d3feb', 'zs', 'en', 'word内。', '0', '2018-10-24 17:40:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:40:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '4cda08dc-8e70-4add-8119-3f9aa8910f24');
INSERT INTO `MET_Record` VALUES ('a4f53a1e-21a3-49fd-ab09-532540ab67f0', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en', '测试3', '-1', '2018-10-25 14:59:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:59:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'd4d9270b-815d-4d30-9c5a-ea2aea8d2661');
INSERT INTO `MET_Record` VALUES ('a8e2b511-a37e-4f60-8556-019afdfec8d5', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'en', '', ' What\'s your name?', '0', '2018-10-24 18:03:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:03:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'c53ed952-277d-4289-b13d-9a46b8a477c9');
INSERT INTO `MET_Record` VALUES ('ab8f53e3-f450-438a-8ea5-025f938e9bfb', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', '可爱hello.', '0', '2018-10-24 17:45:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:45:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'a2c45887-75b9-4757-99ad-37b09b759efc');
INSERT INTO `MET_Record` VALUES ('b0422fea-feb3-4ee8-aeee-20658c00e539', 'f147fb33-908c-4e6b-b37a-cedc1e313252', 'zs', 'en', '很长的内容测试', '0', '2018-10-24 17:18:45', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:18:45', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '39e707b4-9719-43ba-8a6d-9954aa0d513b');
INSERT INTO `MET_Record` VALUES ('b480866c-0a6d-4f1f-a26b-95d6aeaf05ed', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', '那车呢hello.', '0', '2018-10-24 17:43:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:43:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '9c3782fb-a746-467a-bb28-13c895b5876f');
INSERT INTO `MET_Record` VALUES ('b48894aa-0230-4e70-a067-02b6386742c0', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '这是一则笑话。', '0', '2018-10-24 16:17:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '5dbd10b4-a963-4b8c-903e-6f8d21306f0b');
INSERT INTO `MET_Record` VALUES ('ce4d0eb9-95e0-4eb6-a84a-2093ec1083d6', '33816c36-7e64-4674-93b1-b7cc4944a702', 'zs', 'en', '这是加入测试。', '0', '2018-10-25 19:03:37', '310acdce-5bf7-4aca-8651-b054f71328b1', '2018-10-25 19:03:37', '310acdce-5bf7-4aca-8651-b054f71328b1', '0', 'ffb0b95e-9180-48da-8688-afc40375217c');
INSERT INTO `MET_Record` VALUES ('d25bb270-9577-4a71-9888-2f50311e9b2e', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '会是很强的内容测试。', '0', '2018-10-24 17:27:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '52dbcc7f-341f-4e3b-8946-17bb58eafe13');
INSERT INTO `MET_Record` VALUES ('d4e593ee-e60f-4d6f-87cf-4c9fec318336', '24845e95-feff-4301-a484-7537ae90ff96', 'zs', 'en', '这是一车沙。', '0', '2018-10-24 16:19:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'e13c86a3-4af5-41bc-9529-f30b151d8262');
INSERT INTO `MET_Record` VALUES ('d94cacea-70d0-4bd1-9b5b-58ceb7893a8b', '43ffa003-942e-4939-b60c-4daf680d3feb', 'en', '', 'hello！', '0', '2018-10-24 17:44:23', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:44:23', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '06c1a954-50c5-4307-9874-b6379e595fff');
INSERT INTO `MET_Record` VALUES ('dbd1554e-15e3-4a7e-9c8f-f9605497f4fc', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '很长的那种姿势很长的内容测试。', '0', '2018-10-24 17:26:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '1c9257a1-7f2d-4400-8793-621a9e9e56d7');
INSERT INTO `MET_Record` VALUES ('de40c6c9-b268-4aad-bd3b-3fa7d371bb66', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'en', 'de|ru|fr', ' Tension.', '0', '2018-10-25 13:40:07', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:07', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '9624ba7e-0341-47c8-bdaf-5bf15a1cdeb1');
INSERT INTO `MET_Record` VALUES ('de773b3c-696a-451e-8f20-abe51e608c16', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'zs', 'en', '很长的内容，很长的内容，很长的内容，很长的内容，很长的内容，很长的内容，很长的内容，很长的那种姿势，很长的内容，很长的内容这是。', '0', '2018-10-24 17:27:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:20', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '89e10cdc-9b50-462e-a19f-4eba40e2376d');
INSERT INTO `MET_Record` VALUES ('df8a01e3-a341-4ed1-81f8-e641b057b7ef', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'zs', 'en|ru', '测试。', '0', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '0e7d263f-5209-468b-8a81-fb123d368907');
INSERT INTO `MET_Record` VALUES ('e29b18fa-a612-4d4b-9f66-271b2a799061', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '这首。', '0', '2018-10-24 16:17:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '26bd5cbd-7a1e-4c21-9022-3059ef33d528');
INSERT INTO `MET_Record` VALUES ('e32e687d-c0ef-4d23-ba59-a09b79724748', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'zs', 'en|ru', '这些话。', '0', '2018-10-25 13:40:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:54', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '9c784e16-321f-4cf5-8238-05d096a007ad');
INSERT INTO `MET_Record` VALUES ('e38d23a6-8da2-4c6c-9e22-eea3fc9caee6', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '本地测试一。', '0', '2018-10-25 14:57:41', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:41', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'c7fe49bb-1aed-401c-be74-67a9e6bec63d');
INSERT INTO `MET_Record` VALUES ('e498c4cf-5e93-40ee-9f58-2d0f5b183dd2', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'zs', 'en|ru', '这是酒。', '0', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'ca042857-445b-449d-90bc-eb45bb8d3356');
INSERT INTO `MET_Record` VALUES ('e9925477-e125-4316-844c-59fa7475c47f', '90116997-d320-499d-a82e-944559451345', 'zs', 'en|ru', '测试三。', '0', '2018-10-25 15:03:04', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:03:04', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '606a99a4-0ba5-4279-8df7-de73db503b4c');
INSERT INTO `MET_Record` VALUES ('eaaf8ba5-165d-4666-bdcf-bbf8cc6bc63f', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '才16×17×十八四十九。', '0', '2018-10-24 16:17:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'cb91c564-9bb4-4cbd-bda7-28750c753684');
INSERT INTO `MET_Record` VALUES ('eb020266-4180-473d-96fe-ea24c9a230f8', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'zs', 'en', '我tm。', '0', '2018-10-24 18:03:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:03:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '21881fc8-3b68-484d-9964-cb7953ab5922');
INSERT INTO `MET_Record` VALUES ('fa9477af-6caa-4bd6-95c0-f0dcdb454916', '90116997-d320-499d-a82e-944559451345', 'zs', 'en', '测试1', '-1', '2018-10-25 15:02:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:02:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', '31873cae-1194-4ef7-aef2-a1ec66acccf2');
INSERT INTO `MET_Record` VALUES ('fabe4aa4-7eb0-4cbe-b4f7-0d2abdb422f0', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'zs', 'en', '这是一。', '0', '2018-10-24 16:17:21', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:21', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0', 'bfe1f123-f56e-4dc4-8bf5-8bacfb5fa703');

-- ----------------------------
-- Table structure for MET_Translation
-- ----------------------------
DROP TABLE IF EXISTS `MET_Translation`;
CREATE TABLE `MET_Translation` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `MeetingId` varchar(60) COLLATE utf8_unicode_ci NOT NULL COMMENT '会议Id',
  `RecordId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Langue` varchar(60) COLLATE utf8_unicode_ci NOT NULL COMMENT '内容语言',
  `Content` text COLLATE utf8_unicode_ci NOT NULL COMMENT '会议内容',
  `Sort` int(11) NOT NULL COMMENT '序号',
  `CreateDt` datetime DEFAULT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastDt` datetime DEFAULT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of MET_Translation
-- ----------------------------
INSERT INTO `MET_Translation` VALUES ('08459f25-d023-4740-9c5f-6ca8038ad741', 'd4518961-07f1-43f7-8b9b-4f3e735406ee', '1ce90b26-bb7f-4624-823e-e75c18e4096b', 'ru', 'Возьмите.', '0', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('0e74947f-fe95-4caf-83fd-6ba13a4f5cf6', '072215ee-1281-4dc3-82bf-5607399a6bdd', '149b6862-05c3-4736-8d69-94d74f444a2a', 'ru', 'через три секунды.', '0', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('106e3ab5-5fea-484e-8e97-5335cb6acd80', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', '3f6a06f1-ef98-429e-bb96-420e0adf66af', 'en', 'A long content test.', '0', '2018-10-24 17:26:35', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:35', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('12521717-7506-4c21-9343-470e28251971', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'a4f53a1e-21a3-49fd-ab09-532540ab67f0', 'en', 'Test 3', '0', '2018-10-25 14:59:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:59:43', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('18c4d50e-98d1-41d9-a1ba-247e72ce08e0', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '338bded3-45c6-442b-9fc8-6f6fbcbb0d64', 'ru', 'местный тест один.', '0', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('19a79f97-79ce-4950-b18a-d2eb9b71ec5e', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '74e60dfd-87a9-4341-a540-56bb7e842376', 'ru', 'это один.', '0', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('1a683280-5129-4080-9163-cad9dff2d5e2', 'f147fb33-908c-4e6b-b37a-cedc1e313252', '8ade1826-f1e3-46c9-96f5-ad345f6d5702', 'en', 'A long posture.', '0', '2018-10-24 17:19:03', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:19:03', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('1bc70101-afd9-459b-a059-c6a1a1476d00', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'e29b18fa-a612-4d4b-9f66-271b2a799061', 'en', 'This one.', '0', '2018-10-24 16:17:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('1d521383-e956-42d4-9e65-7992f92a8997', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'e38d23a6-8da2-4c6c-9e22-eea3fc9caee6', 'ru', 'локальный тест 1.', '0', '2018-10-25 14:57:42', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:42', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('1da06c26-8d97-4c17-b09d-56a6e2863fd2', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'de40c6c9-b268-4aad-bd3b-3fa7d371bb66', 'ru', 'Напряжение.', '0', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('202045cf-d181-4035-81c7-4054045481ea', 'd4518961-07f1-43f7-8b9b-4f3e735406ee', '1ce90b26-bb7f-4624-823e-e75c18e4096b', 'de', 'Nehmen.', '0', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('2f88d675-1aa2-478f-a371-ec9cce524683', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '4e28d342-7ccb-4d66-b5dd-a1e5d8e4b7da', 'ru', 'это дело.', '0', '2018-10-25 15:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('3277b5f2-8b88-4636-9817-9fdfab3e75a2', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', '26c7da47-05da-4eec-b584-ee3ed5321ceb', 'en', 'This is a mountain.', '0', '2018-10-24 16:17:56', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:56', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('36c17287-2d75-4cd6-a8b1-f68e086225dc', 'f147fb33-908c-4e6b-b37a-cedc1e313252', 'b0422fea-feb3-4ee8-aeee-20658c00e539', 'en', 'Long content testing', '0', '2018-10-24 17:18:47', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:18:47', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('3a8d5afa-8b2b-4623-9201-ef145ec47634', '072215ee-1281-4dc3-82bf-5607399a6bdd', '149b6862-05c3-4736-8d69-94d74f444a2a', 'en', 'Every three seconds.', '0', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('4bcd0489-e1b1-4ccc-8b04-30873d0f1091', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '31e1cfb5-7933-4806-9359-de13a4a43107', 'en', 'This is me, this 16 is seven.', '0', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('5251a1ff-b3e8-44ff-9c10-8c87bf041507', '2d2ae484-a52d-4d7f-825a-2c28077b1207', '47673469-3d51-4482-97c5-7f61ba038cb5', 'en', 'Long posture, long posture, long content test, long posture, long posture.', '0', '2018-10-24 16:44:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:44:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('5a592d6e-b289-4a78-b608-e0c5a683b74d', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', '9bbceaa9-ed7d-40d3-8766-e31e948eeeee', 'en', 'Tension。', '0', '2018-10-25 13:40:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('5a5fe7fe-f965-4bdb-9a2c-7175b49b9216', '072215ee-1281-4dc3-82bf-5607399a6bdd', '85e5dac7-308b-4efa-8c4e-c980c97d36d2', 'ru', 'вся динамика встречи.', '0', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('5f86bdc0-5c7c-4b90-87fa-c9df9e752580', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'e32e687d-c0ef-4d23-ba59-a09b79724748', 'ru', 'эти слова.', '0', '2018-10-25 13:40:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('66df08be-4ead-4501-b9ee-0118b05c26d3', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '07c2338c-d22f-44fe-8a48-38ca88c07bc9', 'ru', 'в чем проблема?', '0', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('687eddb5-7328-471c-bfd8-36ea199368ce', '24845e95-feff-4301-a484-7537ae90ff96', 'd4e593ee-e60f-4d6f-87cf-4c9fec318336', 'en', 'This is a load of sand.', '0', '2018-10-24 16:19:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:38', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('6ad77edb-24f8-496f-baea-91f77391b038', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'de773b3c-696a-451e-8f20-abe51e608c16', 'en', 'Long content, long content, long content, long content, long content, long content, long content, long content, long posture, long content, long content.', '0', '2018-10-24 17:27:21', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:21', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('6b394372-5d7c-49d9-b453-0a5988562d29', '90116997-d320-499d-a82e-944559451345', 'e9925477-e125-4316-844c-59fa7475c47f', 'ru', 'тест три.', '0', '2018-10-25 15:03:05', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:03:05', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('741b7f60-96e3-4c10-807f-76176a7586a1', '90116997-d320-499d-a82e-944559451345', '06279cd7-7707-4ec8-85ce-884365af321e', 'en', 'Test 2', '0', '2018-10-25 15:02:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:02:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('798b1fe9-5b24-4eae-bbf1-f319270f78d3', 'd4518961-07f1-43f7-8b9b-4f3e735406ee', '1ce90b26-bb7f-4624-823e-e75c18e4096b', 'fr', 'Prendre.', '0', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:33:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('7decb028-75f3-4eef-8104-6fa2fc8af816', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '07c2338c-d22f-44fe-8a48-38ca88c07bc9', 'en', 'The problem is what.', '0', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:00', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('84f65c46-c569-4245-91ae-0de265b85d38', 'c8c19145-a034-47ef-a15a-ba991387a2eb', '93f5e118-f79d-492a-be40-7dd0855e4db8', 'en', 'Test one.', '0', '2018-10-25 14:45:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:45:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('8a03640d-b042-4dbf-8dcc-d4e1dd46712c', '90116997-d320-499d-a82e-944559451345', '463a6707-e27b-4880-a5db-7b879e42602a', 'en', 'Test four.', '0', '2018-10-25 15:04:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:04:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('8c3ce8b9-20e6-4a07-89f5-e35adf594ba5', '072215ee-1281-4dc3-82bf-5607399a6bdd', '17fc32c5-6cf9-483e-aaf4-cbd69c7ed765', 'en', 'This is.', '0', '2018-10-25 18:30:14', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:14', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('8da06f5d-6c3b-410f-b4bd-a6f0f5c1cdc6', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '4e28d342-7ccb-4d66-b5dd-a1e5d8e4b7da', 'en', 'This is a matter.', '0', '2018-10-25 15:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:12', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('902f5858-3be2-47e1-bdc1-6859e07a92cd', '072215ee-1281-4dc3-82bf-5607399a6bdd', '955f47f2-4148-4d59-b8df-83c12bb8d274', 'en', 'This is a moment.', '0', '2018-10-25 18:30:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('97216781-3a54-4251-9452-92583124daae', '072215ee-1281-4dc3-82bf-5607399a6bdd', '955f47f2-4148-4d59-b8df-83c12bb8d274', 'ru', 'вот оно.', '0', '2018-10-25 18:30:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('9aaebf55-d8fc-4c11-b020-af51d8df999f', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', '9bbceaa9-ed7d-40d3-8766-e31e948eeeee', 'ru', 'Tension.', '0', '2018-10-25 13:40:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:25', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('9c6a8e13-c7e4-4fb6-994d-787eaa28e367', '90116997-d320-499d-a82e-944559451345', 'fa9477af-6caa-4bd6-95c0-f0dcdb454916', 'en', 'Test 1', '0', '2018-10-25 15:02:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:02:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('9f17acfd-3a5b-497e-b6ad-9341b3539430', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', '842de96f-870e-4a97-9467-7ccdab045508', 'en', 'This is what.', '0', '2018-10-24 16:17:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:37', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('a1e2e94e-ec4e-4aa4-a974-6a694c6fd9ca', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'a3913866-4a92-4714-b611-26466f86360a', 'en', 'It\'s not color.', '0', '2018-10-24 16:17:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:57', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('a221fab5-bc5c-4e58-9a3e-35ca9213e0bd', '90116997-d320-499d-a82e-944559451345', 'e9925477-e125-4316-844c-59fa7475c47f', 'en', 'Test three.', '0', '2018-10-25 15:03:05', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:03:05', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('a46b7cae-608d-46e2-bf82-7d7af713e05b', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'e498c4cf-5e93-40ee-9f58-2d0f5b183dd2', 'en', 'This is wine.', '0', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('add3550f-05ca-4569-9f73-cc5734fac865', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'dbd1554e-15e3-4a7e-9c8f-f9605497f4fc', 'en', 'A long pose, a long content test.', '0', '2018-10-24 17:26:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:51', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('af7a0690-b832-49cd-a8e5-59b5feba8849', '90116997-d320-499d-a82e-944559451345', '463a6707-e27b-4880-a5db-7b879e42602a', 'ru', 'тест 4.', '0', '2018-10-25 15:04:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:04:26', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('b05389d2-e3d9-465c-81cb-929497199760', 'c86789e7-cba8-4805-a23d-f105277dfc16', 'eb020266-4180-473d-96fe-ea24c9a230f8', 'en', 'I\'m TM.', '0', '2018-10-24 18:03:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 18:03:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('b0692180-65bd-4542-a054-41111e409f73', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'eaaf8ba5-165d-4666-bdcf-bbf8cc6bc63f', 'en', 'Only 16× 17× 18 49.', '0', '2018-10-24 16:17:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('b3e892ed-1397-4e3a-9ef1-21be26b6c861', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'df8a01e3-a341-4ed1-81f8-e641b057b7ef', 'ru', 'тест.', '0', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('b80b2ffc-d797-4f16-a918-682e41c459b1', 'f147fb33-908c-4e6b-b37a-cedc1e313252', '5f8853e0-0a83-41bb-b087-221467c44440', 'en', 'A long content test.', '0', '2018-10-24 17:19:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:19:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('bc42c945-6957-4449-9ab1-2d2e780952b9', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '338bded3-45c6-442b-9fc8-6f6fbcbb0d64', 'en', 'One local test one.', '0', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:11', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('bff8023f-1b83-4c8e-bd24-0f65f848f6e0', '43ffa003-942e-4939-b60c-4daf680d3feb', 'a46e7a86-ae01-4987-94e4-5866131711ba', 'en', 'Word.', '0', '2018-10-24 17:40:59', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:40:59', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('c14b8064-fdce-418a-b447-2362ac54a707', 'c8c19145-a034-47ef-a15a-ba991387a2eb', '93f5e118-f79d-492a-be40-7dd0855e4db8', 'ru', 'тест один.', '0', '2018-10-25 14:45:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:45:30', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('c5371739-14ce-4cbd-beb7-4d6b5ac52359', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'e38d23a6-8da2-4c6c-9e22-eea3fc9caee6', 'en', 'Local Test One.', '0', '2018-10-25 14:57:42', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:57:42', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('c65d3635-550c-4955-9e79-dabbf55c2e5d', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'de40c6c9-b268-4aad-bd3b-3fa7d371bb66', 'fr', 'Tension.', '0', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d0e69768-cc4a-44ff-af8a-e40224e04418', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'df8a01e3-a341-4ed1-81f8-e641b057b7ef', 'en', 'Testing.', '0', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:34', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d15cf225-9c49-4cef-bec9-b2b13378e5ee', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', '034ef513-dc95-45a3-a259-8dc48b67f0e7', 'en', 'Its content is a long content test, a long content test. Is this a long content test?', '0', '2018-10-24 17:27:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d1eb56f9-8298-4547-b7d7-07270f7acb8d', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', '0b0a1517-a3d2-45e5-8155-45006cd691ef', 'en', 'A long posture.', '0', '2018-10-24 17:27:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d4d9d513-dcdb-4306-a0fb-4dce0d5fb7e4', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '31e1cfb5-7933-4806-9359-de13a4a43107', 'ru', 'это я, 16 - семь.', '0', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:19', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d59d63a8-d8d4-4068-9918-c524edf9436f', 'b6718ae9-ca4d-4038-8452-72030a5513fa', '79e682d7-524d-4127-a180-07cd3283d961', 'en', 'This is an allegedly continuous paragraph to see how long it is.', '0', '2018-10-24 16:41:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:41:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d5e04c69-1604-46b7-be25-9bc6bc6738ce', '072215ee-1281-4dc3-82bf-5607399a6bdd', '85e5dac7-308b-4efa-8c4e-c980c97d36d2', 'en', 'All contents of meeting dynamics.', '0', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:33', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d827b4bf-1286-4438-bdfa-f61d8b37e85b', '43ffa003-942e-4939-b60c-4daf680d3feb', '3410348d-3c27-4bd8-8ffe-a99b21a45704', 'en', 'Long posture, long posture, long content test, long content test, Nanchang content test.', '0', '2018-10-24 17:30:28', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:30:28', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d86066a6-3ebf-448f-9f27-6d39cfc5c864', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '74e60dfd-87a9-4341-a540-56bb7e842376', 'en', 'This is one.', '0', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:56:48', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('d9d14ea3-612f-418b-9001-a963683247e3', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'b48894aa-0230-4e70-a067-02b6386742c0', 'en', 'This is a joke.', '0', '2018-10-24 16:17:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('dc89c499-ea74-40de-9650-fa1b2d1d3cf9', '072215ee-1281-4dc3-82bf-5607399a6bdd', '17fc32c5-6cf9-483e-aaf4-cbd69c7ed765', 'ru', 'это так.', '0', '2018-10-25 18:30:14', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 18:30:14', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('dddf69ab-c038-42e3-aca4-b072f043204b', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'de40c6c9-b268-4aad-bd3b-3fa7d371bb66', 'de', 'Spannung.', '0', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:08', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('e2c69777-a513-4143-b516-ee5494b15bbb', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', '0a38fbd6-a4c1-4dd1-bcd7-a96b539fcd2a', 'en', 'Eat what, eat what.', '0', '2018-10-24 16:17:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:27', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('e474ed5f-1645-482c-bfd1-fa93d67ccfe7', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', '56b5d964-27cb-4f83-9997-7f2f787ee673', 'en', 'Test 2', '0', '2018-10-25 14:59:28', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 14:59:28', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('e892efed-ee87-434b-9bdc-a65b2e464490', '24845e95-feff-4301-a484-7537ae90ff96', '4be15097-b048-4527-9b7c-2c75452c15ac', 'en', 'This is a mistake.', '0', '2018-10-24 16:19:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:46', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('e9a99eec-3d4e-4bff-814c-5f78390c5fc5', '33816c36-7e64-4674-93b1-b7cc4944a702', 'ce4d0eb9-95e0-4eb6-a84a-2093ec1083d6', 'en', 'This is a join test.', '0', '2018-10-25 19:03:39', '310acdce-5bf7-4aca-8651-b054f71328b1', '2018-10-25 19:03:39', '310acdce-5bf7-4aca-8651-b054f71328b1', '0');
INSERT INTO `MET_Translation` VALUES ('ef4fb2ca-7c16-416e-b897-894f90a3c19b', 'c4c9b11a-d741-4284-9824-a2f5472d7ee4', 'e498c4cf-5e93-40ee-9f58-2d0f5b183dd2', 'ru', 'это вино.', '0', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 15:00:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('ef8c81a7-d773-4ca8-afdc-e8fdc197fca5', '7072f4b8-b5ff-4a14-bb4b-527316fe667f', 'fabe4aa4-7eb0-4cbe-b4f7-0d2abdb422f0', 'en', 'This is one.', '0', '2018-10-24 16:17:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:17:22', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('f98323bd-f780-44b6-9762-803ec731a19f', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', '78704ac5-cc47-4cc6-a39c-2ff88f1aca35', 'en', 'A long content test.', '0', '2018-10-24 17:26:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:26:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('fdde0ffa-a044-47d2-894a-c67506ab7fe9', '89d2ecc4-79b5-4f4f-885a-ef507c5614a5', 'd25bb270-9577-4a71-9888-2f50311e9b2e', 'en', 'Will be a strong content test.', '0', '2018-10-24 17:27:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 17:27:58', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('feaca2f8-9a54-4e42-afc8-25663945b27d', '24845e95-feff-4301-a484-7537ae90ff96', '47f7de1a-1e19-4e9a-b6a2-1486b0f20b9c', 'en', 'This is one forty-two forty - three.', '0', '2018-10-24 16:19:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-24 16:19:44', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `MET_Translation` VALUES ('ff31252a-a9d6-4640-8102-193eb3f1b307', '0e08283b-8cdf-441d-bb38-7519ed63ed5e', 'e32e687d-c0ef-4d23-ba59-a09b79724748', 'en', 'These words.', '0', '2018-10-25 13:40:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:40:55', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');

-- ----------------------------
-- Table structure for UC_Account
-- ----------------------------
DROP TABLE IF EXISTS `UC_Account`;
CREATE TABLE `UC_Account` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `No` varchar(10) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Account` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Mobile` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Email` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Password` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MobileAuth` tinyint(4) DEFAULT NULL,
  `EmailAuth` tinyint(4) DEFAULT NULL,
  `CertificateType` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CertificateNo` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CertificateAuth` tinyint(4) DEFAULT NULL,
  `RealName` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `NormalizedEmail` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `NormalizedAccount` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsTwoFactorEnabled` tinyint(4) DEFAULT NULL,
  `IsLockoutEnabled` tinyint(4) DEFAULT NULL,
  `LockoutEndDateUtc` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `AccessFailedCount` int(11) DEFAULT NULL,
  `SecurityStamp` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ConcurrencyStamp` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InviterId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LoginCount` int(11) DEFAULT NULL,
  `LoginLastIp` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LoginLastDt` datetime NOT NULL,
  `Type` int(11) DEFAULT NULL,
  `Status` int(11) DEFAULT NULL,
  `TenantId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreateDt` datetime DEFAULT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `LastDt` datetime DEFAULT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of UC_Account
-- ----------------------------
INSERT INTO `UC_Account` VALUES ('310acdce-5bf7-4aca-8651-b054f71328b1', '400000001', 'Test2@18616790017', null, null, 'AQAAAAEAACcQAAAAEJht8DQUpn98i8zSMyYj0MSvVTXNunHZTAQHaR70IWeHpeJNTe606jieCmGfL9UZRw==', '0', '0', null, null, '0', null, '', 'TEST2@18616790017', '0', '0', '2018-10-25 11:03:34', '0', null, null, '00000000-0000-0000-0000-000000000000', '1', '127.0.0.2', '2018-10-25 19:03:29', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-10-25 19:03:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 19:03:29', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `UC_Account` VALUES ('5f91e76d-88de-4195-a6c2-8fd4feea4b71', '400000000', 'Test1@18616790017', null, null, 'AQAAAAEAACcQAAAAEFdKYRKjml15L+RyLpqkQFZaGoy3hB+8f31XPK7Bi3Vv5/oQRCX3puEyIr3NZYVDiA==', '0', '0', null, null, '0', null, '', 'TEST1@18616790017', '0', '0', '2018-10-25 05:43:18', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `UC_Account` VALUES ('79e775ec-c1f2-4865-883f-82d8ee777468', '100000000', 'Admin', '18600000000', null, 'AQAAAAEAACcQAAAAEP+xGn28GHEioG6RKhZFwYN42w3g0zy1uViTgVcEr9IewzqYIZH691pNmszXaVRL2w==', '1', '0', null, null, '0', null, '', 'ADMIN', '0', '0', '2018-06-11 01:19:48', '0', null, null, '00000000-0000-0000-0000-000000000000', '1508', '127.0.0.1', '2018-10-26 16:33:58', '2', '1', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-10-26 16:33:58', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `UC_Account` VALUES ('afba732c-474f-4475-8dd3-d05aaeaf860f', '300000000', 'AUTO_AFBA732C474F44758DD3D05AAEAF860F', '18616790017', null, 'AQAAAAEAACcQAAAAEP5a6URMx4lh8BYSQlhqC4OvuQR+bkuWNMB3XDk7FOGnkQ3bGo6cM1+CQ6ugJphTvA==', '1', '0', null, null, '0', null, '', 'AUTO_AFBA732C474F44758DD3D05AAEAF860F', '0', '0', '2018-10-19 08:55:38', '0', null, null, '00000000-0000-0000-0000-000000000000', '128', '127.0.0.2', '2018-10-25 19:02:55', '3', '1', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '2018-10-25 19:02:55', '00000000-0000-0000-0000-000000000000', '0');

-- ----------------------------
-- Table structure for UC_Employe
-- ----------------------------
DROP TABLE IF EXISTS `UC_Employe`;
CREATE TABLE `UC_Employe` (
  `Id` varchar(36) COLLATE utf8_unicode_ci NOT NULL,
  `Organize` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `No` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Phone` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TenantId` longtext COLLATE utf8_unicode_ci,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of UC_Employe
-- ----------------------------
INSERT INTO `UC_Employe` VALUES ('310acdce-5bf7-4aca-8651-b054f71328b1', '18616790017', 'Test2', '2', null, '00000000-0000-0000-0000-000000000000', '2018-10-25 19:03:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 19:03:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `UC_Employe` VALUES ('5f91e76d-88de-4195-a6c2-8fd4feea4b71', '18616790017', 'Test1', '1', null, '00000000-0000-0000-0000-000000000000', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');

-- ----------------------------
-- Table structure for UC_Member
-- ----------------------------
DROP TABLE IF EXISTS `UC_Member`;
CREATE TABLE `UC_Member` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Nickname` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Gender` int(11) DEFAULT NULL,
  `Birthdate` datetime DEFAULT NULL,
  `TenantId` longtext COLLATE utf8_unicode_ci,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of UC_Member
-- ----------------------------
INSERT INTO `UC_Member` VALUES ('310acdce-5bf7-4aca-8651-b054f71328b1', '2', '1', '0001-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '2018-10-25 19:03:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 19:03:18', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `UC_Member` VALUES ('5f91e76d-88de-4195-a6c2-8fd4feea4b71', '1', '1', '0001-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '2018-10-25 13:43:02', 'afba732c-474f-4475-8dd3-d05aaeaf860f', '0');
INSERT INTO `UC_Member` VALUES ('79e775ec-c1f2-4865-883f-82d8ee777468', '管理员', '1', '0001-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '1900-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '1900-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `UC_Member` VALUES ('afba732c-474f-4475-8dd3-d05aaeaf860f', 'AUTO_AFBA732C474F44758DD3D05AAEAF860F', '0', '0001-01-01 00:00:00', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '0');

-- ----------------------------
-- Table structure for UC_Organize
-- ----------------------------
DROP TABLE IF EXISTS `UC_Organize`;
CREATE TABLE `UC_Organize` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `No` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Phone` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TenantId` longtext COLLATE utf8_unicode_ci,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of UC_Organize
-- ----------------------------
INSERT INTO `UC_Organize` VALUES ('afba732c-474f-4475-8dd3-d05aaeaf860f', '18616790017', '', null, '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '0');
