namespace Disciples_2_Database;

public class DbfSinfo
{
    public DbfSinfo(
        string noScene,
        string infoId, 
        string name,
        string description,
        string briefing,
        IEnumerable<string> brieflongs, 
        IEnumerable<string> debunkWin,
        string debunkLose)
    {
        NoScene = noScene;
        InfoId = infoId;
        Name = name;
        Description = description;
        Briefing = briefing;
        DebunkLose = debunkLose;
        Brieflongs = new List<string>(brieflongs);
        DebunkWin = new List<string>(debunkWin);
    }
    
    public string NoScene { get; set; }
    public string InfoId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Briefing { get; set; }
    public List<string> Brieflongs { get; set; }
    public List<string> DebunkWin { get; set; }
    public string DebunkLose { get; set; }

}