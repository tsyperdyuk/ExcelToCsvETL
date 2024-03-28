using CsvHelper;
using ExcelToCsvETL.Models;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvETL.Services
{
    public class SourceDataWriter : ISourceDataWriter
    {
        public async Task CsvWriterAsync(List<RigCount> data)
        {
            // TODO: maybe read from appsettings.json using IConfiguration or EtlConfig class
            using (var writer = new StreamWriter("d:\\output.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
               await csv.WriteRecordsAsync(data);             
            }                 
        }
    }
}
