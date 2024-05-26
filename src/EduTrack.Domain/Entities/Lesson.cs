using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class Lesson : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public IEnumerable<Attendence> Attendences { get; set; }
}