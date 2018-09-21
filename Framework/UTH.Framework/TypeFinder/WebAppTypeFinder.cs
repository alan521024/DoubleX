namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Reflection;
    using System.Diagnostics;
    using System.IO;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// WebAppTypeFinder(Web应用程序类型查找器)
    /// 继承自AppDomainTypeFinder。比AppDomainTypeFinder多支持了Web项目的类型查找
    /// WebAppTypeFinder 这个类就可以对网站下面BIN文件夹所有的DLL文件进行反射查找程序集，可以根据类型 也可以跟特性查找，总之性能不错，大家可以拷贝代码 进行测试
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder, ITypeFinder
    {
    }
}
