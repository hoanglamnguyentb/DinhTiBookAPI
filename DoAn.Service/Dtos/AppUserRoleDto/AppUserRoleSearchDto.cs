using DoAn.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Service.Dtos.AppUserRoleDto
{
    public class AppUserRoleSearchDto : SearchBase
    {
        public Guid UserId { get; set; }
    }
}
