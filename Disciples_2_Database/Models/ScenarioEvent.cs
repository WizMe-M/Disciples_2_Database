namespace Disciples_2_Database.Models;

public class ScenarioEvent
{
    public ScenarioEvent(DBFSevent sevent)
    {
        SceneNumber = sevent.NO_SCENE;
        Id = sevent.ID;
        TriggeredByRaces = new List<Race>();
        for (var i = 0; i < sevent.RACES.Length; i++)
        {
            if(sevent.RACES[i])
                TriggeredByRaces.Add((Race)i);
        }
        
        TriggeredOnTurn = new List<Race>();
        for (var i = 0; i < sevent.VERRACES.Length; i++)
        {
            if(sevent.RACES[i])
                TriggeredOnTurn.Add((Race)i);
        }

        Initially = sevent.ENABLED;
        TriggeredOnce = sevent.OCCUR_ONCE;
        Order = sevent.ORDER;
    }

    public string SceneNumber { get; set; }
    public string Id { get; set; }
    public List<Race> TriggeredByRaces { get; set; }
    public List<Race> TriggeredOnTurn { get; set; }
    public bool Initially { get; set; }
    public bool TriggeredOnce { get; set; }
    public int Order { get; set; }
}