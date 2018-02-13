using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Garage2.DataAccessLayer;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
        public class ParkedVehicle
        {      
            
        //ParkedVehicle class contains list of available vehicle 
        public int Id { get; set; }
        //Nessesary to create validation for number
        [CustomRegistrationNumberValidator(ErrorMessage = "Registration Number should be Uniq and have format XXXXXX")]
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
            Undefined, Car, Bus, Boat
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
        private GarageContext db = new GarageContext();
            public CustomRegistrationNumberValidator() : base("{0} Is to wrong")
            {

            }

            protected override ValidationResult IsValid(object value, ValidationContext Context)
            {
                if (value != null)
                {
                    var valueAsString = value.ToString().Trim();
                var alreadyExist = db.ParkedVehicles.Where(r => r.RegistrationNumber.Equals(valueAsString));

                    if (valueAsString.Length > 6 || alreadyExist.Count()>1 )
                    {
                    var errorMessage = FormatErrorMessage(Context.DisplayName);
                    return new ValidationResult(errorMessage);

                    }

                }
                return ValidationResult.Success;
            }
        }
        
    
}