using EduTrack.Domain.Commons;
using EduTrack.Domain.Enums;

namespace EduTrack.Domain.Entities;

public class Attendence : Auditable
{
    public long LessonId { get; set; }
    public Lesson Lesson { get; set; }
    public long StudentId { get; set; }
    public Student Student { get; set; }
    AttendanceStatus Status { get; set; }
}
