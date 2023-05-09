namespace Valozient.DeliveryService.Interfaces
{
    public interface IFileService
	{
        Task<string> ReadFileAsStringAsync(IFormFile inputFile);
        MemoryStream CreateTextFileAsStream(string content);
    }
}