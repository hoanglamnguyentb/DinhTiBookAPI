using DoAn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.AppUserRoleDto
{
    public class AppUserRoleDto : AppUserRole
    {
        public string RoleName { get; set; }
    }
}
