using DBF;
using Disciples_2_Database.Models;

namespace Disciples_2_Database;

public class DisciplesContext : DBContext
{
    public DBSet<DbfSinfo> Sinfos;
    public DBSet<DBFSevent> Sevents;
}