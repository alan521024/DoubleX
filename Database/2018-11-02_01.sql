/*
Navicat MySQL Data Transfer

Source Server         : 192.168.1.99
Source Server Version : 50556
Source Host           : 192.168.1.99:53306
Source Database       : UTH_Meeting

Target Server Type    : MYSQL
Target Server Version : 50556
File Encoding         : 65001

Date: 2018-11-02 14:42:13
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
