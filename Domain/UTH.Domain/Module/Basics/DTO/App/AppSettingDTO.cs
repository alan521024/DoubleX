namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using FluentValidation;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 应用设置基本信息(DTO)
    /// </summary>
    public class AppSettingDTO : IKeys, IOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ids
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// 用户设置(JSON)
        /// </summary>
        public string UserJson { get; set; }

        /// <summary>
        /// 用户设置信息
        /// </summary>
        public UserSetting User
        {
            get
            {
                if (UserJson.IsEmpty())
                {
                    return new UserSetting();
                }
                return JsonHelper.Deserialize<UserSetting>(UserJson);
            }
        }

        /// <summary>
        /// 应用程序名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 应用程序Code
        /// </summary>

        public string AppCode { get; set; }
    }
}
