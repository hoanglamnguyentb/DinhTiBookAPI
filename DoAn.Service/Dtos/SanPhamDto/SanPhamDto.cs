using DoAn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.FileManagerDto;

namespace DoAn.Service.Dtos.SanPhamDto
{
    public class SanPhamDto : SanPham
    {
        public string pathAnh {  get; set; }
        public string? TenDanhMuc { get; set; }
        public string? TenNXB { get; set; }
        public string? TenNhomDoTuoi { get; set; }

    }
}
