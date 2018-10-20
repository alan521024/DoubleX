/*
Navicat MySQL Data Transfer

Source Server         : 192.168.1.99
Source Server Version : 50556
Source Host           : 192.168.1.99:53306
Source Database       : UTH_Meeting

Target Server Type    : MYSQL
Target Server Version : 50556
File Encoding         : 65001

Date: 2018-10-20 22:56:51
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
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810000000', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810010000', '79e775ec-c1f2-4865-883f-82d8ee100100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a820010000', '79e775ec-c1f2-4865-883f-82d8ee200100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010100', '79e775ec-c1f2-4865-883f-82d8ee900101', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010200', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '0', '#', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010201', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.0.0.1', '更新测试', '0', '1900-01-01 00:00:00', '12380662', 'http://localhost:8101/api/app/download', '498783EBC25B416AAB7E12F8A4CC38E0', 'v1001.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');

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
INSERT INTO `MET_Profile` VALUES ('9edb3308-9c75-44bd-a8e2-d93c8e8501ce', '0c7247d0-8882-407d-b34c-efbe1ca671ea', 'zs', 'en', '5', '16', '2018-10-19 10:22:21', '0c7247d0-8882-407d-b34c-efbe1ca671ea', '2018-10-19 10:22:21', '0c7247d0-8882-407d-b34c-efbe1ca671ea', '0');
INSERT INTO `MET_Profile` VALUES ('cbcbafc7-ad09-4c77-81a4-b785cc10fdd9', '830e979e-43d9-4425-8ce8-bea6b381d7b2', 'zs', 'en', '5', '16', '2018-10-19 14:42:49', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '2018-10-19 14:42:49', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '0');

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
INSERT INTO `MET_Record` VALUES ('0b2aacf2-4503-40bd-937a-0c6dcfe4f8a3', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', 'zs', 'en', '这是一。', '0', '2018-10-19 14:56:58', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '2018-10-19 14:56:58', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '0', '74bfe164-e1c0-49fb-a296-7055f957346b');
INSERT INTO `MET_Record` VALUES ('0b693d5f-cb3b-4eff-bf0e-b3fec9a986b8', 'cd068906-8b81-4a90-b320-8327ec438c4b', 'zs', 'en', '测试', '-1', '2018-10-19 14:56:15', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:56:15', '79e775ec-c1f2-4865-883f-82d8ee777468', '0', '22d0fb6c-9962-4982-a1ab-056be7f46f5d');
INSERT INTO `MET_Record` VALUES ('6012f7b1-d165-4457-978c-3fef78cb83c2', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', 'zs', 'en', '这是三。', '0', '2018-10-19 14:57:24', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '2018-10-19 14:57:24', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '0', '476786aa-7d9b-4a57-8f3e-9d17468897d8');
INSERT INTO `MET_Record` VALUES ('91bcd431-0342-4b9d-a0ff-1f1f461c57a3', 'cd068906-8b81-4a90-b320-8327ec438c4b', 'zs', 'en', '测试2', '-1', '2018-10-19 14:56:27', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:56:27', '79e775ec-c1f2-4865-883f-82d8ee777468', '0', 'eafb5445-4251-4477-8005-9fdaa3bece68');
INSERT INTO `MET_Record` VALUES ('d987d128-e70b-496f-be3a-1b5549beffd1', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', 'zs', 'en', '这是二', '-1', '2018-10-19 14:57:11', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:57:11', '79e775ec-c1f2-4865-883f-82d8ee777468', '0', 'a1e0f8a6-d2de-4715-9443-aa77ccb2f3f2');
INSERT INTO `MET_Record` VALUES ('ebda3b4a-ff37-418e-830e-b6bad766f816', 'cd068906-8b81-4a90-b320-8327ec438c4b', 'zs', 'en', '测试', '-1', '2018-10-19 14:52:02', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:52:02', '79e775ec-c1f2-4865-883f-82d8ee777468', '0', '9160dab6-3732-4aaf-8dee-0abbe3eab759');

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
INSERT INTO `MET_Translation` VALUES ('1b54362a-03e5-4360-b823-4160087210d7', 'cd068906-8b81-4a90-b320-8327ec438c4b', '0b693d5f-cb3b-4eff-bf0e-b3fec9a986b8', 'en', 'test', '0', '2018-10-19 14:56:16', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:56:16', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `MET_Translation` VALUES ('1f0dabfc-6029-4af7-b426-a6049009f123', 'cd068906-8b81-4a90-b320-8327ec438c4b', '91bcd431-0342-4b9d-a0ff-1f1f461c57a3', 'en', 'Test 2', '0', '2018-10-19 14:56:27', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:56:27', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `MET_Translation` VALUES ('337d4576-1fd4-44cd-bbf4-17b342959d50', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', '6012f7b1-d165-4457-978c-3fef78cb83c2', 'en', 'This is three.', '0', '2018-10-19 14:57:24', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '2018-10-19 14:57:24', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '0');
INSERT INTO `MET_Translation` VALUES ('7ad07bc6-ed54-4bb3-8481-3d5be1f54c51', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', 'd987d128-e70b-496f-be3a-1b5549beffd1', 'en', 'This is two', '0', '2018-10-19 14:57:12', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-10-19 14:57:12', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `MET_Translation` VALUES ('9cd6d076-7962-4e40-ad51-a687e3b359dc', 'b24c2d68-05c9-4770-93a7-cefc6db3fc7e', '0b2aacf2-4503-40bd-937a-0c6dcfe4f8a3', 'en', 'This is one.', '0', '2018-10-19 14:56:59', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '2018-10-19 14:56:59', '830e979e-43d9-4425-8ce8-bea6b381d7b2', '0');

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
INSERT INTO `UC_Account` VALUES ('79e775ec-c1f2-4865-883f-82d8ee777468', '100000000', 'Admin', '18600000000', null, 'AQAAAAEAACcQAAAAEP+xGn28GHEioG6RKhZFwYN42w3g0zy1uViTgVcEr9IewzqYIZH691pNmszXaVRL2w==', '1', '0', null, null, '0', null, '', 'ADMIN', '0', '0', '2018-06-11 01:19:48', '0', null, null, '00000000-0000-0000-0000-000000000000', '1494', '127.0.0.2', '2018-10-19 16:41:13', '2', '1', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:41:13', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `UC_Account` VALUES ('afba732c-474f-4475-8dd3-d05aaeaf860f', '300000000', 'AUTO_AFBA732C474F44758DD3D05AAEAF860F', '18616790017', null, 'AQAAAAEAACcQAAAAEP5a6URMx4lh8BYSQlhqC4OvuQR+bkuWNMB3XDk7FOGnkQ3bGo6cM1+CQ6ugJphTvA==', '1', '0', null, null, '0', null, '', 'AUTO_AFBA732C474F44758DD3D05AAEAF860F', '0', '0', '2018-10-19 08:55:38', '0', null, null, '00000000-0000-0000-0000-000000000000', '1', '127.0.0.2', '2018-10-19 16:55:26', '3', '1', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:20', '00000000-0000-0000-0000-000000000000', '2018-10-19 16:55:26', '00000000-0000-0000-0000-000000000000', '0');

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
