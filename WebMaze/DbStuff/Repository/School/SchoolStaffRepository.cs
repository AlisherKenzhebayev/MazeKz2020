using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolStaffRepository : BaseRepository<SchoolStaff>
    {
        public SchoolStaffRepository(WebMazeContext context) : base(context)
        {
        }
    }
}