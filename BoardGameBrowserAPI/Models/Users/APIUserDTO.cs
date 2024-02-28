using System.ComponentModel.DataAnnotations;

namespace BoardGameBrowserAPI.Models.Users
{
    public class APIUserDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
