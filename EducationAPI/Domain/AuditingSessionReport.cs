namespace EducationAPI.Models
{
    public class AuditingSessionReportModel
    {
        public string AuditorID { get; set; }
        public DateTime AuditDate { get; set; }
        public string CourseID { get; set;}
        public string ProviderID { get; set;}
        public string InstructorID { get; set;}
        public string StudyGroupID { get; set;}
        public string SessionType { get; set;} //Class/Lab/Test
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NumRegistered { get; set; }
        public int NumAttended { get; set; }
        public string ClassType { get; set;} //Online/InPerson
        public int Conducted { get; set; }
        public string CurrentChapter { get; set; }
        public int MaterialDelivered { get; set; }
        public int FilesDelivered { get; set; }
        public string ConnectionQuality { get; set; }
        public string VoiceQuality { get; set; }
        public string VideoQuality { get; set; }
        public string LinktoPrevSession { get; set;}
        public string AttendanceEvidence { get;}
    }
}
