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
}