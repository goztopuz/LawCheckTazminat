using Syncfusion.SfDataGrid.XForms.UWP;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace LawCheckTazminat.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            SfDataGridRenderer.Init();
            // Applying localization for UWP
            CultureInfo.CurrentUICulture = new CultureInfo("tr");

            var dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.sqlite");
            LoadApplication(new LawCheckTazminat.App(dbPath));

        }
    }
}
