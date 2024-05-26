using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class Assignment : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime Dedline { get; set; }
    public long Coin { get; set; }
    public long? AssetId { get; set; }
    public Asset Asset { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }    
    public IEnumerable<AssignmentMark> AssignmentMarks { get; set; }
}
