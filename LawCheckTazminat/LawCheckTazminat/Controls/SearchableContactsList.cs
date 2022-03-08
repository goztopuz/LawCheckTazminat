using LawCheckTazminat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LawCheckTazminat.Controls
{

 public   class SearchableContactsList : SearchableListView
    {
        /// <summary>
        /// Filtering the list view items based on the search text.
        /// </summary>
        /// <param name="obj">The list view item</param>
        /// <returns>Returns the filtered item</returns>
        public override bool FilterContacts(object obj)
        {
            if (base.FilterContacts(obj))
            {
                var taskInfo = obj as Contact;
                if (taskInfo == null || string.IsNullOrEmpty(taskInfo.Ad))
                {
                    return false;
                }

                return taskInfo.SearchTerm.ToUpperInvariant().Contains(this.SearchText.ToUpperInvariant());
            }

            return false;
        }


    }

}
