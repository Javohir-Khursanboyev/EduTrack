using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class AssignmentMark : Auditable
{
    public long AssignmentId { get; set; }
    public Assignment Assignment { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    public long Coin { get; set; }
    public string Comment { get; set; }
}