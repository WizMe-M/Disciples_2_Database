using DbfDataReader;
using Disciples_2_Database.Models;
using DbfReader = DbfDataReader.DbfDataReader;

namespace Disciples_2_Database;

public class DisciplesReader
{
    public IEnumerable<DbfSinfo> GetScenInfos()
    {
        var path = @"C:\Program Files (x86)\Steam\steamapps\common\Disciples II Rise of the Elves\Scens\Sinfo.DBF";
        var options = new DbfDataReaderOptions
        {
            SkipDeletedRecords = true,
            Encoding = EncodingProvider.GetEncoding(1252) //ANSI
        };

        using var reader = new DbfReader(path, options);
        while (reader.Read())
        {
            var noScene = reader.GetString(0);
            var infoId = reader.GetString(1);
            var name = reader.GetString(28);
            var description = reader.GetString(29);
            var briefing = reader.GetString(30);
            var brieflongs = new string[5];
            for (var i = 0; i < 5; i++)
            {
                var index = 31 + i;
                var brieflong = reader.GetString(index);
                brieflongs[i] = brieflong;
            }

            var debunkWinStrings = new string[5];
            for (var i = 0; i < 5; i++)
            {
                var index = 36 + i;
                var debunkWin = reader.GetString(index);
                debunkWinStrings[i] = debunkWin;
            }

            var debunkLose = reader.GetString(41);

            var scenInfo = new DbfSinfo(noScene, infoId, name, description,
                briefing, brieflongs, debunkWinStrings, debunkLose);

            yield return scenInfo;
        }
    }

    public IEnumerable<DBFSevent> GetScenEvents()
    {
        var path = @"C:\Program Files (x86)\Steam\steamapps\common\Disciples II Rise of the Elves\Scens\SEvent.dbf";
        var options = new DbfDataReaderOptions
        {
            SkipDeletedRecords = true,
            Encoding = EncodingProvider.GetEncoding(1252) //ANSI
        };

        using var reader = new DbfReader(path, options);
        while (reader.Read())
        {
            var noScene = reader.GetString(0);
            var id = reader.GetString(1);
            var races = new bool[6];
            for (var i = 0; i < races.Length; i++)
            {
                bool forRace;
                try
                {
                    forRace = reader.GetBoolean(2 + i);
                }
                catch (Exception e)
                {
                    forRace = false;
                }
                races[i] = forRace;
            }

            var isEnabled = reader.GetBoolean(8);
            var occurOnce = reader.GetBoolean(9);

            var verraces = new bool[6];
            for (var i = 0; i < verraces.Length; i++)
            {
                
                bool byRace;
                try
                {
                    byRace = reader.GetBoolean(13 + i);
                }
                catch (Exception e)
                {
                    byRace = false;
                }

                verraces[i] = byRace;
            }

            var order = reader.GetInt32(19);

            var scenEvent = new DBFSevent(noScene, id, races, verraces, occurOnce, order, isEnabled);

            yield return scenEvent;
        }
    }
}