using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("Order")]
    public class Order : AuditableEntity
    {
        public Guid IdUser { get; set; }
        public string? TenKhachHang { get; set; }
        public DateTime? NgayDatHang { get; set; }
        public string? SoDienThoai { get; set; }
        public string? DiaChi {  get; set; }
        public string? Tinh {  get; set; }
        public string? Huyen {  get; set; }
        public string? Xa {  get; set; }
        public int TrangThaiDonHang { get; set; }
    }
}
