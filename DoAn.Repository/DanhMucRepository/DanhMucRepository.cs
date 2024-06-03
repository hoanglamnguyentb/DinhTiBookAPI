using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.DanhMucRepository
{
    internal class DanhMucRepository:Repository<DanhMuc>, IDanhMucRepository
    {
        public DanhMucRepository(DoAnContext context) : base(context) { }
    }
}
