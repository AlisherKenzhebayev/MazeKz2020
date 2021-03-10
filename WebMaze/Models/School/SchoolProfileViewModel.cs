using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.DbStuff.Model;
using WebMaze.DbStuff.Model.School;
using WebMaze.DbStuff.Model.School.Enums;
using WebMaze.Models.Account;

namespace WebMaze.Models.School
{
    /// <summary>
    /// Profile model for both staff and students
    /// </summary>
    public class SchoolProfileViewModel
    {
        private string statusString;

        public MyProfileViewModel User { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        public StaffMember? StaffRank { get; set; } = null;

        /// <summary>
        /// Can signify both staff and student statuses
        /// </summary>
        public string StatusString 
        {
            get
            {
                return StaffRank switch
                {
                    StaffMember.Dean => "Декан",
                    StaffMember.President => "Президент Школы",
                    StaffMember.MaintenanceStaff => "Служебный рабочий",
                    StaffMember.MedicalStaff => "Медик",
                    StaffMember.SchoolBusDriver => "Водитель автобуса",
                    StaffMember.TeachingAssistant => "Ассистент",
                    StaffMember.TeachingStaff => "Учитель",
                    StaffMember.ViceDean => "Вице Декан",
                    null => "Student",
                    _ => "Non registered"
                };
            }
            set { statusString = value; } 
        }

        public long MainSchoolId { get; set; }
    }
}
