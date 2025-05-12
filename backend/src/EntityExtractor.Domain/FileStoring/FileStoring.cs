using eKultura.EntityExtractor.Contracts;
using System.IO.Abstractions;

namespace eKultura.EntityExtractor.Domain.FileStoring;
public class FileStoring : IFileStoring
{
    private readonly IFileSystem _fileSystem;
    private readonly string _baseFolder;

    public FileStoring(IFileSystem fileSystem, string baseFolder)
    {
        _fileSystem = fileSystem;
        _baseFolder = baseFolder;
    }
    public FileStoringDTO StoreFile(string topic, byte[] pdfFile)
    {
        if (string.IsNullOrEmpty(topic))
        {
            throw new ArgumentException("Topic field is required.");
        }

        if(pdfFile == null)
        {
            throw new ArgumentException("Pdf file has to be selected before storing.");
        }

        return new FileStoringDTO
        {
            Project = topic,
            PdfFile = pdfFile,
            CreatedAt = DateTime.Now
        };
    }
}
