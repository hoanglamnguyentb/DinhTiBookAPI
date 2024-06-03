using DoAn.Service.DanhMucService;
using DoAn.Service.DanhMucTinTucService;
using DoAn.Service.NhaXuatBanService;
using DoAn.Service.SanPhamService;
using DoAn.Service.TinTucService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Slugify;

namespace DoAnBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {


        private readonly ITinTucService _tinTucService;
        private readonly ISanPhamService _sanPhamService;
        private readonly INhaXuatBanService _nhaXuatBanService;
        private readonly IDanhMucTinTucService _danhMucTinTucService;
        private readonly IDanhMucService _danhMucService;
        public HomeController(ITinTucService tinTucService,
            ISanPhamService sanPhamService,
            INhaXuatBanService nhaXuatBanService,
            IDanhMucTinTucService danhMucTinTucService,
            IDanhMucService danhMucService)
        {
            _tinTucService = tinTucService;
            _sanPhamService = sanPhamService;
            _nhaXuatBanService = nhaXuatBanService;
            _danhMucTinTucService = danhMucTinTucService;
            _danhMucService = danhMucService;
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            SlugHelper helper = new SlugHelper();
            var tintuc = _tinTucService.GetQueryable().ToList();
            //foreach (var item in tintuc)
            //{
            //    item.Slug = helper.GenerateSlug(item.TieuDe);
            //    _tinTucService.Update(item);
            //}
            var sanpham = _sanPhamService.GetQueryable().ToList();
            foreach (var item in sanpham)
            {
                item.Slug = helper.GenerateSlug(item.TenSach);
                _sanPhamService.Update(item);
            }
            //var nhaxuatban = _nhaXuatBanService.GetQueryable().ToList();
            //foreach (var item in nhaxuatban)
            //{
            //    item.Slug = helper.GenerateSlug(item.TenNXB);
            //    _nhaXuatBanService.Update(item);
            //}
            //var danhmuctin = _danhMucService.GetQueryable().ToList();
            //foreach (var item in danhmuctin)
            //{
            //    item.Slug = helper.GenerateSlug(item.CategoryName);
            //    _danhMucService.Update(item);
            //}
            //var danhmuctinTuc = _danhMucTinTucService.GetQueryable().ToList();
            //foreach (var item in danhmuctinTuc)
            //{
            //    item.Slug = helper.GenerateSlug(item.TenDanhMuc);
            //    _danhMucTinTucService.Update(item);
            //}
            return Ok("Thanh cong");
        }
    }
}
