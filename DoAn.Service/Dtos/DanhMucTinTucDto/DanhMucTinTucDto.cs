using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.DanhMucTinTucDto
{
    public class DanhMucTinTucDto
    {
        public Guid Id { get; set; }
        public string? MaDanhMuc { get; set; }
        public string? TenDanhMuc { get; set; }
        public bool isHienThi {  get; set; }
    }
}
