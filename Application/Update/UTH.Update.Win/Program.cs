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

            WpfHelper.SystemLog("UTH.UPDATE.INIT", string.Format("CONFIG_DEBUG: {0} CONFIG_RES:{1}",
                FilesHelper.GetPath("Engine.config", isAppWork: true),
                FilesHelper.GetPath("../Engine.config", isAppWork: true)));

            WpfHelper.SystemLog("UTH.UPDATE.ARGS {0}", string.Join("|", args));

            //无参退出
            if (args.IsEmpty() || (!args.IsEmpty() && args.Length != 4))
            {
                EngineHelper.GlobalPath = FilesHelper.GetPath("Engine.config", isAppWork: true);
                if (EngineHelper.Configuration.IsDebugger)
                {
                    //调试使用会议系统Code获取应用信息
                    args = new string[] { "100100", "1.0.0.0", "0", System.Windows.Forms.Application.ExecutablePath };
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                EngineHelper.GlobalPath = FilesHelper.GetPath("../Engine.config", isAppWork: true);
            }


            AppHelper.ExecuteAppCode = args[0];
            AppHelper.ExecuteAppVersion = args[1];
            AppHelper.ExecuteAppProcessIds = args[2];
            AppHelper.ExecuteAppPath = args[3];

            UTH.Update.Win.App app = new UTH.Update.Win.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
