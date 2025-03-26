using Azure;
using EducationAPI.Common;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Enums;
using EducationAPI.Models;
using EducationAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;

namespace EducationAPI.Services
{
    public class DatatManagementService
    {
        StudentRepository _studentRepository = new StudentRepository();
        StudyGroupRepository _studyGroupRepository = new StudyGroupRepository();
        ProviderStudyGroupRepository _providerStudyGroupRepository = new ProviderStudyGroupRepository();
        TrackRepository _tracksRepository = new TrackRepository();
        TrainingProviderRepository _trainingProviderRepository = new TrainingProviderRepository();
        JobProfileRepository _jobProfileRepository = new JobProfileRepository();
        InstructorsRepository _instructorsRepository = new InstructorsRepository();
        AuditingSessionRepository _sessionRepo = new AuditingSessionRepository();

        int countRecords = 0;

        #region Student Management
        public async Task<CommonResponse<StudentViewModel>> EditStudent(StudentEditModel model)
        {
            var response = new CommonResponse<StudentViewModel>();

            //Validate
            var student = await _studentRepository.getStudentByIntID(model.StudentID);
            if (student == null)
            {
                response.Errors.Add(new Error { Message = "Student with ID: " + model.StudentID + " not found!" });
                return response;
            }

            //Change details if found

            if (!string.IsNullOrEmpty(model.StudentAppID))
                student.StudentAppId = model.StudentAppID;
            if (!string.IsNullOrEmpty(model.SocialID))
                student.SocialId = model.SocialID;
            if (!string.IsNullOrEmpty(model.NameEn))
                student.NameEn = model.NameEn;
            if (!string.IsNullOrEmpty(model.NameAr))
                student.NameAr = model.NameAr;
            if (model.TrackID != null)
                student.TrackId = model.TrackID;
            if (!string.IsNullOrEmpty(model.RoundCode))
                student.RoundCode = model.RoundCode;
            if (model.GroupIntID != null)
                student.GroupIntID = (int)model.GroupIntID;
            if (!string.IsNullOrEmpty(model.StudyGovernorate))
                student.StudyGovernorate = model.StudyGovernorate;
            if (!string.IsNullOrEmpty(model.Address))
                student.Address = model.Address;
            if (!string.IsNullOrEmpty(model.City))
                student.City = model.City;
            if (!string.IsNullOrEmpty(model.Email))
                student.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Mobile))
                student.Mobile = model.Mobile;
            if (!string.IsNullOrEmpty(model.Status))
                student.Status = model.Status;
            if (model.Active != null)
                student.Active = (bool)model.Active;
            if (model.ModifiedUser != null)
                student.ModifiedUser = model.ModifiedUser;
            student.ModifiedDate = DateTime.Now;


            await _studentRepository.Save();

            response = await GetStudent(model.StudentID);

            return response;
        }

        public async Task<CommonResponse<StudentViewModel>> AddStudent(StudentAddModel model)
        {
            var response = new CommonResponse<StudentViewModel>();

            //Validate
            //var student = await _studentRepository.getStudentByAppID(model.StudentAppID);
            var student = await _studentRepository.getStudentByEmail(model.Email);

            if (student != null)
            {
                response.Errors.Add(new Error { Message = "Student with ID: " + model.Email + " already exists! " });
                return response;
            }

            student = await _studentRepository.getStudentByMobile(model.Mobile);

            if (student != null)
            {
                response.Errors.Add(new Error { Message = "Student with ID: " + model.Mobile + " already exists! " });
                return response;
            }

            student = new Trainee();
            //Change details if found
            if (!string.IsNullOrEmpty(model.StudentAppID))
                student.StudentAppId = model.StudentAppID;
            if (!string.IsNullOrEmpty(model.SocialID))
                student.SocialId = model.SocialID;
            if (!string.IsNullOrEmpty(model.NameEn))
                student.NameEn = model.NameEn;
            if (!string.IsNullOrEmpty(model.NameAr))
                student.NameAr = model.NameAr;
            if (model.TrackID != null)
                student.TrackId = model.TrackID;
            if (!string.IsNullOrEmpty(model.RoundCode))
                student.RoundCode = model.RoundCode;
            if (model.GroupIntID != null)
                student.GroupIntID = (int)model.GroupIntID;
            if (!string.IsNullOrEmpty(model.StudyGovernorate))
                student.StudyGovernorate = model.StudyGovernorate;
            if (!string.IsNullOrEmpty(model.Address))
                student.Address = model.Address;
            if (!string.IsNullOrEmpty(model.City))
                student.City = model.City;
            if (!string.IsNullOrEmpty(model.Email))
                student.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Mobile))
                student.Mobile = model.Mobile;
            if (!string.IsNullOrEmpty(model.Status))
                student.Status = model.Status;
            if (model.Active != null)
                student.Active = (bool)model.Active;
            if (model.ModifiedUser != null)
                student.ModifiedUser = model.ModifiedUser;
            
            student.CreatedDate = DateTime.Now;
            student.ModifiedDate = DateTime.Now;

            _studentRepository.Add(student);
            await _studentRepository.Save();

            response = await GetStudent(student.TraineeIntId);

            return response;
        }

        public async Task<CommonResponse<StudentViewModel>> GetStudent(int StudentID)
        {
            var response = new CommonResponse<StudentViewModel>();

            StudentViewModel studentModel = new StudentViewModel();

            //Validate
            var student = await _studentRepository.getStudentByIntID(StudentID);
            if (student == null)
            {
                response.Errors.Add(new Error { Message = "Student with ID: " + StudentID + " not found!" });
                return response;
            }

            //return details if found


            studentModel.SocialID = student.SocialId;

            studentModel.NameEn = student.NameEn;

            studentModel.NameAr = student.NameAr;

            studentModel.TrackID = student.TrackId;

            studentModel.RoundCode = student.RoundCode;

            studentModel.GroupIntID = (int)student.GroupIntID;

            studentModel.StudyGovernorate = student.StudyGovernorate;

            studentModel.Address = student.Address;

            studentModel.City = student.City;

            studentModel.Email = student.Email;

            studentModel.Mobile = student.Mobile;

            studentModel.Status = student.Status;

            studentModel.Active = (bool)student.Active;
            studentModel.ModifiedDate = student.ModifiedDate;
            studentModel.CreatedDate = student.CreatedDate;
            studentModel.ModifiedUser = student.ModifiedUser;

            response.Data = studentModel;

            return response;
        }

        public async Task<CommonResponse<List<StudentDTO>>> getStudentsList()
        {
            var result = new CommonResponse<List<StudentDTO>>();

            var studentsList = await _studentRepository.getAllStudent();
            List<StudentDTO> returnList = new List<StudentDTO>();
            foreach (var student in studentsList)
            {
                var studentDTO = new StudentDTO {
                    Id = student.TraineeIntId,
                    NameAr = student.NameAr,
                    NameEN = student.NameEn
                };
                returnList.Add(studentDTO);
            }
            result.Data = returnList;

            return result;
        }
        #endregion

        #region Study Group Management

        public async Task<CommonResponse<List<RoundCodeDTO>>> getAllRoundCodes()
        {
            var response = new CommonResponse<List<RoundCodeDTO>>();
            List<RoundCodeDTO> StudyGroupsList = new List<RoundCodeDTO>();

            var studyGroups = await _studyGroupRepository.getAllGroups();

            if (studyGroups != null)
            {
                foreach (var code in studyGroups)
                {
                    StudyGroupsList.Add(new RoundCodeDTO
                    {
                        RoundCode = code.RoundCode,
                        GroupIntID = code.GroupIntId
                    });
                }
            }

            response.Data = StudyGroupsList;
            return response;
        }

        public async Task<CommonResponse<ViewStudyGroupDTO>> GetGroupDetails(int groupIntID)
        {
            var result = new CommonResponse<ViewStudyGroupDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(groupIntID);

            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "VIEW: Group with ID: " + groupIntID + " not found!" });
                return result;
            }

            var provider = await _providerStudyGroupRepository.getProviderbyStudyGroup(studyGroup.GroupIntId);
            if (provider == null)
            {
                result.Errors.Add(new Common.Error { Message = "Error: Unable to find provider for this group" });
                return result;
            }


            ViewStudyGroupDTO studyGroupDTO = new ViewStudyGroupDTO
            {
                RoundCode = studyGroup.RoundCode,
                GroupIntId = groupIntID,
                Capacity = studyGroup.Capacity,
                ExpectedEndTime = studyGroup.ExpectedEndTime,
                Governorate = studyGroup.Governorate,
                GroupStartTime = studyGroup.GroupStartTime,
                //InstructorId = studyGroup.InstructorId,
                InstructorName = studyGroup.InstructorName,
                //JobProfileIntId = studyGroup.JobProfileIntId,
                LocationAddress = studyGroup.LocationAddress,
                LocationGoogleMap = studyGroup.LocationGoogleMap,
                MeetingLink = studyGroup.MeetingLink,
                MeetingLinkId = studyGroup.MeetingLinkId,
                MeetingLinkPasscode = studyGroup.MeetingLinkPasscode,
                NumberOfStudents = studyGroup.NumberOfStudents,
                StudyGroupType = studyGroup.StudyGroupType,
                TrackCode = studyGroup.TrackCode,
                //TrackIntId = studyGroup.TrackIntId,
                Provider = provider!= null ? provider.ProviderName: "not assigned",
                TraineeType = studyGroup.TraineeType,
                WeekDayEndFlag = studyGroup.WeekDayEndFlag,
                WelcomeMessage = studyGroup.WelcomeMessage,
                YearSemester = studyGroup.YearSemester,
                Status = studyGroup.Status,
                StatusComment = studyGroup.StatusComment,
                StatStatusCommentCatID = studyGroup.StatStatusCommentCatIDusComment
            };

            if (studyGroup.StudyGroupDays != null || studyGroup.StudyGroupDays.FirstOrDefault() != null )
            {
                var schedule = studyGroup.StudyGroupDays.OrderByDescending(c => c.SrlNo).FirstOrDefault();
                if (schedule != null)
                {
                    var studyGroupDays = new StudyGroupDaysDTO
                    {
                        StudyGroupId = schedule.StudyGroupId,
                        RoundCode = schedule.RoundCode,
                        OnlineDay1 = schedule.OnlineDay1,
                        OnlineDay2 = schedule.OnlineDay2,
                        OnlineDay3 = schedule.OnlineDay3,
                        PhysicalDay = schedule.PhysicalDay,
                        SoftskillDay = schedule.SoftskillDay,
                        CoachingDay = schedule.CoachingDay,
                        EnglishDay = schedule.EnglishDay,

                        OnlineTimeInterval = schedule.OnlineTimeInterval,
                        PhysicalTimeInterval = schedule.PhysicalTimeInterval,
                        SoftskillTimeInterval = schedule.SoftskillTimeInterval,
                        CoachingTimeInterval = schedule.CoachingTimeInterval,
                        EnglishTimeInterval = schedule.EnglishTimeInterval
                    };
                    studyGroupDTO.studyGroupDaysDTO = studyGroupDays;
                }
            }

            result.Data = studyGroupDTO;

            return result;
        }

        public async Task<CommonResponse<ViewStudyGroupDTO>> EditGroup(EditStudyGroupDTO model)
        {
            var result = new CommonResponse<ViewStudyGroupDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(model.GroupIntId);
            
            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "EDIT: Group with ID: " + model.GroupIntId + " not found!" });
                return result;
            }

            if (!string.IsNullOrEmpty(model.RoundCode))
                studyGroup.RoundCode = model.RoundCode;

            if (model.Capacity != null)
                studyGroup.Capacity = model.Capacity;
            if (!string.IsNullOrEmpty(model.ExpectedEndTime))
                studyGroup.ExpectedEndTime = model.ExpectedEndTime;
            if (!string.IsNullOrEmpty(model.Governorate))
                studyGroup.Governorate = model.Governorate;
            if (!string.IsNullOrEmpty(model.GroupStartTime))
                studyGroup.GroupStartTime = model.GroupStartTime;
            if (model.InstructorId != null && model.InstructorId != 0)
                studyGroup.InstructorId = model.InstructorId;
            if (!string.IsNullOrEmpty(model.InstructorName))
                studyGroup.InstructorName = model.InstructorName;
            if (model.JobProfileIntId != null)
                studyGroup.JobProfileIntId = model.JobProfileIntId;
            if (!string.IsNullOrEmpty(model.LocationAddress))
                studyGroup.LocationAddress = model.LocationAddress;
            if (!string.IsNullOrEmpty(model.LocationGoogleMap))
                studyGroup.LocationGoogleMap = model.LocationGoogleMap;
            if (!string.IsNullOrEmpty(model.MeetingLink))
                studyGroup.MeetingLink = model.MeetingLink;
            if (!string.IsNullOrEmpty(model.MeetingLinkId))
                studyGroup.MeetingLinkId = model.MeetingLinkId;
            if (!string.IsNullOrEmpty(model.MeetingLinkPasscode))
                studyGroup.MeetingLinkPasscode = model.MeetingLinkPasscode;
            if (model.NumberOfStudents != null)
                studyGroup.NumberOfStudents = model.NumberOfStudents;
            if (!string.IsNullOrEmpty(model.StudyGroupType))
                studyGroup.StudyGroupType = model.StudyGroupType;
            if (!string.IsNullOrEmpty(model.TrackCode))
                studyGroup.TrackCode = model.TrackCode;
            if (model.TrackIntId != null)
                studyGroup.TrackIntId = model.TrackIntId;
            if (!string.IsNullOrEmpty(model.TraineeType))
                studyGroup.TraineeType = model.TraineeType;
            if (!string.IsNullOrEmpty(model.WeekDayEndFlag))
                studyGroup.WeekDayEndFlag = model.WeekDayEndFlag;
            if (model.WelcomeMessage != null)
                studyGroup.WelcomeMessage = model.WelcomeMessage;
            if (!string.IsNullOrEmpty(model.YearSemester))
                studyGroup.YearSemester = model.YearSemester;

            if (model.Status!= null)
                studyGroup.Status = model.Status;
            if (!string.IsNullOrEmpty(model.StatusComment))
                studyGroup.StatusComment = model.StatusComment;
            
            if (model.StatStatusCommentCatID != null)
                studyGroup.StatStatusCommentCatIDusComment = model.StatStatusCommentCatID;

            //Edit Provider
            if (model.Provider != null)
            {
                var provider = await _trainingProviderRepository.getProviderByID((int)model.Provider);
                if (provider == null)
                {
                    result.Errors.Add(new Error { Message = "Add Group Error : Provider with ID: " + model.Provider + " Not found" });
                    return result;
                }
                var providerLink = await _providerStudyGroupRepository.getProviderbyStudyGroup(model.GroupIntId);
                if (providerLink == null)
                {
                    var providerStudyGroup = new ProviderStudyGroup
                    {
                        ProviderId = model.Provider,
                        ProviderName = provider.NameEn,
                        RoundCode = model.RoundCode,
                        Remarks = "Link Created : " + DateTime.Now,
                        YearSemester = DateTime.Now.Year,
                        //StudyGroupIntId = studyGroup.GroupIntId,
                        //StudyGroup = studyGroup
                    };
                    studyGroup.TrainingProvider = providerStudyGroup;
                } else
                {
                    providerLink.ProviderId = model.Provider;
                }

            }
                

            //Check if days is null, if not then edit days
            if (model.studyGroupDaysDTO != null && model.studyGroupDaysDTO.StudyGroupId == model.GroupIntId)
            {
                if (studyGroup.StudyGroupDays != null || studyGroup.StudyGroupDays.FirstOrDefault() != null)
                {
                    var groupDays = studyGroup.StudyGroupDays.OrderByDescending(c => c.SrlNo).FirstOrDefault();
                    
                    groupDays.OnlineDay1 = model.studyGroupDaysDTO.OnlineDay1;
                    groupDays.OnlineDay2 = model.studyGroupDaysDTO.OnlineDay2;
                    groupDays.OnlineDay3 = model.studyGroupDaysDTO.OnlineDay3;
                    groupDays.PhysicalDay = model.studyGroupDaysDTO.PhysicalDay;
                    groupDays.SoftskillDay = model.studyGroupDaysDTO.SoftskillDay;
                    groupDays.CoachingDay = model.studyGroupDaysDTO.CoachingDay;
                    groupDays.EnglishDay = model.studyGroupDaysDTO.EnglishDay;

                    groupDays.OnlineTimeInterval = model.studyGroupDaysDTO.OnlineTimeInterval;
                    groupDays.PhysicalTimeInterval = model.studyGroupDaysDTO.PhysicalTimeInterval;
                    groupDays.SoftskillTimeInterval = model.studyGroupDaysDTO.SoftskillTimeInterval;
                    groupDays.CoachingTimeInterval = model.studyGroupDaysDTO.CoachingTimeInterval;
                    groupDays.EnglishTimeInterval = model.studyGroupDaysDTO.EnglishTimeInterval;
                }
            }

            await _studyGroupRepository.Save();

            result = await GetGroupDetails(model.GroupIntId);

            return result;
        }

        public async Task<CommonResponse<ViewStudyGroupDTO>> AddGroup(AddStudyGroupDTO model)
        {
            var result = new CommonResponse<ViewStudyGroupDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByCode(model.RoundCode);

            if (studyGroup != null)
            {
                result.Errors.Add(new Error { Message = "Add Group Error : Group with ID: " + model.RoundCode + " Already exists!" });
                return result;
            }
            studyGroup = new StudyGroup();
            ProviderStudyGroup providerStudyGroup = null;
            if (!string.IsNullOrEmpty(model.RoundCode))
                studyGroup.RoundCode = model.RoundCode;

            if (model.Capacity != null)
                studyGroup.Capacity = model.Capacity;
            if (!string.IsNullOrEmpty(model.ExpectedEndTime))
                studyGroup.ExpectedEndTime = model.ExpectedEndTime;
            if (!string.IsNullOrEmpty(model.Governorate))
                studyGroup.Governorate = model.Governorate;
            if (!string.IsNullOrEmpty(model.GroupStartTime))
                studyGroup.GroupStartTime = model.GroupStartTime;
            if (model.InstructorId != null && model.InstructorId != 0)
                studyGroup.InstructorId = model.InstructorId;
            if (!string.IsNullOrEmpty(model.InstructorName))
                studyGroup.InstructorName = model.InstructorName;
            if (model.JobProfileIntId != null)
                studyGroup.JobProfileIntId = model.JobProfileIntId;
            if (!string.IsNullOrEmpty(model.LocationAddress))
                studyGroup.LocationAddress = model.LocationAddress;
            if (!string.IsNullOrEmpty(model.LocationGoogleMap))
                studyGroup.LocationGoogleMap = model.LocationGoogleMap;
            if (!string.IsNullOrEmpty(model.MeetingLink))
                studyGroup.MeetingLink = model.MeetingLink;
            if (!string.IsNullOrEmpty(model.MeetingLinkId))
                studyGroup.MeetingLinkId = model.MeetingLinkId;
            if (!string.IsNullOrEmpty(model.MeetingLinkPasscode))
                studyGroup.MeetingLinkPasscode = model.MeetingLinkPasscode;
            if (model.NumberOfStudents != null)
                studyGroup.NumberOfStudents = model.NumberOfStudents;
            if (!string.IsNullOrEmpty(model.StudyGroupType))
                studyGroup.StudyGroupType = model.StudyGroupType;
            if (!string.IsNullOrEmpty(model.TrackCode))
                studyGroup.TrackCode = model.TrackCode;
            if (model.TrackIntId != null)
                studyGroup.TrackIntId = model.TrackIntId;
            if (!string.IsNullOrEmpty(model.TraineeType))
                studyGroup.TraineeType = model.TraineeType;
            if (!string.IsNullOrEmpty(model.WeekDayEndFlag))
                studyGroup.WeekDayEndFlag = model.WeekDayEndFlag;
            if (model.WelcomeMessage != null)
                studyGroup.WelcomeMessage = model.WelcomeMessage;
            if (!string.IsNullOrEmpty(model.YearSemester))
                studyGroup.YearSemester = model.YearSemester;
            if (model.Status != null)
                studyGroup.Status = model.Status;
            if (!string.IsNullOrEmpty(model.StatusComment))
                studyGroup.StatusComment = model.StatusComment;
            
            if (model.StatStatusCommentCatID != null)
                studyGroup.StatStatusCommentCatIDusComment = model.StatStatusCommentCatID;

            if (model.studyGroupDaysDTO != null)
            {
                var studyGroupDays = new StudyGroupDay
                {
                    RoundCode = model.RoundCode,
                    OnlineDay1 = model.studyGroupDaysDTO.OnlineDay1,
                    OnlineDay2 = model.studyGroupDaysDTO.OnlineDay2,
                    OnlineDay3 = model.studyGroupDaysDTO.OnlineDay3,
                    PhysicalDay = model.studyGroupDaysDTO.PhysicalDay,
                    EnglishDay = model.studyGroupDaysDTO.EnglishDay,
                    SoftskillDay = model.studyGroupDaysDTO.SoftskillDay,
                    CoachingDay = model.studyGroupDaysDTO.CoachingDay,
                    OnlineTimeInterval = model.studyGroupDaysDTO.OnlineTimeInterval,
                    PhysicalTimeInterval = model.studyGroupDaysDTO.PhysicalTimeInterval,
                    EnglishTimeInterval = model.studyGroupDaysDTO.EnglishTimeInterval,
                    CoachingTimeInterval = model.studyGroupDaysDTO.CoachingTimeInterval,
                    SoftskillTimeInterval = model.studyGroupDaysDTO.SoftskillTimeInterval,
                    SrlNo = 1,
                    ActiveFrom = DateTime.Now
                };

                studyGroup.StudyGroupDays.Add(studyGroupDays);
            }

            _studyGroupRepository.Add(studyGroup);

            //if (studyGroup.GroupIntId != null && studyGroup.GroupIntId != 0)
            //{
                if (model.Provider.HasValue)
                {
                    var provider = await _trainingProviderRepository.getProviderByID((int)model.Provider);
                    if (provider == null)
                    {
                        result.Errors.Add(new Error { Message = "Add Group Error : Provider with ID: " + model.Provider + " Not found" });
                        return result;
                    }
                    providerStudyGroup = new ProviderStudyGroup
                    {
                        ProviderId = model.Provider,
                        ProviderName = provider.NameEn,
                        RoundCode = model.RoundCode,
                        Remarks = "Link Created : " + DateTime.Now,
                        YearSemester = DateTime.Now.Year,
                        //StudyGroupIntId = studyGroup.GroupIntId,
                        //StudyGroup = studyGroup
                    };
                    studyGroup.TrainingProvider = providerStudyGroup;
                    //studyGroup.TrainingProvider = providerStudyGroup;
                    //if (providerStudyGroup != null)
                    //_providerStudyGroupRepository.Add(providerStudyGroup);
                    //_providerStudyGroupRepository.Save(providerStudyGroup);
                }
            //}
            await _studyGroupRepository.Save();

            result = await GetGroupDetails(studyGroup.GroupIntId);
            //}
            //else
            //{
            //    result.Errors.Add(new Error { Message = "Error saving Group" });
            //    return result;
            //}

            return result;
        }

        public async Task<CommonResponse<GroupCriteriaModel>> GetGroupCriteria()
        {
            var result = new CommonResponse<GroupCriteriaModel>();

            var tracksList = await _tracksRepository.getAllTracks();
            var providersList = await _trainingProviderRepository.getAllProviders();
            var instructorsList = await _instructorsRepository.getAllInstructors();
            var jobProfilesList = await _jobProfileRepository.getAllJobProfiles();

            GroupCriteriaModel model = new GroupCriteriaModel();

            foreach (var track in tracksList)
            {
                model.Tracks.Add(new CommonDTO { Id = track.TrackIntId.ToString(), Name = track.TrackNameEn, NameAr = track.TrackNameAr });
            }

            foreach (var provider in providersList)
            {
                model.Providers.Add(new CommonDTO { Id = provider.ProviderIntId.ToString(), Name = provider.NameEn, NameAr = provider.NameAr });
            }

            foreach (var inst in instructorsList)
            {
                model.Instructors.Add(new CommonDTO { Id = inst.InstructorIntId.ToString(), Name = inst.NameEn, NameAr = inst.NameAr });
            }


            result.Data = model;

            return result;
        }

        #endregion

        #region ONE TIME
        public async Task<int> EditGroupAttendance()
        {

            var allGroups = await _studyGroupRepository.getAllGroupsWTrainee();
            
            foreach (var group in allGroups)
            {
                DateTime startT = new DateTime(2024, 11, 3);
                DateTime endT = new DateTime(2024, 11, 16);
                var allAttendance = await _sessionRepo.getSessionsByGroupNoTrack(group.GroupIntId, startT, endT);
                if (allAttendance != null && allAttendance.FirstOrDefault() != null) {
                    foreach (var session in allAttendance)
                    {
                        if(session.AuditingSessionAttendances != null)
                        {
                            if (session.AuditingSessionAttendances.Count < group.Trainees.Count())
                            {
                                AddMissing(group.Trainees, session);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Missing records: " + countRecords);
            await _sessionRepo.Save();
            return countRecords;
        }

        private void AddMissing (List<Trainee> trainees, AuditingSession session)
        {
            if (trainees != null && trainees.Count > 0)
            {
                var missingTrainees = trainees.Where(c => !session.AuditingSessionAttendances.Select(x => x.StudentId).Contains(c.TraineeIntId)).ToList();
                foreach (var trainee in missingTrainees)
                {
                    var attendence = new AuditingSessionAttendance
                    {
                        presense = false,
                        StudentId = trainee.TraineeIntId,
                        StudentName = !trainee.NameEn.IsNullOrEmpty() ? trainee.NameEn : trainee.NameAr,
                        SessionId = session.SessionId,
                        SendDate = session.SessionDateTimeStart,
                        auditingSession = session
                    };
                    session.AuditingSessionAttendances.Add(attendence);
                }
                countRecords += missingTrainees.Count();
            }
        }

        public async Task<int> AddAssignmentsForSessions()
        {
            DateTime start = new DateTime(2024, 10, 25);
            DateTime end = new DateTime(2024, 11, 02);
            var allsessions = await _sessionRepo.getSessionsByDate(start, end);
            countRecords = 0;

            //FindDuplicates(allsessions);
            foreach (var session in allsessions)
            {
                if (session.AssignmentSessionID == null)
                {
                    if (session.CourseName != "u")
                    {
                        var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(int.Parse(session.StudyGroupId));
                        var assignment = new AuditorRoundCodeAssignment
                        {
                            AuditingSessionID = session.SessionId,
                            AuditorId = session.AuditorId,
                            Conducted = (int)RoundCodeStates.Done,
                            Date = (DateTime)(session.SessionDateTimeStart != null ? session.SessionDateTimeStart : new DateTime()),
                            GroupIntID = int.Parse(session.StudyGroupId),
                            SessionType = session.SessionType,
                            StudyGroupRoundCode = studyGroup.RoundCode
                        };
                        session.AuditorRoundCodeAssignment = assignment;
                        countRecords++;
                    }
                }
            }
            Console.WriteLine("Missing assignments: " + countRecords);
            await _sessionRepo.Save();
            return countRecords;
        }

        public List<int> FindDuplicates(List<AuditingSession> auditingSessions)
        {
            //var result =  auditingSessions.GroupBy(i => new { i.SessionDateTimeStart, i.StudyGroupId, i.SessionType })
            //         .Where(x => x.Count() > 1)
            //         .Select(val => val.Key).ToList();
            var result = auditingSessions.Distinct().ToList();
            return null;
        }

        #endregion
    }
}
