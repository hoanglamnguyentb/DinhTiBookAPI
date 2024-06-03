using DoAn.Domain.Entities;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.AppUserDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.AppUserService
{
    public interface IAppUserService : IService<AppUser>
    {
        ResponseWithDataDto<AppUserDto> GetByIdAndRole(Guid id);
    }
}
