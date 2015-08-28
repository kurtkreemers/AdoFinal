using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace Videotheek
{
    public class DecimalIngaveRule :ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                decimal result; 
                
                if (value == null || value.ToString() == string.Empty)
                {
                    return new ValidationResult(false,"Getal moet ingevuld zijn");
                }
                if (!decimal.TryParse(value.ToString(), NumberStyles.AllowCurrencySymbol, CultureInfo.CreateSpecificCulture("be-BE"), out result))
                    if (!decimal.TryParse(value.ToString(), out result))
                    {
                        return new ValidationResult(false, "Waarde moet een getal zijn");
                    }
                if(result <= 0)
                {
                    return new ValidationResult(false, "Getal moet groter zijn dan nul");
                }
                   
               
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, ex.Message);
            }
            return new ValidationResult(true, null);
        }
    }
}
