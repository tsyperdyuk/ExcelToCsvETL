using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io;
using BaseLibS.Ms;
using ExcelDataReader;
using Ganss.Excel;
using HtmlAgilityPack;
using NPOI.HPSF;
using NPOI.SS.Formula.Functions;
using OfficeOpenXml;
using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Formats.Tar;
using System.IO;
using System.Linq;
using System.Text;


namespace ExcelToCsvETL.Services;

public class SourceDataReader: ISourceDataReader
{
    public async Task<Stream> ReadExcelFileFromWeb(Uri address, string fileName, CancellationToken cancellationToken = default)
    {
        var path = await GetFilePath(address, fileName, cancellationToken);
        if (string.IsNullOrEmpty(path)) { 
            throw new ArgumentNullException(nameof(path));
        }
        var result = await GetXlsxContent(path, cancellationToken);
        return result;
    }

    private async Task<string?> GetFilePath(Uri address, string fileName, CancellationToken cancellationToken) {
        try
        {
            var requester = new DefaultHttpRequester();
            requester.Headers["User-Agent"] = "PostmanRuntime/7.32.3";
            var config = Configuration.Default.With(requester).WithDefaultLoader();
            var context = BrowsingContext.New(config);
            IDocument document = await context
                .OpenAsync(address.ToString(), cancellationToken);
            var hrefElement = document.QuerySelector($"a[title='{fileName}']");

            if (hrefElement != null)
            {
                var url = hrefElement.GetAttribute("href");
                if (url != null)
                {
                    if (url.StartsWith('/'))
                    {
                        return url.Substring(1);
                    }
                }
            }
            return string.Empty;
        }
        catch (Exception ex)
        {
            // TODO: need to return EtlResult instead
            Console.WriteLine($"Error message: {ex.Message}");
            throw new FileNotFoundException($"Xlsx file not found");             
        }               
    }

    private async Task<Stream> GetXlsxContent(string? path, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new HttpClient();
            // TODO: maybe read from appsettings.json using IConfiguration or EtlConfig class
            client.BaseAddress = new Uri("https://bakerhughesrigcount.gcs-web.com");
            client.DefaultRequestHeaders.Add("User-Agent", "PostmanRuntime/7.32.3");
            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            var response = await client.GetAsync(path, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var file = await response.Content.ReadAsByteArrayAsync(cancellationToken);               
                return new MemoryStream(file);
            }
        }
        catch (Exception ex)
        {
            // TODO: need to return EtlResult instead
            Console.WriteLine($"Could not download the Xlsx file: {ex.Message}");
        }

        return await Task.FromResult(Stream.Null);
    }
}
