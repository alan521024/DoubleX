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
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 翻译结果
    /// </summary>
    public class OpenApiTranslationOutput
    {
        //返回JSON
        //{
        //    "SrcLang": "zs",
        //    "SrcText": "你好\n世界",
        //    "TgtLang": null,
        //    "TgtText": null,
        //    "Translations": [
        //        {
        //            "SrcLang": null,
        //            "SrcText": null,
        //            "TgtLang": "en",
        //            "TgtText": "Hello\n The world"
        //        },
        //        {
        //            "SrcLang": null,
        //            "SrcText": null,
        //            "TgtLang": "de",
        //            "TgtText": "Hallo.\n Die Welt."
        //        }
        //    ],
        //    "Code": 0,
        //    "Message": null
        //}

        /// <summary>
        /// 返回结果代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 源文语言
        /// </summary>
        public string SrcLang { get; set; }

        /// <summary>
        /// 源文内容
        /// </summary>
        public string SrcText { get; set; }

        /// <summary>
        /// 译文语言
        /// </summary>
        public string TgtLang { get; set; }

        /// <summary>
        /// 译文内容
        /// </summary>
        public string TgtText { get; set; }

        /// <summary>
        /// 译文内容
        /// </summary>
        public List<OpenApiTranslationModel> Translations { get; set; }

    }

    public class OpenApiTranslationModel
    {
        /// <summary>
        /// 源文语言
        /// </summary>
        public string SrcLang { get; set; }

        /// <summary>
        /// 源文内容
        /// </summary>
        public string SrcText { get; set; }

        /// <summary>
        /// 译文语言
        /// </summary>
        public string TgtLang { get; set; }

        /// <summary>
        /// 译文内容
        /// </summary>
        public string TgtText { get; set; }
    }
}
