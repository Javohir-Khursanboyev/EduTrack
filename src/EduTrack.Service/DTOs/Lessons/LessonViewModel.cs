namespace EduTrack.Service.DTOs.Lessons;

public class LessonViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public long GroupId { get; set; }
}