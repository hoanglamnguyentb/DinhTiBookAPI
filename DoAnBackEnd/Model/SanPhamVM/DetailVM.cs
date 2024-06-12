using DoAn.Domain.Entities;
using DoAn.Service.Dtos.SanPhamDto;
using System.ComponentModel.DataAnnotations;

namespace DoAnBackEnd.Model.SanPhamVM
{
    public class DetailVM
    {
      
        public SanPham objInfo { get; set; }
        public List<FileManager> AnhSanPham { get; set; }
    }
}
