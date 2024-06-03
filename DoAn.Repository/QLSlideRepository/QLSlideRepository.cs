using DoAn.Domain;
using DoAn.Domain.Entities;
using DoAn.Repository.Core;
using DoAn.Repository.FileManagerRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.QLSlideRepository
{
    internal class QLSlideRepository : Repository<QLSlide>, IQLSlideRepository
    {
        public QLSlideRepository(DoAnContext context) : base(context)
        {
        }
    }
}
