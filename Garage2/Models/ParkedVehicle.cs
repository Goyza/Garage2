using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Garage2.DataAccessLayer;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
        public class ParkedVehicle//: IValidatableObject
        {

        private Parking parking = new Parking();
        //ParkedVehicle class contains list of available vehicle 
        public int Id { get; set; }
        //Nessesary to create validation for number
        [Required(ErrorMessage = "Please type Registratin Number")]
        public string RegistrationNumber { get; set; }
        // Brand of vehicle see enum Brands(optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string Brand { get; set; }
        // Type of vehicle, see enum VehicleTypes (optional)
        //[StringLength(30, ErrorMessage = "Should be less than 30")]
        //public string VehicleType { get; set; }
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
        public virtual ICollection<Parking> Parking { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int VehicleTypeListId { get; set; }
        public virtual VehicleTypeList VehicleTypeList { get; set; }



        //Parking Validation
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{

        //    var newParkingPlace = parking.GetFreeParkingPlace(VehicleType);
        //    if (newParkingPlace.Count()==0)
        //    {
        //        yield return new ValidationResult("Not Enogth space!");
        //    }
        //}

    }

        public enum Brands
        {
            Undefined, Toyota, Ford, Volvo
        }
        public enum VehicleTypes
        {
            Undefined, Car, Bus, Boat, Moto
        }
        public enum Colors
        {
            Undefined, White, Red, Blue, Yellow, Orange, Green, Purple, Black
        }
        public enum FuelTypes
        {
            Undefined, Diesel, Gasoline, Electric
        }
        


        
    
}