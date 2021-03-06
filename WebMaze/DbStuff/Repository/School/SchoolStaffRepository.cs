using System.Linq;
using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolStaffRepository : BaseRepository<SchoolStaff>
    {
        public SchoolStaffRepository(WebMazeContext context) : base(context)
        {
        }

        public IQueryable<SchoolStaff> GetAllFromSchool(long schoolId)
        {
            var retVal = dbSet
                .Where(o => o.SchoolId == schoolId);
            return retVal;
        }
    }
}