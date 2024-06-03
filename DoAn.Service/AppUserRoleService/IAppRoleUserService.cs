using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos.AppUserRoleDto;
using DoAn.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.AppUserRoleService
{
    public interface IAppUserRoleService : IService<AppUserRole>
    {
        Task<List<string>> GetAllRoleByUser(AppUser user);
        ResponseWithDataDto<PagedList<AppUserRoleDto>> GetDataByPage(AppUserRoleSearchDto searchDto);
        bool CheckExist(Guid user, Guid role);
    }
}
