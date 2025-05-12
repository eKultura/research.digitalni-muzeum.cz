using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Models;
using eKultura.EntityExtractor.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace eKultura.EntityExtractor.Controllers;

[ApiController]
[Route("api/v1/documents")]
public class DocumentController : ControllerBase
{
    private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
    private static readonly ConcurrentDictionary<string, TokenizedFile> _files = new();

    [HttpPost]
    [Route("upload")]
    public Task<IActionResult> Upload([FromForm] UploadFileRequest request)
    {
        // Mock implementation
        var mockedTokens = new List<IEnumerable<string>>
        {
            new List<string>
            {
                "Alexandre", "Dumas", "Count", "Monte", "Cristo", "Edmond", "Dantes"
            },
            new List<string>
            {
               "Château", "d'If", "prison", "Marseille"
            }
        };

        var tokenizedFile = new TokenizedFile(FileId.Create(), request.PDFFile.FileName, mockedTokens);

        _files.AddOrUpdate(tokenizedFile.FileId.Id, tokenizedFile, (id, file) => file);

        IActionResult result = Created($"api/v1/documents/{tokenizedFile.FileId.Id}", tokenizedFile);
        return Task.FromResult(result);
    }

    [HttpGet]
    [Route("{fileId}")]
    public Task<IActionResult> GetFile(string fileId)
    {
        IActionResult result;

        if (!_files.TryGetValue(fileId, out var file))
        {
            result = NotFound();
        }
        else
        {
            result = Ok(file);
        }

        return Task.FromResult(result);
    }

    [HttpGet]
    public ActionResult<IEnumerable<FileModel>> GetFiles([FromQuery] string path)
    {
        //var path = @"C:\Users\jakub\OneDrive\Počítač\TestingDirectory";
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest("Path is required.");

        if (!Directory.Exists(path))
            return NotFound($"Directory not found: {path}");

        try
        {
            var files = Directory.GetFiles(path);
            var folders = Directory.GetDirectories(path);
            var result = files.Select(filePath => new FileModel
            {
                FileName = Path.GetFileName(filePath),
                FullPath = filePath
            });
            return Ok(result);
        }
        catch
        {

        }

        return NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> UploadPdf(IFormFile file)
    {

        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        if (!file.FileName.EndsWith(".pdf"))
            return BadRequest("Only PDF files are allowed.");

        if (!Directory.Exists(_uploadFolder))
            Directory.CreateDirectory(_uploadFolder);

        var filePath = Path.Combine(_uploadFolder, Path.GetFileName(file.FileName));

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return Ok(new { file = file.FileName, size = file.Length });
    }
}