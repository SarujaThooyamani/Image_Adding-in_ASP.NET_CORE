using System.ComponentModel.DataAnnotations;

namespace mile3image.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string HashPassword { get; set; }
        public string? ProfileImage {  get; set; }
    }
}
