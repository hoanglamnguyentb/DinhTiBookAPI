using System.ComponentModel.DataAnnotations;

namespace DoAnBackEnd.Model.AppUserRoleVM
{
    public class ActionVM
    {
        [Required]
        public required Guid UserId { get; set; }
        [Required]
        public required Guid RoleId { get; set; }
    }
}
