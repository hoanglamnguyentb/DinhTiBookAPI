using DoAn.Domain.Entities;
using DoAn.Domain;
using DoAn.Repository.Core;
using DoAn.Repository.OrderRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.OrderDetailRepository
{
    internal class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(DoAnContext context) : base(context)
        {
        }
    }
}
