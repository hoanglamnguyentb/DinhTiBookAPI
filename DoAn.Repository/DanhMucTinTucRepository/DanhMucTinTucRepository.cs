using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.SanPhamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.DanhMucTinTucRepository
{
    public class DanhMucTinTucRepository : Repository<DanhMucTinTuc>, IDanhMucTinTucRepository
    {
        public DanhMucTinTucRepository(DoAnContext context) : base(context) { }
    }
}

