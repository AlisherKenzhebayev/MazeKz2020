using System.Linq;
using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolBuildingRepository : BaseRepository<SchoolBuilding>
    {
        public SchoolBuildingRepository(WebMazeContext context) : base(context)
        {
        }

        public SchoolBuilding GetByName(string schoolName) 
        {
            var retStr = dbSet
                .SingleOrDefault(o=>o.SchoolName == schoolName);
            return retStr;
        }
    }
}