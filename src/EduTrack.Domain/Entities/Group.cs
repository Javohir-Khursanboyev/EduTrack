using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class Group : Auditable
{
    public string Name { get; set; }
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public IEnumerable<Student> Students { get; set; }
    public IEnumerable<Lesson> Lessons { get; set; }
    public IEnumerable<Attendence> Attendences { get; set; }
}