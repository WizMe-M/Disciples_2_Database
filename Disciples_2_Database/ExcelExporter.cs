using Disciples_2_Database.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Disciples_2_Database;

public class ExcelExporter
{
    public const string filePath = @"C:\Users\ender\OneDrive\Рабочий стол\ВАЖНО\ScenInfo.xlsx";

    public byte[] Generate(ScenarioInfo scenario)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Информация о сценариях");

        //header
        sheet.Cells["A1"].Value = "Номер сценария:";
        sheet.Cells["A2"].Value = "Название:";
        sheet.Cells["A3"].Value = "Описание:";
        sheet.Cells["A4"].Value = "Цель:";
        sheet.Cells["A5"].Value = "Брифинг:";
        sheet.Cells["A6"].Value = "Текст при победе:";
        sheet.Cells["A7"].Value = "Текст при поражении:";

        //data
        sheet.Cells["B1"].Value = scenario.Number;
        sheet.Cells["B2"].Value = scenario.Name;
        sheet.Cells["B3"].Value = scenario.Description;
        sheet.Cells["B4"].Value = scenario.Objective;
        sheet.Cells["B5"].Value = scenario.Briefing;
        sheet.Cells["B6"].Value = scenario.EndingWin;
        sheet.Cells["B7"].Value = scenario.EndingLose;

        //stylizes
        sheet.Cells["A1:B7"].AutoFitColumns();
        sheet.Cells["B1:B7"].Style.WrapText = true;

        return package.GetAsByteArray();
    }

    public byte[] Generate(IEnumerable<ScenarioInfo> scenarios)
    {
        var scenarioInfos = scenarios.ToArray();
        var itemsCount = scenarioInfos.Count();
        const int fieldCount = 7;
        
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();
        var sheet1 = package.Workbook.Worksheets.Add("Вид 1");
        //header
        sheet1.Cells["A1"].Value = "Номер сценария:";
        sheet1.Cells["B1"].Value = "Название:";
        sheet1.Cells["C1"].Value = "Описание:";
        sheet1.Cells["D1"].Value = "Цель:";
        sheet1.Cells["E1"].Value = "Брифинг:";
        sheet1.Cells["F1"].Value = "Текст при победе:";
        sheet1.Cells["G1"].Value = "Текст при поражении:";

        //load
        sheet1.Cells["A2"].LoadFromCollection(scenarioInfos);
        
        //stylize
        var lastRow = itemsCount + 1;
        var tableRange = sheet1.Cells[1, 1, lastRow, fieldCount];
        var table = sheet1.Tables.Add(tableRange, "ScenariosTable");
        table.TableStyle = TableStyles.Medium15;
        sheet1.Cells[1, 1, lastRow, fieldCount].AutoFitColumns();
        sheet1.Cells[1, 1, lastRow, fieldCount].Style.WrapText = true;
        sheet1.Cells["C1"].EntireColumn.Width = 50;
        sheet1.Cells["D1"].EntireColumn.Width = 40;
        sheet1.Cells["E1"].EntireColumn.Width = 125;
        sheet1.Cells["F1"].EntireColumn.Width = 55;
        sheet1.Cells["G1"].EntireColumn.Width = 60;

        //delimiter
        
        var sheet2 = package.Workbook.Worksheets.Add("Вид 2");
        //header
        sheet2.Cells["A1"].Value = "№";
        sheet2.Cells["A2"].Value = "Номер сценария:";
        sheet2.Cells["A3"].Value = "Название:";
        sheet2.Cells["A4"].Value = "Описание:";
        sheet2.Cells["A5"].Value = "Цель:";
        sheet2.Cells["A6"].Value = "Брифинг:";
        sheet2.Cells["A7"].Value = "Текст при победе:";
        sheet2.Cells["A8"].Value = "Текст при поражении:";

        //load
        for (var i = 0; i < scenarioInfos.Length; i++)
        {
            var currentColumn = i + 2;
            var scenario = scenarioInfos[i];
            sheet2.Cells[1, currentColumn].Value = currentColumn - 1;
            sheet2.Cells[2, currentColumn].Value = scenario.Number;
            sheet2.Cells[3, currentColumn].Value = scenario.Name;
            sheet2.Cells[4, currentColumn].Value = scenario.Description;
            sheet2.Cells[5, currentColumn].Value = scenario.Objective;
            sheet2.Cells[6, currentColumn].Value = scenario.Briefing;
            sheet2.Cells[7, currentColumn].Value = scenario.EndingWin;
            sheet2.Cells[8, currentColumn].Value = scenario.EndingLose;
        }

        var lastColumn = scenarioInfos.Length + 1;

        //stylize
        sheet2.Cells[1, 1].EntireColumn.Width = 20;
        sheet2.Cells[1, 2, 8, lastColumn].EntireColumn.Width = 50;
        sheet2.Cells[4, 1, 4, lastColumn].EntireRow.Height = 75; 
        sheet2.Cells[5, 1, 5, lastColumn].EntireRow.Height = 50; 
        sheet2.Cells[6, 1, 6, lastColumn].EntireRow.Height = 410; 
        sheet2.Cells[7, 1, 7, lastColumn].EntireRow.Height = 350; 
        sheet2.Cells[8, 1, 8, lastColumn].EntireRow.Height = 75; 
        sheet2.Cells[1, 1, 8, lastColumn].Style.WrapText = true;
        
        return package.GetAsByteArray();
    }
}