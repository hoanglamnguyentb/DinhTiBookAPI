using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.NhaXuatBanRepository
{
    internal class NhaXuatBanRepository : Repository<NhaXuatBan>, INhaXuatBanRepository
    {
        public NhaXuatBanRepository(DoAnContext context) : base(context) { }
    }
}
