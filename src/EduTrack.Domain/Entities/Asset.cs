using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}