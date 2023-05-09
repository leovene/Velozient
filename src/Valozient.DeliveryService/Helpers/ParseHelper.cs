using System;
using System.Drawing;
using Valozient.DeliveryService.Models;

namespace Valozient.DeliveryService.Helpers
{
	public static class ParseHelper
	{
        public static List<Drone> ParseDrones(string inputLine)
        {
            var droneData = inputLine.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var drones = new List<Drone>();

            for (int i = 0; i < droneData.Length - 1; i += 2)
            {
                drones.Add(new Drone { Name = droneData[i].Trim('[', ']'), MaxWeight = double.Parse(droneData[i + 1].Trim('[', ']')) });
            }

            return drones;
        }

        public static List<Location> ParseLocations(string[] inputLines)
        {
            var locations = new List<Location>();

            foreach (var line in inputLines)
            {
                var locationData = line.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                locations.Add(new Location { Name = locationData[0].Trim('[', ']'), PackageWeight = double.Parse(locationData[1].Trim('[', ']')) });
            }

            return locations;
        }
    }
}