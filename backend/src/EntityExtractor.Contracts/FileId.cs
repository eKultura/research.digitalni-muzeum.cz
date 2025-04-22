namespace EntityExtractor.Contracts;

public record FileId
{
    private const string HyphenFormat = "D";

    public string Id { get; private init; }

    private FileId(string id)
    {
        Id = id;
    }

    public static FileId Create()
    {
        return new FileId(Guid.NewGuid().ToString(HyphenFormat));
    }
}
