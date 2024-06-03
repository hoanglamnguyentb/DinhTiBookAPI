using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("DanhMucTinTuc")]
    public class DanhMucTinTuc : AuditableEntity
    {
        public required string MaDanhMuc { get; set; }
        public string? TenDanhMuc {  get; set; }
        public bool isHienThi { get; set; }
        public string? Slug { get; set; }

    }
}
