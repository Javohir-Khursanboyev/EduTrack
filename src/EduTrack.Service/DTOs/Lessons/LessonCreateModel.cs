namespace EduTrack.Service.DTOs.Lessons;

public class LessonCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
}