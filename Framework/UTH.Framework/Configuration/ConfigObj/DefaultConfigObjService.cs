namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    /// <summary>
    /// 配置对象默认服务
    /// </summary>
    public class DefaultConfigObjService<TEntity> : IConfigObjService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 缓存Key
        /// </summary>
        const string ConfigObjectCacheFormat = "_CONFIG_OBJECT_{0}";

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object lockObject = new object();

        /// <summary>
        /// 配置文件上次修改时间
        /// </summary>
        private DateTime fileOldDateTime { get; set; }

        /// <summary>
        /// 配置对象对应保存的.config文件名称(保存路由为引擎配置config值/或重写FilePath指定)
        /// </summary>
        public virtual string FileName
        {
            get
            {
                if (VerifyHelper.IsEmpty(fileName))
                {
                    var type = typeof(TEntity);
                    fileName = string.Format("{0}.config", type.Name.Replace("ConfigModel", ""));
                }
                return fileName;
            }
            set { fileName = value; }
        }
        private string fileName = string.Empty;

        /// <summary>
        /// 配置对象对应路径
        /// </summary>
        public virtual string FilePath
        {
            get
            {
                if (VerifyHelper.IsEmpty(filePath))
                {
                    filePath = FilesHelper.GetPath(EngineHelper.Configuration.ConfigPath, FileName);
                }
                return filePath;
            }
            set { filePath = value; }
        }
        private string filePath = string.Empty;

        /// <summary>
        /// 配置节点缓存Key
        /// </summary>
        public virtual string CacheKey
        {
            get
            {
                return string.Format(ConfigObjectCacheFormat, FileName.ToLower());
            }
        }

        /// <summary>
        /// 加载配置对象
        /// </summary>
        /// <returns></returns>
        public TEntity Load(bool checkTime = true)
        {
            lock (lockObject)
            {
                TEntity item = CachingHelper.Get<TEntity>(CacheKey);
                if (item == null)
                {
                    if (!File.Exists(FilePath))
                    {
                        throw new DbxException(EnumCode.路径错误, string.Format(Lang.sysConfigFileError, FilePath));
                    }
                    item = XmlDocumentHelper.Load(typeof(TEntity), FilePath) as TEntity;
                    setCache(item);
                }
                return item;
            }
        }

        /// <summary>
        /// 保存配置对象
        /// </summary>
        /// <returns></returns>
        public bool Save(TEntity entity)
        {
            if (XmlDocumentHelper.Save(entity, FilePath))
            {
                setCache(entity);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 设置文件缓存(文件依赖)
        /// </summary>
        /// <param name="entity"></param>
        private void setCache(TEntity entity)
        {
            CachingHelper.Set<TEntity>(CacheKey, entity, new List<string>() { FilePath });
        }

    }
}
