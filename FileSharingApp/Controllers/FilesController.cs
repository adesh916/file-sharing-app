using FileSharingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileSharingApp.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            return View(await _context.Files.ToListAsync());
        }

        // GET: Files/Upload
        public IActionResult Upload()
        {
            return View();
        }

        // POST: Files/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload([Bind("Id,FileName,ContentType,Data,UploadDate")] FileModel fileModel)
        {
            var files = HttpContext.Request.Form.Files;

            if (files != null && files.Count > 0)
            {
                var file = files[0];

                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.FileName = file.FileName;
                    fileModel.ContentType = file.ContentType;
                    fileModel.Data = dataStream.ToArray();
                    fileModel.UploadDate = DateTime.Now;
                }

                _context.Files.Add(fileModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("File", "Please upload a file.");
            return View(fileModel);
        }

        // GET: Files/Download/5
        public async Task<IActionResult> Download(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileModel = await _context.Files.FindAsync(id);
            if (fileModel == null)
            {
                return NotFound();
            }

            return File(fileModel.Data, fileModel.ContentType, fileModel.FileName);
        }
    }
}