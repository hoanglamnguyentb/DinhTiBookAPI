using DoAn.Domain.Entities;

namespace DoAnBackEnd.Model.OrderDetailVM
{
    public class CreateVM
    {
        public Guid OrderId { get; set; }
        public Guid IdSanPham { get; set; }
        public float GiaTien { get; set; }
        public int SoLuong { get; set; }
    }
}
