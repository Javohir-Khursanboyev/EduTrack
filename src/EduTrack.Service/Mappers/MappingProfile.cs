using AutoMapper;
using EduTrack.Domain.Entities;
using EduTrack.Service.DTOs.Assets;
using EduTrack.Service.DTOs.AssignmentMarks;
using EduTrack.Service.DTOs.Assignments;
using EduTrack.Service.DTOs.Attendences;
using EduTrack.Service.DTOs.Groups;
using EduTrack.Service.DTOs.Lessons;
using EduTrack.Service.DTOs.Students;
using EduTrack.Service.DTOs.Teachers;

namespace EduTrack.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AssetViewModel, Asset>().ReverseMap();

        CreateMap<Student, StudentCreateModel>().ReverseMap();
        CreateMap<Student, StudentUpdateModel>().ReverseMap();
        CreateMap<StudentViewModel, Student>().ReverseMap();

        CreateMap<Teacher, TeacherCreateModel>().ReverseMap();
        CreateMap<Teacher, TeacherUpdateModel>().ReverseMap();
        CreateMap<TeacherViewModel, Teacher>().ReverseMap();

        CreateMap<Group, GroupCreateModel>().ReverseMap();
        CreateMap<Group, GroupUpdateModel>().ReverseMap();
        CreateMap<GroupViewModel, Group>().ReverseMap();

        CreateMap<Lesson, LessonCreateModel>().ReverseMap();
        CreateMap<Lesson, LessonUpdateModel>().ReverseMap();
        CreateMap<LessonViewModel, Lesson>().ReverseMap();

        CreateMap<Attendence, AttendenceCreateModel>().ReverseMap();
        CreateMap<Attendence, AttendenceUpdateModel>().ReverseMap();
        CreateMap<AttendenceViewModel, Attendence>().ReverseMap();

        CreateMap<Assignment, AssignmentCreateModel>().ReverseMap();
        CreateMap<Assignment, AssignmentUpdateModel>().ReverseMap();
        CreateMap<AssignmentViewModel, Assignment>().ReverseMap();

        CreateMap<AssignmentMark, AssignmentMarkCreateModel>().ReverseMap();
        CreateMap<AssignmentMark, AssignmentMarkCreateModel>().ReverseMap();
        CreateMap<AssignmentMarkViewModel, AssignmentMark>().ReverseMap();
    }
}