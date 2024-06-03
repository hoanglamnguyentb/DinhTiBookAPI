using DoAn.Domain.Entities;
using DoAn.Domain;
using DoAn.Repository.Core;
using DoAn.Repository.QLSlideRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Repository.TTSlideRepository
{
    internal class TTSlideRepository : Repository<TTSlide>, ITTSlideRepository
    {
        public TTSlideRepository(DoAnContext context) : base(context)
        {
        }
    }
}
