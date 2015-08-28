using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Videotheek
{
    public class StringIngaveRule :ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
     {
            try
            {
                if (String.IsNullOrWhiteSpace((string)value))
                    return new ValidationResult(false, "Waarde moet ingevuld zijn");
                else if (Regex.IsMatch(((string)value),@"^[a-zA-Z ]+$"))
                    return new ValidationResult(true, null);
                else
                    return new ValidationResult(false, "Enkel geldige karakters ingeven");

            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
        }           
    }
}
