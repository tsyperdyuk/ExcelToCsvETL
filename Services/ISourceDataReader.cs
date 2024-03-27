using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvETL.Services
{
    public interface ISourceDataReader
    {
        Task<Stream> ReadExcelFileFromWeb(Uri address, string fileName, CancellationToken cancellationToken = default);
    }
}
