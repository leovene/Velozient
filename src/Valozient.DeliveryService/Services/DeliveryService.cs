using System;
using Valozient.DeliveryService.Extensions;
using Valozient.DeliveryService.Interfaces;
using Valozient.DeliveryService.Models;

namespace Valozient.DeliveryService.Services
{
	public class DeliveryService : IDeliveryService
	{
        public List<DeliveryOutput> ProcessDeliveries(DeliveryInput input)
        {
            if (string.IsNullOrWhiteSpace(input.RawInput))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            var inputLines = input.RawInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var drones = inputLines[0].ParseDrones();
            var locations = inputLines.Skip(1).ToArray().ParseLocations();

            return OptimizeDeliveries(drones, locations);
        }

        private static List<DeliveryOutput> OptimizeDeliveries(List<Drone> drones, List<Location> locations)
        {
            var optimizedDeliveries = new List<DeliveryOutput>();

            // Initialize the DeliveryOutput objects for all drones
            foreach (var drone in drones)
            {
                optimizedDeliveries.Add(new DeliveryOutput { DroneName = drone.Name, Trips = new List<Trip>() });
            }

            while (locations.Count > 0)
            {
                foreach (var drone in drones)
                {
                    var deliveryOutput = optimizedDeliveries.First(d => d.DroneName == drone.Name);

                    var trip = new Trip { Locations = new List<Location>() };
                    var remainingWeight = drone.MaxWeight;

                    var candidateLocations = locations
                        .Where(l => l.PackageWeight <= remainingWeight)
                        .OrderByDescending(l => l.PackageWeight)
                        .ToList();

                    while (candidateLocations.Count > 0)
                    {
                        var bestLocation = candidateLocations.First();
                        trip.Locations.Add(bestLocation);
                        remainingWeight -= bestLocation.PackageWeight;
                        locations.Remove(bestLocation);

                        candidateLocations = locations
                            .Where(l => l.PackageWeight <= remainingWeight)
                            .OrderByDescending(l => l.PackageWeight)
                            .ToList();
                    }

                    if (trip.Locations.Count > 0)
                    {
                        deliveryOutput.Trips.Add(trip);
                    }
                }
            }

            return optimizedDeliveries;
        }
    }
}

