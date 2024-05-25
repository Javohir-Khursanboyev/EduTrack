namespace EduTrack.Domain.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreateByUserId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long UpdateByUserId { get; set; }
    public DateTime DeletedAt { get; set; }
    public long DeletedByUserId { get;set; }
    public bool IsDeleted { get; set; } 
}