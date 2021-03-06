using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolStudentRepository : BaseRepository<SchoolStudent>
    {
        public SchoolStudentRepository(WebMazeContext context) : base(context)
        {
        }
    }
}