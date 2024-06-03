using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("NhaXuatBan")]
    public class NhaXuatBan : AuditableEntity
    {
        public string? MaNXB {  get; set; }
        public string? TenNXB {  get; set; }
        public string? Slug { get; set; }

    }
}
