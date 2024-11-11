using mile3image.DTO_s.RequestDTO;
using mile3image.DTO_s.ResponceDTO;
using mile3image.Entities;
using mile3image.IRepository;
using mile3image.IService;
using System.IO;
using System.Threading.Tasks;

namespace mile3image.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _imageFolderPath = "wwwroot/images";  

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponceDTO> CreateUser(UserRequestDTO userRequestDTO)
        {
           
            string profileImagePath = null;
            if (userRequestDTO.ProfileImage != null)
            {
                var fileName = $"{Guid.NewGuid()}_{userRequestDTO.ProfileImage.FileName}";
                profileImagePath = Path.Combine(_imageFolderPath, fileName);

                using (var stream = new FileStream(profileImagePath, FileMode.Create))
                {
                    await userRequestDTO.ProfileImage.CopyToAsync(stream);
                }
            }

            
            var user = new User
            {
                UserName = userRequestDTO.UserName,
                HashPassword = userRequestDTO.Password,  
                ProfileImage = profileImagePath
            };

            var createdUser = await _userRepository.CreateUser(user);

        
            return new UserResponceDTO
            {
                UserId = createdUser.UserId,
                UserName = createdUser.UserName,
                ProfileImageUrl = profileImagePath 
            };
        }

        public async Task<List<UserResponceDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers(); 
            var data = new List<UserResponceDTO>();

            foreach (var user in users)
            {
                var userDto = new UserResponceDTO
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImage  
                };

                data.Add(userDto);
            }

            return data;
        }
        public async Task<UserResponceDTO> GetUserById(int userId)
        {
            var data = await _userRepository.GetUserById(userId);

            if (data == null)
            {
                throw new Exception("User not found.");
            }

            var user = new UserResponceDTO
            {
                UserId = data.UserId,
                UserName = data.UserName,
                ProfileImageUrl = data.ProfileImage 
            };

            return user;
        }
        public async Task<UserResponceDTO> UpdateUser(int userId, UserRequestDTO userRequestDTO)
        {
           
            var existingUser = await _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }

            
            existingUser.UserName = userRequestDTO.UserName;
            existingUser.HashPassword = userRequestDTO.Password;

            
            if (userRequestDTO.ProfileImage != null)
            {
                
                if (!string.IsNullOrEmpty(existingUser.ProfileImage) && File.Exists(existingUser.ProfileImage))
                {
                    File.Delete(existingUser.ProfileImage);
                }

               
                var fileName = $"{Guid.NewGuid()}_{userRequestDTO.ProfileImage.FileName}";
                var profileImagePath = Path.Combine(_imageFolderPath, fileName);

                using (var stream = new FileStream(profileImagePath, FileMode.Create))
                {
                    await userRequestDTO.ProfileImage.CopyToAsync(stream);
                }

                
                existingUser.ProfileImage = profileImagePath;
            }

            
            await _userRepository.UpdateUser(existingUser);

          
            return new UserResponceDTO
            {
                UserId = existingUser.UserId,
                UserName = existingUser.UserName,
                ProfileImageUrl = existingUser.ProfileImage
            };
        }
        public async Task DeleteUser(int userId)
        {
            await _userRepository.DeleteUser(userId);
        }
    }
}

