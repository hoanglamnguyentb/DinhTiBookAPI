using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("HinhAnh")]
    public class HinhAnh : AuditableEntity
    {
        public string? IdSach {  get; set; }
        public string? Image_url { get; set; }
    }
}
