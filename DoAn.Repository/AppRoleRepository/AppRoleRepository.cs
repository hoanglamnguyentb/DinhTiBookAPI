using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.AppRoleRepository
{
    public class AppRoleRepository : Repository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(DoAnContext context) : base(context)
        {
        }
    }
}
