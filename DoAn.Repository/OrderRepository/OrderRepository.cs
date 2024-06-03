using DoAn.Domain.Entities;
using DoAn.Domain;
using DoAn.Repository.Core;
using DoAn.Repository.TTSlideRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.OrderRepository
{
    internal class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(DoAnContext context) : base(context)
        {
        }
    }
}
