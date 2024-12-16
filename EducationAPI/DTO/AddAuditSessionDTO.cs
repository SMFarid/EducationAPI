namespace EducationAPI.DTO
{
    public class AddAuditSessionDTO
    {
        public AddAuditSessionDTO() {
            StudentsAttendedList = new List<AttendanceDTO>();
        }
        //public int Auditing_Session_ID { get; set; }
        //public int? Assignment_Session_ID { get; set; }
        public int? AuditorId { get; set; }
        public string? RoundCode { get; set; }
        public int Study_Group_ID { get; set; }
        public Boolean? Conducted { get; set; }
        public Boolean? MaterialDelivered { get; set; }
        public Boolean? Lab_Flag { get; set; }
        public Boolean? Test_Flag { get; set; }
        public Boolean? Depi_Logo_Flag { get; set; }
        public string? Current_Chapter { get; set; }
        public List<AttendanceDTO>? StudentsAttendedList { get; set; }
        public int? Instructor_ID { get; set; }
        public string? OtherInstructorName { get; set; }
        public string? ConnectionQuality { get; set; }
        public string? VoiceQuality {  get; set; }
        public string? VideoQuality { get; set; }
        public string? Remarks { get; set; }

        public string? HardwareProficiency { get; set; }
        public bool? UnderstoodExamples { get; set; }
        public bool? UnderstoonExplaination { get; set; }
        public bool? TimeForQuestions { get; set; }
        public bool? InstructorEncouragement { get; set; }
        public bool? MaterialIsClear { get; set; }
        public string? ACCondition { get; set; }
        public bool? CenterEnvironment { get; set; }
        public bool? InitiativeClear { get; set; }
        public bool? PrevLinks { get; set; }
        public string? CommentCategory { get; set; }
        public String? SessionType { get; set; }
        public string? AttendanceType { get; set; }
        public bool? IsCameraOpen { get; set; }
        public bool? IsLastSessionExam { get; set; }
        public string? InstructorPicturePath { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? SessionDateTimeStart { get; set; }

        public DateTime? SessionDateTimeClose { get; set; }

    }
}
