using Valozient.DeliveryService.Interfaces;

namespace Valozient.DeliveryService.Services
{
    public class FileService : IFileService
	{
        public async Task<string> ReadFileAsStringAsync(IFormFile inputFile)
        {
            using var streamReader = new StreamReader(inputFile.OpenReadStream());
            var content = await streamReader.ReadToEndAsync();
            return content;
        }

        public MemoryStream CreateTextFileAsStream(string content)
        {
            var outputStream = new MemoryStream();
            var writer = new StreamWriter(outputStream);
            writer.Write(content);
            writer.Flush();
            outputStream.Position = 0;
            return outputStream;
        }
    }
}