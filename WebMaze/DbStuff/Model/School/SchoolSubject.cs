using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebMaze.DbStuff.Model.School.Enums;

namespace WebMaze.DbStuff.Model.School
{
    /// <summary>
    /// Table for school subjects,  
    /// </summary>
    [Table("school_subject")]
    public class SchoolSubject : BaseModel
    {
        [Required]
        public virtual long SchoolId { get; set; }
        
        public virtual long? OfficeNumber { get; set; }
        
        [Required]
        public virtual string SubjectCode { get; set; }
        
        [Required]
        public virtual string SubjectName { get; set; }
        
        [Required]
        public virtual string Description { get; set; }
        
        [Required]
        public virtual Major RelevantMajor { get; set; }
        
        [Required]
        public virtual bool IsMandatory { get; set; }

        #region navProperties
            public virtual SchoolBuilding SchoolBuilding { get; set; }
        #endregion
    }
}