using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class KvittoViewModel
    {
        Decimal costPerMinute = Models.CostPerMinute.costPerMinute;

        public string RegistrationNumber { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd\\:hh\\:mm}", ApplyFormatInEditMode = true) ]
        public TimeSpan ParkingTime { get; set; }
        public Decimal Price => (Decimal) ParkingTime.TotalMinutes * CostPerMinute;
        public Decimal CostPerMinute { get { return costPerMinute; } }
        public string Customer { get; set; }
    }
}