using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
    public class Customer
    {
        public int Id { get; set; }        
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int PersonalIdentityNumber { get; set; }
        public string TelephoNnumber { get; set; }       
        public virtual ICollection<ParkedVehicle> ParkedVehicles { get; set; }

       



    }
}