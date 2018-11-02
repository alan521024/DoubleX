using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MahApps.Metro;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;

namespace UTH.Update.Win
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (!args.IsNull() && args.Length > 3)
            {
                args[3] = CodingHelper.UrlDecode(args[3]);
            }
            
            WpfHelper.SystemLog("UTH.UPDATE.Config", $"{ FilesHelper.GetFilePath(".", "Engine.config") }  //  { FilesHelper.GetFilePath("../", "Engine.config") }");
            WpfHelper.SystemLog("UTH.UPDATE.ARGS", string.Join("|", args));

            //无参退出
            if (args.IsEmpty() || (!args.IsEmpty() && args.Length != 4))
            {
                EngineHelper.GlobalPath = FilesHelper.GetFilePath(".", "Engine.config");
                if (EngineHelper.Configuration.IsDebugger)
                {
                    //调试使用会议系统Code获取应用信息
                    args = new string[] { "900102", "1.0.0.0", "0", System.Windows.Forms.Application.ExecutablePath };
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                EngineHelper.GlobalPath = FilesHelper.GetFilePath("../", "Engine.config");
            }
            
            AppHelper.MainAppCode = args[0];
            AppHelper.MainAppVersion = args[1];
            AppHelper.MainAppProcessIds = args[2];
            AppHelper.MainAppPath = args[3];

            UTH.Update.Win.App app = new UTH.Update.Win.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
