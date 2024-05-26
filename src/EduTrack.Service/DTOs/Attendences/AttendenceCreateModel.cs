using EduTrack.Domain.Enums;

namespace EduTrack.Service.DTOs.Attendences;

public class AttendenceCreateModel
{
    public long LessonId { get; set; }
    public long StudentId { get; set; }
    AttendanceStatus Status { get; set; }
}
