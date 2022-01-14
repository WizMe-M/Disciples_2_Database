using DbfDataReader;
using DbfReader = DbfDataReader.DbfDataReader;

namespace Disciples_2_Database;

public class DisciplesReader
{
    public IEnumerable<DbfSinfo> GetScenInfos()
    {
        var scenaries = new List<DbfSinfo>();
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
}