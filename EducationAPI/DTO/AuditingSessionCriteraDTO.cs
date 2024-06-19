namespace EducationAPI.DTO
{
    public class AuditingSessionCriteraDTO
    {
        public AuditingSessionCriteraDTO()
        {
            Students = new List<StudentDTO>();
            Instructors = new List<InstructorDTO>();
        }
        public int Session_ID { get; set; }
        public string TrainingCenterName { get; set; }
        public string TrainingProvider { get; set; }
        public string CourseName { get; set; }
        public List<StudentDTO> Students { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<InstructorDTO> Instructors { get; set; }
        public string SessionType { get; set; }
        public int NumberRegistered { get; set; }
    }
}
