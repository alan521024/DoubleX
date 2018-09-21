using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UTH.Infrastructure.Utility
{
    /// <summary>
    /// HTTP 动词
    /// </summary>
    public enum EnumHttpVerb
    {
        DEFAULT, //Default
        GET,//（SELECT）从服务器取出资源（一项或多项）。
        POST,//（CREATE）：在服务器新建一个资源。
        PUT,//（UPDATE）：在服务器更新资源（客户端提供改变后的完整资源）
        PATCH,//（UPDATE）：在服务器更新资源（客户端提供改变的属性）。
        DELETE,//（DELETE）：从服务器删除资源。
    }
}