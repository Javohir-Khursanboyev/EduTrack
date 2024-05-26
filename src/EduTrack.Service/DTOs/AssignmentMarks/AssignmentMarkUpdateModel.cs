namespace EduTrack.Service.DTOs.Assignments;

public class AssignmentMarkUpdateModel
{
    public long AssignmentId { get; set; }
    public long StudentId { get; set; }
    public long Coin { get; set; }
    public string Comment { get; set; }
}
