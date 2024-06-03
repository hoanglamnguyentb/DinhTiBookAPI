using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.SanPhamDto
{
    public class SanPhamSearchFilterDto : SanPhamSearchDto
    {
        public List<string> Categories { get;} = new List<string>();
        public List<string> Ages { get;} = new List<string>();
        public List<string> Types { get;} = new List<string>();
        public List<string> Prices { get;} = new List<string>();
        public List<string> Discounts { get;} = new List<string>();
        public string SortBy { get; set; } = string.Empty;

    }
}
