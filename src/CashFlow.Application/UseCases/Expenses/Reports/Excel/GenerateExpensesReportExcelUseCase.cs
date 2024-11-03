using System;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase(IExpensesReadOnlyRepository repository) : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        var expenses = await repository.GetByMonthAsync(month);
        if (expenses.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook(); // cira como se fosse uma planilha do excel

        // configurações gerais do arquivo
        workbook.Author = "CashFlow";
        workbook.Style.Font.FontSize = 12;
        workbook.Style.Font.FontName = "Times New Roman";

        // Criação de uma aba (folha/página) na planilha
        var worksheet = workbook.Worksheets.Add($"Page01 - {month:Y}");

        // Deifnição das colunas/céluas de cabeçalho
        InsertHeader(worksheet);

        var raw = 2;
        foreach (var registered in expenses)
        {
            worksheet.Cell($"A{raw}").Value = registered.Title;
            worksheet.Cell($"B{raw}").Value = registered.Date;
            worksheet.Cell($"C{raw}").Value = ConvertType(registered.PaymentType);

            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = "-R$ #,##0.00"; // formatação de moeda
            worksheet.Cell($"D{raw}").Value = registered.Amount;

            worksheet.Cell($"E{raw}").Value = registered.Description;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        using var fileStream = new MemoryStream();

        // vc pode sefinir um caminho para salvar o arquivo, seja um path de arquivo (c:\\seu-path\) , ou em memória (stream)
        workbook.SaveAs(fileStream); // salva a planilha em arquivo (file ou stream)

        return fileStream.ToArray();
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        // TO-DO: Crie depois um resouce file para mapear esses valores em difernetes idiomas
        worksheet.Cell("A1").Value = "Title";
        worksheet.Cell("B1").Value = "Date";
        worksheet.Cell("C1").Value = "Payment Type";
        worksheet.Cell("D1").Value = "Amount";
        worksheet.Cell("E1").Value = "Description";

        worksheet.Cells("A1:E1").Style.Font.Bold = true;
        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#FFD700");
        // worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.LightGray;
        worksheet.Cells("A1:E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        // worksheet.Cells("A1:E1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
    }

    private static string ConvertType(PaymentType type)
    {
        // TO-DO: Crie depois um resouce file para mapear esses valores em difernetes idiomas
        return type switch
        {
            PaymentType.Cash => "Money",
            PaymentType.CreditCard => "Credit Card",
            PaymentType.DebitCard => "Debit Card",
            PaymentType.EletronicTransfer => "Eletronic Transfer",
            _ => "Unknown"
        };
    }
}
