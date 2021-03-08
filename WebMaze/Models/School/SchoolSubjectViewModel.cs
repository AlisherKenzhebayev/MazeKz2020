namespace WebMaze.Models.School
{
    public class SchoolSubjectViewModel
    {
        public long? OfficeNumber { get; set; } = null;
        public virtual string SubjectCode { get; set; } 
        public virtual string SubjectName { get; set; }
        public virtual string Description { get; set; }
        public virtual string RelevantMajor { get; set; }
        public virtual bool IsMandatory { get; set; }
    }
}