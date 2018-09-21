namespace UTH.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;

    /// <summary>
    /// 数据字典业务接口
    /// </summary>
    public interface IDictionaryService : IApplicationService
    {
        /// <summary>
        /// 获取所有数据字典内容
        /// </summary>
        /// <returns></returns>
        List<DictionaryEntity> GetAll();

        /// <summary>
        /// 批量增加
        /// </summary>
        int BatchInsert(List<DictionaryEntity> sources);

        /// <summary>
        /// 根据类型删除
        /// </summary>
        int DeleteByGenres(string[] genres);
    }
}
