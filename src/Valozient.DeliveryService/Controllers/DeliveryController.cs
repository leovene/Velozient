using Microsoft.AspNetCore.Mvc;
using Valozient.DeliveryService.Formatters;
using Valozient.DeliveryService.Interfaces;
using Valozient.DeliveryService.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Valozient.DeliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly IFileService _fileService;

        public DeliveryController(IDeliveryService deliverService, IFileService fileService)
        {
            _deliveryService = deliverService;
            _fileService = fileService;
        }

        [HttpPost("optimize")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ProcessDeliveries([FromForm] FileInput input)
        {
            if (input.InputFile == null || input.InputFile.Length == 0)
            {
                return BadRequest("Invalid input file provided.");
            }

            var rawInput = await _fileService.ReadFileAsStringAsync(input.InputFile);

            var deliveryInput = new DeliveryInput { RawInput = rawInput };
            var optimizedDeliveries = _deliveryService.ProcessDeliveries(deliveryInput);

            var formattedOutput = FormatterDeliveries.FormatDeliveries(optimizedDeliveries);

            var outputStream = _fileService.CreateTextFileAsStream(formattedOutput.ToString());

            // Return the memory stream as a file
            return File(outputStream, "text/plain", "Output.txt");
        }
    }
}