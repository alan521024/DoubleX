namespace UTH.Module.Basics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Text;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    /// <summary>
    /// 数据字典业务
    /// </summary>
    public class DictionaryService : ApplicationService, IDictionaryService
    {
        public DictionaryService(IRepository<DictionaryEntity> _repository)
        {
            repository = _repository;
        }

        private readonly IRepository<DictionaryEntity> repository;

        public List<DictionaryEntity> GetAll()
        {
            return repository.Find();
        }

        public int BatchInsert(List<DictionaryEntity> sources)
        {
            if (sources.IsEmpty())
                return 0;
            return repository.Insert(sources);
        }

        public int DeleteByGenres(string[] genres)
        {
            if (genres.IsEmpty())
                return 0;
            //移除所有
            return repository.Delete(x=>true);
        }

    }
}
