using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class StudyGroup
{
    public string RoundCode { get; set; } = null!;

    public int? TrackIntId { get; set; }

    public string? TrackCode { get; set; }

    public int GroupIntId { get; set; }

    public int? JobProfileIntId { get; set; }

    public string? Governorate { get; set; }

    public string? InstructorName { get; set; }

    public int? InstructorId { get; set; }

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

    public virtual ICollection<GroupSession> GroupSessions { get; set; } = new List<GroupSession>();

    public virtual Instructor? Instructor { get; set; }

    public virtual JobProfile? JobProfileInt { get; set; }

    public virtual ICollection<StudyGroupSession> StudyGroupSessions { get; set; } = new List<StudyGroupSession>();

    public virtual TrackProvider? TrackInt { get; set; }

    public virtual Track? TrackIntNavigation { get; set; }
    public virtual ICollection<Trainee>? Trainees { get;}
    public virtual TrainingProvider trainingProvider { get; set; }
}
