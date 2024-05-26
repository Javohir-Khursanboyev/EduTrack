namespace EduTrack.Service.DTOs.AssignmentMarks;

public class AssignmentMarkCreateModel
{
    public long AssignmentId { get; set; }
    public long StudentId { get; set; }
    public long Coin { get; set; }
    public string Comment { get; set; }
}