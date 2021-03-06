using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolCertificateRepository : BaseRepository<SchoolBuilding>
    {
        public SchoolCertificateRepository(WebMazeContext context) : base(context)
        {
        }
    }
}