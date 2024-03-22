using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace cTest.Controllers
{
    public class FileController : Controller
    {
        public static string? SaveFile(IFormFile photo)
        {
            string rootPath = Directory.GetCurrentDirectory();
            if (photo != null)
            {
                var originalFileName = Path.GetFileName(photo.FileName);
                var uniqueFilePath = $@"{rootPath}\wwwroot\img\{originalFileName}";
                var stream = System.IO.File.Create(uniqueFilePath);
                photo.CopyToAsync(stream);
                return $@"/img/{originalFileName}";
            }
            return null;
        } 
    }
}
