namespace MvcCoreLinqToXML.Helpers
{
    public enum Folders { Images = 0, Documents = 1 };

    public class HelperPathProvider
    {
        private IWebHostEnvironment webHostEnvironment;

        public HelperPathProvider(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string MapPath(string filename, Folders folders)
        {
            string carpeta = "";
            if (folders == Folders.Images)
            {
                carpeta = "images";

            }
            else if (folders == Folders.Documents) {
                carpeta = "documents";
            }

            string path = Path.Combine(this.webHostEnvironment.WebRootPath, carpeta, filename);

            return path;

        }
    }
}
