namespace ExcelToCsvETL.Models;
public class EtlResult
{
    public EtlResult(bool isSuccess, string? info)
    {
        IsSuccess = isSuccess;
        Info = info;
    }

    public bool IsSuccess { get; set; }

    public string? Info { get; set; }
}