using EduTrack.Service.DTOs.Students;
using EduTrack.Service.DTOs.Teachers;
using X.PagedList;

namespace EduTrack.Web.Models.Students;

public class StudentListModel
{
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public IPagedList<StudentViewModel> Students { get; set; }
}