using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebFileManagement.Service.Services;

namespace WebFileManagement.Server.Controllers;

[Route("api/storage")]
[ApiController]
public class StorageController : ControllerBase
{
    private IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }

    [HttpPost("createFolder")]
    public async Task CreateFolder(string folderPath)
    {
        await _storageService.CreateFolderAsync(folderPath);
    }

    [HttpPost("uploadFile")]
    public async Task UploadFile(IFormFile file, string? filePath)
    {
        filePath ??= string.Empty;
        filePath = Path.Combine(filePath, file.FileName);
        var stream = file.OpenReadStream();
        await _storageService.UploadFileAsync(stream, filePath);
    }

    [HttpPost("uploadFileWithChuncks")]
    public async Task UploadFileWithChuncksAsync(IFormFile file, string? filePath)
    {
        filePath ??= string.Empty;
        filePath = Path.Combine(filePath, file.FileName);
        var stream = file.OpenReadStream();
        await _storageService.UploadFileWithChuncksAsync(stream, filePath);
    }

    [HttpPost("uploadFiles")]
    public async Task UploadFiles(List<IFormFile> files, string? filePath)
    {
        filePath ??= string.Empty;
        var mainPath = filePath;
        foreach (var file in files)
        {
            filePath = Path.Combine(mainPath, file.FileName);
            var stream = file.OpenReadStream();
            await _storageService.UploadFileAsync(stream, filePath);
        }
    }

    [HttpDelete("deleteFile")]
    public async Task DeleteFile(string filePath)
    {
        await _storageService.DeleteFileAsync(filePath);
    }

    [HttpDelete("deleteFolder")]
    public async Task DeleteFolder(string filePath)
    {
        await _storageService.DeleteFolderAsync(filePath);
    }

    [HttpGet("downloadFile")]
    public async Task<FileStreamResult> DownloadFile(string filePath)
    {
        var fileName = Path.GetFileName(filePath);
        var stream = await _storageService.DownloadFileAsync(filePath);
        var file = new FileStreamResult(stream, "application/octet-stream");
        file.FileDownloadName = fileName;
        return file;
    }

    [HttpGet("downloadFolderAsZip")]
    public async Task<FileStreamResult> DownloadFolderAsZip(string folderPath)
    {
        var folderName = Path.GetDirectoryName(folderPath);
        var stream = await _storageService.DownloadFolderAsZipAsync(folderPath);
        var folder = new FileStreamResult(stream, "application/octet-stream");
        folder.FileDownloadName = folderName + ".zip";
        return folder;
    }

    [HttpGet("getContentOfTxtFile")]
    public async Task<string> GetContentOfTxtFile(string txtFilePath)
    {
        var content = await _storageService.GetContentOfTxtFileAsync(txtFilePath);
        return content;
    }

    [HttpPut("updateContentOfTxtFile")]
    public async Task UpdateContentOfTxtFile(string filePath, string newContent)
    {
        await _storageService.UpdateContentOfTxtFileAsync(filePath, newContent);
    }

    [HttpGet("getAllInFolderPath")]
    public async Task<List<string>> GetAllInFolderPathAsync(string? folderPath)
    {
        folderPath ??= string.Empty;
        var all = await _storageService.GetAllInFolderPathAsync(folderPath);
        return all;
    }

}
