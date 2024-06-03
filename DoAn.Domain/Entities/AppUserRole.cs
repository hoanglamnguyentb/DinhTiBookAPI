using DoAn.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Domain.Entities
{
    [Table("AppUserRole")]
    public class AppUserRole : AuditableEntity // IdentityUserRole<Guid>, 
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
