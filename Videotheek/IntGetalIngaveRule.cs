using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Videotheek
{
    public class IntGetalIngaveRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int result;
                if (((string)value).Length == 0)
                    return new ValidationResult(false, "Getal moet ingevuld zijn");
                else if (!int.TryParse((string)value, out result))
                    return new ValidationResult(false, "Enkel getallen ingeven");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return new ValidationResult(true, null);
        }
    }
}
