using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    public class TTSlide : AuditableEntity
    {
        public string? ThongTin1 { get; set; }
        public string? ThongTin2 { get; set; }
        public string? ThongTin3 { get; set; }
        public string? Type { get; set; }
    }
}
