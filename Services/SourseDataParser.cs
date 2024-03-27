using ExcelToCsvETL.Models;
using Ganss.Excel;
using LanguageExt.ClassInstances;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelToCsvETL.Services
{
    public class SourseDataParser : ISourceDataParser
    {
        public async Task<List<RigCount>> ReadExcelDataAsync(Stream excelFileStream)
        {
            // TODO: maybe read from appsettings.json using IConfiguration or EtlConfig class
            var excel = new ExcelMapper()
            {                
                CreateMissingHeaders = true,
                HeaderRow = true,        
                HeaderRowNumber = 6,
                MaxRowNumber = 35
            };
           
            excel.AddMapping<RigCount>(2, p => p.MounthName);
            excel.AddMapping<RigCount>(3, p => p.LatinAmerica);
            excel.AddMapping<RigCount>(4, p => p.Europe);
            excel.AddMapping<RigCount>(5, p => p.Africa);
            excel.AddMapping<RigCount>(6, p => p.MiddleEast);
            excel.AddMapping<RigCount>(7, p => p.AsiaPacific);
            excel.AddMapping<RigCount>(8, p => p.TotalInternational);
            excel.AddMapping<RigCount>(9, p => p.Canada);
            excel.AddMapping<RigCount>(10, p => p.US);
            excel.AddMapping<RigCount>(11, p => p.TotalWorld);            
            
            var data = (await excel.FetchAsync<RigCount>(excelFileStream)).ToList();           
            return data;
        }
    }
}
