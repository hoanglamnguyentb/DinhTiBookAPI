using DoAn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.OrderDetailDto
{
    public class OrderDetailDto : OrderDetail
    {
        public string? TenSach { get; set; }
    }
}
