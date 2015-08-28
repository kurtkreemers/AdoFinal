using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Videotheek
{
    public class GetalGroterDanNulRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                int result;           
                 if (int.TryParse((string)value, out result) && result <= 0)
                    return new ValidationResult(false, "Getal moet groter zijn dan nul");
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return new ValidationResult(true, null);
        }
    }
}
