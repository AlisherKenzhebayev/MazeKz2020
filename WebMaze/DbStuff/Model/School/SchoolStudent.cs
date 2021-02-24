using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMaze.DbStuff.Model.School.Enums;

namespace WebMaze.DbStuff.Model.School
{
    [Table("school_student")]
    public class SchoolStudent : BaseModel
    {
        [Required]
        public virtual long CitizenUserId { get; set; }
        
        [Required]
        public virtual long SchoolId { get; set; }
        
        [Required]
        public virtual DateTime EnrollmentDate { get; set; }
        
        public virtual EducationStatus EducationStatus { get; set; }
        
        public virtual DateTime? GraduationDate { get; set; }
        
        public virtual Major Major { get; set; }

        #region navProperties
            public virtual CitizenUser CitizenUser { get; set; }
            public virtual SchoolBuilding SchoolBuilding { get; set; }
        #endregion
    }
}