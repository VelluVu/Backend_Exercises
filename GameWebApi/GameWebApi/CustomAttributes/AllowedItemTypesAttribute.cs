using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GameWebApi.Items
{
    internal class AllowedItemTypesAttribute : ValidationAttribute
    {
        private ItemType [ ] _validItemTypes;

        public AllowedItemTypesAttribute ( params ItemType [ ] validItemTypes )
        {
            _validItemTypes = validItemTypes;
        }

        protected override ValidationResult IsValid ( object value, ValidationContext context )
        {
            ItemType type = ( ItemType ) value;

            if ( !_validItemTypes.Contains ( type ) )
            {
                return new ValidationResult ( GetErrorMessage ( ) );
            }

            return ValidationResult.Success;
        }

        private string GetErrorMessage ( )
        {
            return string.Format ( "Item type should be one of: %s",
                string.Join ( ", ", _validItemTypes ) );
        }
    }
}