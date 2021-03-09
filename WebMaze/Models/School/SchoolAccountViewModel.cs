using System.Collections.Generic;

namespace WebMaze.Models.School
{
    public class SchoolAccountViewModel
    {
        public SchoolProfileViewModel ProfileViewModel { get; set; }
        
        public List<SchoolCertificateViewModel> CertificatesViewModel { get; set; }
        
        public List<SchoolScheduleViewModel> FullScheduleViewModel { get; set; }
    }
}