
using System.IO;
using System.Threading.Tasks;

namespace LawCheckTazminat.Dependency
{
   public interface ISave
    {
        Task<string> SaveAndView(string filename, string contentType, MemoryStream stream);

        Task<string> SaveAndView2(string filename, string contentType, MemoryStream stream);

       Stream SaveView3(string fileName, string ext, string contentType, MemoryStream stream);
        string YoluOgrenPdf();

        Task<FileStream> PdfMemoryToFileStream(MemoryStream mS);



    }

  


}
