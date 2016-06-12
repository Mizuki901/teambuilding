using System.ComponentModel.DataAnnotations;

namespace TeamBuilding.Models
{
    public class UserViewModels
    {
        [Required]
        public string Name { get; set; }
    }
}