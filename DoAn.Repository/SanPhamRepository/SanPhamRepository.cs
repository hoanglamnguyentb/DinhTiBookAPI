using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.SanPhamRepository
{
    internal class SanPhamRepository:Repository<SanPham>, ISanPhamRepository
    {
        public SanPhamRepository(DoAnContext context) : base(context) { }
    }
}
