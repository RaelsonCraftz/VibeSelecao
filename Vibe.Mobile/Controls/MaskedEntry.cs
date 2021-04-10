using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Vibe.Mobile.Controls
{
    public class MaskedEntry : Entry
    {
        private readonly string numericRegex = @"[^0-9]";

        public MaskedEntry()
        {
            Visual = VisualMarker.Material;
        }

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);

            if (newValue != oldValue)
            {
                string text = Regex.Replace(newValue, numericRegex, "");
                text = FormataCPF(text);

                if (Text != text)
                    Text = text;
            }
        }

        private string FormataCPF(string text)
        {
            // Normaliza o texto para 11 caracteres
            text = text.PadRight(11);

            // Removendo todos os digitos excedentes que apareçam a mais (caso o número digitado tenha mais de 11 caracteres)
            if (text.Length > 11)
            {
                text = text.Remove(11);
            }

            text = text
                // Insere o primeiro ponto da máscara
                .Insert(3, ".")
                // Insere o segundo ponto da máscara
                .Insert(7, ".")
                // Insere o hífen que precede o dígito final
                .Insert(11, "-")
                // Remove os seguintes caracteres caso apareçam no final do texto
                .TrimEnd(new char[] { ' ', '.', '-' });
            return text;
        }
    }
}
