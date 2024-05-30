using EduTrack.Domain.Commons;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class Student : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public long Coins { get; set; }
    public long GroupId { get; set; }
    public Role Role { get; set; }
    public Group Group { get; set; }
    public IEnumerable<AssignmentMark> AssignmentMarks { get; set; }
}
