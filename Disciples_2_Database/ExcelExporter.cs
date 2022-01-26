using Disciples_2_Database.Models;
using OfficeOpenXml;
using OfficeOpenXml.Table;

namespace Disciples_2_Database;

public class ExcelExporter
{
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
        var itemsCount = scenarioInfos.Length;
        const int fieldCount = 7;

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var package = new ExcelPackage();

        var sageSheet = package.Workbook.Worksheets.Add("Карты саги");
        //header
        sageSheet.Cells["A1"].Value = "Номер сценария:";
        sageSheet.Cells["B1"].Value = "Название:";
        sageSheet.Cells["C1"].Value = "Описание:";
        sageSheet.Cells["D1"].Value = "Цель:";
        sageSheet.Cells["E1"].Value = "Брифинг:";
        sageSheet.Cells["F1"].Value = "Текст при победе:";
        sageSheet.Cells["G1"].Value = "Текст при поражении:";

        //load
        for (var i = 0; i < scenarioInfos.Length; i++)
        {
            var scenario = scenarioInfos[i];
            var events = scenario.ScenarioEvents.ToArray();

            sageSheet.Cells[i + 2, 1].Value = scenario.Number;
            sageSheet.Cells[i + 2, 2].Value = scenario.Name;
            sageSheet.Cells[i + 2, 3].Value = scenario.Description;
            sageSheet.Cells[i + 2, 4].Value = scenario.Objective;
            sageSheet.Cells[i + 2, 5].Value = scenario.Briefing;
            sageSheet.Cells[i + 2, 6].Value = scenario.EndingWin;
            sageSheet.Cells[i + 2, 7].Value = scenario.EndingLose;


            var sceneSheet = package.Workbook.Worksheets.Add(scenario.Name);

            //header
            sceneSheet.Cells["A1"].Value = "ID события";
            sceneSheet.Cells["B1"].Value = "Включен";
            sceneSheet.Cells["C1"].Value = "Единовременно";
            sceneSheet.Cells["D1"].Value = "Порядок";
            sceneSheet.Cells["E1"].Value = "Событие и эффекты срабатывают для рас";
            sceneSheet.Cells["F1"].Value = "Событие срабатывает во время хода рас";

            for (var j = 0; j < events.Length; j++)
            {
                var scenarioEvent = events[j];
                sceneSheet.Cells[j + 2, 1].Value = scenarioEvent.Id;
                sceneSheet.Cells[j + 2, 2].Value = scenarioEvent.Initially;
                sceneSheet.Cells[j + 2, 3].Value = scenarioEvent.TriggeredOnce;
                sceneSheet.Cells[j + 2, 4].Value = scenarioEvent.Order;

                var races = scenarioEvent.TriggeredByRaces.Select(triggeredByRace => triggeredByRace switch
                {
                    Race.Empire => "Империя",
                    Race.UndeadHordes => "Орды Нежити",
                    Race.LegionsOfDamned => "Легионы Проклятых",
                    Race.MountainClans => "Горные Кланы",
                    Race.ElvenAlliance => "Эльфийский Альянс",
                    _ => "Нейтралы"
                });
                sceneSheet.Cells[j + 2, 5].Value = string.Join(", ", races);

                var verraces = scenarioEvent.TriggeredOnTurn.Select(triggeredOnTurn => triggeredOnTurn switch
                {
                    Race.Empire => "Империя",
                    Race.UndeadHordes => "Орды Нежити",
                    Race.LegionsOfDamned => "Легионы Проклятых",
                    Race.MountainClans => "Горные Кланы",
                    Race.ElvenAlliance => "Эльфийский Альянс",
                    _ => "Нейтралы"
                });
                sceneSheet.Cells[j + 2, 6].Value = string.Join(", ", verraces);
            }
            
            
            var sceneEventsLastRow = events.Length + 1;
            var sceneEventTableRange = sceneSheet.Cells[1, 1, sceneEventsLastRow, 6];
            var sceneEventTable = sceneSheet.Tables.Add(sceneEventTableRange, $"EventTable{i}");
            sceneEventTable.TableStyle = TableStyles.Medium15;
            sceneSheet.Cells[1, 1, sceneEventsLastRow, fieldCount].AutoFitColumns();
        }

        // sheet1.Cells["A2"].LoadFromCollection(scenarioInfos);

        //stylize
        var lastRow = itemsCount + 1;
        var tableRange = sageSheet.Cells[1, 1, lastRow, fieldCount];
        var table = sageSheet.Tables.Add(tableRange, "ScenariosTable");
        table.TableStyle = TableStyles.Medium15;
        sageSheet.Cells[1, 1, lastRow, fieldCount].AutoFitColumns();
        sageSheet.Cells[1, 1, lastRow, fieldCount].Style.WrapText = true;
        sageSheet.Cells["C1"].EntireColumn.Width = 50;
        sageSheet.Cells["D1"].EntireColumn.Width = 40;
        sageSheet.Cells["E1"].EntireColumn.Width = 125;
        sageSheet.Cells["F1"].EntireColumn.Width = 55;
        sageSheet.Cells["G1"].EntireColumn.Width = 60;

        //


        return package.GetAsByteArray();
    }
}