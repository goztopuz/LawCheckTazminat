using LawCheckTazminat.ViewModels.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LawCheckTazminat.Views.Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KisiAramaView : ContentPage
    {
        public KisiAramaView(string tur)
        {
            InitializeComponent();

            this.BindingContext = new KisiAramaViewModel(tur);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var ab= lstKisiler.ItemsSource;
        }

        private void SearchButton_Clicked(object sender, EventArgs e)
        {

        }

        private void BackToTitle_Clicked(object sender, EventArgs e)
        {

        }

        private void SearchButton_Clicked_1(object sender, EventArgs e)
        {
            Search.IsVisible = true;
            this.ContactsTitle.IsVisible = false;

            if (this.ContactsTitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from 0 to full width when it added to the view.
                var expandAnimation = new Animation(
                    property =>
                    {
                        Search.WidthRequest = property;
                        opacity = property / ContactsTitleView.Width;
                        Search.Opacity = opacity;
                    }, 0, ContactsTitleView.Width, Easing.Linear);
                expandAnimation.Commit(Search, "Expand", 16, 250, Easing.Linear, (p, q) => this.SearchExpandAnimationCompleted());
            }
        }

        private void BackToTitle_Clicked_1(object sender, EventArgs e)
        {
            this.SearchButton.IsVisible = true;
            if (this.ContactsTitleView != null)
            {
                double opacity;

                // Animating Width of the search box, from full width to 0 before it removed from view.
                var shrinkAnimation = new Animation(property =>
                {
                    Search.WidthRequest = property;
                    opacity = property / ContactsTitleView.Width;
                    Search.Opacity = opacity;
                },
                ContactsTitleView.Width, 0, Easing.Linear);
                shrinkAnimation.Commit(Search, "Shrink", 16, 250, Easing.Linear, (p, q) => this.SearchBoxAnimationCompleted());
            }

            SearchEntry.Text = string.Empty;


        }

        private void SearchBoxAnimationCompleted()
        {
            this.Search.IsVisible = false;
            this.ContactsTitle.IsVisible = true;
        }

        /// <summary>
        /// Invokes when search expand Animation completed.
        /// </summary>
        private void SearchExpandAnimationCompleted()
        {
            this.SearchEntry.Focus();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                if (Search.IsVisible)
                {
                    Search.WidthRequest = width;
                }
            }
        }


    }
}