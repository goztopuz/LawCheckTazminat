using Syncfusion.DataSource;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Behaviors
{
   public class ContactsListViewBehavior : Behavior<Syncfusion.ListView.XForms.SfListView>
    {
        #region Fields

        private Syncfusion.ListView.XForms.SfListView listView;

        #endregion

        #region Overrides
        /// <summary>
        /// Invoked when adding the SfListView to view.
        /// </summary>
        /// <param name="bindable">The SfListView</param>

        protected override void OnAttachedTo(Syncfusion.ListView.XForms.SfListView bindable)
        {
            if (bindable != null)
            {
                listView = bindable;
                listView.DataSource.GroupDescriptors.Add(new GroupDescriptor()
                {
                    PropertyName = "Ad",
                    KeySelector = (object obj1) =>
                    {
                        var item = (obj1 as Models.Contact);
                        return item.Ad[0].ToString(CultureInfo.CurrentCulture);
                    },
                });
                base.OnAttachedTo(bindable);
            }
        }
        /// <summary>
        /// Invoked when the list view is detached. 
        /// </summary>
        /// <param name="bindable">The SfListView</param>

        protected override void OnDetachingFrom(Syncfusion.ListView.XForms.SfListView bindable)
        {
            listView = null;
            base.OnDetachingFrom(bindable);
        }

        #endregion


    }
}
