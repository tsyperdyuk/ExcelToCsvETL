using ExcelToCsvETL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvETL.Services
{
    public interface ISourceDataWriter
    {        
        Task CsvWriterAsync(List<RigCount> data);       
    }
}
