﻿using Garage2.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class ParkingVehicleEdit : IValidatableObject
    {
        public int Id { get; set; }
        private Parking parking = new Parking();
        //Nessesary to create validation for number
        [Required(ErrorMessage = "Please type Registratin Number")]
        [CustomRegistrationNumberValidator(ErrorMessage = "Registration Number should be Uniq and have format XXXXXX")]
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

        //Parking Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            var newParkingPlace = parking.GetFreeParkingPlace(VehicleType);
            if (newParkingPlace.Count() == 0)
            {
                yield return new ValidationResult("Not Enogth space!");
            }
        }

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

                if (valueAsString.Length > 6 || alreadyExist.Count() > 0)
                {
                    var errorMessage = FormatErrorMessage(Context.DisplayName);
                    return new ValidationResult(errorMessage);

                }

            }
            return ValidationResult.Success;
        }
    }

}