using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WebFileManagement.StorageBroker.Services;

public class LocalStorageBrokerService : IStorageBrokerService
{
    private string _dataPath;
    public LocalStorageBrokerService()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_dataPath))
            Directory.CreateDirectory(_dataPath);
    }

    public async Task CreateFolderAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        await CheckParent(folderPath);
        if (Directory.Exists(folderPath))
            throw new Exception("The folder already exists!");
        Directory.CreateDirectory(folderPath);
    }

    public async Task DeleteFileAsync(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        await CheckParent(filePath);
        if (!File.Exists(filePath))
            throw new Exception("The file does not exist!");
        File.Delete(filePath);
    }

    public async Task DeleteFolderAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        await CheckParent(folderPath);
        if (!Directory.Exists(folderPath))
            throw new Exception("The folder does not exist!");
        Directory.Delete(folderPath, true);
    }

    public async Task<Stream> DownloadFileAsync(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        await CheckParent(filePath);
        if (!File.Exists(filePath))
            throw new Exception("The file does not exist!");
        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public async Task<Stream> DownloadFolderAsZipAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        await CheckParent(folderPath);
        if(!Directory.Exists(folderPath))
            throw new Exception("The folder does not exist!");

        var zipPath = folderPath + ".zip";
        ZipFile.CreateFromDirectory(folderPath, zipPath);
        var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public async Task<List<string>> GetAllInFolderPathAsync(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        await CheckParent(folderPath);
        if (!Directory.Exists(folderPath))
            throw new Exception("The folder does not exist");
        var allFilesAndFolders = Directory.EnumerateFileSystemEntries(folderPath).ToList();
        allFilesAndFolders = allFilesAndFolders.Select(df => df.Remove(0, folderPath.Length + 1)).ToList();
        return allFilesAndFolders;
    }

    public async Task<string> GetContentOfTxtFileAsync(string txtFilePath)
    {
        txtFilePath = Path.Combine(_dataPath, txtFilePath);
        await CheckParent(txtFilePath);
        if (!File.Exists(txtFilePath))
            throw new Exception("The txt file does not exists!");

        var allTexts = await File.ReadAllTextAsync(txtFilePath);
        return allTexts;
    }

    public async Task UpdateContentOfTxtFileAsync(string filePath, string newContent)
    {
        filePath = Path.Combine(_dataPath, filePath);
        await CheckParent(filePath);
        if (!File.Exists(filePath))
            throw new Exception("The file to undete is not found!");
        await File.WriteAllTextAsync(filePath, newContent);
    }

    public async Task UploadFileAsync(Stream stream, string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        await CheckParent(filePath);
        if (File.Exists(filePath))
            throw new Exception("The file already exists!");
        using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        stream.CopyTo(fileStream);
    }

    public async Task UploadFileWithChuncksAsync(Stream stream, string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        await CheckParent(filePath);
        if (File.Exists(filePath))
            throw new Exception("The file already exists!");

        byte[] buffer = new byte[1024 * 1024 * 5];
        using StreamWriter streamWriter = new StreamWriter(stream, Encoding.UTF8, buffer.Length);
        stream.Write(buffer, 0, buffer.Length); 

    }
    private async Task CheckParent(string path)
    {
        var parent = Directory.GetParent(path);
        if (!Directory.Exists(parent.FullName))
            throw new Exception("The parent folder does not exist!");
    }
}
