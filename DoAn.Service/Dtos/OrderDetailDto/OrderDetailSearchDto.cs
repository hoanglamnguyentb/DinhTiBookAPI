using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.OrderDetailDto
{
    public class OrderDetailSearchDto : SearchBase
    {
        public Guid OrderIdFilter { get; set; }
    }
}
