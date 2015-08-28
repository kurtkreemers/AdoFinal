using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Videotheek
{
     public class GenreSelectRule : ValidationRule
    {
         public override ValidationResult Validate(object value, CultureInfo cultureInfo)
         {
             try
             {
                 if(value == null)
                     return new ValidationResult(false, "Genre moet gekozen worden");
             }
             catch (Exception ex)
             {
                 return new ValidationResult(false, ex.Message);
             }
             return new ValidationResult(true, null);
         }
    }
}
