using System;
using System.IO;
using LawCheckTazminat.Dependency;
using LawCheckTazminat.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(GeriYukle))]

namespace LawCheckTazminat.iOS
{


    public class GeriYukle:IGeriYukle
    {
        public GeriYukle()
        {
        }

        public void GeriYukle2(Stream ss)
        {

            //throw new NotImplementedException();


            var dosya = App.baglantiDB;
            try
            {
                FileStream _fileStream = new FileStream(dosya, FileMode.Create, System.IO.FileAccess.Write);
                byte[] bytes = new byte[ss.Length];
                ss.Read(bytes, 0, (int)ss.Length);
                _fileStream.Write(bytes, 0, bytes.Length);
                ss.Close();
                _fileStream.Close();

            }catch(Exception ex)
            {

            }



        }
    }
}
