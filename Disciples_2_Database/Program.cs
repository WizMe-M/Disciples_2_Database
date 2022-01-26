using Disciples_2_Database;
using Disciples_2_Database.Models;

const string filePath = @"C:\Users\ender\OneDrive\Рабочий стол\ВАЖНО\ScenInfo.xlsx";

var exporter = new ExcelExporter();
var disciplesReader = new DisciplesReader();

var sevents = disciplesReader.GetScenEvents();
var events = sevents.Select(sevent => new ScenarioEvent(sevent));
var sinfos = disciplesReader.GetScenInfos();
var scenarios = sinfos.Select(sinfo => new ScenarioInfo(sinfo));
scenarios = ScenarioInfo.MatchScenariosWithEvents(scenarios, events);

var excel = exporter.Generate(scenarios);
File.WriteAllBytes(filePath, excel); 