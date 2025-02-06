using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFileManagement.StorageBroker.Services;

public interface IStorageBrokerService
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
