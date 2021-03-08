using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.School;
using WebMaze.Models.Account;

namespace WebMaze.Models.School
{
    /// <summary>
    /// Model for singular schedule item 
    /// </summary>
    public class SchoolScheduleViewModel
    {
        public SchoolSubjectViewModel SchoolSubject { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartHoursMinutes { get; set; }
        public TimeSpan EndHoursMinutes { get; set; }
    }
}
