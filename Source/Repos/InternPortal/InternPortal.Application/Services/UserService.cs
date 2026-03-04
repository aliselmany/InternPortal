using BCrypt.Net;
using InternPortal.Application.Interfaces;
using InternPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using InternPortal.Domain.Enums;
using InternPortal.Infrastructure.Persistence;
using InternPortal.Application.Dtos; 
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace InternPortal.Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

   
    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Surname = u.Surname,
                Email = u.Email
            }).ToListAsync();
    }

    public async Task<bool> RegisterAsync(CreateUserDto dto)
    {
        
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            return false;

        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Surname = dto.Surname,
            Email = dto.Email,
            Password = hashedPassword,
            Role = UserRole.Intern
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<User?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            return null;

        return user;
    }
}