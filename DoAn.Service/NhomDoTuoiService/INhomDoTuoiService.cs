using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos.NhaXuatBanDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.NhomDoTuoiDto;

namespace DoAn.Service.NhomDoTuoiService
{
    public interface INhomDoTuoiService : IService<NhomDoTuoi>
    {
        ResponseWithDataDto<PagedList<NhomDoTuoiDto>> GetDataByPage(NhomDoTuoiSearchDto searchDto);
        ResponseWithDataDto<NhomDoTuoi> Add(NhomDoTuoiDto nhomDoTuoi);
        ResponseWithMessageDto Delete(Guid id);
        ResponseWithMessageDto Update(Guid id, NhomDoTuoiDto nhomDoTuoi);
        ResponseWithDataDto<NhomDoTuoiDto> FindById(Guid id);
    }
}
