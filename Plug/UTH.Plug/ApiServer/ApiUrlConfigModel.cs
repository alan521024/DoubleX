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
    /// 基础模块
    /// </summary>
    public class ApiUrlBasics
    {
        #region App

        public string AppGetId { get; set; } = "/api/basics/app/get";
        public string AppGetIds { get; set; } = "/api/basics/app/getids";
        public string AppInsert { get; set; } = "/api/basics/app/insert";
        public string AppUpdate { get; set; } = "/api/basics/app/update";
        public string AppDelete { get; set; } = "/api/basics/app/delete";
        public string AppDeleteId { get; set; } = "/api/basics/app/deleteid";
        public string AppDeleteIds { get; set; } = "/api/basics/app/deleteids";
        public string AppQuery { get; set; } = "/api/basics/app/query";
        public string AppPaging { get; set; } = "/api/basics/app/paging";

        public string AppGetModel { get; set; } = "/api/basics/app/getmodel";

        #endregion
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
    }

    /// <summary>
    /// 用户模块
    /// </summary>
    public class ApiUrlUser
    {
        public string SignIn { get; set; } = "/api/user/account/signin";
        public string SignOut { get; set; } = "/api/user/account/signout";
        public string Refresh { get; set; } = "/api/user/account/refresh";
        public string Regist { get; set; } = "/api/user/account/regist";
        public string FindPwd { get; set; } = "/api/user/account/findpwd";
        public string EditPwd { get; set; } = "/api/user/account/editpwd";

        #region employe

        public string EmployeGetId { get; set; } = "/api/user/employe/get";
        public string EmployeGetIds { get; set; } = "/api/user/employe/getids";
        public string EmployeInsert { get; set; } = "/api/user/employe/insert";
        public string EmployeUpdate { get; set; } = "/api/user/employe/update";
        public string EmployeDelete { get; set; } = "/api/user/employe/delete";
        public string EmployeDeleteId { get; set; } = "/api/user/employe/deleteid";
        public string EmployeDeleteIds { get; set; } = "/api/user/v/deleteids";
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
        #region Sync

        public string MeetingSyncQuery { get; set; } = "/api/meet/meeting/syncquery";

        #endregion

        #region Meeting
        public string MeetingGetCode { get; set; } = "/api/meet/meeting/getbycode";
        public string MeetingGetId { get; set; } = "/api/meet/meeting/get";
        public string MeetingGetIds { get; set; } = "/api/meet/meeting/getids";
        public string MeetingInsert { get; set; } = "/api/meet/meeting/insert";
        public string MeetingUpdate { get; set; } = "/api/meet/meeting/update";
        public string MeetingDelete { get; set; } = "/api/meet/meeting/delete";
        public string MeetingDeleteId { get; set; } = "/api/meet/meeting/deleteid";
        public string MeetingDeleteIds { get; set; } = "/api/meet/v/deleteids";
        public string MeetingQuery { get; set; } = "/api/meet/meeting/query";
        public string MeetingPaging { get; set; } = "/api/meet/meeting/paging";

        #endregion

        #region Record

        public string MeetingRecordGetId { get; set; } = "/api/meet/meetingrecord/get";
        public string MeetingRecordGetIds { get; set; } = "/api/meet/meetingrecord/getids";
        public string MeetingRecordInsert { get; set; } = "/api/meet/meetingrecord/insert";
        public string MeetingRecordUpdate { get; set; } = "/api/meet/meetingrecord/update";
        public string MeetingRecordDelete { get; set; } = "/api/meet/meetingrecord/delete";
        public string MeetingRecordDeleteId { get; set; } = "/api/meet/meetingrecord/deleteid";
        public string MeetingRecordDeleteIds { get; set; } = "/api/meet/v/deleteids";
        public string MeetingRecordQuery { get; set; } = "/api/meet/meetingrecord/query";
        public string MeetingRecordPaging { get; set; } = "/api/meet/meetingrecord/paging";
        public string MeetingRecordAdd { get; set; } = "/api/meet/meetingrecord/add";

        #endregion

        #region Translation

        public string MeetingTranslationGetId { get; set; } = "/api/meet/meetingtranslation/get";
        public string MeetingTranslationGetIds { get; set; } = "/api/meet/meetingtranslation/getids";
        public string MeetingTranslationInsert { get; set; } = "/api/meet/meetingtranslation/insert";
        public string MeetingTranslationUpdate { get; set; } = "/api/meet/meetingtranslation/update";
        public string MeetingTranslationDelete { get; set; } = "/api/meet/meetingtranslation/delete";
        public string MeetingTranslationDeleteId { get; set; } = "/api/meet/meetingtranslation/deleteid";
        public string MeetingTranslationDeleteIds { get; set; } = "/api/meet/v/deleteids";
        public string MeetingTranslationQuery { get; set; } = "/api/meet/meetingtranslation/query";
        public string MeetingTranslationPaging { get; set; } = "/api/meet/meetingtranslation/paging";

        #endregion

        #region Profile

        public string MeetingProfileLoginAccountGet { get; set; } = "/api/meet/meetingprofile/getloginaccountprofile";

        public string MeetingProfileLoginAccountSave { get; set; } = "/api/meet/meetingprofile/saveloginaccountprofile";

        #endregion
    }
}
