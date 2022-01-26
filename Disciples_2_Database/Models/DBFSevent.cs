namespace Disciples_2_Database.Models;

public class DBFSevent
{
    public DBFSevent(string noScene, string id, bool[] races, bool[] verraces, bool occurOnce, int order, bool enabled)
    {
        NO_SCENE = noScene;
        ID = id;
        RACES = races;
        VERRACES = verraces;
        OCCUR_ONCE = occurOnce;
        ORDER = order;
        ENABLED = enabled;
    }

    public string NO_SCENE { get; set; }
    public string ID { get; set; }
    
    public bool[] RACES { get; set; }
    public bool[] VERRACES { get; set; }
    public bool OCCUR_ONCE { get; set; }
    public int ORDER { get; set; }
    public bool ENABLED { get; set; }
}