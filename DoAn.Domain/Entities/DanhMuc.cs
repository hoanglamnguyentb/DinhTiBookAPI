using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    public class DanhMuc : AuditableEntity
    {
        public string MaCategory { get; set; }
        [DisplayName("Tên danh mục")]
        public string? CategoryName { get; set; }
        public string? MoTa { get; set; }
        public string? Slug { get; set; }

    }
}
