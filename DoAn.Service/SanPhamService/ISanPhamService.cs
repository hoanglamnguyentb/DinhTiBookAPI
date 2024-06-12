using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.SanPhamDto;

namespace DoAn.Service.SanPhamService
{
    public interface ISanPhamService : IService<SanPham>
    {
        ResponseWithDataDto<PagedList<SanPhamDto>> GetDataByPage(SanPhamSearchDto searchDto);
        ResponseWithDataDto<SanPham> AddSanPham(SanPhamDto sanpham);
        ResponseWithMessageDto DeleteSanPham(Guid id);
        ResponseWithMessageDto UpdateSanPham(Guid id, SanPhamDto sanpham);
        ResponseWithDataDto<SanPhamDto> FindById(Guid id);
        SanPham GetById(Guid id);
        ResponseWithDataDto<PagedList<SanPhamDto>> GetDataByPageWithFilter(SanPhamSearchFilterDto searchDto);
        ResponseWithMessageDto TruSoLuongDaMua(Guid id, int SoLuong);
        SanPham FindBySlug(string Slug);
    }
}
