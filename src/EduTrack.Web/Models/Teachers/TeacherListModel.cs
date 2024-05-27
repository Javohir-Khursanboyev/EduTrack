using EduTrack.Service.DTOs.Teachers;
using X.PagedList;

namespace EduTrack.Web.Models.Teachers;

public class TeacherListModel
{
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public IPagedList<TeacherViewModel> Teachers { get; set; }
}