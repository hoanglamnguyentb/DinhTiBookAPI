using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.TinTucRepository
{
    internal class TinTucRepository : Repository<TinTuc> , ITinTucRepository
    {
        public TinTucRepository(DoAnContext context) : base(context) { }
    }
}
