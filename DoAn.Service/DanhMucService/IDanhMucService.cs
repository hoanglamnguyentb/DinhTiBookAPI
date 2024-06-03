using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.DanhMucDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.DanhMucService
{
    public interface IDanhMucService : IService<DanhMuc>
    {
        ResponseWithDataDto<PagedList<DanhMucDto>> GetDataByPage(DanhMucSearchDto searchDto);
        ResponseWithDataDto<DanhMuc> AddDanhMuc (DanhMucDto danhmuc);
        ResponseWithMessageDto DeleteDanhMuc(Guid id);
        ResponseWithMessageDto UpdateDanhMuc(Guid id, DanhMucDto danhmuc);
        ResponseWithDataDto<DanhMucDto> FindById(Guid id);




    }
}
