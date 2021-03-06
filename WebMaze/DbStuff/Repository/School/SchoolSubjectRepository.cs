using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model.School;
using WebMaze.DbStuff.Model.School.Enums;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolSubjectRepository : BaseRepository<SchoolSubject>
    {
        public SchoolSubjectRepository(WebMazeContext context) : base(context)
        {
        }

        public SchoolSubject GetBySubjectCode(string subjectCode)
        {
            var retVal = dbSet
                .SingleOrDefault(o => o.SubjectCode == subjectCode);
            return retVal;
        }

        public IEnumerable<SchoolSubject> GetAllForSchoolOffice(long schoolId, long officeNumber)
        {
            var retVal = this.GetSchool(schoolId)
                .Where(s=> s.OfficeNumber == officeNumber);
            return retVal;
        }
        
        public IEnumerable<SchoolSubject> GetAllForSchoolBuilding(long schoolId)
        {
            var retVal = this.GetSchool(schoolId);
            return retVal;
        }

        public IEnumerable<SchoolSubject> GetAllMajorRelated(Major major)
        {
            var retVal = this.GetMajorRelated(major);
            return retVal;
        }
        
        public IEnumerable<SchoolSubject> GetMandatoryMajorRelated(Major major)
        {
            var retVal = this.GetMajorRelated(major)
                .Where(s=> s.IsMandatory);
            return retVal;
        }
        
        private IQueryable<SchoolSubject> GetMajorRelated(Major major)
        {
            var retVal = dbSet
                .Where(s => s.RelevantMajor == major);
            return retVal;
        }
        
        private IQueryable<SchoolSubject> GetSchool(long schoolId)
        {
            var retVal = dbSet
                .Where(s => s.SchoolId == schoolId);
            return retVal;
        }
    }
}