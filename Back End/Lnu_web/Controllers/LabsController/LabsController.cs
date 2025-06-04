using Lnu_web.Data;
using Lnu_web.Dbo.Labs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Lnu_web.Controllers.LabsController
{
    [ApiController]
    [Route("Labs")]
    public class LabsController : Controller
    {
        private readonly DataContext _dataContext;

        public LabsController(DataContext dataContext)
        {
            _dataContext = dataContext;        
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Invalid file.");

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileRecord = new FileEntity
            {
                FileName = file.FileName, 
                FilePath = filePath, 
                UploadDate = DateTime.UtcNow
            };

            _dataContext.Files.Add(fileRecord);
            await _dataContext.SaveChangesAsync();

            return Ok(new { message = "File uploaded successfully", filePath = filePath });
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadFile(string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                return BadRequest("Invalid GUID format.");
            }

            var fileRecord = _dataContext.Files.FirstOrDefault(f => f.Id == guid);
            if (fileRecord == null)
            {
                return NotFound("File not found in database.");
            }

            var filePath = fileRecord.FilePath;
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found on disk.");
            }

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/octet-stream", fileRecord.FileName);
        }

    }
}
