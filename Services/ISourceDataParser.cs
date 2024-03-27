using ExcelToCsvETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvETL.Services
{
    public interface ISourceDataParser
    {
        Task<List<RigCount>> ReadExcelDataAsync(Stream excelFileStream);
    }
}
