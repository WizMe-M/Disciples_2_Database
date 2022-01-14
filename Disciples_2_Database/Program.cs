using Disciples_2_Database;
using Disciples_2_Database.Models;

const string filePath = @"C:\Users\ender\OneDrive\Рабочий стол\ВАЖНО\ScenInfo.xlsx";

var exporter = new ExcelExporter();
var disciplesReader = new DisciplesReader();
var sinfos = disciplesReader.GetScenInfos().ToArray();
var scenarios = sinfos.Select(sinfo => new ScenarioInfo(sinfo)).ToList();
var scenarioExcel = exporter.Generate(scenarios);
File.WriteAllBytes(filePath, scenarioExcel); 