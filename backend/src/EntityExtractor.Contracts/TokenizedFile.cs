namespace EntityExtractor.Contracts;

public record TokenizedFile(FileId FileId, string FileName, IEnumerable<IEnumerable<string>> tokens);
