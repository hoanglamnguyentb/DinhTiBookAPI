using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.AppUserRepository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(DoAnContext context) : base(context)
        {
        }
    }
}
