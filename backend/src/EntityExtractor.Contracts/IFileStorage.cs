using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Contracts;

public interface IFileStorage
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="topic"></param>
    /// <param name="pdfFile"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">When topic is null or empty</exception>
    Task<PdfDocument> StoreAsync(RawFile rawFile);
}
