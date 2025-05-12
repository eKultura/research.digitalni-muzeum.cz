using eKultura.EntityExtractor.Contracts;
using System.IO.Abstractions;

namespace eKultura.EntityExtractor.Domain;
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

        if (pdfFile == null || pdfFile.Length == 0)
        {
            throw new ArgumentException("Pdf file has to be selected before storing.");
        }

        string projectFolder = _fileSystem.Path.Combine(_baseFolder, topic);
        _fileSystem.Directory.CreateDirectory(projectFolder);

        string fileName = $"document_{DateTime.UtcNow:yyyyMMddHHmmssfff}.pdf"; // este otestuj

        string filePath = _fileSystem.Path.Combine(projectFolder, fileName);

        _fileSystem.File.WriteAllBytes(filePath, pdfFile);

        return new FileStoringDTO
        {
            Project = topic,
            PdfFile = pdfFile,
            CreatedAt = DateTime.Now
        };
    }
}
