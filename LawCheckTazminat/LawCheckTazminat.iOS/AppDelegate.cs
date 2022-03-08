using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using StoreKit;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.MaskedEdit;
using UIKit;

namespace LawCheckTazminat.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Syncfusion.SfDataGrid.XForms.iOS.SfDataGridRenderer.Init();

            new SfBusyIndicatorRenderer();

            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.SfPdfViewer.XForms.iOS.SfPdfDocumentViewRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Xamarin.Forms.Svg.iOS.SvgImage.Init();
            Syncfusion.XForms.iOS.PopupLayout.SfPopupLayoutRenderer.Init();
            new Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer();
            ImageCircleRenderer.Init();
            Plugin.InAppBilling.InAppBillingImplementation.OnShouldAddStorePayment = OnShouldAddStorePayment;
            var current = Plugin.InAppBilling.CrossInAppBilling.Current; 
            var libPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
                "..", "Library");

            if(! Directory.Exists(libPath))
            {
                Directory.CreateDirectory(libPath);
            }

            var dbPath = Path.Combine(libPath, "database.sqlite");

            //var dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "database.sqlite");

            SQLitePCL.Batteries.Init();

            SfMaskedEditRenderer.Init();


            LoadApplication(new App(dbPath));


            return base.FinishedLaunching(app, options);
        }

        bool OnShouldAddStorePayment(SKPaymentQueue queue, SKPayment payment, SKProduct product)
        {
            //Process and check purchases
            return true;
        }

    }
}
