using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Reflection;
using System.IO;
using LawCheckTazminat.Dependency;
using System.Net.Http.Headers;

namespace LawCheckTazminat.Views.Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RaporTest1 : ContentPage
    {
        public RaporTest1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;

            WordDocument document = new WordDocument();

            //Adding a new section to the document
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new Syncfusion.Drawing.SizeF(612, 792);


            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            paragraph = section.AddParagraph();

            WTextRange textRange;


            //Appends paragraph
            paragraph = section.AddParagraph();

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("BİLİRKİŞİ RAPORU") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("______________ MAHKEMESİ'NE") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("\t\t\t______________ ") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            // Dosya Bilgileri......

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DOSYA NO \t\t\t:") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            paragraph.ParagraphFormat.LineSpacing = 18f;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVACI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVALI \t\t\t: ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("DAVA KONUSU \t\t: Destekten Yoksunluk") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TALEP") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";


            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("SAVUNMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("TESPİT") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("............................................") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
            textRange = paragraph.AppendText("DEĞERLENDİRME VE HESAPLAMA") as WTextRange;
            textRange.CharacterFormat.FontSize = 14f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";

            paragraph = section.AddParagraph();
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Olay Tarihi \t\t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            string olayTarihi = "4.03.2019";
            textRange = paragraph.AppendText(olayTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";


            //RaporTarihi
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Rapor Tarihi \t\t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            string raporTarihi = "4.03.2019";
            textRange = paragraph.AppendText(raporTarihi) as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";

            //Paylaştırma Türü	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Paylaştırma Türü \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            string paylastirmaTuru = "Progresif Rant";
            textRange = paragraph.AppendText(paylastirmaTuru) as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";


            //Yaşam Tablosu	
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Yaşam Tablosu \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            string yasamTablosu = "PMF---";
            textRange = paragraph.AppendText(yasamTablosu) as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";

            //Davalı Kusur Oranı
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.LineSpacing = 18f;
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Davalı Kusur Oranı \t\t : ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.Bold = true;
            textRange.CharacterFormat.FontName = "Calibri";
            string kusurOrani = "%" + "100";
            textRange = paragraph.AppendText(kusurOrani) as WTextRange;
            textRange.CharacterFormat.FontName = "Calibri";


            // PAY TABLOLARI OLUŞTURMA--------------------------------------------



            //Pay tablosu bitişi...................................................













            //Appends paragraph
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.FirstLineIndent = 36;
            //paragraph.BreakCharacterFormat.FontSize = 12f;
            //textRange = paragraph.AppendText("Adventure Works Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, " +
            //    "European and Asian commercial markets. While its base operation is in Bothell, " +
            //    "Washington with 290 employees, several regional sales teams are located throughout their market base.") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;




            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);

            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Sample.docx", "application/msword", stream);


        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {

            //Creates an instance of WordDocument class
            WordDocument document = new WordDocument();
            IWSection section = document.AddSection();
            section.AddParagraph().AppendText("Price Details");
            section.AddParagraph();
            //Adds a new table into Word document
            IWTable table = section.AddTable();
            table.TableFormat.Borders.LineWidth = 2f;

            table.TableFormat.Borders.Horizontal.Color = Syncfusion.Drawing.Color.Black;
            //Adds the first row into table
            WTableRow row = table.AddRow();
            //Adds the first cell into first row 
            WTableCell cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Item");
            //Adds the second cell into first row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Price($)");
            //Adds the second row into table
            row = table.AddRow(true, false);
            //Adds the first cell into second row
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Apple");
            //Adds the second cell into second row
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("50");
            //Adds the third row into table
            row = table.AddRow(true, false);
            //Adds the first cell into third row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Orange");
            //Adds the second cell into third row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("30");
            //Adds the fourth row into table
            row = table.AddRow(true, false);
            //Adds the first cell into fourth row
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Banana");
            //Adds the second cell into fourth row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("20");
            //Adds the fifth row to table
            row = table.AddRow(true, false);
            //Adds the first cell into fifth row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("Grapes");
            //Adds the second cell into fifth row 
            cell = row.AddCell();
            //Specifies the cell width
            cell.Width = 200;
            cell.AddParagraph().AppendText("70");
            //Saves the Word document to MemoryStream
            MemoryStream stream = new MemoryStream();
            document.Save(stream, FormatType.Docx);
            //Save the stream as a file in the device and invoke it for viewing
            Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Table.docx", "application/msword", stream);
            //Closes the document instance
            document.Close();
            //Please download the helper files from the below link to save the stream as file and open the file for viewing in Xamarin platform
            //https://help.syncfusion.com/file-formats/docio/create-word-document-in-xamarin#helper-files-for-xamarin



        }
    }
}