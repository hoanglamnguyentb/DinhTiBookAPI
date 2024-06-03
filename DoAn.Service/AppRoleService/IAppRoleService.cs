using DoAn.Domain.Entities;
using DoAn.Service.Common;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.AppRoleDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.AppRoleService
{
    public interface IAppRoleService : IService<AppRole>
    {
        ResponseWithDataDto<PagedList<AppRoleDto>> GetDataByPage(AppRoleSearchDto search);

        bool CheckExist(string name);
        AppRoleDto GetById(string id);
    }
}
