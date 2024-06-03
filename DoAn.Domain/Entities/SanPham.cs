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
    [Table("SanPham")]
    public class SanPham : AuditableEntity
    {
        [StringLength(50)]
        public string? TenSach {  get; set; }
        [StringLength(50)]
        public string? TenTacGia { get; set; }
        public string? IdNhaXuatBan { get; set; }
        public string? NamXuatBan { get; set; }
        public string? IdDanhMuc {  get; set; }
        public double GiaTien {  get; set; }
        public int SoLuongTon {  get; set; }
        public string? MoTaSach { get; set; }
        public string? IdNhomDoTuoi { get; set; }
        public bool NoiBat {  get; set; }
        public int GiamGia {  get; set; }
        public bool SachKhuyenDoc { get; set; }
        public int LuotXem { get; set; }
        public string? Slug { get; set; }

    }
}
