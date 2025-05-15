using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eKultura.EntityExtractor.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileUploadController : ControllerBase
{
    private readonly IFileStorage _fileStorageService;

    public FileUploadController(IFileStorage fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] RawFile rawFile)
    {
        if (rawFile.Bytes == null || rawFile.Bytes.Length == 0)
        {
            return BadRequest("PDF file is required.");
        }

        using var memoryStream = new MemoryStream(rawFile.Bytes);

        var result = await _fileStorageService.StoreAsync(rawFile);

        return Created();
    }

}
