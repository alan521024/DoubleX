namespace UTH.Plug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.IO;
    using System.Net;
    using Newtonsoft.Json.Linq;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// App Helper 应用基类
    /// </summary>
    public abstract class AppBaseHelper
    {
        #region 应用程序/版本更新/注册文件/正式/试用/过期

        //major.minor[.build[.修订]]
        //组件使用约定，如下所示︰
        //主要(major)   ︰ 具有相同名称但不同主要版本的程序集不能互换。 更高版本的版本号可能表示无法假定向后兼容性的其中一个产品的主要重写。
        //次要(minor)   ︰ 如果的名称和在两个程序集上的主版本号相同，但的次版本号是不同，这指示目的是要向后兼容性的显著增强。 产品的点版本或产品的完全向后兼容新版本，则可能表示此更高的次版本号。
        //生成(build)   ︰ 生成号的不同表示同一源的重新编译。 处理器、 平台或编译器更改时，可能会使用不同的生成号。
        //修订(Revision)︰ 具有相同名称、 主要和次要版本但不同修订号的程序集旨在是完全可互换。 在修复的以前发布的程序集的安全漏洞的版本中可能会使用更高版本的修订号。
        //                 只有内部版本号或修订号不同的后续版本的程序集视为可对先前版本的修补程序更新。

        /// <summary>
        /// 公钥文件
        /// </summary>
        public static string PublicKeyFilePath { get { return string.Format(@"{0}Assets\License\uth.key", AppDomain.CurrentDomain.BaseDirectory); } }

        /// <summary>
        /// 授权文件
        /// </summary>
        public static string LicenseFilePath { get { return string.Format("{0}Assets\\License\\license.key", AppDomain.CurrentDomain.BaseDirectory); } }

        /// <summary>
        /// 获取应用程序信息
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public static ApplicationModel GetApplication(string appCode = null)
        {
            if (appCode.IsEmpty())
            {
                appCode = EngineHelper.Configuration.AppCode;
            }
            var result = PlugCoreHelper.ApiUrl.Basics.AppGetModel
               .GetApiUrl()
               .SetQuerys(new KeyValueModel("appcode", appCode))
               .GetResult<ApplicationModel>();
            if (result.Code == EnumCode.成功)
            {
                return result.Obj;
            }
            throw new DbxException(result.Code, result.Message);
        }

        /// <summary>
        /// 授权文件信息
        /// </summary>
        /// <param name="licenseFile"></param>
        /// <param name="publicFiles"></param>
        /// <returns></returns>
        public static LicenseModel GetLicenseModel(string licenseFile = null, string publicFile = null)
        {
            if (licenseFile.IsEmpty())
            {
                licenseFile = LicenseFilePath;
            }
            if (publicFile.IsEmpty())
            {
                publicFile = PublicKeyFilePath;
            }

            if (licenseFile.IsEmpty() || publicFile.IsEmpty())
            {
                throw new DbxException(EnumCode.授权文件, Lang.sysShouQuanWenJianCuoWu);
            }

            if (!File.Exists(licenseFile) || !File.Exists(publicFile))
            {
                throw new DbxException(EnumCode.授权文件, Lang.sysShouQuanWenJianCuoWu);
            }

            var model = LicenseFileHelper.GetPemLicense(licenseFile, publicFile);
            if (model.IsNull())
            {
                throw new DbxException(EnumCode.授权文件, Lang.sysShouQuanXinXiCuoWu);
            }

            return model;
        }

        /// <summary>
        /// 程序版本更新
        /// </summary>
        /// <param name="version">当前版本</param>
        /// <param name="model">应用信息</param>
        /// <returns></returns>
        public static EnumUpdateType VersionUpdate(Version version, ApplicationModel model)
        {
            version.CheckNull();
            model.CheckNull();

            model.Versions.CheckNull();
            model.Versions.No.CheckNull();

            var lastVersion = model.Versions.No;

            if (version == lastVersion)
            {
                return EnumUpdateType.Default;
            }
            else
            {
                if (model.Versions.UpdateType == EnumUpdateType.Forced)
                {
                    return EnumUpdateType.Forced;
                }

                if (version.Major != lastVersion.Major || version.Minor != lastVersion.Minor)
                {
                    return EnumUpdateType.Forced;
                }

                if (version.Build != lastVersion.Build || version.Revision != lastVersion.Revision)
                {
                    return EnumUpdateType.Incremental;
                }
            }

            return EnumUpdateType.Default;
        }

        #endregion

        /// <summary>
        /// 根据MD5获取路径
        /// </summary>
        /// <param name="md5"></param>
        /// <param name="fileName"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        public static string GetPathByMd5(string md5, string fileName = null, string basePath = null)
        {
            md5.CheckEmpty();

            if (md5.Length < 4)
                throw new ArgumentException(nameof(md5));

            if (fileName.IsEmpty())
            {
                fileName = md5;
            }

            if (!basePath.IsEmpty())
            {
                basePath = basePath + "/";
            }

            var dir = string.Format("{0}{1}/{2}/{3}", basePath, md5.Substring(0, 1), md5.Substring(1, 1), md5.Substring(2, 2));

            return FilesHelper.GetPath(dir, fileName);
        }
    }
}
