using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("QLSlide")]
    public class QLSlide : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Path { get; set; }
        public required string Mine { get; set; }
        public Guid? ParentId { get; set; }
        public double Size { get; set; }
        public string? Type { get; set; }
    }
}
