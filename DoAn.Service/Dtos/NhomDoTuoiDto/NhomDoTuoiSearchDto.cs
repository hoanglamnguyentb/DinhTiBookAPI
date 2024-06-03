using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.NhomDoTuoiDto
{
    public class NhomDoTuoiSearchDto : SearchBase
    {
        public string? TenNhomFilter { get; set; }
        public string? MaNhomFilter { get; set; }
    }
}
