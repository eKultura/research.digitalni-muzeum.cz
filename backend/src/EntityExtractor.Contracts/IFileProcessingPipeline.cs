namespace eKultura.EntityExtractor.Contracts;

/// <summary>
/// 
/// </summary>
public interface IFileProcessingPipeline
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="rawFile"></param>
    /// <returns></returns>
    Task<TokenizedFile> ExecuteAsync(RawFile rawFile);
}
