using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.TinTucDto
{
    public class TinTucSearchDto : SearchBase
    {
        public string? TieuDeFilter { get; set; }
        public string? DanhMucFilter { get; set; }
    }
}
