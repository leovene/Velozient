using Valozient.DeliveryService.Helpers;
using Valozient.DeliveryService.Models;

namespace Valozient.DeliveryService.Extensions
{
    public static class ParseExntension
	{
        public static List<Drone> ParseDrones(this string inputLine) => ParseHelper.ParseDrones(inputLine);
        public static List<Location> ParseLocations(this string[] inputLines) => ParseHelper.ParseLocations(inputLines);
    }
}

