using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eKultura.EntityExtractor.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IFileStoring _fileStorageService;

    public FileUploadController(IFileStoring fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadFileRequest request)
    {
        if (request.PDFFile == null || request.PDFFile.Length == 0)
            return BadRequest("PDF file is required.");

        using var memoryStream = new MemoryStream();
        await request.PDFFile.CopyToAsync(memoryStream);

        var result = _fileStorageService.StoreFile(request.Topic, memoryStream.ToArray());

        return Ok(result);
    }

}
