using eKultura.EntityExtractor.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Contracts;
public class UploadFileRequest
{
    [Required]
    public IFormFile File { get; set; }

    [Required]
    public string Topic { get; set; }
}
