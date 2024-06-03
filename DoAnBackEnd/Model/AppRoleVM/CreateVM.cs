using System.ComponentModel.DataAnnotations;

namespace DoAnBackEnd.Model.AppRoleVM
{
    public class CreateVM
    {
        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public required string RoleCode { get; set; }
    }
}
