using Lnu_web.Data;
using Lnu_web.Interfaces.Services.IDepartamentService;
using Lnu_web.Models.DepartamentModel;
using Lnu_web.Models.User.Teacher;
using Lnu_web.Reposotories.DepartamentRepository;

namespace Lnu_web.Services.DepartamentService
{
    public class DepartamentService : IDepartamentService
    {
        private readonly DepartamentRepository _departamentRepository;

        public DepartamentService(DepartamentRepository departamentRepository)
        {
            _departamentRepository = departamentRepository;
        }

        public async Task<DepartamentListModel> GetDepartamentById(int Id)
        {
            var departament = await _departamentRepository.GetDepartamentById(Id);

            if (departament == null)
            {
                return null;
            }

            return departament;
        }

        public async Task<DepartamentAboutModel> GetDepartamentAbout(int Id)
        {
            var departamentAbout = await _departamentRepository.GetDepartamentAbout(Id);

            if (departamentAbout == null)
            {
                return null;
            }

            return departamentAbout;
        }

        public async Task<List<DepartamentListModel>> GetDepartamentList()
        {
            var departamentList = await _departamentRepository.GetDepartamentList();

            if(departamentList == null)
            {
                return null;
            }

            return departamentList;
        }

        public async Task<List<CoreTeacherModel>> GetTeacherByDepartamentId(int departamentId)
        {
            var teachers = await _departamentRepository.GetTeacherByDepartamentId(departamentId);

            if(teachers == null)
            {
                return null;
            }

            return teachers;
        }
    }
}
