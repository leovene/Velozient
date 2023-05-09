using System;
using Valozient.DeliveryService.Models;

namespace Valozient.DeliveryService.Interfaces
{
	public interface IDeliveryService
	{
        public List<DeliveryOutput> ProcessDeliveries(DeliveryInput input);
    }
}