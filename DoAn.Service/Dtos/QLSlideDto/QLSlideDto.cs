using DoAn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.QLSlideDto
{
    public class QLSlideDto : QLSlide
    {
        public Guid? ParentId { get; set; }
    }
}
