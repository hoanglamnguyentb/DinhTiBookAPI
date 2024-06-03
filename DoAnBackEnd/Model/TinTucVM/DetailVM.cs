using DoAn.Domain.Entities;

namespace DoAnBackEnd.Model.TinTucVM
{
    public class DetailVM
    {
        public TinTuc objInfo { get; set; }
        public List<FileManager> hinhAnh { get; set; }
    }
}
