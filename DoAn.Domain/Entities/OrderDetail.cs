using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("OrderDetail")]
    public class OrderDetail : AuditableEntity
    {
        public Guid OrderId { get; set; }
        public Guid IdSanPham {  get; set; }
        public float GiaTien { get; set; }
        public int SoLuong { get; set; }
    }
}
