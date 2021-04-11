using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Vibe.Mobile.Extensions;
using Xamarin.Forms;

namespace Vibe.Mobile.Controls
{
    public class MaskedEntry : Entry
    {
        public MaskedEntry()
        {
            Visual = VisualMarker.Material;
        }

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (newValue != oldValue)
            {
                string text = newValue.ToCpfFormat();

                if (Text != text)
                    Text = text;
            }
        }
    }
}
