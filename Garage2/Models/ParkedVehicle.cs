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
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        [Required(ErrorMessage = "Please type Registratin Number")]
        public string  RegistrationNumber{ get; set; }
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
        public string Color  { get; set; }
        //Fuel see enum Fueltypes(optional)
        [StringLength(30, ErrorMessage = "Should be less than 30")]
        public string FuelType { get; set; }
        public DateTime CheckInTime { get; set; }

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
    }
}