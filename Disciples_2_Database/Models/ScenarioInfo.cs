using System.Text;

namespace Disciples_2_Database.Models;

public class ScenarioInfo
{
    public ScenarioInfo(DbfSinfo sinfo)
    {
        Number = sinfo.NoScene;
        Name = sinfo.Name;
        Description = sinfo.Description;
        Objective = sinfo.Briefing;
        EndingLose = sinfo.DebunkLose;

        Briefing = string.Join("", sinfo.Brieflongs)
            .Replace("_", "")
            .Trim();
        
        EndingWin = string.Join("", sinfo.DebunkWin)
            .Replace("_", "")
            .Trim();
    }
    public string Number { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Objective { get; set; }
    public string Briefing { get; set; }
    
    //rename
    public string EndingWin { get; set; }
    public string EndingLose { get; set; }
    
    public IEnumerable<ScenarioEvent> ScenarioEvents { get; set; }

    public static IEnumerable<ScenarioInfo> MatchScenariosWithEvents(
        IEnumerable<ScenarioInfo> infos,
        IEnumerable<ScenarioEvent> events)
    {
        var groupedEvents = events.GroupBy(@event => @event.SceneNumber);

        foreach (var group in groupedEvents)
        {
            var sceneNumber = group.Key;
            
            foreach (var scenarioInfo in infos)
            {
                if (scenarioInfo.Number != sceneNumber) continue;
                
                scenarioInfo.ScenarioEvents = group.OrderBy(@event => @event.Order);
                yield return scenarioInfo;
                break;
            }
        }
    }


}