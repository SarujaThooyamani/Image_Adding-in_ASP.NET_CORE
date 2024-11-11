namespace mile3image.DTO_s.RequestDTO
{
    public class UserRequestDTO
    {
       
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
