namespace ExcelToCsvETL.Models;

public class EtlConfig
{
    public EtlConfig(Uri excelFileUri, string fileName, string csvFilePath)
    {
        ExcelFileUri = excelFileUri;
        FileName = fileName;
        CsvFilePath = csvFilePath;        
    }   

    public Uri ExcelFileUri { get; set; }
    public string FileName { get; set; }
    public string CsvFilePath { get; set; }
}