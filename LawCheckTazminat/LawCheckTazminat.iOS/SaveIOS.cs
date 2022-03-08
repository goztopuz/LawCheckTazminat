using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using Xamarin.Forms;
using UIKit;
using QuickLook;
using LawCheckTazminat.Dependency;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.OfficeChart;
using Syncfusion.Pdf;

[assembly: Dependency(typeof(SaveIOS))]

class SaveIOS : ISave
{
    //Method to save document as a file and view the saved document
public async Task<string> SaveAndView(string filename, string contentType, MemoryStream stream)
    {
        //Get the root path in iOS device.
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, filename);

        //Create a file and write the stream into it.
        FileStream fileStream = File.Open(filePath, FileMode.Create);
        stream.Position = 0;
        stream.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();

        return filePath;

        //Invoke the saved document for viewing
        UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        //while (currentController.PresentedViewController != null)
        //    currentController = currentController.PresentedViewController;
        //UIView currentView = currentController.View;

        //QLPreviewController qlPreview = new QLPreviewController();
        //QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
        //qlPreview.DataSource = new PreviewControllerDS(item);

        //currentController.PresentViewController(qlPreview, true, null);

    }

    public async Task<string> SaveAndView2(string filename, string contentType, MemoryStream stream)
    {
        //Get the root path in iOS device.
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, filename);

        //Create a file and write the stream into it.
        FileStream fileStream = File.Open(filePath, FileMode.Create);
        stream.Position = 0;
        stream.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();


        //Invoke the saved document for viewing
        UIViewController currentController = UIApplication.SharedApplication.KeyWindow.RootViewController;
        while (currentController.PresentedViewController != null)
            currentController = currentController.PresentedViewController;
        UIView currentView = currentController.View;

        QLPreviewController qlPreview = new QLPreviewController();
        QLPreviewItem item = new QLPreviewItemBundle(filename, filePath);
        qlPreview.DataSource = new PreviewControllerDS(item);

        currentController.PresentViewController(qlPreview, true, null);

        return filePath;

    }


    public Stream SaveView3(string fileName, string ext, string contentType, MemoryStream stream)
    {
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, fileName);

        //Create a file and write the stream into it.
        FileStream fileStream = File.Open(filePath, FileMode.Create);
        stream.Position = 0;
        stream.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();
        LawCheckTazminat.App.WordDosyasi = filePath;



        // Loads the stream into Word Document.
        WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Automatic);
        //Instantiation of DocIORenderer for Word to PDF conversion
        DocIORenderer render = new DocIORenderer();
        //Sets Chart rendering Options.
        render.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
        //Converts Word document into PDF document
        PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);
        //Releases all resources used by the Word document and DocIO Renderer objects
        render.Settings.EmbedCompleteFonts = true;

        render.Dispose();
        wordDocument.Dispose();
        //Saves the PDF file
        MemoryStream outputStream = new MemoryStream();
        pdfDocument.Save(outputStream);
        //Closes the instance of PDF document object
        pdfDocument.Close();



        return outputStream;

    }
        public async Task<FileStream> PdfMemoryToFileStream(MemoryStream mS)
    {

        string fileName = "Sample.pdf";
        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, fileName);
        FileStream fileStream = File.Open(filePath, FileMode.Create);
        mS.Position = 0;
        mS.CopyTo(fileStream);
        fileStream.Flush();
        fileStream.Close();

        return fileStream;
    }




    public string YoluOgrenPdf()
    {

        string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        string filePath = Path.Combine(path, "Sample.pdf");

        return filePath ;

    }


}
