using System.Linq;
using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolScheduleRepository : BaseRepository<SchoolSchedule>
    {
        public SchoolScheduleRepository(WebMazeContext context) : base(context)
        {
        }

        public IQueryable<SchoolSchedule> GetFullSchedule(long citizenId) 
        {
            var retVal = dbSet
                .Where(o => o.CitizenUserId == citizenId)
                .OrderBy(o => o.DayOfWeek)
                .ThenBy(o => o.StartHoursMinutes);
            return retVal;
        }

        /// <summary>
        /// Returns a list of all people perticipating in a study group for subject
        /// </summary>
        /// <param name="subjectId"> Study subject's ID </param>
        /// <returns> IQueryable of citizenIDs </returns>
        public IQueryable<long> GetAllParticipants(long subjectId)
        {
            var retVal = dbSet
                .Where(o => o.SubjectId == subjectId)
                .Select(o => o.CitizenUserId);
            return retVal;
        }
    }
}