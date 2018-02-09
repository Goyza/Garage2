using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ParkedVehicle
    {
        //ParkedVehicle class contains list of available vehicle 
        public int Id { get; set; }
        //Nessesary to create validation for number
        [CustomRegistrationNumberValidator(ErrorMessage = "Registration Number should have XXX letters and 000 number")]
        [Required(ErrorMessage = "Please type Registratin Number")]
        public string RegistrationNumber { get; set; }
        // Brand of vehicle see enum Brands(optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string Brand { get; set; }
        // Type of vehicle, see enum VehicleTypes (optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string VehicleType { get; set; }
        // Model of vehicle (optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string Model { get; set; }
        // Vehicle color, see enum Color(optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string Color { get; set; }
        //Fuel see enum Fueltypes(optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string FuelType { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CheckInTime { get; set; }

    }

        public enum Brands
        {
            Undefined, Toyota, Ford, Volvo
        }
        public enum VehicleTypes
        {
            Undefined, Sedan, Hatchback, Coupe
        }
        public enum Colors
        {
            Undefined, White, Red, Blue, Yellow, Orange, Green, Purple, Black
        }
        public enum FuelTypes
        {
            Undefined, Diesel, Gasoline, Electric
        }


        public class CustomRegistrationNumberValidator : ValidationAttribute
        {
            public CustomRegistrationNumberValidator() : base("{0} Is to wrong")
            {

            }

            protected override ValidationResult IsValid(object value, ValidationContext Context)
            {
                if (value != null)
                {
                    var valueAsString = value.ToString().Trim();
                    if (valueAsString.Length>6)
                    {
                        var errorMessage = FormatErrorMessage(Context.DisplayName);
                        return new ValidationResult(errorMessage);
                    }

                }
                return ValidationResult.Success;
            }
        }
        public class RestrictedDate : ValidationAttribute
        {
            public RestrictedDate() : base("{0} Is to early")
            {

            }

            protected override ValidationResult IsValid(object value, ValidationContext Context)
            {
                if (value != null)
                {
                    DateTime _date = (DateTime)value;
                    if (_date < DateTime.Now)
                    {
                        var errorMessage = FormatErrorMessage(Context.DisplayName);
                        return new ValidationResult(errorMessage);
                    }

                }
                return ValidationResult.Success;
            }
        
    }
}