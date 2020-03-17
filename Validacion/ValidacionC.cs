using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace Parcial2.Validacion
{
    public class ValidacionC :ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Mensaje = value as string;
            if(Mensaje!=null)
            {
                if (Mensaje.Length <= 0)
                    return new ValidationResult(false, "no debe estar vacio");
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "no dejar vacio");
        }
    }
}
