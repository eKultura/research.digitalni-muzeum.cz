using FileExtractor.Models;
using Microsoft.AspNetCore.Mvc;

namespace FileExtractor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileLoaderController : ControllerBase
{
    private readonly string _uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

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
    //[HttpPost]
    //public ActionResult<IEnumerable<FileModel>> UploadFiles()
    //{

    //}
    //[HttpPost]
    //public ActionResult<FileModel> Post([FromBody] FileModel newProduct)
    //{
    //    newProduct.Id = _products.Count + 1;
    //    _products.Add(newProduct);
    //    return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
    //}

}
