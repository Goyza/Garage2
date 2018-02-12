using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Garage2.Models
{
    public class KvittoViewModel
    {
        double costPerMinute = 0.02;

        public string RegistrationNumber { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd\\:hh\\:mm}", ApplyFormatInEditMode = true) ]
        public TimeSpan ParkingTime { get; set; }
        public double Price { get { return ParkingTime.TotalMinutes * CostPerMinute; } }
        public double CostPerMinute { get { return costPerMinute; } }
    }
}