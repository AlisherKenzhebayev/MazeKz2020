using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolScheduleRepository : BaseRepository<SchoolBuilding>
    {
        public SchoolScheduleRepository(WebMazeContext context) : base(context)
        {
        }
    }
}