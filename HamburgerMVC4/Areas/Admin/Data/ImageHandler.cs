using HamburgerMVC4.Areas.Admin.Models;
using HamburgerMVC4.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HamburgerMVC4.Areas.Admin.Data
{
    public static class ImageHandler
    {
        public static string CreateImage(IFormFile formfile,string root)
        {
            string ext = Path.GetExtension(formfile.FileName);

            var fileName = Guid.NewGuid() + ext;

            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image",root, fileName);

            var fs = new FileStream(location, FileMode.Create);

           formfile.CopyTo(fs);

            fs.Close();

            return fileName;
        }

        public static string EditImage(IFormFile formfile,string root,string updatedImageFileName)
        {
            string file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image",root, updatedImageFileName);
            System.IO.File.Delete(file);

            string ext = Path.GetExtension(formfile.FileName);

            var fileName = Guid.NewGuid() + ext;

            var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image",root, fileName);

            var fs = new FileStream(location, FileMode.Create);

            formfile.CopyTo(fs);

            fs.Close();

            return fileName;
        }
    }
}
