using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.DanhMucTinTucDto
{
    public class DanhMucTinTucSearchDto : SearchBase
    {
        public string? TenDanhMucFilter { get; set; }
        public string? MaDanhMucFilter { get; set; }
    }
}
