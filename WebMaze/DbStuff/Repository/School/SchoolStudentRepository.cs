using System;
using System.Collections.Generic;
using System.Linq;
using WebMaze.DbStuff.Model.School;

namespace WebMaze.DbStuff.Repository.School
{
    public class SchoolStudentRepository : BaseRepository<SchoolStudent>
    {
        public SchoolStudentRepository(WebMazeContext context) : base(context)
        {
        }

        public long GetSchoolId(long studentId)
        {
            var retVal = base.Get(studentId).SchoolId;
            return retVal;
        }

        public bool CheckExists(SchoolStudent student) 
        {
            var retVal = dbSet
                .SingleOrDefault(o=> o.CitizenUserId == student.CitizenUserId 
                && o.EducationStatus == student.EducationStatus
                && o.EnrollmentDate == student.EnrollmentDate)
                != default;
            return retVal;
        }

        public IQueryable<SchoolStudent> GetGraduationCohortDateYear(DateTime timeEnrollment) 
        {
            var retVal = dbSet
                .Where(o=> o.EnrollmentDate.Year == timeEnrollment.Year);
            return retVal;
        }
    }
}