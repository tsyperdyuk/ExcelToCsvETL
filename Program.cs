using ExcelToCsvETL.Models;
using ExcelToCsvETL.Services;


// TODO: maybe read from appsettings.json using IConfiguration
var config = new EtlConfig(
    new Uri("https://bakerhughesrigcount.gcs-web.com/intl-rig-count?c=79687&p=irol-rigcountsintl"),
    "Worldwide Rig Count Feb 2024.xlsx",
    "result.csv"
    );

var etlService = new EtlService(
    new SourceDataReader(), 
    new SourseDataParser(), 
    new SourceDataWriter()
    );
var result = await etlService.Run(config);
Console.WriteLine(result.IsSuccess ? $"Success: {result.Info}" : $"Failed: {result.Info}");