using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFileManagement.StorageBroker.Services;

namespace WebFileManagement.Service.Services;

public class StorageService : IStorageService
{
    private IStorageBrokerService _storageBrokerService;

    public StorageService(IStorageBrokerService storageBrokerService)
    {
        _storageBrokerService = storageBrokerService;
    }

    public async Task CreateFolderAsync(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
            throw new Exception("String is emply");
        await _storageBrokerService.CreateFolderAsync(folderPath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new Exception("String is emply");
        await _storageBrokerService.DeleteFileAsync(filePath);
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
            throw new Exception("String is emply");
        await _storageBrokerService.DeleteFolderAsync(folderPath);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new Exception("String is emply");
        var stream = await _storageBrokerService.DownloadFileAsync(filePath);
        return stream;
    }

    public async Task<Stream> DownloadFolderAsZipAsync(string folderPath)
    {
        if (string.IsNullOrEmpty(folderPath))
            throw new Exception("String is emply");
        var stream = await _storageBrokerService.DownloadFolderAsZipAsync(folderPath);
        return stream;
    }

    public async Task<List<string>> GetAllInFolderPathAsync(string folderPath)
    {
        var all = await _storageBrokerService.GetAllInFolderPathAsync(folderPath);
        return all;
    }

    public async Task<string> GetContentOfTxtFileAsync(string txtFilePath)
    {
        if (string.IsNullOrEmpty(txtFilePath))
            throw new Exception("String is emply");
        var context = await _storageBrokerService.GetContentOfTxtFileAsync(txtFilePath);
        return context;
    }

    public async Task UpdateContentOfTxtFileAsync(string filePath, string newContent)
    {
        if (string.IsNullOrEmpty(filePath))
            throw new Exception("String is emply");
        else if (string.IsNullOrEmpty(newContent))
            throw new Exception("New Content is emply");
        await _storageBrokerService.UpdateContentOfTxtFileAsync(filePath, newContent);
    }

    public async Task UploadFileAsync(Stream stream, string filePath)
    {
        await _storageBrokerService.UploadFileAsync(stream, filePath);
    }

    public async Task UploadFileWithChuncksAsync(Stream stream, string filePath)
    {
        await _storageBrokerService.UploadFileWithChuncksAsync(stream, filePath);
    }
}
