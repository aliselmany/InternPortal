using InternPortal.Domain.Entities; 
using InternPortal.Application.Dtos;     
using System.Collections.Generic;
using System.Threading.Tasks;
namespace InternPortal.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(CreateUserDto createUserDto);
            
        Task<User?> LoginAsync(LoginRequest loginRequest);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}
