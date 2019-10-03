using System;
using System.ComponentModel.DataAnnotations;

namespace GameWebApi.Items
{
    internal class DateFromPastAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid ( object value, ValidationContext context )
        {
            // should account for any Time desync between server and client
            DateTime currentTime = DateTime.Now + TimeSpan.FromMinutes ( 10 );

            DateTime date = ( DateTime ) value;

            if ( date > currentTime )
            {
                return new ValidationResult ( "Invalid datetime" );
            }

            return ValidationResult.Success;
        }
    }
}