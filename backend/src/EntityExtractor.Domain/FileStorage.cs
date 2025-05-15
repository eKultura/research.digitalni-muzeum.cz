using eKultura.EntityExtractor.Contracts;
using System.IO.Abstractions;

namespace eKultura.EntityExtractor.Domain;

public class FileStorage : IFileStorage
{
    private readonly IFileSystem _fileSystem;
    private readonly string _baseFolder;

    public FileStorage(IFileSystem fileSystem, string baseFolder)
    {
        _fileSystem = fileSystem;
        _baseFolder = baseFolder;
    }

    public async Task<PdfDocument> StoreAsync(RawFile rawFile)
    {
        if (string.IsNullOrEmpty(rawFile.Project))
        {
            throw new ArgumentException("Project field is required.");
        }

        if (rawFile.Bytes == null || rawFile.Bytes.Length == 0)
        {
            throw new ArgumentException("Pdf file has to be selected before storing.");
        }

        string projectFolder = _fileSystem.Path.Combine(_baseFolder, rawFile.Project);
        _fileSystem.Directory.CreateDirectory(projectFolder);

        
        string documentId = $"{Guid.NewGuid().ToString("N")}.pdf";

        string filePath = _fileSystem.Path.Combine(projectFolder, documentId);

        _fileSystem.File.WriteAllBytes(filePath, rawFile.Bytes);

        return new PdfDocument(documentId, rawFile.Bytes, rawFile.Project, rawFile.Name);
    }
}
