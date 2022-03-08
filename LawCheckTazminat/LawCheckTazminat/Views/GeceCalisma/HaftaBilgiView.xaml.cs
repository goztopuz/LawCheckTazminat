using System;
using System.Collections.Generic;
using LawCheckTazminat.ViewModels.GeceCalismaB;
using Xamarin.Forms;

namespace LawCheckTazminat.Views.GeceCalisma
{
    public partial class HaftaBilgiView : ContentPage
    {

        //HaftaBilgiViewModel vm;
        Models.GeceCalisma gece2 = new Models.GeceCalisma();
        Models.GeceMesaiHaftalar hafta2 = new Models.GeceMesaiHaftalar();
        public HaftaBilgiView(Models.GeceCalisma _gece, Models.GeceMesaiHaftalar _hafta)
        {
            InitializeComponent();
            gece2 = _gece;
            hafta2 = _hafta;

       

            this.BindingContext = new HaftaBilgiViewModel(_gece,_hafta);


        }

        void chkGun1_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun2_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun3_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun4_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun5_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun6_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        void chkGun7_CheckedChanged(System.Object sender, Xamarin.Forms.CheckedChangedEventArgs e)
        {
        }

        private void Gun1BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Time")
            {
                Gun1SaatHesapla();
            }
        
        }

        private void Gun1BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun1SaatHesapla();
        }

        private void Gun1SaatHesapla()
        {
            string aa = "";
            var vm = BindingContext as HaftaBilgiViewModel;

            try
            {
                vm.Gun1SaatCommand.Execute(aa);
            }
            catch (Exception ex)
            {


            }
        }
        private void Gun2BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun2SaatHesapla();
        }

        private void Gun2BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun2SaatHesapla();
        }
        private void Gun2SaatHesapla()
        {
            string aa = "";
            var vm = BindingContext as HaftaBilgiViewModel;

            if(vm != null)
            {
                vm.Gun2SaatCommand.Execute(aa);

            }
        }

        private void Gun3BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun3SaatHesapla();      
        }

        private void Gun3BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun3SaatHesapla();
        }
        private void Gun3SaatHesapla()
        {
            string aa = "";
            
            var vm = BindingContext as HaftaBilgiViewModel;

            if(vm != null)
            {
                vm.Gun3SaatCommand.Execute(aa);
            }

    
        }
        private void Gun4SaatHesapla()
        {
            string aa = "";
            var vm = BindingContext as HaftaBilgiViewModel;

            if( vm != null)
            {
                vm.Gun4SaatCommand.Execute(aa);

            }
        }

        private void Gun5SaatHesapla()
        {
            string aa = "";

            var vm = BindingContext as HaftaBilgiViewModel;

            if( vm != null)
            {
                vm.Gun5SaatCommand.Execute(aa);
            }
        }

        private void Gun6SaatHesapla()
        {
            string aa = "";

            var vm = BindingContext as HaftaBilgiViewModel;

            if(  vm != null)
            {
                vm.Gun6SaatCommand.Execute(aa);
            }


        }
        private void Gun7SaatHesapla()
        {
            string aa = "";

            var vm = BindingContext as HaftaBilgiViewModel;

            if( vm != null)
            {
                vm.Gun7SaatCommand.Execute(aa);

            }


        }

        private void chkGun3_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Gun4BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun4SaatHesapla();
        }

        private void Gun4BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun4SaatHesapla();
        }

        private void chkGun4_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Gun5BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun5SaatHesapla();
        }

        private void Gun5BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun5SaatHesapla();
        }

        private void chkGun5_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Gun6BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun6SaatHesapla();
        }

        private void Gun6BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun6SaatHesapla();

        }

        private void chkGun6_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

        }

        private void chkGun7_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {

        }

        private void Gun7BasTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun7SaatHesapla();

        }

        private void Gun7BitTimer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Gun7SaatHesapla();

        }

        public static implicit operator HaftaBilgiView(HaftaBilgiViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
