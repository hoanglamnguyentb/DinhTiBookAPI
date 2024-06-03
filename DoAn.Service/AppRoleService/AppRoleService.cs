using DoAn.Domain.Entities;
using DoAn.Repository.AppRoleRepository;
using DoAn.Service.Common;
using DoAn.Service.Constants;
using DoAn.Service.Core;
using DoAn.Service.Dtos.AppRoleDto;
using DoAn.Service.Dtos;
using System.Linq.Dynamic.Core;

namespace DoAn.Service.AppRoleService
{
    public class AppRoleService : Service<AppRole>, IAppRoleService
    {
        private readonly IAppRoleRepository _appRoleRepository;
        public AppRoleService(IAppRoleRepository repository) : base(repository)
        {
            _appRoleRepository = repository;
        }
        public ResponseWithDataDto<PagedList<AppRoleDto>> GetDataByPage(AppRoleSearchDto search)
        {
            try
            {
                var query = from role in GetQueryable()
                            select new AppRoleDto()
                            {
                                Id = role.Id,
                                ConcurrencyStamp = role.ConcurrencyStamp,
                                Name = role.Name,
                                NormalizedName = role.NormalizedName,
                                RoleCode = role.RoleCode,
                            };
                if (search != null)
                {
                    if (!string.IsNullOrEmpty(search.fieldName) && !search.fieldName.Equals("undefined"))
                    {
                       
                    }
                    if (!string.IsNullOrEmpty(search.Name))
                    {
                        query = query.Where(x => x.Name.RemoveAccentsUnicode().ToLower().Contains(search.Name.RemoveAccentsUnicode().ToLower()));
                    }
                }
                var result = PagedList<AppRoleDto>.Create(query, search);
                return new ResponseWithDataDto<PagedList<AppRoleDto>>()
                {
                    Data = result,
                    Status = StatusConstant.SUCCESS
                };
            }
            catch (Exception ex)
            {
                return new ResponseWithDataDto<PagedList<AppRoleDto>>()
                {
                    Data = null,
                    Status = StatusConstant.ERROR,
                    Message = ex.Message
                };
            }
        }


        public bool CheckExist(string name)
        {
            return _appRoleRepository.FindBy(x => x.Name != null && x.Name.ToLower() == name.ToLower()).Any();
        }

        public AppRoleDto GetById(string id)
        {
            var query = from role in _appRoleRepository.GetQueryable()
                        where role.Id.ToString() == id
                        select new AppRoleDto
                        {
                            RoleCode = role.RoleCode,
                        };
            return query.FirstOrDefault();
        }
    }
}
