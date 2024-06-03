using DoAn.Domain.Entities;
using DoAn.Repository.AppRoleRepository;
using DoAn.Repository.AppUserRoleRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
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
    public class AppUserRoleService : Service<AppUserRole>, IAppUserRoleService
    {
        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly IAppRoleRepository _appRoleRepository;
        public AppUserRoleService(IAppUserRoleRepository repository, IAppRoleRepository appRoleRepository) : base(repository)
        {
            _appUserRoleRepository = repository;
            _appRoleRepository = appRoleRepository;
        }

        public bool CheckExist(Guid user, Guid role)
        {
            return _appUserRoleRepository.FindBy(x => x.UserId == user && x.RoleId == role).Any();
        }

        public ResponseWithDataDto<PagedList<AppUserRoleDto>> GetDataByPage(AppUserRoleSearchDto searchDto)
        {
            try
            {
                var query = from userrole in GetQueryable().Where(x => x.UserId == searchDto.UserId)
                            join role in _appRoleRepository.GetQueryable()
                            on userrole.RoleId equals role.Id
                            select new AppUserRoleDto()
                            {
                                RoleId = userrole.RoleId,
                                RoleName = role.Name,
                                UserId = userrole.UserId,
                                Id = userrole.Id
                            };
                var reusult = PagedList<AppUserRoleDto>.Create(query, searchDto);
                return new ResponseWithDataDto<PagedList<AppUserRoleDto>>()
                {
                    Data = reusult,
                    Status = StatusConstant.SUCCESS
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<AppUserRoleDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }
        public async Task<List<string>> GetAllRoleByUser(AppUser user)
        {
            var allRole = _appUserRoleRepository.GetAll().Where(x => x.UserId == user.Id).Select
                (x => x.RoleId.ToString()).ToList();
            return allRole;
        }
    }
}
