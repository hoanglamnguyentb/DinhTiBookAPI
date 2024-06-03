using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos.TinTucDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.DanhMucTinTucDto;

namespace DoAn.Service.DanhMucTinTucService
{
    public interface IDanhMucTinTucService : IService<DanhMucTinTuc>
    {
        ResponseWithDataDto<PagedList<DanhMucTinTucDto>> GetDataByPage(DanhMucTinTucSearchDto searchDto);
        ResponseWithMessageDto Delete(Guid id);
        ResponseWithMessageDto Update(Guid id, DanhMucTinTucDto tintuc);
        ResponseWithDataDto<DanhMucTinTucDto> FindById(Guid id);
    }
}
