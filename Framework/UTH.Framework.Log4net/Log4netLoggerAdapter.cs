namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using log4net.Appender;
    using log4net.Config;
    using log4net.Core;
    using log4net.Filter;
    using log4net.Layout;
    using log4net.Repository;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    public class Log4netLoggerAdapter : AbsLoggerAdapter
    {
        string repositoryName = "NETCoreRepository";
        ILoggerRepository repository { get; }

        /// <summary>
        /// 初始化一个Log4netLoggerAdapter类型的新实例
        /// </summary>
        public Log4netLoggerAdapter()
        {
            repository = log4net.LogManager.CreateRepository(repositoryName);

            string configFile = FilesHelper.GetPath(EngineHelper.Configuration.ConfigPath, "log4net.config");
            if (File.Exists(configFile))
            {
                XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(configFile));
                return;
            }
            RollingFileAppender appender = new RollingFileAppender
            {
                Name = "root",
                File = "Log/default",
                AppendToFile = true,
                LockingModel = new FileAppender.MinimalLock(),
                RollingStyle = RollingFileAppender.RollingMode.Date,
                DatePattern = "yyyyMMdd-HH\".log\"",
                StaticLogFileName = false,
                MaxSizeRollBackups = 10,
                Layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c.%M %t %w %n%m%n")
                //Layout = new PatternLayout("[%d [%t] %-5p %c [%x] - %m%n]")
            };
            appender.ClearFilters();
            appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Info });
            //PatternLayout layout = new PatternLayout("[%d{yyyy-MM-dd HH:mm:ss.fff}] %c.%M %t %n%m%n");
            //appender.Layout = layout;
            BasicConfigurator.Configure(repository, appender);
            appender.ActivateOptions();
        }

        #region Overrides of LoggerAdapterBase

        /// <summary>
        /// 创建指定名称的缓存实例
        /// </summary>
        /// <param name="name">指定名称</param>
        /// <returns></returns>
        protected override ILogger Create(string name)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(repositoryName, name);
            return new Log4netLogger(log);
        }

        #endregion
    }
}
