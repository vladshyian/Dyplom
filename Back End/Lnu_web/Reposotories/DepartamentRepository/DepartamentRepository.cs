using AutoMapper;
using Lnu_web.Data;
using Lnu_web.Dbo.User.Student;
using Lnu_web.Interfaces.Reposotories.IDepartamentRepository;
using Lnu_web.Models.DepartamentModel;
using Lnu_web.Models.User.Student;
using Lnu_web.Models.User.Teacher;
using Microsoft.EntityFrameworkCore;

namespace Lnu_web.Reposotories.DepartamentRepository
{
    public class DepartamentRepository : IDepartamentRepository
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DepartamentRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }
        public async Task<DepartamentListModel> GetDepartamentById(int Id)
        {
            var departament = await _dataContext.departaments.Where(s=>s.Id == Id)
                                                       .Include(s=>s.DepartamentsList)
                                                       .FirstOrDefaultAsync();
            if (departament == null)
            {
                return null;
            }

            var departamentModel = _mapper.Map<DepartamentListModel>(departament);

            return departamentModel;
        }

        public async Task<DepartamentAboutModel> GetDepartamentAbout(int Id)
        {
            var departamentAbout = await _dataContext.departaments.Where(s => s.Id == Id)
                                                            .Include(s => s.DepartamentAbout)
                                                            .FirstOrDefaultAsync();

            if(departamentAbout == null)
            {
                return null;
            }

            var departamentAboutModel = _mapper.Map<DepartamentAboutModel>(departamentAbout);

            return departamentAboutModel;
        }

        public async Task<List<DepartamentListModel>> GetDepartamentList()
        {
            var departamentList = await _dataContext.departaments
                                                .Include(s => s.DepartamentsList)
                                                .ToListAsync();

            if(departamentList.Count == 0)
            {
                return null;
            }

            var departamentListModel = _mapper.Map<List<DepartamentListModel>>(departamentList);

            return departamentListModel;

        }
        public async Task<List<CoreTeacherModel>> GetTeacherByDepartamentId(int departamentId)
        {
            var teachers = await _dataContext.Teachers
                .Where(t => t.CoreInfo.DepartamentId == departamentId)
                .Include(t=>t.CoreInfo)
                .ToListAsync();

            if (teachers.Count == 0)
            {
                return null;
            }

           

            var teachersModel = _mapper.Map<List<CoreTeacherModel>>(teachers);

            return teachersModel;
        }
    }
}
