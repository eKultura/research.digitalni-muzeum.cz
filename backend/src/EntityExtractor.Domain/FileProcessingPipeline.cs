using eKultura.EntityExtractor.Contracts;
using eKultura.EntityExtractor.Domain.PdfReading;

namespace eKultura.EntityExtractor.Domain;

public class FileProcessingPipeline : IFileProcessingPipeline
{
    private readonly IFileStorage _fileStorage;
    private readonly IPdfTextReader _pdfTextReader;

    public FileProcessingPipeline(IFileStorage fileStorage,
        IPdfTextReader pdfTextReader)
    {
        _fileStorage = fileStorage;
        _pdfTextReader = pdfTextReader;
    }

    public async Task<TokenizedFile> ExecuteAsync(RawFile rawFile)
    {
        var pdfDocument = await _fileStorage.StoreAsync(rawFile);

        var textDocument = await _pdfTextReader.ReadTextAsync(pdfDocument);

        throw new NotImplementedException();
    }
}
