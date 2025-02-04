
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebApplication1.Presentation.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public IActionResult Index()
        {
            return View();
        }

        // POST: Upload
        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                // Define the path to save the file
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create a unique file name
                var filePath = Path.Combine(uploadsFolder, uploadedFile.FileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(stream);
                }

                return Json(new { success = true, message = "File uploaded successfully!" });
            }

            return Json(new { success = false, message = "File upload failed." });
        }
    }
}
