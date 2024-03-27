using ExcelToCsvETL.Models;
using Ganss.Excel;

namespace ExcelToCsvETL.Services;

public class EtlService
{
    private readonly ISourceDataReader _sourceDataReader;
    private readonly ISourceDataParser _sourceParser;
    private readonly ISourceDataWriter _sourceWriter;

    public EtlService(ISourceDataReader sourceDataReader, ISourceDataParser sourceParser, ISourceDataWriter sourceWriter)
    {
        _sourceDataReader = sourceDataReader;
        _sourceParser = sourceParser;
        _sourceWriter = sourceWriter;
    }

    public async Task<EtlResult> Run(EtlConfig config, CancellationToken cancellationToken = default)
    {
        try
        {
            var excelFileStream = await _sourceDataReader.ReadExcelFileFromWeb(config.ExcelFileUri, config.FileName, cancellationToken);
            var data = await _sourceParser.ReadExcelDataAsync(excelFileStream);
            var result = _sourceWriter.CsvWriter(data);            
        }
        catch (Exception ex)
        {
            return new EtlResult(false, ex.Message);
        }

        return new EtlResult(true, "OK");
    }
}
