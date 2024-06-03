using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.AppUserRoleRepository
{
    public class AppUserRoleRepository : Repository<AppUserRole>, IAppUserRoleRepository
    {
        public AppUserRoleRepository(DoAnContext context) : base(context)
        {
        }
    }
}
