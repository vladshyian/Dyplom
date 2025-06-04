using Lnu_web.Models.DepartamentModel;
using Lnu_web.Models.ScheduleModel;
using Lnu_web.Models.User.Teacher;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lnu_web.Interfaces.Services.IDepartamentService
{
    public interface IDepartamentService
    {
        Task<List<DepartamentListModel>> GetDepartamentList();
        Task<DepartamentAboutModel> GetDepartamentAbout(int Id);
        Task<List<CoreTeacherModel>> GetTeacherByDepartamentId(int departamentId);

    }
}
