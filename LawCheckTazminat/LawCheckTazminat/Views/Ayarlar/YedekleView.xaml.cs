using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.Ayarlar;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.Ayarlar
{
    public partial class YedekleView : ContentPage
    {
        public YedekleView()
        {
            InitializeComponent();

            this.BindingContext = new YedekleViewModel();
        }

        async void btnYedekle_Clicked(System.Object sender, System.EventArgs e)
        {

            await Share.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(App.baglantiDB)
               
               
            }); ;

            //var message = new EmailMessage
            //{
            //    Subject = "Tazminat Yedekleme",
            //    Body = "Yedek Dosyası Ektedir.",
            //};


            //var file = App.baglantiDB;
           

            //message.Attachments.Add(new EmailAttachment(file));

            //await Email.ComposeAsync(message);



        }
    }
}
