using Lnu_web.Models.DepartamentModel;
using Lnu_web.Services.DepartamentService;
using Microsoft.AspNetCore.Mvc;

namespace Lnu_web.Controllers.DepartamentController
{
    [ApiController]
    [Route("Departament")]
    public class DepartamentController : Controller
    {
        private readonly DepartamentService _departamentService;

        public DepartamentController(DepartamentService departamentService)
        {
            _departamentService = departamentService;
        }

        [HttpGet("getDepartamentList")]
        public async Task<ActionResult<DepartamentListModel>> GetDepartamentList()
        {
            var departamentList = await _departamentService.GetDepartamentList();

            if(departamentList == null)
            {
                return NotFound();
            }

            return Ok(departamentList);
        }

        [HttpGet("getDepartamentById/{Id}")]
        public async Task<ActionResult<DepartamentListModel>> GetDepartamentById(int Id)
        {
            var departament = await _departamentService.GetDepartamentById(Id);

            if(departament == null)
            {
                return NotFound();
            }

            return Ok(departament);
        }

        [HttpGet("getDepartamentAbout/{Id}")]
        public async Task<ActionResult<DepartamentAboutModel>> GetDepartamentAbout(int Id)
        {
            var departamentAbout = await _departamentService.GetDepartamentAbout(Id);

            if(departamentAbout == null)
            {
                return NotFound();
            }

            return Ok(departamentAbout);
        }

        [HttpGet("getTeachersByDepartament/{Id}")]
        public async Task<ActionResult> GetTeachersByDepartament(int Id)
        {
            var teachersList = await _departamentService.GetTeacherByDepartamentId(Id);

            if(teachersList == null)
            {
                return null;
            }

            return Ok(teachersList);
        }

    }
}
