/*
Navicat MySQL Data Transfer

Source Server         : 192.168.1.99
Source Server Version : 50556
Source Host           : 192.168.1.99:53306
Source Database       : UTH_Meeting

Target Server Type    : MYSQL
Target Server Version : 50556
File Encoding         : 65001

Date: 2018-11-18 13:42:33
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
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee100000', '接口中心', '4', '100000', '', '', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-11-14 17:17:38', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee100100', '更新程序', '4', '100100', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee200100', '授权工具', '4', '200100', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee900101', '会议系统微信端', '4', '900101', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_App` VALUES ('79e775ec-c1f2-4865-883f-82d8ee900102', '会议系统客户端', '4', '900102', null, null, '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');

-- ----------------------------
-- Table structure for BAS_AppSetting
-- ----------------------------
DROP TABLE IF EXISTS `BAS_AppSetting`;
CREATE TABLE `BAS_AppSetting` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `AppId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `UserJson` text COLLATE utf8_unicode_ci NOT NULL,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of BAS_AppSetting
-- ----------------------------
INSERT INTO `BAS_AppSetting` VALUES ('3b428783-f458-4ef6-9c34-1240e9f2bcb5', '79e775ec-c1f2-4865-883f-82d8ee100100', '{\"member\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"origin\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":\"0\"},\"employe\":{\"allowLogin\":true,\"allowRegist\":false,\"employeLimit\":0}}', '2018-11-06 11:22:54', '00000000-0000-0000-0000-000000000000', '2018-11-14 17:17:30', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppSetting` VALUES ('b207ebe3-8c39-409e-84d3-87c88dbb08d9', '79e775ec-c1f2-4865-883f-82d8ee900101', '{\"member\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"origin\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":\"0\"},\"employe\":{\"allowLogin\":true,\"allowRegist\":false,\"employeLimit\":0}}', '2018-11-06 11:23:23', '00000000-0000-0000-0000-000000000000', '2018-11-14 17:15:52', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppSetting` VALUES ('c019c023-4ec6-41d5-99e0-fa83ab73ebb6', '79e775ec-c1f2-4865-883f-82d8ee100000', '{\"member\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"origin\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":\"5\"},\"employe\":{\"allowLogin\":false,\"allowRegist\":false,\"employeLimit\":0}}', '2018-11-06 09:53:36', '00000000-0000-0000-0000-000000000000', '2018-11-14 17:15:37', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppSetting` VALUES ('e509b079-e14c-45cc-a82c-1be9d716932c', '00000000-0000-0000-0000-000000000000', '{\"member\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"origin\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"employe\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0}}', '2018-11-06 16:48:59', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-06 16:48:59', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppSetting` VALUES ('ecef02c2-9c65-428e-b8c3-986ca65eb459', '79e775ec-c1f2-4865-883f-82d8ee900102', '{\"member\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"origin\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0},\"employe\":{\"allowLogin\":true,\"allowRegist\":true,\"employeLimit\":0}}', '2018-11-16 13:46:29', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 13:46:29', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');

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
INSERT INTO `BAS_AppVersion` VALUES ('00fedee6-ce95-4077-9c26-ad0b3bd8dde4', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.0.0.1', 'aaaaaaaaaaaaaaaa', '2', '2018-10-29 16:35:33', 'e32c8cd23b6d560f7b54dc3e1967f703', 'v1.2.0.2.zip', '2018-10-26 16:35:45', '00000000-0000-0000-0000-000000000000', '2018-11-15 10:15:29', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810000000', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a810010000', '79e775ec-c1f2-4865-883f-82d8ee100100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', 'e32c8cd23b6d560f7b54dc3e1967f703', 'v1.2.0.2.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-11-15 14:15:21', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a820010000', '79e775ec-c1f2-4865-883f-82d8ee200100', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010100', '79e775ec-c1f2-4865-883f-82d8ee900101', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010200', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.0.0.0', '初始版本', '0', '1900-01-01 00:00:00', '00000000000000000000000000000000', 'v1000.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('35a6449a-cd3a-48c7-82f3-b1a890010201', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.1.0.0', '当前版本在线更新被中止，请联系客服获取新版。', '0', '1900-01-01 00:00:00', '498783EBC25B416AAB7E12F8A4CC38E0', 'v1100.zip', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('694252e9-62e3-4e55-8544-7e851de2f4ce', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.2.0.0', '更新测试', '2', '2018-11-02 15:49:07', 'e32c8cd23b6d560f7b54dc3e1967f703', 'v1.2.0.2.zip', '2018-11-02 15:50:52', '00000000-0000-0000-0000-000000000000', '2018-11-02 17:35:37', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('86364e94-5498-4481-a8d9-d2bbe0e867ed', '79e775ec-c1f2-4865-883f-82d8ee100000', '1.2.0.1', '测试2222', '2', '2018-11-02 15:52:06', 'e32c8cd23b6d560f7b54dc3e1967f703', 'v1.2.0.2.zip', '2018-11-02 15:52:57', '00000000-0000-0000-0000-000000000000', '2018-11-02 17:10:51', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `BAS_AppVersion` VALUES ('f594147a-90ca-4f6f-9f89-cb50daed2e98', '79e775ec-c1f2-4865-883f-82d8ee900102', '1.2.0.1', '更新识别速度，修复版本更新工具。', '2', '2018-11-16 13:49:01', '37a68a90c308660eb2f7c269b5462e55', '1.2.0.1.zip', '2018-11-16 13:49:58', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 13:50:18', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');

-- ----------------------------
-- Table structure for BAS_Assets
-- ----------------------------
DROP TABLE IF EXISTS `BAS_Assets`;
CREATE TABLE `BAS_Assets` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `AssetsType` int(11) NOT NULL DEFAULT '0',
  `Name` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `MD5` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Size` int(11) DEFAULT NULL,
  `Type` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AccountId` varchar(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AppCode` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of BAS_Assets
-- ----------------------------
INSERT INTO `BAS_Assets` VALUES ('08b8689b-18c9-43fa-8048-3a507fa08741', '0', '用户数据.xlsx', 'fce16bf8f5653e341e289e819853a8fe', '10708', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('0dc6bd12-f8f4-4024-8aac-dd33f050ac9c', '0', '用户数据.xlsx', 'f9fb9389c0af7216941aee75f90068d0', '10735', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('126e2a06-930d-42b4-a292-204d17d0d0fb', '0', '用户数据.xlsx', 'fce16bf8f5653e341e289e819853a8fe', '10708', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('162e0cbf-14ee-49aa-a83f-2b14f79c1b6c', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('236af665-5dba-4773-aa5a-b35c90c7c21f', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('28c29add-7644-4bbf-8d1a-8148c96d2292', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('29454130-b96c-4498-8f45-d2413656dda0', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('367942f8-e3f7-4d56-ac74-555d02398f13', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('3e8e883e-e5e8-4682-9469-e3bee71dd542', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('3fc717b1-ff08-43cf-be39-1c39db78faa1', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('4a64f95e-13bb-48d5-b4e7-66c18761c8c0', '0', '用户数据.xlsx', 'fce16bf8f5653e341e289e819853a8fe', '10708', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('5c8caffc-86fe-4668-80a6-5f3189d48123', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('603e468c-4620-4bfa-b815-cbbdaf4dc9bd', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('65d1917c-43af-45b8-b1aa-d247247c1350', '0', '1.2.0.1.zip', '37a68a90c308660eb2f7c269b5462e55', '30357271', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('68420517-5fcf-48ff-abd9-45d84994b0f6', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('77e08220-57a5-4f67-88b5-ea204a3a3d5b', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('7d9bf97c-c68f-41ea-bd46-bc3dc4b831cb', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('8507780f-9931-4347-a569-0d69532a92b3', '0', '用户数据.xlsx', 'f9fb9389c0af7216941aee75f90068d0', '10735', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('872a1fb3-52e5-4a68-94f7-70da7bb3d068', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('911cb0eb-14d2-4c67-9345-43414445bacd', '0', '用户数据.xlsx', '9015fd0d7f9a0f3d3cd66a55873d7d30', '10701', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('93f13277-c67c-485d-865b-a7cda9c9ebe3', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('9447fe34-a6e4-4843-ad97-551316d3bd40', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('987906a2-05d4-4ec7-8a31-8f1a29cf7705', '0', '用户数据.xlsx', 'fce16bf8f5653e341e289e819853a8fe', '10708', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('9b729a5a-f74c-41ac-b93e-96b19742237d', '0', '用户数据.xlsx', 'f9fb9389c0af7216941aee75f90068d0', '10735', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('a3ac4f9b-e0aa-4131-a9ca-9ecfc9c99cea', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('a6a2e6b7-cd56-4bd7-9e2b-152c2fc888e6', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('aabfd0a9-bea1-4455-bda3-39a377e902ca', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('af558cd6-0f88-4569-9756-449d2ee98422', '0', '用户数据.xlsx', 'f9fb9389c0af7216941aee75f90068d0', '10735', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('b165c44a-d6ac-48a5-a13c-62dcb0bbc471', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('bc645527-7d8d-454e-be6f-630576dd52c1', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('bd885855-c739-440c-93eb-7eb57e0f5cb5', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('c0acb367-7db8-4d22-b53a-ffe17164d941', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('dd2c148d-bcdf-4a43-9eba-c2961ee791d3', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('debfcbbd-5f30-4599-9e60-3f3610c7a6db', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('ed2e9bf6-4cd9-46b9-b26c-7d5961c96b51', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('f0ebf132-9834-42e3-a3b9-9431cc3ebc17', '0', '浙江越秀外国语学院语料领域划分.xlsx', '976c0ab6dc39c607f896320234127b9f', '11462', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('f354693b-d5da-4ae0-a64a-2d561d7e7c86', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('f3987470-5aff-42ed-952b-41c7a2bbeb87', '0', 'v1.2.0.2.zip', 'e32c8cd23b6d560f7b54dc3e1967f703', '30336146', '.zip', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('fed937fb-fb8f-4814-96a2-fe2fb5bc02b9', '0', '用户数据.xlsx', 'a35665df2dd05dd3867eaf0c9b0a05e0', '10761', '.xlsx', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');
INSERT INTO `BAS_Assets` VALUES ('ff7cfd86-f1f3-48b9-9e6d-3636a79f9592', '0', '20180706.txt', '25b0b79be4f1a0b94c4e57bd24549995', '4970', '.txt', '79e775ec-c1f2-4865-883f-82d8ee777468', '100200', '0');

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
INSERT INTO `MET_Meeting` VALUES ('0f4764e7-f94e-4210-9c48-4a9c63bdd767', '100001', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2500,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-11-16 10:07:28', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:28', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `MET_Meeting` VALUES ('8e75e782-266a-4a1c-aacb-e7bd1b61cffe', '100002', '芝麻秘语-多语言即时翻译会议系统', '芝麻秘语-多语言及时翻译会议系统是一款支持当多语言的在线会议系统。支持全球主流的16种语言（汉语、英语、阿拉伯语、葡萄牙语、日语、泰语、西班牙语、印尼语）同声翻译：即时将当前获取到的语言转换成多种目标语言，并以文字的方式展现出来。 同时，通过扫描二维码，系统支持与手机的在线系统。', '{\"speed\":5,\"rate\":16000,\"channel\":1,\"bitDepth\":16,\"bufferMilliseconds\":150,\"sentenceMilliseconds\":2500,\"byteLength\":4800,\"profileId\":\"00000000-0000-0000-0000-000000000000\",\"accountId\":\"00000000-0000-0000-0000-000000000000\",\"sourceLang\":\"zs\",\"targetLangs\":\"en\",\"accent\":\"mandarin\",\"fontSize\":16,\"encode\":\"UTF-8\",\"remoteAddress\":\"ipc://channel/ServerRemoteObject.rem\"}', '2018-11-16 13:05:29', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 13:05:29', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');

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
INSERT INTO `MET_Profile` VALUES ('8e8ede3b-aada-46e3-b821-c5b2fd2a9352', 'fc7e2655-2688-4684-9cf8-bc223e158931', 'zs', 'en', '5', '16', '2018-11-16 10:01:19', 'fc7e2655-2688-4684-9cf8-bc223e158931', '2018-11-16 10:01:19', 'fc7e2655-2688-4684-9cf8-bc223e158931', '0');
INSERT INTO `MET_Profile` VALUES ('bf97b40a-4911-4ca8-b4f9-d718da2ee732', '5f95d78b-a398-437a-b8d8-ae418214f5b8', 'zs', 'en', '5', '16', '2018-11-16 10:07:28', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:28', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');

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
INSERT INTO `MET_Record` VALUES ('5863934a-4ea6-461f-87dd-733fc7541961', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', 'zs', 'en', '会议测试会一次是会，依次是。', '0', '2018-11-16 10:07:41', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:41', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0', '891b82f7-bdae-47ce-b952-2199dda22571');
INSERT INTO `MET_Record` VALUES ('58b7bb23-3eca-4ef6-a6d0-bf7cc7d733d3', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', 'zs', 'en', '这是一次会议测试内容是。', '0', '2018-11-16 10:07:51', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:51', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0', '2dde6b04-5865-4c94-b729-8bcabe108607');
INSERT INTO `MET_Record` VALUES ('6f73c6d7-5459-493c-92e6-2c5d768e7f19', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', 'zs', 'en', '麦克风。', '0', '2018-11-16 10:08:01', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:08:01', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0', 'f2351940-585b-413e-b170-9bc3fa04c098');
INSERT INTO `MET_Record` VALUES ('76ddb4db-11db-422f-9549-cf1239a5187e', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', 'zs', 'en', '当前的汇率是好事。', '0', '2018-11-16 10:07:59', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:59', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0', '518d9cb2-e050-4588-b829-6ff42259e2f1');
INSERT INTO `MET_Record` VALUES ('8b46e3cc-3e0b-46f3-9b4f-d75d996b1d05', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', 'zs', 'en', '另一个用户登录测试。', '0', '2018-11-16 10:08:58', 'fc7e2655-2688-4684-9cf8-bc223e158931', '2018-11-16 10:08:58', 'fc7e2655-2688-4684-9cf8-bc223e158931', '0', 'c6b81795-0e71-4bcd-bdb3-f9fc72e2ede8');

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
INSERT INTO `MET_Translation` VALUES ('279892a0-ca9b-49ef-a058-32bd54976a27', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', '6f73c6d7-5459-493c-92e6-2c5d768e7f19', 'en', 'Microphone.', '0', '2018-11-16 10:08:02', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:08:02', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `MET_Translation` VALUES ('7d9baa37-1f7f-49dc-8266-b62372511adf', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', '76ddb4db-11db-422f-9549-cf1239a5187e', 'en', 'The current exchange rate is a good thing.', '0', '2018-11-16 10:08:00', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:08:00', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `MET_Translation` VALUES ('bc6c9c84-8a3c-4c5d-b7a9-8af758df9fee', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', '8b46e3cc-3e0b-46f3-9b4f-d75d996b1d05', 'en', 'Another user logs in to the test.', '0', '2018-11-16 10:08:58', 'fc7e2655-2688-4684-9cf8-bc223e158931', '2018-11-16 10:08:58', 'fc7e2655-2688-4684-9cf8-bc223e158931', '0');
INSERT INTO `MET_Translation` VALUES ('dfea2557-caf1-40af-9234-f1fc164e27ce', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', '5863934a-4ea6-461f-87dd-733fc7541961', 'en', 'The meeting test will be held once and then.', '0', '2018-11-16 10:07:43', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:43', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `MET_Translation` VALUES ('ed27beac-06c3-4dd6-9e08-48f1bb44e610', '0f4764e7-f94e-4210-9c48-4a9c63bdd767', '58b7bb23-3eca-4ef6-a6d0-bf7cc7d733d3', 'en', 'This is a meeting. Test content is.', '0', '2018-11-16 10:07:51', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:07:51', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');

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
INSERT INTO `UC_Account` VALUES ('5f95d78b-a398-437a-b8d8-ae418214f5b8', '300000000', 'company1', null, null, 'AQAAAAEAACcQAAAAEM1X+kgRUdBbAWQk6CcOKT1zoaLOY/0pgYirl+MpANiCYSnuTpi2dRqqjMr6GPQCaQ==', '0', '0', null, null, '0', null, '', 'COMPANY1', '0', '0', '2018-11-16 01:24:29', '0', null, null, '00000000-0000-0000-0000-000000000000', '11', '127.0.0.2', '2018-11-16 13:29:07', '3', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:24:28', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 13:29:07', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `UC_Account` VALUES ('79e775ec-c1f2-4865-883f-82d8ee777468', '100000000', 'Admin', '18600000000', 'admin@admin.com', 'AQAAAAEAACcQAAAAEP+xGn28GHEioG6RKhZFwYN42w3g0zy1uViTgVcEr9IewzqYIZH691pNmszXaVRL2w==', '1', '1', null, null, '0', '超级管理员', 'ADMIN@ADMIN.COM', 'ADMIN', '0', '0', '2018-06-11 01:19:48', '0', null, null, '00000000-0000-0000-0000-000000000000', '1608', '127.0.0.1', '2018-11-16 13:46:18', '1', '1', '00000000-0000-0000-0000-000000000000', '2018-06-11 09:19:43', '00000000-0000-0000-0000-000000000000', '2018-11-16 13:46:18', '00000000-0000-0000-0000-000000000000', '0');
INSERT INTO `UC_Account` VALUES ('8caf36e3-b7d7-45cd-a875-9bf0e155d862', '400000004', 'emp5@company1', null, null, 'AQAAAAEAACcQAAAAEGPZSfQDpkDnqhVOvZ2i9M2h1F0k0k+DYyWzdJ46xr+cFI5pWK01ZqG25+Y1EqyTkA==', '0', '0', null, null, '0', null, '', 'EMP5@COMPANY1', '0', '0', '2018-11-16 01:58:06', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Account` VALUES ('a1a0d18c-e9bc-4b88-827c-80754aed2bc8', '400000005', 'emp6@company1', null, null, 'AQAAAAEAACcQAAAAEK82n/x1D0MF6AJqSkoUpHKyXg+SlSGJj3mmRKGhw9GxqJLE79TwH60XrCdMWwcN/Q==', '0', '0', null, null, '0', null, '', 'EMP6@COMPANY1', '0', '0', '2018-11-16 01:58:06', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Account` VALUES ('dc8926e0-b3de-477c-b09a-988324e11f58', '400000003', 'emp4@company1', null, null, 'AQAAAAEAACcQAAAAEObkjzgOdT9g/EKOtnhc9ARdCVHQULQTeXmq6laykwXcCi+D9XMcyNB6FL9o7s+w/g==', '0', '0', null, null, '0', null, '', 'EMP4@COMPANY1', '0', '0', '2018-11-16 01:58:06', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Account` VALUES ('e28cc704-7834-444c-8439-4d2b50d9e523', '400000000', 'emp1', null, null, 'AQAAAAEAACcQAAAAEDPd/vlojPhs9pHkxa6Y+9QXcA/az27INAuxSKnpD9o3GyyQRCluj9bOmfhCP2+76w==', '0', '0', null, null, '0', null, '', 'EMP1', '0', '0', '2018-11-16 01:25:17', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:25:17', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 09:25:17', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `UC_Account` VALUES ('f60c9673-ae5b-47b8-8112-31be670ca97b', '400000002', 'emp3@company1', null, null, 'AQAAAAEAACcQAAAAEFEdKj52qSxH9QVoh7xakUsymT9256Pyjywz2nLdzQhXPyVjnrr4Y2gGRuP5t8a/Cw==', '0', '0', null, null, '0', null, '', 'EMP3@COMPANY1', '0', '0', '2018-11-16 01:57:24', '0', null, null, '00000000-0000-0000-0000-000000000000', '0', '127.0.0.1', '1900-01-01 00:00:00', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:57:23', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:57:23', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Account` VALUES ('fc7e2655-2688-4684-9cf8-bc223e158931', '400000001', 'emp2@company1', null, null, 'AQAAAAEAACcQAAAAEBhE+BhB6Uq/Zp4lx74Amr+0eC7JTguvIlB6jWp8J7+xeaI6oHaMm2d+Xe1+eWjh+w==', '0', '0', null, null, '0', null, '', 'EMP2@COMPANY1', '0', '0', '2018-11-16 01:57:13', '0', null, null, '00000000-0000-0000-0000-000000000000', '6', '127.0.0.2', '2018-11-16 10:08:39', '4', '1', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:57:12', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 10:08:39', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');

-- ----------------------------
-- Table structure for UC_Employe
-- ----------------------------
DROP TABLE IF EXISTS `UC_Employe`;
CREATE TABLE `UC_Employe` (
  `Id` varchar(36) COLLATE utf8_unicode_ci NOT NULL,
  `Organize` varchar(20) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Code` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
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
INSERT INTO `UC_Employe` VALUES ('8caf36e3-b7d7-45cd-a875-9bf0e155d862', 'company1', 'emp5', 'emp4', null, '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Employe` VALUES ('a1a0d18c-e9bc-4b88-827c-80754aed2bc8', 'company1', 'emp6', 'emp4', null, '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Employe` VALUES ('dc8926e0-b3de-477c-b09a-988324e11f58', 'company1', 'emp4', 'emp4', null, '00000000-0000-0000-0000-000000000000', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:58:05', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Employe` VALUES ('e28cc704-7834-444c-8439-4d2b50d9e523', 'company1', 'emp1', 'emp1', '', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:25:17', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 09:25:17', '79e775ec-c1f2-4865-883f-82d8ee777468', '0');
INSERT INTO `UC_Employe` VALUES ('f60c9673-ae5b-47b8-8112-31be670ca97b', 'company1', 'emp3', 'emp3', null, '00000000-0000-0000-0000-000000000000', '2018-11-16 09:57:23', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:57:23', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');
INSERT INTO `UC_Employe` VALUES ('fc7e2655-2688-4684-9cf8-bc223e158931', 'company1', 'emp2', 'emp2', null, '00000000-0000-0000-0000-000000000000', '2018-11-16 09:57:12', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '2018-11-16 09:57:12', '5f95d78b-a398-437a-b8d8-ae418214f5b8', '0');

-- ----------------------------
-- Table structure for UC_Member
-- ----------------------------
DROP TABLE IF EXISTS `UC_Member`;
CREATE TABLE `UC_Member` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
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

-- ----------------------------
-- Table structure for UC_Organize
-- ----------------------------
DROP TABLE IF EXISTS `UC_Organize`;
CREATE TABLE `UC_Organize` (
  `Id` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Code` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Name` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `Phone` varchar(60) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TenantId` longtext COLLATE utf8_unicode_ci,
  `CreateDt` datetime NOT NULL,
  `CreateId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `LastDt` datetime NOT NULL,
  `LastId` varchar(60) COLLATE utf8_unicode_ci NOT NULL,
  `IsDelete` tinyint(4) NOT NULL,
  `Fax` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AreaCode` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(600) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of UC_Organize
-- ----------------------------
INSERT INTO `UC_Organize` VALUES ('5f95d78b-a398-437a-b8d8-ae418214f5b8', 'company1', 'company1', '', '00000000-0000-0000-0000-000000000000', '2018-11-16 09:24:28', '79e775ec-c1f2-4865-883f-82d8ee777468', '2018-11-16 09:24:28', '79e775ec-c1f2-4865-883f-82d8ee777468', '0', '', null, '');
