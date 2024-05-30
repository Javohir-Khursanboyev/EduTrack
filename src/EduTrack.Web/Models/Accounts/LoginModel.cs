using System.ComponentModel.DataAnnotations;

namespace EduTrack.Web.Models.Accounts;

public class LoginModel
{
    public string Login { get; set; }

    public string Password { get; set; }
}