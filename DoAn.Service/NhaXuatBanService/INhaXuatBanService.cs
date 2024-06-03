using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Service.Dtos.NhaXuatBanDto;

namespace DoAn.Service.NhaXuatBanService
{
    public interface INhaXuatBanService : IService<NhaXuatBan>
    {
        ResponseWithDataDto<PagedList<NhaXuatBanDto>> GetDataByPage(NhaXuatBanSearchDto searchDto);
        ResponseWithDataDto<NhaXuatBan> AddNhaXuatBan(NhaXuatBanDto nhaXuatBan);
        ResponseWithMessageDto DeleteNhaXuatBan(Guid id);
        ResponseWithMessageDto UpdateNhaXuatBan(Guid id, NhaXuatBanDto nhaXuatBan);
        ResponseWithDataDto<NhaXuatBanDto> FindById(Guid id);
    }
}
