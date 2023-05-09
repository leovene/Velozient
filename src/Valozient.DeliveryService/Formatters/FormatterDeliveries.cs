using System.Text;
using Valozient.DeliveryService.Models;

namespace Valozient.DeliveryService.Formatters
{
    public static class FormatterDeliveries
	{
        public static string FormatDeliveries(List<DeliveryOutput> deliveries)
        {
            var outputStringBuilder = new StringBuilder();
            foreach (var delivery in deliveries)
            {
                outputStringBuilder.AppendLine($"[{delivery.DroneName}]");
                int tripNumber = 1;
                foreach (var trip in delivery.Trips)
                {
                    outputStringBuilder.AppendLine($"Trip #{tripNumber}");
                    outputStringBuilder.AppendLine(string.Join(", ", trip.Locations.Select(loc => $"[{loc.Name}]")));
                    tripNumber++;
                }
                outputStringBuilder.AppendLine();
            }

            return outputStringBuilder.ToString();
        }
    }
}