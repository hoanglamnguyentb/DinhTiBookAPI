using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("NhomDoTuoi")]
    public class NhomDoTuoi : AuditableEntity
    {
        public string MaNhomDoTuoi { get; set; }
        [StringLength(50)]
        public string? TenNhom {  get; set; }
        public string? MoTaDoTuoi { get; set; }
    }
}
