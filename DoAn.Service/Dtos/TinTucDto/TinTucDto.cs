using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.TinTucDto
{
    public class TinTucDto
    {
        public Guid Id { get; set; }    
        public string? TieuDe { get; set; }
        public string? DanhMuc { get; set; }
        public string? MoTa { get; set; }
        public string? NoiDung { get; set; }
        public string? HinhAnh { get; set; }
        public bool isNoiBat { get; set; }
        public DateTime? NgayTao { get; set; }
        public int LuotXem { get; set; }

    }
}
