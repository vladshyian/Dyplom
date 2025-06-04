using Lnu_web.Models.DepartamentModel;
using Lnu_web.Models.ScheduleModel;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Interfaces.Reposotories.IDepartamentRepository
{
    public interface IDepartamentRepository
    {
        Task<List<DepartamentListModel>> GetDepartamentList();
        Task<DepartamentAboutModel> GetDepartamentAbout(int Id);
        Task<List<CoreTeacherModel>> GetTeacherByDepartamentId(int departamentId);
 
    }
}
