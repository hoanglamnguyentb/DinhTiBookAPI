using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.NhaXuatBanDto
{
    public class NhaXuatBanSearchDto : SearchBase
    {
        public string? TenNXBFilter { get; set; }
    }
}
