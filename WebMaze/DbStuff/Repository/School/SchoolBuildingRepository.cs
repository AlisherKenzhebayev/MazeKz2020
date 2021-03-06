using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolBuildingRepository : BaseRepository<SchoolBuilding>
    {
        public SchoolBuildingRepository(WebMazeContext context) : base(context)
        {
        }
    }
}