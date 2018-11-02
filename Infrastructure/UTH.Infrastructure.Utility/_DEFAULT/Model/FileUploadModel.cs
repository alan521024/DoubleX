namespace UTH.Infrastructure.Utility
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;

    /// <summary>
    /// 上传信息
    /// </summary>
    public class FileUploadModel
    {
        /// <summary>
        /// 键值
        /// </summary>
        public Hashtable Items = new Hashtable();

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, object value)
        {
            Items.Add(key, value);
        }

        /// <summary>
        /// 资源类型
        /// </summary>
        public EnumAssetsType AssetsType { get { return EnumsHelper.Get<EnumAssetsType>(IntHelper.Get("assetsType")); } }

        /// <summary>
        /// 文件标识
        /// </summary>
        public string Id { get { return StringHelper.Get(Items["id"]); } }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get { return StringHelper.Get(Items["name"]); } }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get { return StringHelper.Get(Items["type"]); } }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get { return LongHelper.Get(Items["size"]); } }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string Md5 { get { var md5 = StringHelper.Get(Items["md5"]); return md5.IsEmpty() ? Guid.NewGuid().ToString("N") : StringHelper.Get(Items["md5"]); } }

        /// <summary>
        /// 当前分块
        /// </summary>
        public long Chunk { get { return LongHelper.Get(Items["chunk"]); } }

        /// <summary>
        /// 分块总数
        /// </summary>
        public long Chunks { get { return LongHelper.Get(Items["chunks"]); } }

        /// <summary>
        /// 文件最后修改时间
        /// </summary>
        public DateTime LastModifiedDate { get { return DateTimeHelper.Get(Items["lastModifiedDate"]); } }

        /// <summary>
        /// 分块上传完成后自合并
        /// </summary>
        public bool AutoMerge { get { return BoolHelper.Get(Items["autoMerge"]); } }

        /// <summary>
        /// 文件byte
        /// </summary>
        public Byte[] Bytes { get; set; }
    }
}
