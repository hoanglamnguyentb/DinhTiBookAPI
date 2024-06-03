using DoAn.Domain.Entities;
using DoAn.Repository.AppRoleRepository;
using DoAn.Repository.AppUserRepository;
using DoAn.Repository.AppUserRoleRepository;
using DoAn.Repository.Core;
using DoAn.Repository.NhaXuatBanRepository;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos;
using DoAn.Service.Dtos.AppUserDto;
using DoAn.Service.Dtos.NhaXuatBanDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DoAn.Service.AppUserService
{
    public class AppUserService : Service<AppUser>, IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly IAppRoleRepository _appRoleRepository;

        public AppUserService(IAppUserRepository appUserRepository, IAppUserRoleRepository appUserRoleRepository, IAppRoleRepository appRoleRepository) : base(appUserRepository)
        {
            _appUserRepository = appUserRepository;
            _appUserRoleRepository = appUserRoleRepository;
            _appRoleRepository = appRoleRepository;
        }

        public ResponseWithDataDto<AppUserDto> GetByIdAndRole(Guid id)
        {
            var query = from AppUsertbl in _appUserRepository.GetQueryable()
                        join AppUserRoletbl in _appUserRoleRepository.GetQueryable()
                        on AppUsertbl.Id equals AppUserRoletbl.UserId into userRole
                        from userRoleGroup in userRole.DefaultIfEmpty()
                        join AppRoletbl in _appRoleRepository.GetQueryable()
                        on userRoleGroup.RoleId equals AppRoletbl.Id into role
                        from roleGroup in role.DefaultIfEmpty()
                        where AppUsertbl.Id == id
                        select new AppUserDto
                        {
                            Id = AppUsertbl.Id,
                            FullName = AppUsertbl.FullName,
                            UserName = AppUsertbl.UserName,
                            NormalizedUserName = AppUsertbl.NormalizedUserName,
                            Email = AppUsertbl.Email,
                            NormalizedEmail = AppUsertbl.NormalizedEmail,
                            EmailConfirmed = AppUsertbl.EmailConfirmed,
                            PasswordHash = AppUsertbl.PasswordHash,
                            SecurityStamp = AppUsertbl.SecurityStamp,
                            ConcurrencyStamp = AppUsertbl.ConcurrencyStamp,
                            PhoneNumber = AppUsertbl.PhoneNumber,
                            RoleCode = roleGroup.RoleCode
                        };

            return new ResponseWithDataDto<AppUserDto>()
            {
                Data = query.FirstOrDefault(),
                Status = StatusConstant.SUCCESS,
                Message = "Lấy thành công"
            };
        }
    }
}
