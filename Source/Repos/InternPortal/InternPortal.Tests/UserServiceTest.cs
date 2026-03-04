using InternPortal.Application.Services;
using InternPortal.Application.Dtos;
using InternPortal.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace InternPortal.Tests;

public class UserServiceTests
{
 
    private AppDbContext GetDatabaseContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDbContext(options);
        databaseContext.Database.EnsureCreated();
        return databaseContext;
    }

    [Fact]
    public async Task RegisterAsync_ShouldReturnTrue_WhenUserIsSuccessfullyCreated()
    {
       
        var context = GetDatabaseContext();
        var service = new UserService(context);
        var newUser = new CreateUserDto
        {
            Email = "aliselmanly@gmail.com",
            Password = "123456",
            Name = "ali selman",
            Surname = "yılmaz"
        };

     
        var result = await service.RegisterAsync(newUser);

        Assert.True(result); 

        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
        Assert.NotNull(savedUser); 
    }
}