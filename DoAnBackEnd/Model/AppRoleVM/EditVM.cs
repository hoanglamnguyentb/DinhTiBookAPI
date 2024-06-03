using System.ComponentModel.DataAnnotations;

namespace HiNet.API.Model.AppRoleVM
{
    public class EditVM
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public required string RoleCode { get; set; }
    }
}
