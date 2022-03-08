using LawCheckTazminat.Dependency;
using LawCheckTazminat.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.ViewModels.PdfViewB
{
     public  class PdfViewModel : ViewModelBase
    {
        private Stream m_pdfDocumentStream;

        public PdfViewModel(MemoryStream stream, string documentName, string documentExtension)
        {
            var yoll =  Xamarin.Forms.DependencyService.Get<ISave>().SaveView3(documentName, "docx",
                "application/msword", stream);
            m_pdfDocumentStream = yoll;
        }

        public Stream PdfDocumentStream
        {
            get
            {
                return m_pdfDocumentStream;
            }
            set
            {
                m_pdfDocumentStream = value;
                OnPropertyChanged();
            }
        }




        public ICommand RaporBitisCommand => new Command(OnRaporBitis);

        async private void OnRaporBitis(object obj)
        {
            if (IsBusy == true)
            {
                return;
            }
            IsBusy = true;
            //  var sayfa = new Views.AnaSayfaV.Anasayfa();
            await Application.Current.MainPage.Navigation.PopModalAsync();
            IsBusy = false;
        }


        public ICommand PaylasCommand => new Command(OnPaylas);
        
        async private void OnPaylas(object obj)
        {
            if(IsBusy== true)
            {
                return;
            }
            IsBusy = true;
            //var fn = "Attachment.txt";
            //var file = Path.Combine(FileSystem.CacheDirectory, fn);
            //File.WriteAllText(file, "Hello World");

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Dosya Gönderme",
                File = new ShareFile(App.WordDosyasi)
            });

            IsBusy = false;
        }

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}
