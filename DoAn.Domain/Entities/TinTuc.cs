using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("TinTuc")]
    public class TinTuc : AuditableEntity
    {
        public string? TieuDe {  get; set; }
        public string? DanhMuc { get; set; }
        public string? MoTa { get; set; }
        public string? NoiDung { get; set; }
        public string? HinhAnh { get; set; }
        public bool isNoiBat { get; set; }
        public int LuotXem { get; set; }
        public string? Slug { get; set; }
        public string? Type { get; set; }
    }
}
