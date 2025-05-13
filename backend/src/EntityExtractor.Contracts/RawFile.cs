namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// Represents an unprocessed file 
/// </summary>
/// <param name="Project">Project to which the file belongs</param>
/// <param name="Bytes">File bytes</param>
/// <param name="Name">Name of the file</param>
public record RawFile(string Project, byte[] Bytes, string Name);