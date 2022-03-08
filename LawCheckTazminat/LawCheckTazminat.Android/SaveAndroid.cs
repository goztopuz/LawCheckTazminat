using System;
using System.IO;
using Android.Content;
using Java.IO;
using Xamarin.Forms;
using System.Threading.Tasks;
using LawCheckTazminat.Droid;
using LawCheckTazminat.Dependency;
using Syncfusion.DocIORenderer;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf;
using Syncfusion.OfficeChart;
using AndroidX.Core.Content;

[assembly: Dependency(typeof(SaveAndroid))]
namespace LawCheckTazminat.Droid
{
    class SaveAndroid : ISave
    {
        public Task<FileStream> PdfMemoryToFileStream(MemoryStream mS)
        {
            throw new NotImplementedException();
        }

        //Method to save document as a file in Android and view the saved document
        public async Task<string> SaveAndView2(string fileName, String contentType, MemoryStream stream)
        {

            //WordDocument wordDocument = new WordDocument(stream2, Syncfusion.DocIO.FormatType.Automatic);

            ////Instantiation of DocIORenderer for Word to PDF conversion

            //DocIORenderer render = new DocIORenderer();

            ////Converts Word document into PDF document

            //PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);

            ////Releases all resources used by the Word document and DocIO Renderer objects

            //render.Dispose();

            //wordDocument.Dispose();

            ////Save the document into memory stream

            //MemoryStream stream = new MemoryStream();

            //pdfDocument.Save(stream);

            //stream.Position = 0;

            ////Close the document 

            //pdfDocument.Close();


            string root = null;
            //Get the root path in android device.
            if (Android.OS.Environment.IsExternalStorageEmulated)
            {
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            }
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Create directory and file 
            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            //Remove if the file exists
            if (file.Exists()) file.Delete();

            //Write the stream into the file
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();
            
            // Loads the stream into Word Document.
            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Automatic);
            //Instantiation of DocIORenderer for Word to PDF conversion
            DocIORenderer render = new DocIORenderer();
            //Sets Chart rendering Options.
            render.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
            //Converts Word document into PDF document
            PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);
            //Releases all resources used by the Word document and DocIO Renderer objects
            render.Dispose();
            wordDocument.Dispose();
            //Saves the PDF file
            MemoryStream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);
            //Closes the instance of PDF document object
            pdfDocument.Close();

            

            string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());

            string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);

            Intent intent = new Intent(Intent.ActionView);

            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);

            Android.Net.Uri path = FileProvider.GetUriForFile(Forms.Context, Android.App.Application.Context.PackageName + ".provider", file);

            intent.SetDataAndType(path, mimeType);

            intent.AddFlags(ActivityFlags.GrantReadUriPermission);

            Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));






         







            ////Invoke the created file for viewing
            //if (file.Exists())
            //{
            //    Android.Net.Uri path = Android.Net.Uri.FromFile(file);
            //    string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
            //    string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
            //    Intent intent = new Intent(Intent.ActionView);
            //    intent.SetDataAndType(path, mimeType);
            //    Forms.Context.StartActivity(Intent.CreateChooser(intent, "Choose App"));
            //}

            return "AA";
        }


        public  Stream SaveView3(string fileName, string ext, string contentType, MemoryStream stream)
        {
            string root = null;
            //Get the root path in android device.
            //if (Android.OS.Environment.IsExternalStorageEmulated)
            //{
            //    root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            //}
            //else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //Create directory and file 
            Java.IO.File myDir = new Java.IO.File(root + "/Syncfusion");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);

            //Remove if the file exists
            if (file.Exists()) file.Delete();

            //Write the stream into the file
            FileOutputStream outs = new FileOutputStream(file);
            outs.Write(stream.ToArray());

            outs.Flush();
            outs.Close();


            App.WordDosyasi = file.Path;

            // Loads the stream into Word Document.
            WordDocument wordDocument = new WordDocument(stream, Syncfusion.DocIO.FormatType.Automatic);
            //Instantiation of DocIORenderer for Word to PDF conversion
            DocIORenderer render = new DocIORenderer();
            //Sets Chart rendering Options.
            render.Settings.ChartRenderingOptions.ImageFormat = ExportImageFormat.Jpeg;
            //Converts Word document into PDF document
            PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);
            //Releases all resources used by the Word document and DocIO Renderer objects
            render.Dispose();
            wordDocument.Dispose();
            //Saves the PDF file
            MemoryStream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);
            //Closes the instance of PDF document object
            pdfDocument.Close();



            return outputStream;

        }

        public Task<string> SaveAndView(string filename, string contentType, MemoryStream stream)
        {
            throw new NotImplementedException();
        }

        public string YoluOgrenPdf()
        {
            throw new NotImplementedException();
        }

     

    }
}