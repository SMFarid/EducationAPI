namespace EducationAPI.DTO
{
    public class ViewStudyGroupDTO
    {
        public string? RoundCode { get; set; }

        //public int? TrackIntId { get; set; }

        public string? TrackCode { get; set; }

        public int GroupIntId { get; set; }
        public string? Provider { get; set; }

        //public int? JobProfileIntId { get; set; }

        public string? Governorate { get; set; }

        public string? InstructorName { get; set; }

        //public int? InstructorId { get; set; }

        public string? StudyGroupType { get; set; }

        public decimal? NumberOfStudents { get; set; }

        public decimal? Capacity { get; set; }

        public string? YearSemester { get; set; }

        public string? LocationAddress { get; set; }

        public string? LocationGoogleMap { get; set; }

        public string? MeetingLink { get; set; }

        public string? MeetingLinkId { get; set; }

        public string? MeetingLinkPasscode { get; set; }

        public string? GroupStartTime { get; set; }

        public string? ExpectedEndTime { get; set; }

        public bool? WelcomeMessage { get; set; }

        public string? TraineeType { get; set; }

        public string? WeekDayEndFlag { get; set; }
    }
}
