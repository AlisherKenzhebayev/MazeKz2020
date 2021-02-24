namespace WebMaze.DbStuff.Model.School.Enums
{
    /// <summary>
    /// Enum of staff positions, both including the teaching positions as well as support and maintenance ones.
    /// </summary>
    public enum StaffMember
    {
        President = 0,
        Dean = 1,
        ViceDean = 2,
        TeachingStaff = 3,
        TeachingAssistant = 4,
        MedicalStaff = 5, 
        MaintenanceStaff = 6,
        SchoolBusDriver = 7,
    }
}