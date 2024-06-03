using DoAn.Domain.Entities;

namespace DoAnBackEnd.Model.TTSlideVM
{
    public class DetailVM
    {
        public TTSlide objInfo { get; set; }
        public List<FileManager> Anh { get; set; }
    }
}
