﻿using EduTrack.Domain.Commons;

namespace EduTrack.Domain.Entities;

public class Teacher : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public IEnumerable<Group> Groups { get; set; }
}