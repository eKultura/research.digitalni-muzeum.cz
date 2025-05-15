using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eKultura.EntityExtractor.Contracts;
public record FileStorageOptions
{
    public string BaseFolder { get; set; } = string.Empty;
}
