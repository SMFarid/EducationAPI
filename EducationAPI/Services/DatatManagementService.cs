using Azure;
using EducationAPI.Common;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Enums;
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
            if (model.GroupIntID!= null)
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

            await _studentRepository.Save();

            response = await GetStudent(model.StudentID);
            
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

            response.Data = studentModel;

            return response;
        }

        public async Task<CommonResponse<List<StudentDTO>>> getStudentsList()
        {
            var result = new CommonResponse<List<StudentDTO>>();

            var studentsList = await _studentRepository.getAllStudent();
            List <StudentDTO> returnList = new List<StudentDTO>();
            foreach ( var student in studentsList )
            {
                var studentDTO = new StudentDTO{
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

        public async Task<CommonResponse<ViewStudyGroupDTO>> GetGroupDetails (int groupIntID)
        {
            var result = new CommonResponse<ViewStudyGroupDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(groupIntID);

            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "Group with ID: " + groupIntID + " not found!" });
                return result;
            }

            var provider = await _providerStudyGroupRepository.getProviderbyStudyGroup(studyGroup.GroupIntId);
            if (provider == null)
            {
                result.Errors.Add(new Common.Error { Message = "Error: Unable to find provider for this group" });
                //return response;
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
                Provider = provider.ProviderName,
                TraineeType = studyGroup.TraineeType,
                WeekDayEndFlag = studyGroup.WeekDayEndFlag,
                WelcomeMessage = studyGroup.WelcomeMessage,
                YearSemester = studyGroup.YearSemester
            };

            result.Data = studyGroupDTO;

            return result;
        }

        public async Task<CommonResponse<ViewStudyGroupDTO>> EditGroup(EditStudyGroupDTO model)
        {
            var result = new CommonResponse<ViewStudyGroupDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(model.GroupIntId);

            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "Group with ID: " + model.GroupIntId + " not found!" });
                return result;
            }



            if (string.IsNullOrEmpty(model.RoundCode))
                studyGroup.RoundCode = model.RoundCode;

            if (model.Capacity != null)
                studyGroup.Capacity = model.Capacity;
            if (string.IsNullOrEmpty(model.ExpectedEndTime))
                studyGroup.ExpectedEndTime = model.ExpectedEndTime;
            if (string.IsNullOrEmpty(model.Governorate))
                studyGroup.Governorate = model.Governorate;
            if (string.IsNullOrEmpty(model.GroupStartTime))
                studyGroup.GroupStartTime = model.GroupStartTime;
            if (model.InstructorId != null && model.InstructorId != 0)
                studyGroup.InstructorId = model.InstructorId;
            if (string.IsNullOrEmpty(model.InstructorName))
                studyGroup.InstructorName = model.InstructorName;
            if (model.JobProfileIntId != null)
                studyGroup.JobProfileIntId = model.JobProfileIntId;
            if (string.IsNullOrEmpty(model.LocationAddress))
                studyGroup.LocationAddress = model.LocationAddress;
            if (string.IsNullOrEmpty(model.LocationGoogleMap))
                studyGroup.LocationGoogleMap = model.LocationGoogleMap;
            if (string.IsNullOrEmpty(model.MeetingLink))
                studyGroup.MeetingLink = model.MeetingLink;
            if (string.IsNullOrEmpty(model.MeetingLinkId))
                studyGroup.MeetingLinkId = model.MeetingLinkId;
            if (string.IsNullOrEmpty(model.MeetingLinkPasscode))
                studyGroup.MeetingLinkPasscode = model.MeetingLinkPasscode;
            if (model.NumberOfStudents != null)
                studyGroup.NumberOfStudents = model.NumberOfStudents;
            if (string.IsNullOrEmpty(model.StudyGroupType))
                studyGroup.StudyGroupType = model.StudyGroupType;
            if (string.IsNullOrEmpty(model.TrackCode))
                studyGroup.TrackCode = model.TrackCode;
            if (model.TrackIntId != null)
                studyGroup.TrackIntId = model.TrackIntId;
            if (string.IsNullOrEmpty(model.TraineeType))
                studyGroup.TraineeType = model.TraineeType;
            if (string.IsNullOrEmpty(model.WeekDayEndFlag))
                studyGroup.WeekDayEndFlag = model.WeekDayEndFlag;
            if (model.WelcomeMessage != null)
                studyGroup.WelcomeMessage = model.WelcomeMessage;
            if (string.IsNullOrEmpty(model.YearSemester))
                studyGroup.YearSemester = model.YearSemester;

            _studyGroupRepository.Save();

            result = await GetGroupDetails(model.GroupIntId);

            return result;
        }

        public async Task<CommonResponse<GroupCriteriaModel>> GetGroupCriteria()
        {
            var result = new CommonResponse<GroupCriteriaModel>();

            var tracksList = await _tracksRepository.getAllTracks();
            var providersList = await _trainingProviderRepository.getAllProviders();

            GroupCriteriaModel model = new GroupCriteriaModel();

            foreach (var track in tracksList)
            {
                model.Tracks.Add(new CommonDTO { Id = track.TrackIntId.ToString(), Name = track.TrackNameEn, NameAr = track.TrackNameAr });
            }

            foreach (var provider in providersList)
            {
                model.Providers.Add(new CommonDTO { Id = provider.ProviderIntId.ToString(), Name = provider.NameEn, NameAr = provider.NameAr });
            }

            result.Data = model;

            return result;
        }

        #endregion
    }
}
