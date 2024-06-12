using DoAn.Domain.Entities;
using DoAn.Repository.DanhMucRepository;
using DoAn.Repository.SanPhamRepository;
using DoAn.Service.Core;
using DoAn.Service.Dtos.DanhMucDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.SanPhamDto;
using DoAn.Service.Constants;
using DoAn.Service.Common;
using DoAn.Service.FileManagerService;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Repository.NhomDoTuoiRepository;

namespace DoAn.Service.SanPhamService
{
    public class SanPhamService : Service<SanPham>, ISanPhamService
    {
        private readonly ISanPhamRepository _sanphamRepository;
        private readonly IFileManagerService _fileManagerService;
        private readonly INhaXuatBanRepository _nhaXuatBanRepository;
        private readonly IDanhMucRepository _danhMucRepository;
        private readonly INhomDoTuoiRepository _nhomDoTuoiRepository;

        public SanPhamService
            (
                ISanPhamRepository sanPhamRepository,
                IFileManagerService fileManagerService,
                INhaXuatBanRepository nhaXuatBanRepository,
                IDanhMucRepository danhMucRepository,
                INhomDoTuoiRepository nhomDoTuoiRepository
            ) : base(sanPhamRepository)
        {
            _sanphamRepository = sanPhamRepository;
            _fileManagerService = fileManagerService;
            _nhaXuatBanRepository = nhaXuatBanRepository;
            _danhMucRepository = danhMucRepository;
            _nhomDoTuoiRepository = nhomDoTuoiRepository;

        }
        public ResponseWithDataDto<SanPham> AddSanPham(SanPhamDto sanpham)
        {
            try
            {
                var data = _sanphamRepository.Add(sanpham);
                return data != null ? new ResponseWithDataDto<SanPham>()
                {
                    Data = data,
                    Message = "Thêm sản phẩm thành công",
                    Status = StatusConstant.SUCCESS,

                } : new ResponseWithDataDto<SanPham>()
                {
                    Data = null,
                    Message = "Thêm sản phẩm lỗi",
                    Status = "Error"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<SanPham>()
                {
                    Data = null,
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithMessageDto DeleteSanPham(Guid id)
        {
            try
            {
                var sanpham = _sanphamRepository.GetById(id);
                if (sanpham == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tồn tại sản phẩm",
                        Status = StatusConstant.ERROR
                    };
                }
                var data = _sanphamRepository.Delete(sanpham);
                return new ResponseWithMessageDto()
                {
                    Message = "Xóa sản phẩm thành công",
                    Status = StatusConstant.SUCCESS
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = StatusConstant.ERROR
                };
            }
        }

        public ResponseWithDataDto<SanPhamDto> FindById(Guid id)
        {
            try
            {
                var data = from tblSanPham in _sanphamRepository.GetQueryable()
                           where tblSanPham.Id == id
                           join tblNXB in _nhaXuatBanRepository.GetQueryable()
                            on tblSanPham.IdNhaXuatBan equals tblNXB.MaNXB into jSanPham
                           from nxb in jSanPham.DefaultIfEmpty()
                           join tblDanhMuc in _danhMucRepository.GetQueryable()
                           on tblSanPham.IdDanhMuc equals tblDanhMuc.MaCategory into jdanhmucCategory
                           from danhmuc in jdanhmucCategory.DefaultIfEmpty()
                           join tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                           on tblSanPham.IdNhomDoTuoi equals tblNhomDoTuoi.MaNhomDoTuoi into jDoTuoi
                           from dotuoi in jDoTuoi.DefaultIfEmpty()
                           select new SanPhamDto
                           {
                               Id = tblSanPham.Id,
                               TenSach = tblSanPham.TenSach,
                               TenTacGia = tblSanPham.TenTacGia,
                               IdNhaXuatBan = tblSanPham.IdNhaXuatBan,
                               NamXuatBan = tblSanPham.NamXuatBan,
                               IdDanhMuc = tblSanPham.IdDanhMuc,
                               GiaTien = tblSanPham.GiaTien,
                               SoLuongTon = tblSanPham.SoLuongTon,
                               MoTaSach = tblSanPham.MoTaSach,
                               IdNhomDoTuoi = tblSanPham.IdNhomDoTuoi,
                               NoiBat = tblSanPham.NoiBat,
                               GiamGia = tblSanPham.GiamGia,
                               SachKhuyenDoc = tblSanPham.SachKhuyenDoc,
                               LuotXem = tblSanPham.LuotXem,
                               TenDanhMuc = danhmuc.CategoryName,
                               TenNhomDoTuoi = dotuoi.TenNhom,
                               Slug = tblSanPham.Slug,
                               SoLuongDaBan = tblSanPham.SoLuongDaBan,
                               TenNXB = nxb.TenNXB
                           };
                return new ResponseWithDataDto<SanPhamDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy sản phẩm thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<SanPhamDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public SanPham GetById(Guid id)
        {
            return _sanphamRepository.GetById(id);
        }

        public ResponseWithDataDto<PagedList<SanPhamDto>> GetDataByPage(SanPhamSearchDto searchDto)
        {
            try
            {
                var query = from tblSanPham in _sanphamRepository.GetQueryable()
                            join tblNXB in _nhaXuatBanRepository.GetQueryable()
                            on tblSanPham.IdNhaXuatBan equals tblNXB.MaNXB into jSanPham
                            from nxb in jSanPham.DefaultIfEmpty()
                            join tblDanhMuc in _danhMucRepository.GetQueryable()
                            on tblSanPham.IdDanhMuc equals tblDanhMuc.MaCategory into jdanhmucCategory
                            from danhmuc in jdanhmucCategory.DefaultIfEmpty()
                            join tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                            on tblSanPham.IdNhomDoTuoi equals tblNhomDoTuoi.MaNhomDoTuoi into jDoTuoi
                            from dotuoi in jDoTuoi.DefaultIfEmpty()

                            select new SanPhamDto
                            {
                                Id = tblSanPham.Id,
                                TenSach = tblSanPham.TenSach,
                                TenTacGia = tblSanPham.TenTacGia,
                                IdNhaXuatBan = tblSanPham.IdNhaXuatBan,
                                NamXuatBan = tblSanPham.NamXuatBan,
                                IdDanhMuc = tblSanPham.IdDanhMuc,
                                GiaTien = tblSanPham.GiaTien,
                                SoLuongTon = tblSanPham.SoLuongTon,
                                MoTaSach = tblSanPham.MoTaSach,
                                IdNhomDoTuoi = tblSanPham.IdNhomDoTuoi,
                                NoiBat = tblSanPham.NoiBat,
                                GiamGia = tblSanPham.GiamGia,
                                SachKhuyenDoc = tblSanPham.SachKhuyenDoc,
                                TenNXB = nxb.TenNXB,
                                TenDanhMuc = danhmuc.CategoryName,
                                TenNhomDoTuoi = dotuoi.TenNhom,
                                LuotXem = tblSanPham.LuotXem,
                                Slug = tblSanPham.Slug,
                                
                                SoLuongDaBan = tblSanPham.SoLuongDaBan,
                            };

                if (searchDto != null)
                {
                    if (searchDto.TenSanPhamFilter != null)
                    {
                        query = query.Where(record => record.TenSach.Trim().ToLower().Contains(searchDto.TenSanPhamFilter.Trim().ToLower()));
                    }
                    if(searchDto.NhomDoTuoiFilter != null)
                    {
                        query = query.Where(record => record.IdNhomDoTuoi.Trim().ToLower().Contains(searchDto.NhomDoTuoiFilter.Trim().ToLower()));
                    }
                    if (searchDto.DanhMucFilter != null)
                    {
                        query = query.Where(record => record.IdDanhMuc.Trim().ToLower().Contains(searchDto.DanhMucFilter.Trim().ToLower()));
                    }
                    if (searchDto.isNoiBatFilter == true)
                    {
                        query = query.Where(record => record.NoiBat);
                    }
                    if (searchDto.isKhuyenDocFilter == true)
                    {
                        query = query.Where(record => record.SachKhuyenDoc);
                    }

                }
                var result = PagedList<SanPhamDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<SanPhamDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<SanPhamDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithDataDto<PagedList<SanPhamDto>> GetDataByPageWithFilter(SanPhamSearchFilterDto searchDto)
        {
            try
            {
                var query = from tblSanPham in _sanphamRepository.GetQueryable()
                            join tblNXB in _nhaXuatBanRepository.GetQueryable()
                            on tblSanPham.IdNhaXuatBan equals tblNXB.MaNXB into jSanPham
                            from nxb in jSanPham.DefaultIfEmpty()
                            join tblDanhMuc in _danhMucRepository.GetQueryable()
                            on tblSanPham.IdDanhMuc equals tblDanhMuc.MaCategory into jdanhmucCategory
                            from danhmuc in jdanhmucCategory.DefaultIfEmpty()
                            join tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                            on tblSanPham.IdNhomDoTuoi equals tblNhomDoTuoi.MaNhomDoTuoi into jDoTuoi
                            from dotuoi in jDoTuoi.DefaultIfEmpty()

                            select new SanPhamDto
                            {
                                Id = tblSanPham.Id,
                                TenSach = tblSanPham.TenSach,
                                TenTacGia = tblSanPham.TenTacGia,
                                IdNhaXuatBan = tblSanPham.IdNhaXuatBan,
                                NamXuatBan = tblSanPham.NamXuatBan,
                                IdDanhMuc = tblSanPham.IdDanhMuc,
                                GiaTien = tblSanPham.GiaTien,
                                SoLuongTon = tblSanPham.SoLuongTon,
                                MoTaSach = tblSanPham.MoTaSach,
                                IdNhomDoTuoi = tblSanPham.IdNhomDoTuoi,
                                NoiBat = tblSanPham.NoiBat,
                                GiamGia = tblSanPham.GiamGia,
                                SachKhuyenDoc = tblSanPham.SachKhuyenDoc,
                                TenNXB = nxb.TenNXB,
                                TenDanhMuc = danhmuc.CategoryName,
                                TenNhomDoTuoi = dotuoi.TenNhom,
                                LuotXem = tblSanPham.LuotXem,
                                Slug = tblSanPham.Slug,
                                SoLuongDaBan = tblSanPham.SoLuongDaBan
                            };

                if (searchDto != null)
                {
                    if (searchDto.TenSanPhamFilter != null)
                    {
                        query = query.Where(record => record.TenSach.Trim().ToLower().Contains(searchDto.TenSanPhamFilter.Trim().ToLower()));
                    }
                    if (searchDto.NhomDoTuoiFilter != null)
                    {
                        query = query.Where(record => record.IdNhomDoTuoi.Trim().ToLower().Contains(searchDto.NhomDoTuoiFilter.Trim().ToLower()));
                    }
                    if (searchDto.DanhMucFilter != null)
                    {
                        query = query.Where(record => record.IdDanhMuc.Trim().ToLower().Contains(searchDto.DanhMucFilter.Trim().ToLower()));
                    }
                    if(searchDto.Categories.Count > 0)
                    {
                        query = query.Where(x => searchDto.Categories.Contains(x.IdDanhMuc));
                    }
                    if (searchDto.Ages.Count > 0)
                    {
                        query = query.Where(x => searchDto.Ages.Contains(x.IdNhomDoTuoi));
                    }
                    if (searchDto.Types.Count > 0)
                    {
                        if (searchDto.Types.Contains("noiBat"))
                        {
                            query = query.Where(x => x.NoiBat == true);
                        }

                        if (searchDto.Types.Contains("khuyenDoc"))
                        {
                            query = query.OrderBy(x => x.SachKhuyenDoc == true);
                        }

                    }
                    if (searchDto.Prices.Count > 0)
                    {
                        if (!searchDto.Prices.Contains("0"))
                        {
                            query = query.Where(x =>
                            (searchDto.Prices.Contains("<50") && x.GiaTien / 1000 <= 50) ||
                            (searchDto.Prices.Contains("50-99") && x.GiaTien / 1000 >= 50 && x.GiaTien / 1000 <= 99) ||
                            (searchDto.Prices.Contains("100-199") && x.GiaTien / 1000 >= 100 && x.GiaTien / 1000 <= 199) ||
                            (searchDto.Prices.Contains("200-299") && x.GiaTien / 1000 >= 200 && x.GiaTien / 1000 <= 299) ||
                            (searchDto.Prices.Contains(">300") && x.GiaTien / 1000 >= 300)
                        );
                        }
                    }
                    if (searchDto.Discounts.Count > 0)
                    {
                        query = query.Where(x => (searchDto.Discounts.Contains("0") && x.GiamGia == 0) ||
                              (searchDto.Discounts.Contains("1") && x.GiamGia > 0));
                    }
                    if (searchDto.SortBy != null)
                    {
                        if(searchDto.SortBy == "price_asc")
                        {
                            query = query.OrderBy(x => x.GiaTien);   
                        }
                        if (searchDto.SortBy == "price_desc")
                        {
                            query = query.OrderByDescending(x => x.GiaTien);
                        }
                    }
                }
                var result = PagedList<SanPhamDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<SanPhamDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy danh mục thành công"
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<SanPhamDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }

        public ResponseWithMessageDto UpdateSanPham(Guid id, SanPhamDto sanpham)
        {
            try
            {
                var sanphamNew = _sanphamRepository.GetById(id);
                if (sanphamNew == null)
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Không tìm thấy sản phẩm",
                        Status = StatusConstant.ERROR,
                    };
                }
                else
                {
                    sanphamNew.TenSach = sanpham.TenSach;
                    _sanphamRepository.Edit(sanphamNew);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật danh mục thành công",
                        Status = StatusConstant.SUCCESS,
                    };
                }

            }
            catch (Exception ex)
            {
                return new ResponseWithMessageDto()
                {
                    Message = ex.Message,
                    Status = "Error"
                };
            }
        }
        public ResponseWithMessageDto TruSoLuongDaMua(Guid id, int SoLuong)
        {
            var sanPham = _sanphamRepository.GetById(id);
            if (sanPham != null)
            {
                if (sanPham.SoLuongTon >= SoLuong)
                {
                    sanPham.SoLuongTon -= SoLuong;
                    sanPham.SoLuongDaBan += SoLuong;
                    _sanphamRepository.Edit(sanPham);
                    return new ResponseWithMessageDto()
                    {
                        Message = "Cập nhật số lượng thành công",
                        Status = StatusConstant.SUCCESS,
                    };
                }
                else
                {
                    return new ResponseWithMessageDto()
                    {
                        Message = "Lỗi",
                        Status = "Error"
                    };
                }
            }
            else
            {
                return new ResponseWithMessageDto()
                {
                    Message = "Không tìm thấy sản phẩm",
                    Status = StatusConstant.ERROR,
                };
            }
        }

        public SanPham FindBySlug(string Slug)
        {
            try
            {
                var data = from tblSanPham in _sanphamRepository.GetQueryable()
                           where tblSanPham.Slug == Slug
                           join tblNXB in _nhaXuatBanRepository.GetQueryable()
                            on tblSanPham.IdNhaXuatBan equals tblNXB.MaNXB into jSanPham
                           from nxb in jSanPham.DefaultIfEmpty()
                           join tblDanhMuc in _danhMucRepository.GetQueryable()
                           on tblSanPham.IdDanhMuc equals tblDanhMuc.MaCategory into jdanhmucCategory
                           from danhmuc in jdanhmucCategory.DefaultIfEmpty()
                           join tblNhomDoTuoi in _nhomDoTuoiRepository.GetQueryable()
                           on tblSanPham.IdNhomDoTuoi equals tblNhomDoTuoi.MaNhomDoTuoi into jDoTuoi
                           from dotuoi in jDoTuoi.DefaultIfEmpty()
                           select new SanPhamDto
                           {
                               Id = tblSanPham.Id,
                               TenSach = tblSanPham.TenSach,
                               TenTacGia = tblSanPham.TenTacGia,
                               IdNhaXuatBan = tblSanPham.IdNhaXuatBan,
                               NamXuatBan = tblSanPham.NamXuatBan,
                               IdDanhMuc = tblSanPham.IdDanhMuc,
                               GiaTien = tblSanPham.GiaTien,
                               SoLuongTon = tblSanPham.SoLuongTon,
                               MoTaSach = tblSanPham.MoTaSach,
                               IdNhomDoTuoi = tblSanPham.IdNhomDoTuoi,
                               NoiBat = tblSanPham.NoiBat,
                               GiamGia = tblSanPham.GiamGia,
                               SachKhuyenDoc = tblSanPham.SachKhuyenDoc,
                               LuotXem = tblSanPham.LuotXem,
                               TenDanhMuc = danhmuc.CategoryName,
                               TenNhomDoTuoi = dotuoi.TenNhom,
                               Slug = tblSanPham.Slug,
                               TenNXB = nxb.TenNXB,
                               SoLuongDaBan = tblSanPham.SoLuongDaBan
                           };
                /*return new ResponseWithDataDto<SanPhamDto>()
                {
                    Data = data.FirstOrDefault(),
                    Status = StatusConstant.SUCCESS,
                    Message = "Lấy sản phẩm thành công"
                };*/
                return data.FirstOrDefault();
            }
            catch (Exception ex)
            {
                /*return new ResponseWithDataDto<SanPhamDto>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };*/
                return null;
            }
        }
    }
}
