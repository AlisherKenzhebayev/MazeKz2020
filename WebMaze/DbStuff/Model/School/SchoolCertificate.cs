using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebMaze.DbStuff.Model.UserAccount;

namespace WebMaze.DbStuff.Model.School
{
    
    /// <summary>
    /// Table for certificates, both students and staff
    /// </summary>
    [Table("school_certificate")]
    public class SchoolCertificate : BaseModel
    {
        [Required]
        public virtual long CitizenUserId { get; set; }
        [Required]
        public virtual long CertificateId { get; set; }

        #region navProperties
            public virtual CitizenUser CitizenUser { get; set; }
            public virtual Certificate Certificate { get; set; }
        #endregion
    }
}