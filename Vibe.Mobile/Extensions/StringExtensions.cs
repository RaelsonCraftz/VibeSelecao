using System.Text.RegularExpressions;

namespace Vibe.Mobile.Extensions
{
    public static class StringExtensions
    {
        public static string ToCpfFormat(this string str)
        {
            string numericRegex = @"[^0-9]";

            string text = Regex.Replace(str, numericRegex, "");

            // Normaliza o texto para 11 caracteres
            text = text.PadRight(11);

            // Removendo todos os digitos excedentes que apareçam a mais (caso o número digitado tenha mais de 11 caracteres)
            if (text.Length > 11)
            {
                text = text.Remove(11);
            }

            return text
                // Insere o primeiro ponto da máscara
                .Insert(3, ".")
                // Insere o segundo ponto da máscara
                .Insert(7, ".")
                // Insere o hífen que precede o dígito final
                .Insert(11, "-")
                // Remove os seguintes caracteres caso apareçam no final do texto
                .TrimEnd(new char[] { ' ', '.', '-' });
        }
    }
}
