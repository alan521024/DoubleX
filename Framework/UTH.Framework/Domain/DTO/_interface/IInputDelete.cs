namespace UTH.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;

    public interface IInputDelete<TId>
    {
        TId Id { get; set; }
        List<TId> Ids { get; set; }
    }

    public interface IInputDelete : IInputDelete<Guid> { }
}
