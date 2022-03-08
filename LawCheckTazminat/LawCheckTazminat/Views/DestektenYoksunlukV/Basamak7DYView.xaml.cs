using LawCheckTazminat.Models;
using LawCheckTazminat.ViewModels.DestektenYoksunlukB;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Syncfusion.SfDataGrid.XForms;
using Syncfusion.SfDataGrid.XForms.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.DestektenYoksunlukV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Basamak7DYView : ContentPage
    {
        Basamak7DYViewModel vm;
        public Basamak7DYView(DestektenYoksunluk dyy)
        {
            InitializeComponent();
            this.BindingContext =vm= new Basamak7DYViewModel(dyy);
            txtIsleyecekMaas.Culture = new System.Globalization.CultureInfo("tr-TR");

            //dataGrid.CellRenderers.Remove("TextView");
            //dataGrid.CellRenderers.Add("TextView", new GridCellTextViewRendererExt());

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.BekarDurumKontrolCommand.Execute("-");
        }

        private void dataGrid_GridLoaded(object sender, Syncfusion.SfDataGrid.XForms.GridLoadedEventArgs e)
        {
            //this.dataGrid.BeginEdit(2, 2);
        }
        bool guncelle = false;
        private void dataGrid_CurrentCellEndEdit(object sender, GridCurrentCellEndEditEventArgs e)
        {
            //var datagrid = sender as SfDataGrid;

            //int j = e.RowColumnIndex.ColumnIndex;
            //int i = e.RowColumnIndex.RowIndex;

            //string aa = e.NewValue.ToString();
            var vm = BindingContext as Basamak7DYViewModel;


            MaasCell mc = new MaasCell();
            ////mc.sutun = j;
            ////mc.satır = i;
            ////mc.deger = aa;
            vm.MaasGuncelle.Execute(mc);

            guncelle = true;

        }

        //private void dataGrid_ValueChanged(object sender, Syncfusion.SfDataGrid.XForms.ValueChangedEventArgs e)
        //{
        //    var vm = BindingContext as Basamak7DYViewModel;


        //    MaasCell mc = new MaasCell();
        //    //mc.sutun = j;
        //    //mc.satır = i;
        //    //mc.deger = aa;
        //    vm.MaasGuncelle.Execute(mc);

        //}

        private void dataGrid_SelectionChanged(object sender, GridSelectionChangedEventArgs e)
        {
            if (guncelle == false)
            {
                return;
            }else
            {
                //Güncelleme işlemi
                guncelle = false;


                MaasCell mc = new MaasCell();
                var vm = BindingContext as Basamak7DYViewModel;
                ////mc.sutun = j;
                ////mc.satır = i;
                ////mc.deger = aa;
                vm.MaasGuncelle.Execute(mc);
            }

        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string aa = "";
            var vm = BindingContext as Basamak7DYViewModel;
            vm.IptalCommand.Execute(aa);

        }
    }

    public class GridCellTextViewRendererExt : GridCellTextViewRenderer
    {
        public GridCellTextViewRendererExt()
        {
        }
        public override void OnInitializeEditView(DataColumnBase dataColumn, SfEntry view)
        {
            base.OnInitializeEditView(dataColumn, view);
        }

        protected override void OnEnteredEditMode(DataColumnBase dataColumn, View currentRendererElement)
        {
            base.OnEnteredEditMode(dataColumn, currentRendererElement);
            
            (currentRendererElement as SfEntry).Keyboard = Keyboard.Numeric;

            //if (dataColumn.GridColumn.MappingName == "OrderID")
            //    (currentRendererElement as SfEntry).Keyboard = Keyboard.Numeric;
            //else
            //    (currentRendererElement as SfEntry).Keyboard = Keyboard.Text;
        }
    }
}