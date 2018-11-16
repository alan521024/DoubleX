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

    public class EmployeViewModel
    {
        public EmployeDTO Employe { get; set; } 

        public OrganizeDTO Organize { get; set; }
    }
}
