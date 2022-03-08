using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LawCheckTazminat.Controls
{
     public  class BorderlessEditor :Editor
    {
        public BorderlessEditor()
        {
            this.TextChanged += this.ExtendableEditor_TextChanged;
        }

        private void ExtendableEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.InvalidateMeasure();
        }
    }
}
