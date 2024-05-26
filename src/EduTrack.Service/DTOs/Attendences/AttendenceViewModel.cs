using EduTrack.Domain.Enums;

namespace EduTrack.Service.DTOs.Attendences;

public class AttendenceViewModel
{
    public long Id { get; set; }
    public long LessonId { get; set; }
    public long StudentId { get; set; }
    AttendanceStatus Status { get; set; }
}
