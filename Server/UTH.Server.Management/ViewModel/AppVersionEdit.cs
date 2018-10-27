namespace UTH.Server.Management
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UTH.Infrastructure.Resource;
    using UTH.Infrastructure.Resource.Culture;
    using UTH.Infrastructure.Utility;
    using UTH.Framework;
    using UTH.Domain;
    using UTH.Plug;

    public class AppVersionEdit
    {
        public List<AppDTO> Apps { get; set; } = new List<AppDTO>();

        public AppVersionDTO Detail { get; set; } = new AppVersionDTO() { UpdateType = EnumUpdateType.Incremental, ReleaseDt = DateTime.Now };
    }
}
