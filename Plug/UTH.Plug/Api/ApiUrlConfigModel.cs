namespace UTH.Plug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using System.Xml.Serialization;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    //***********************
    //
    // (根节点不使用节点Attribute)
    // 区别：<Name></Name> / <item Name="" />
    //
    //***********************

    /// <summary>
    /// Api地址配置
    /// </summary>
    [Serializable]
    [XmlRoot("ApiUrl")]
    public partial class ApiUrlConfigModel
    {
        /// <summary>
        /// 根地址
        /// </summary>
        public virtual string Root { get; set; } = "http://";

        /// <summary>
        /// 基础信息
        /// </summary>
        public virtual ApiUrlBasics Basics { get; set; } = new ApiUrlBasics();

        /// <summary>
        /// 核心功能
        /// </summary>
        public virtual ApiUrlCore Core { get; set; } = new ApiUrlCore();

        /// <summary>
        /// 用户模块
        /// </summary>
        public virtual ApiUrlUser User { get; set; } = new ApiUrlUser();

        /// <summary>
        /// 文档模块
        /// </summary>
        public virtual ApiUrlDocument Document { get; set; } = new ApiUrlDocument();

        /// <summary>
        /// 会议模块
        /// </summary>
        public virtual ApiUrlMeeting Meeting { get; set; } = new ApiUrlMeeting();
    }

    /// <summary>
    /// 核心功能
    /// </summary>
    public class ApiUrlCore
    {
        #region Captcha

        public string CaptchaSend { get; set; } = "/api/core/captcha/send";
        public string CaptchaVerify { get; set; } = "/api/core/captcha/verify";
        public string CaptchaRemove { get; set; } = "/api/core/captcha/remove";

        #endregion

        #region Notify

        public string NotifySend { get; set; } = "/api/core/notify/send";

        #endregion

        #region Assets

        public string AssetsCheck { get; set; } = "/api/assets/check";

        public string AssetsDownload { get; set; } = "/api/assets/download";

        public string AssetsUpload { get; set; } = "/api/assets/upload";

        public string AssetsMerge { get; set; } = "/api/assets/merge";

        #endregion
    }

    /// <summary>
    /// 基础模块
    /// </summary>
    public class ApiUrlBasics
    {
        #region App

        public string AppDetail { get; set; } = "/api/basics/app/detail";
        public string AppGetModel { get; set; } = "/api/basics/app/getmodel";

        public string AppGetId { get; set; } = "/api/basics/app/get";
        public string AppInsert { get; set; } = "/api/basics/app/insert";
        public string AppUpdate { get; set; } = "/api/basics/app/update";
        public string AppDelete { get; set; } = "/api/basics/app/delete";
        public string AppQuery { get; set; } = "/api/basics/app/query";
        public string AppPaging { get; set; } = "/api/basics/app/paging";

        #endregion

        #region version

        public string AppVersionGetId { get; set; } = "/api/basics/appversion/get";
        public string AppVersionInsert { get; set; } = "/api/basics/appversion/insert";
        public string AppVersionUpdate { get; set; } = "/api/basics/appversion/update";
        public string AppVersionDelete { get; set; } = "/api/basics/appversion/delete";
        public string AppVersionQuery { get; set; } = "/api/basics/appversion/query";
        public string AppVersionPaging { get; set; } = "/api/basics/appversion/paging";

        #endregion
    }

    /// <summary>
    /// 用户模块
    /// </summary>
    public class ApiUrlUser
    {
        #region Account

        public string SignIn { get; set; } = "/api/user/account/signin";
        public string SignOut { get; set; } = "/api/user/account/signout";
        public string Refresh { get; set; } = "/api/user/account/refresh";
        public string Regist { get; set; } = "/api/user/account/regist";
        public string FindPwd { get; set; } = "/api/user/account/findpwd";
        public string EditPwd { get; set; } = "/api/user/account/editpwd";
        public string CheckName { get; set; } = "/api/user/account/checkname";

        public string AccountGetId { get; set; } = "/api/user/account/get";
        public string AccountUpdate { get; set; } = "/api/user/account/update";
        public string AccountQuery { get; set; } = "/api/user/account/query";
        public string AccountPaging { get; set; } = "/api/user/account/paging";

        #endregion

        #region member

        public string MemberGetId { get; set; } = "/api/user/member/get";
        public string MemberUpdate { get; set; } = "/api/user/member/update";
        public string MemberQuery { get; set; } = "/api/user/Member/query";
        public string MemberPaging { get; set; } = "/api/user/Member/paging";

        #endregion

        #region Organize

        public string OrganizeGetId { get; set; } = "/api/user/organize/get";
        public string OrganizeUpdate { get; set; } = "/api/user/organize/update";
        public string OrganizeQuery { get; set; } = "/api/user/organize/query";
        public string OrganizePaging { get; set; } = "/api/user/organize/paging";

        #endregion

        #region employe

        public string EmployeGetId { get; set; } = "/api/user/employe/get";
        public string EmployeInsert { get; set; } = "/api/user/employe/insert";
        public string EmployeUpdate { get; set; } = "/api/user/employe/update";
        public string EmployeDelete { get; set; } = "/api/user/employe/delete";
        public string EmployeQuery { get; set; } = "/api/user/employe/query";
        public string EmployePaging { get; set; } = "/api/user/employe/paging";

        #endregion
    }

    /// <summary>
    /// 文档模块
    /// </summary>
    public class ApiUrlDocument
    {
        public string ArticleGet { get; set; } = "/api/cms/article/getarticle";
    }

    /// <summary>
    /// 会议模块
    /// </summary>
    public class ApiUrlMeeting
    {
        #region Meeting

        public string MeetingGetCode { get; set; } = "/api/meet/meeting/getbycode";
        public string MeetingSyncQuery { get; set; } = "/api/meet/meeting/syncquery";

        public string MeetingGetId { get; set; } = "/api/meet/meeting/get";
        public string MeetingInsert { get; set; } = "/api/meet/meeting/insert";
        public string MeetingUpdate { get; set; } = "/api/meet/meeting/update";
        public string MeetingDelete { get; set; } = "/api/meet/meeting/delete";
        public string MeetingQuery { get; set; } = "/api/meet/meeting/query";
        public string MeetingPaging { get; set; } = "/api/meet/meeting/paging";

        #endregion

        #region Record

        public string MeetingRecordAdd { get; set; } = "/api/meet/meetingrecord/add";
        public string MeetingRecordCreate { get; set; } = "/api/meet/meetingrecord/create";

        public string MeetingRecordGetId { get; set; } = "/api/meet/meetingrecord/get";
        public string MeetingRecordInsert { get; set; } = "/api/meet/meetingrecord/insert";
        public string MeetingRecordUpdate { get; set; } = "/api/meet/meetingrecord/update";
        public string MeetingRecordDelete { get; set; } = "/api/meet/meetingrecord/delete";
        public string MeetingRecordQuery { get; set; } = "/api/meet/meetingrecord/query";
        public string MeetingRecordPaging { get; set; } = "/api/meet/meetingrecord/paging";

        #endregion

        #region Translation

        public string MeetingTranslationGetId { get; set; } = "/api/meet/meetingtranslation/get";
        public string MeetingTranslationInsert { get; set; } = "/api/meet/meetingtranslation/insert";
        public string MeetingTranslationUpdate { get; set; } = "/api/meet/meetingtranslation/update";
        public string MeetingTranslationDelete { get; set; } = "/api/meet/meetingtranslation/delete";
        public string MeetingTranslationQuery { get; set; } = "/api/meet/meetingtranslation/query";
        public string MeetingTranslationPaging { get; set; } = "/api/meet/meetingtranslation/paging";

        #endregion

        #region Profile

        public string MeetingProfileLoginAccountGet { get; set; } = "/api/meet/meetingprofile/getloginaccountprofile";
        public string MeetingProfileLoginAccountSave { get; set; } = "/api/meet/meetingprofile/saveloginaccountprofile";

        #endregion
    }
}
