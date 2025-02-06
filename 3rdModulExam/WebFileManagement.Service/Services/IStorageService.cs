namespace WebFileManagement.Service.Services;

public interface IStorageService
{
    Task CreateFolderAsync(string folderPath);
    Task UploadFileAsync(Stream stream, string filePath);
    Task UploadFileWithChuncksAsync(Stream stream, string filePath);
    Task DeleteFileAsync(string filePath);
    Task DeleteFolderAsync(string folderPath);
    Task<Stream> DownloadFileAsync(string filePath);
    Task<Stream> DownloadFolderAsZipAsync(string folderPath);
    Task<string> GetContentOfTxtFileAsync(string txtFilePath);
    Task UpdateContentOfTxtFileAsync(string filePath, string newContent);
    Task<List<string>> GetAllInFolderPathAsync(string folderPath);
}