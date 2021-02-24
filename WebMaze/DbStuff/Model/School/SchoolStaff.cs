using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMaze.DbStuff.Model.School.Enums;

namespace WebMaze.DbStuff.Model.School
{
    /// <summary>
    /// Table for staff members
    /// </summary>
    [Table("school_staff", Schema = "School")]
    public class SchoolStaff : BaseModel
    {
        [Required]
        public virtual long CitizenUserId { get; set; }
        
        [Required]
        public virtual StaffMember StaffMember { get; set; }
        
        public virtual DateTime? StartDate { get; set; }

        public virtual DateTime? EndDate { get; set; }

        #region navProperties
            public virtual CitizenUser CitizenUser { get; set; }
        #endregion
    }
}