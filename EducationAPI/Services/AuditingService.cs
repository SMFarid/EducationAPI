using EducationAPI.Common;
using EducationAPI.Const;
using EducationAPI.Context;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Enums;
using EducationAPI.Models;
using EducationAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = EducationAPI.Common.Error;

namespace EducationAPI.Services
{
    public class AuditingtService
    {

        AuditorRoundCodeAssignmentRepository _assignmentRepository = new AuditorRoundCodeAssignmentRepository();
        StudyGroupRepository _studyGroupRepository = new StudyGroupRepository();
        //AuditorRepository _auditorRepository = new AuditorRepository();
        AuthRepository _authRepository = new AuthRepository();
        AuditingSessionRepository _auditingSessionRepository = new AuditingSessionRepository();
        ProviderStudyGroupRepository _providerStudyGroupRepository = new ProviderStudyGroupRepository();
        StudentRepository _studentRepository = new StudentRepository();

        #region Auditing Session
        public async Task<CommonResponse<AuditingSessionCriteraDTO>> getAuditingCritera(string roundCode, int Audtor_ID)
        {
            var response = new CommonResponse<AuditingSessionCriteraDTO>();
            var criteria = new AuditingSessionCriteraDTO();

            try
            {
                var studyGroup = await _studyGroupRepository.getStudyGroupByCode(roundCode);
                if (studyGroup == null)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Round Code not found" });
                    return response;
                }

                var roundcodeAssignment = await _assignmentRepository.getAssignmentByRoundCode(roundCode);
                if (roundcodeAssignment == null)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Round Code not found" });
                    return response;
                }

                var auditingSession = await _auditingSessionRepository.getSessionByAssignmentID(roundcodeAssignment.AssignmentSessionID);
                if (auditingSession == null)
                {
                    auditingSession = new AuditingSession
                    {
                        AssignmentSessionID = roundcodeAssignment.AssignmentSessionID,
                        SessionDateTimeStart = DateTime.Now,
                        StudyGroupId = studyGroup.GroupIntId.ToString(),
                        AuditorId = Audtor_ID
                    };
                    await _auditingSessionRepository.Add(auditingSession);
                }
                else
                {
                    auditingSession.SessionDateTimeStart = DateTime.Now;
                }


                criteria.SessionStartTime = DateTime.Now;
                criteria.Auditing_Session_ID = auditingSession.SessionId;

                if (roundcodeAssignment.AuditorId != 0 && roundcodeAssignment.AuditorId != null && roundcodeAssignment.AuditorId != Audtor_ID)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Round Code is being reviewed by another auditor" });
                    return response;
                }
                var provider = await _providerStudyGroupRepository.getProviderbyStudyGroup(studyGroup.GroupIntId);
                if (provider == null)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Unable to find provider for this group" });
                    //return response;
                }

                roundcodeAssignment.AuditorId = Audtor_ID;
                roundcodeAssignment.Conducted = (int)RoundCodeStates.InProgress;

                var students = studyGroup.Trainees != null ? studyGroup.Trainees.ToList() : new List<Trainee>();

                criteria.Students = students.Select(c => new StudentDTO { Id = c.TraineeIntId, NameAr = c.NameAr, NameEN = c.NameEn, Email = c.Email }).OrderBy(c => c.NameAr).ToList();

                criteria.CourseName = "";
                //criteria.TrainingCenterName = studyGroup.trainingProvider.NameEn;
                if (studyGroup.Instructor != null)
                {
                    criteria.Instructors.Add(
                        new InstructorDTO() { Id = studyGroup.Instructor.InstructorIntId, NameEN = studyGroup.Instructor.NameEn });
                }

                //criteria.SessionType = roundcodeAssignment.SessionType;
                criteria.NumberRegistered = studyGroup.NumberOfStudents != null ? (int)studyGroup.NumberOfStudents : 0;
                criteria.Study_Group_ID = studyGroup.GroupIntId;
                criteria.Auditing_Session_ID = roundcodeAssignment.AssignmentSessionID;
                criteria.MeetingLink = studyGroup.MeetingLink;
                criteria.Track = studyGroup.TrackCode;
                criteria.TrainingProvider = provider != null ? provider.ProviderName : "";
                if (!roundcodeAssignment.SessionType.IsNullOrEmpty())
                {
                    switch (roundcodeAssignment.SessionType)
                    {
                        case SessionTypes.Online:
                            criteria.SessionType = nameof(SessionTypes.Online);
                            break;
                        case SessionTypes.Physical:
                            criteria.SessionType = nameof(SessionTypes.Physical);
                            break;
                        case SessionTypes.SoftSkill:
                            criteria.SessionType = nameof(SessionTypes.SoftSkill);
                            break;
                        case SessionTypes.Coaching:
                            criteria.SessionType = nameof(SessionTypes.Coaching);
                            break;
                        case SessionTypes.English:
                            criteria.SessionType = nameof(SessionTypes.English);
                            break;
                        default:
                            criteria.SessionType = "Online.";
                            break;
                    }
                }

                var startTime = roundcodeAssignment.Date;

                if (startTime != null)
                {
                    criteria.StartTime = startTime;
                    criteria.EndTime = startTime.AddHours(3);
                }



                await _auditingSessionRepository.SaveSession(auditingSession);
                _assignmentRepository.Save();
                criteria.SessionID = auditingSession.SessionId;

                response.Data = criteria;
                //var center = await _context.TrainingCenters.Where(c => c.Id == center_ID).FirstOrDefaultAsync();
                //var providersCenters = await _context.ProviderCenters.Where(c => c.CenterId == center_ID).Select(c => c.ProviderId).ToListAsync();
                //var providers = await _context.TrainingProviders.Where(c => providersCenters.Contains(c.Id)).Select(c => c.NameEn).ToListAsync();
                //AuditingSessionCriteraDTO

                //response.Data = new AuditingSessionCriteraDTO
                //{
                //    TrainingCenterName = center.NameEn,
                //    TrainingProviders = new List<string>(providers),
                //    AvailableTimes = new List<string> { "6:00pm", "7:00pm", "8:30pm" },
                //    courses = new List<string> { "AI & Data Science", "Data Analytics" },
                //    instructors = new List<string> { "Dr.Ahmed", "Dr.Rasha", "Eng.Khaled" }
                //};
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error { Message = ex.Message });
            }


            return response;
        }

        public async Task<CommonResponse<List<AuditorGroupsDTO>>> getAuditorGroups(int AuditorID, DateTime date)
        {
            var response = new CommonResponse<List<AuditorGroupsDTO>>();
            List<AuditorGroupsDTO> auditorGroups = new List<AuditorGroupsDTO>();

            var roundcodeList = await _assignmentRepository.getAuditorAssignment(AuditorID, date); //edit to use only date and state
            //List<int> groupIDsList = roundcodeList.Select(e => e.GroupIntID).ToList();
            //var studyGroups = await _studyGroupRepository.getListOfGroups(groupIDsList);
            foreach (var item in roundcodeList)
            {
                var provider = await _providerStudyGroupRepository.getProviderbyStudyGroup((int)item.GroupIntID);
                auditorGroups.Add(new AuditorGroupsDTO
                {
                    Doneflag = (int)(item.Conducted != null ? item.Conducted : (int)RoundCodeStates.Open),
                    RoundCode = item.StudyGroupRoundCode,
                    SessionDateTime = item.Date.ToShortTimeString(),
                    GroupIntID = item.GroupIntID!= null? (int)item.GroupIntID: 0,
                    SessionType = item.SessionType!=null ? item.SessionType :"",
                    TrainingProvider = provider.ProviderName
                });

            }
            response.Data = auditorGroups.OrderBy(e=>e.SessionDateTime).ToList();
            return response;
        }

        /*
        public async Task<CommonResponse<List<AuditorDTO>>> getAuditorList(int user_id)
        {
            var response = new CommonResponse<List<AuditorDTO>>();
            List<AuditorDTO> auditors = new List<AuditorDTO>();

            var user = await _auditorRepository.GetAuditorById(user_id);
            if (user == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
                return response;
            }

            auditors.Add(
                new AuditorDTO
                {
                    AuditorID = 0,
                    NameAr = "No Selection",
                    NameEn = "No Selection"
                });

            auditors.Add(
                new AuditorDTO
                {
                    AuditorID = user.Id,
                    NameAr = user.NameAr != null ? user.NameAr : "",
                    NameEn = user.NameEn != null ? user.NameEn : ""
                });

            if (user.Role == (int)UserRoles.Admin || user.Role == (int)UserRoles.TeamLeader)
            {
                var auditorsList = await _auditorRepository.getAllAuditors();
                foreach (var item in auditorsList)
                {
                    if (item.Id != user_id)
                    {
                        auditors.Add(new AuditorDTO
                        {
                            AuditorID = item.Id,
                            NameAr = item.NameAr,
                            NameEn = item.NameEn
                        });
                    }
                }
            }

            response.Data = auditors;
            return response;
        }

        */

        public async Task<CommonResponse<List<AuditorDTO>>> getAuditorList(int user_id) //with auth table
        {
            var response = new CommonResponse<List<AuditorDTO>>();
            List<AuditorDTO> auditors = new List<AuditorDTO>();

            var user = await _authRepository.GetAuditorById(user_id);
            if (user == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
                return response;
            }

          
                var auditorsList = await _authRepository.getAllAuditors();
                foreach (var item in auditorsList)
                {
                    if (item.Id != user_id)
                    {
                        auditors.Add(new AuditorDTO
                        {
                            AuditorID = item.Id,
                            NameAr = item.Username,
                            NameEn = item.Username
                        });
                    }
                }
            

            response.Data = auditors;
            return response;
        }

        public async Task<CommonResponse<string>> assignAuditors(List<AuditorAttendanceDTO> auditorList)
        {
            var response = new CommonResponse<string>();

            var dailyAssignments = await _assignmentRepository.getAssignmentsByDate(DateTime.Now);
            
            //Validate
            if (dailyAssignments == null) {
                response.Errors.Add(new Error { Message = "Error: Daily Assignments not found" });
                return response;
            }
            var attendedList = auditorList.Where(e => e.attended == true).ToList();
            var assignmentsDivision = dailyAssignments.Count() / attendedList.Count();
            int startRange = 0;
            List<AuditorRoundCodeAssignment> records = new List<AuditorRoundCodeAssignment>();
            foreach (var auditor in auditorList)
            {

                if (startRange + assignmentsDivision < dailyAssignments.Count())
                {
                    records = dailyAssignments.Take(new Range(startRange, startRange + assignmentsDivision)).ToList();
                }
                else
                {
                    records = dailyAssignments.Take(new Range(startRange, dailyAssignments.Count())).ToList();
                }
                foreach (var item in records)
                {
                    item.AuditorId = auditor.AuditorID;
                }
                startRange += assignmentsDivision;

            }

            _assignmentRepository.Save();

            response.Data = "Success";
           
            return response;
        }

        public async Task<CommonResponse<string>> SaveAuditSession(AuditSessionSaveModel model)
        {
            var response = new CommonResponse<string>();


            //Retrieve and Validate
            var auditingSession = await _auditingSessionRepository.getSessionBySessionID(model.Auditing_Session_ID);
            if (auditingSession == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditing Session not found!" });
                return response;
            }
            var auditor = await _authRepository.GetAuditorById(model.AuditorId);
            if (auditor == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
                return response;
            }
            var studyGroup = await _studyGroupRepository.getStudyGroupByCode(model.RoundCode);
            if (studyGroup == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }
            //get study group session
            var assignedSession = await _assignmentRepository.getAssignmentByID((int)auditingSession.AssignmentSessionID);
            assignedSession.Conducted = (int)RoundCodeStates.Done;




            auditingSession.AttendanceType = assignedSession.SessionType; //change later
                                                                          //Auditor = auditor,
            auditingSession.AuditorId = model.AuditorId;
            auditingSession.AuditorName = !auditor.Username.IsNullOrEmpty() ? auditor.Username : "";

            //Course = studyGroup.CourseId, //retrieve name later
            //Instructor = studyGroup.Instructor,
            auditingSession.InstructorId = (int)studyGroup.InstructorId != null ? (int)studyGroup.InstructorId : 0;
            auditingSession.InstructorName = studyGroup.InstructorName;
            auditingSession.Conducted = model.Conducted;
            auditingSession.MaterialDelivered = model.MaterialDelivered;
            auditingSession.CurrentChapter = model.Current_Chapter;
            //SessionDateTimeStart = (DateTime)model.ReportStart,
            auditingSession.SessionDateTimeClose = DateTime.Now;
            auditingSession.DepiLogoAdded = model.Depi_Logo_Flag;
            auditingSession.LabFlag = model.Lab_Flag;
            auditingSession.TestFlag = model.Test_Flag;
            auditingSession.HardwareProficiency = model.HardwareProficiency;
            auditingSession.UnderstoodExamples = model.UnderstoodExamples;
            auditingSession.UnderstoonExplaination = model.UnderstoonExplaination;
            auditingSession.TimeForQuestions = model.TimeForQuestions;
            auditingSession.InstructorEncouragement = model.InstructorEncouragement;
            auditingSession.MaterialIsClear = model.MaterialIsClear;
            auditingSession.ACCondition = model.ACCondition;
            auditingSession.CenterEnvironment = model.CenterEnvironment;
            auditingSession.InitiativeClear = model.InitiativeClear;
            auditingSession.PrevLinks = model.PrevLinks;
            auditingSession.ConnectionQuality = model.ConnectionQuality.ToString();
            auditingSession.VoiceQuality = model.VoiceQuality.ToString();
            auditingSession.VideoQuality = model.VideoQuality.ToString();
            auditingSession.StudyGroupId = studyGroup.GroupIntId.ToString();
            auditingSession.SessionType = assignedSession.SessionType;
            auditingSession.Remarks = model.Remarks;
            auditingSession.CommentCategory = model.CommentCategory;


            var studentAttendance = new List<AuditingSessionAttendance>();
            var students = await _studentRepository.getListOfStudents(model.StudentsAttendedList);

            //var old_attendance = await _aud

            foreach (var item in students)
            {
                var student = new AuditingSessionAttendance
                {
                    auditingSession = auditingSession,
                    StudentId = item.TraineeIntId,
                    StudentName = !string.IsNullOrEmpty(item.NameEn) ? item.NameEn : item.NameAr,
                    SessionId = auditingSession.SessionId,
                    SendDate = DateTime.Now
                };
                studentAttendance.Add(student);
            }
            auditingSession.AuditingSessionAttendances = studentAttendance;


            _assignmentRepository.Save();
             response = await _auditingSessionRepository.Save();
            if (!response.IsSuccess)
            {
                response.Errors.Add(new Error { Message = "Error while saving" });
                return response;
            }
            response.Data = "Success";

            //
            return response;
        }

        public async Task<CommonResponse<string>> HoldAuditSession(AuditSessionSaveModel model)
        {
            var response = new CommonResponse<string>();


            ////Retrieve and Validate
            //var auditor = await _auditorRepository.GetAuditorById(model.AuditorId);
            //if (auditor == null)
            //{
            //    response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
            //    return response;
            //}
            //var studyGroup = await _studyGroupRepository.getStudyGroupByID(model.RoundCode);
            //if (studyGroup == null)
            //{
            //    response.Errors.Add(new Error { Message = "Error: Round Code not found" });
            //    return response;
            //}
            //get study group session
            var assignedSession = await _assignmentRepository.getAssignmentByID(model.Auditing_Session_ID);
            assignedSession.Conducted = (int)RoundCodeStates.Abandoned;

            //AuditingSession auditingSession = new AuditingSession
            //{
            //    AttendanceType = "Online", //change later
            //    //Auditor = auditor,
            //    AuditorId = model.AuditorId,
            //    AuditorName = !auditor.NameEn.IsNullOrEmpty() ? auditor.NameEn : auditor.NameAr,

            //    //Course = studyGroup.CourseId, //retrieve name later
            //    //Instructor = studyGroup.Instructor,
            //    InstructorId = (int)studyGroup.InstructorId != null ? (int)studyGroup.InstructorId : 0,
            //    InstructorName = studyGroup.InstructorName,
            //    Conducted = model.Conducted,
            //    MaterialDelivered = model.MaterialDelivered,
            //    CurrentChapter = model.Current_Chapter,
            //    //SessionDateTimeStart = (DateTime)model.ReportStart,
            //    //SessionDateTimeClose = model.ReportEnd,
            //    DepiLogoAdded = model.Depi_Logo_Flag,
            //    LabFlag = model.Lab_Flag,
            //    TestFlag = model.Test_Flag,

            //    ConnectionQuality = model.ConnectionQuality.ToString(),
            //    VoiceQuality = model.VoiceQuality.ToString(),
            //    VideoQuality = model.VideoQuality.ToString(),
            //    StudyGroupId = studyGroup.GroupIntId.ToString(),

            //};

            //var studentAttendance = new List<AuditingSessionAttendance>();

            //foreach (var item in model.StudentsAttendedList)
            //{
            //    var student = new AuditingSessionAttendance
            //    {
            //        auditingSession = auditingSession,
            //        StudentId = item.Id,
            //        StudentName = !string.IsNullOrEmpty(item.NameEN) ? item.NameEN : item.NameAr
            //    };
            //    studentAttendance.Add(student);
            //}
            //auditingSession.AuditingSessionAttendances = studentAttendance;


            
            response.Data = _assignmentRepository.Save(); //await _auditingSessionRepository.SaveSession(auditingSession);

            //
            return response;
        }

        #endregion

        //public async Task<CommonResponse<List<string>>> getAuditSessionCriteria(int AuditorID, string RoundCode)
        //{
        //    var response = new CommonResponse<List<String>>();





        //    return response;
        //}

        #region Editing Assignments

        public async Task<CommonResponse<List<RoundCodeAssignmentDTO>>> getRoundCodes()
        {
            var response = new CommonResponse<List<RoundCodeAssignmentDTO>>();
            List<RoundCodeAssignmentDTO> StudyGroups = new List<RoundCodeAssignmentDTO>();

            var assignedCodes = await _assignmentRepository.getAssignmentsByDate(DateTime.Now);
            
            if (assignedCodes != null)
            {
                foreach (var code in assignedCodes)
                {
                    StudyGroups.Add(new RoundCodeAssignmentDTO
                    {
                        RoundCode = code.StudyGroupRoundCode,
                        AssignmentID = code.AssignmentSessionID,
                        Status = 1,
                        AssignmentDate = code.Date,
                        AuditorID = code.AuditorId,
                        Conducted = code.Conducted,
                        StatusName = Enum.GetName(typeof(RoundCodeStates), code.Conducted),
                        GroupIntID = code.GroupIntID,
                        SessionType = code.SessionType != null ? code.SessionType.ToString() :""
                    }) ;
                }
            }

            var roundCodesList = await _studyGroupRepository.getAllGroups();
            if (roundCodesList == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Codes not found" });
                return response;
            }
            try
            {
                foreach (var code in roundCodesList)
                {
                    if (!StudyGroups.Select(c => c.RoundCode).ToList().Contains(code.RoundCode))
                    {
                        var provider = await _providerStudyGroupRepository.getProviderbyStudyGroup(code.GroupIntId);
                        StudyGroups.Add(new RoundCodeAssignmentDTO
                        {
                            RoundCode = code.RoundCode,
                            AssignmentID = 0,
                            Status = 0,
                            GroupIntID = code.GroupIntId,
                            SessionType = "",
                            TrainingProvider = provider.ProviderName
                        });
                    }
                }
            } catch (Exception ex)
            {
                response.Errors.Add(new Error { Message = "Error: " + ex.Message});
                return response;
            }

            response.Data = StudyGroups;
            return response;
        }



        public async Task<CommonResponse<string>> EditAuditAssignment(EditAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            var roundCodeAssign = await _assignmentRepository.getAssignmentByID(model.AssignmentID);

            if (roundCodeAssign == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }

            StudyGroup group = new StudyGroup();
            try
            {
                group = await _studyGroupRepository.getStudyGroupByCode(model.RoundCode);
                if (group == null)
                {
                    response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error { Message = "Error: " + ex.Message });
                return response;
            }
            roundCodeAssign.AuditorId = model.AuditorID;
            roundCodeAssign.Conducted = (int)model.Conducted;
            roundCodeAssign.Date = (DateTime)model.AssignmentDate;
            roundCodeAssign.GroupIntID = group.GroupIntId;

            response.Data = _assignmentRepository.Save();


            return response;
        }

        public async Task<CommonResponse<string>> DeleteAuditAssignment(EditAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            var roundCodeAssign = await _assignmentRepository.getAssignmentByRoundCode(model.RoundCode);

            if (roundCodeAssign == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }

            response.Data = _assignmentRepository.DeleteAssignmentByRoundCode(roundCodeAssign);


            return response;
        }

        public async Task<CommonResponse<string>> AddAuditAssignment(AddAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            var group = await _studyGroupRepository.getStudyGroupByCode(model.RoundCode);
            if ( group == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }
            AuditorRoundCodeAssignment auditorRoundCode = new AuditorRoundCodeAssignment
            {
                AuditorId = model.AuditorID,
                Conducted = 0,
                Date = model.AssignmentDate,
                StudyGroupRoundCode = model.RoundCode,
                GroupIntID = group.GroupIntId
            };

            response.Data = _assignmentRepository.Add(auditorRoundCode);


            return response;
        }

        #endregion

    }
}
