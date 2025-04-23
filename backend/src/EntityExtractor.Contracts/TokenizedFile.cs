namespace eKultura.EntityExtractor.Contracts;

public record TokenizedFile(FileId FileId, string FileName, IEnumerable<IEnumerable<string>> Tokens);
