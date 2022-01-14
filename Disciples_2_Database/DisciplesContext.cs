using DBF;

namespace Disciples_2_Database;

public class DisciplesContext : DBContext
{
    public DBSet<DbfSinfo> Scenaries;
}