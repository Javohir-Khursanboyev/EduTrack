using EduTrack.Domain.Commons;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class Teacher : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}