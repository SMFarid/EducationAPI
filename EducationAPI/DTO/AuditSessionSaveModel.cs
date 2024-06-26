namespace EducationAPI.DTO
{
    public class AuditSessionSaveModel
    {
        public AuditSessionSaveModel() {
            StudentsAttendedList = new List<StudentDTO>();
        }
        public int Auditing_Session_ID { get; set; }
        public int AuditorId { get; set; }
        public string RoundCode { get; set; }
        public int Study_Group_ID { get; set; }
        public DateTime? ReportStart { get; set; }
        public DateTime? ReportEnd { get; set; }
        public Boolean Conducted { get; set; }
        public Boolean MaterialDelivered { get; set; }
        public Boolean Lab_Flag { get; set; }
        public Boolean Test_Flag { get; set; }
        public Boolean Depi_Logo_Flag { get; set; }
        public string Current_Chapter { get; set; }
        public List<StudentDTO> StudentsAttendedList { get; set; }
        public int Instructor_ID { get; set; }
        public string ConnectionQuality { get; set; }
        public string VoiceQuality {  get; set; }
        public string VideoQuality { get; set; }
    }
}
