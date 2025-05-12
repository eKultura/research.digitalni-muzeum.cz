namespace eKultura.EntityExtractor.Contracts;

public class FileStoringDTO
{
    public string Project { get; set; }
    public byte[] PdfFile { get; set; }
    public DateTime CreatedAt { get; set; }
}