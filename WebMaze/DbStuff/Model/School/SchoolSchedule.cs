using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMaze.DbStuff.Model.School
{
    /// <summary>
    /// Table for student schedules as well as faculty staff schedules.
    /// </summary>
    [Table("school_schedule")]
    public class SchoolSchedule : BaseModel
    {
        [Required]
        public virtual long CitizenUserId { get; set; }

        [Required]
        public virtual long SubjectId { get; set; }

        [Required]
        public virtual DayOfWeek DayOfWeek { get; set; }

        [Required, DataType("time")]
        public virtual TimeSpan StartHoursMinutes { get; set; }

        [Required, DataType("time")]
        public virtual TimeSpan EndHoursMinutes { get; set; }

        #region navProperties
            public virtual CitizenUser CitizenUser { get; set; }
            public virtual SchoolSubject SchoolSubject { get; set; }
        #endregion
    }
}