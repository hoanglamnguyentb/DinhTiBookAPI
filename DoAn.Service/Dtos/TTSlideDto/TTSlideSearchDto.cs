using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.TTSlideDto
{
    public class TTSlideSearchDto : SearchBase
    {
        public string? TenFilter { get; set; }
    }
}
