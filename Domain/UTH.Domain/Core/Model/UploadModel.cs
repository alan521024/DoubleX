namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using Castle.DynamicProxy;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 上传信息
    /// </summary>
    public class UploadModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string LastModifiedDate { get; set; }

        public long Size { get; set; }

        public long Chunks { get; set; }

        public long Chunk { get; set; }

        public Byte[] Bytes { get; set; }
    }
}
