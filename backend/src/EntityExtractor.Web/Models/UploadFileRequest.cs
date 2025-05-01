using System.ComponentModel.DataAnnotations;

namespace eKultura.EntityExtractor.Web.Models;

public record UploadFileRequest(IFormFile File, string Topic);
