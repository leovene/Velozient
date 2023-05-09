using System;

namespace Valozient.DeliveryService.Models
{
    public class DeliveryOutput
    {
        public string? DroneName { get; set; }
        public List<Trip> Trips { get; set; } = new List<Trip>();
        public double TotalWeight { get; set; }
    }
}