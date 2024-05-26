namespace EduTrack.Service.DTOs.Assignments;

public class AssignmentCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime Dedline { get; set; }
    public long Coin { get; set; }
    public long GroupId { get; set; }
}