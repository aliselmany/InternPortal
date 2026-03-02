using InternPortal.Domain.Enums;

namespace InternPortal.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    
    public UserRole Role { get; set; } = UserRole.Intern;

    public virtual Application? Application { get; set; }
}