using System.ComponentModel.DataAnnotations;

namespace eKultura.EntityExtractor.Web.Models;
public record UploadFileRequestModel(IFormFile File, string Topic);
