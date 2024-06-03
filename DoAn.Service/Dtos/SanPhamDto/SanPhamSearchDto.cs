using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.SanPhamDto
{
    public class SanPhamSearchDto : SearchBase
    {
        public string? TenSanPhamFilter { get; set; }
        public string? DanhMucFilter { get; set; }
        public string? NhomDoTuoiFilter { get; set; }
        
    }
}
