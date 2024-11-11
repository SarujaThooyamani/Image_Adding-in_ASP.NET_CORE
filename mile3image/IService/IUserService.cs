using mile3image.DTO_s.RequestDTO;
using mile3image.DTO_s.ResponceDTO;

namespace mile3image.IService
{
    public interface IUserService
    {
        Task<UserResponceDTO> CreateUser(UserRequestDTO userRequestDTO);
        Task<List<UserResponceDTO>> GetAllUsers();
        Task<UserResponceDTO> GetUserById(int userId);
        Task<UserResponceDTO> UpdateUser(int userId, UserRequestDTO userRequestDTO);
        Task DeleteUser(int userId);
    }
}
