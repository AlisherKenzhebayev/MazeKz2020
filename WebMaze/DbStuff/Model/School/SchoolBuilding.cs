using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMaze.DbStuff.Model.School
{
    /// <summary>
    /// Table for School Buildings
    /// </summary>
    [Table("school_building", Schema = "School")]
    public class SchoolBuilding : BaseModel
    {
        public virtual long AddressId { get; set; }
        
        [Required]
        public virtual string SchoolName { get; set; }
        
        public virtual string Description { get; set; }
        
        #region navProperties
            public virtual Adress Address { get; set; }
        #endregion
    }
}