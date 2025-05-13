using eKultura.EntityExtractor.Contracts;
using System.IO.Abstractions;

namespace eKultura.EntityExtractor.Domain;

public class FileStoring : IFileStorage
{
    private readonly IFileSystem _fileSystem;
    private readonly string _baseFolder;

    public FileStoring(IFileSystem fileSystem, string baseFolder)
    {
        _fileSystem = fileSystem;
        _baseFolder = baseFolder;
    }

    public async Task<PdfDocument> StoreAsync(RawFile rawFile)
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

        
        string documentId = $"{Guid.NewGuid().ToString("N")}.pdf";

        string filePath = _fileSystem.Path.Combine(projectFolder, documentId);

        _fileSystem.File.WriteAllBytes(filePath, pdfFile);

        return new PdfDocument(documentId, rawFile.Bytes, rawFile.Project, rawFile.Name);
    }
}
