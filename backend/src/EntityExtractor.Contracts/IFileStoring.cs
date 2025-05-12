using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Contracts;

public interface IFileStoring
{
    FileStoringDTO StoreFile(string topic, byte[] pdfFile);
}
