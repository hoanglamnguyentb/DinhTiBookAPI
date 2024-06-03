using System.ComponentModel;

namespace DoAnBackEnd.Model.DanhMucVM
{
    public class ActionVM
    {
        public string MaCategory { get; set; }

        [DisplayName("Tên danh mục")]
        public string? CategoryName { get; set; }
        public string? MoTa { get; set; }
    }
}
