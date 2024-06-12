using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.OrderDto
{
    public class OrderSearchDto : SearchBase
    {
        public string? IdUserFilter { get; set; }
        public int TrangThaiFilter { get; set; }
        public string? TenKhachHangFilter { get; set; }
        public string? SoDienThoaiFilter { get; set; }
    }
}
