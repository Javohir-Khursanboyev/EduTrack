﻿namespace EduTrack.Service.DTOs.Students;

public class StudentViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public long Coins { get; set; }
    public long GroupId { get; set; }
}