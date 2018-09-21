using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Security.Permissions;
using Newtonsoft.Json.Linq;
using NAudio.Wave;
using Autofac;
using UTH.Infrastructure.Resource;
using culture = UTH.Infrastructure.Resource.Culture;
using UTH.Infrastructure.Utility;
using UTH.Framework;
using UTH.Framework.Wpf;
using UTH.Domain;
using UTH.Plug;
using UTH.Plug.Speech;

namespace UTH.Meeting.Server
{
    [SecurityPermission(SecurityAction.Demand)]
    class Program
    {
        [SecurityPermission(SecurityAction.Demand)]
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //isrun
            bool isAppRunning = false;
            var process = Process.GetCurrentProcess();
            Mutex mutex = new Mutex(true, process.ProcessName, out isAppRunning);
            if (!isAppRunning)
            {
                Console.WriteLine(culture.Lang.sysChengXuYiJingYunXingQingWuChongFuCaoZuo);
                Environment.Exit(1);
            }

            //hosting
            var appHosting = new AppHosting();

            //engine
            EngineHelper.Worker.Startup(appHosting);
            EngineHelper.Worker.OnStart();

            // 创建一个IPC信道 注册这个IPC信道. 向信道暴露一个远程对象.
            IpcServerChannel serverChannel = new IpcServerChannel("channel");
            ChannelServices.RegisterChannel(serverChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(ServerMarshalByRefObject), "ServerMarshalByRefObject.rem", WellKnownObjectMode.Singleton);

            #region  打印这个信道的名称. 优先级. URI数组

            Console.WriteLine("The name of the channel is {0}.", serverChannel.ChannelName);
            Console.WriteLine("The priority of the channel is {0}.", serverChannel.ChannelPriority);
            foreach (string uri in ((ChannelDataStore)serverChannel.ChannelData).ChannelUris)
            {
                Console.WriteLine("The channel URI is {0}.", uri);
            }

            #endregion

            Console.WriteLine("UTH.Meeting.Server started.");
            Console.WriteLine("Press ENTER to exit the server.");
            Console.ReadLine();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            EngineHelper.LoggingError(e.ExceptionObject);
            if (!EngineHelper.Configuration.IsDebugger)
            {
                Environment.Exit(0);
            }
        }
    }
}
