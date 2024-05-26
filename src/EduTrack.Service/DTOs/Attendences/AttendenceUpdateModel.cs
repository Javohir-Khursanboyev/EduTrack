using EduTrack.Domain.Enums;

namespace EduTrack.Service.DTOs.Attendences;

public class AttendenceUpdateModel
{
    public long LessonId { get; set; }
    public long StudentId { get; set; }
    AttendanceStatus Status { get; set; }
}