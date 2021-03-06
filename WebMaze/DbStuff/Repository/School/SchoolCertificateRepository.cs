using System.Linq;
using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolCertificateRepository : BaseRepository<SchoolCertificate>
    {
        public SchoolCertificateRepository(WebMazeContext context) : base(context)
        {
        }

        public IQueryable<long> GetCertificates(long citizenId) 
        {
            var retStr = dbSet
                .Where(o => o.CitizenUserId == citizenId)
                .Select(o => o.CertificateId);
            return retStr;
        }
    }
}