using Azure;
using EducationAPI.Common;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Enums;
using EducationAPI.Models;
using EducationAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace EducationAPI.Services
{
    public class ManageAuditsService
    {
        StudentRepository _studentRepository = new StudentRepository();
        StudyGroupRepository _studyGroupRepository = new StudyGroupRepository();
        ProviderStudyGroupRepository _providerStudyGroupRepository = new ProviderStudyGroupRepository();
        TrackRepository _tracksRepository = new TrackRepository();
        TrainingProviderRepository _trainingProviderRepository = new TrainingProviderRepository();
        AuditingSessionRepository _auditingSessionRepository = new AuditingSessionRepository();
        AuditingSessionAttendanceRepository _auditingSessionAttendanceRepository = new AuditingSessionAttendanceRepository();
        AuthRepository _authRepository = new AuthRepository();


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

        public async Task<CommonResponse<IEnumerable<AuditSessionViewDTO>>> GetRoundCodeAudits(int groupIntID)
        {
            var result = new CommonResponse<IEnumerable<AuditSessionViewDTO>>();

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

            var groupAudits = await _auditingSessionRepository.getSessionsByGroup(groupIntID);
            if (groupAudits == null || groupAudits.Count() == 0 )
            {
                result.Errors.Add(new Common.Error { Message = "Error: Unable to find sessions for this group" });
                return result;
            }

            //if (groupAudits[0] == null)
            //{
            //    result.Errors.Add(new Common.Error { Message = "Error: Unable to find sessions for this group" });
            //    return result;
            //}

            //map from DB to DTO
            Auth auditor = new Auth();
            var sessionsList = new  List<AuditSessionViewDTO>();
            foreach (var session in groupAudits)
            {
                if (session != null)
                {
                    auditor = await _authRepository.GetAuditorById(session.AuditorId);
                    var auditSession = new AuditSessionViewDTO
                    {
                        RoundCode = studyGroup.RoundCode,
                        ACCondition = session.ACCondition,
                        Assignment_Session_ID = (int)session.AssignmentSessionID,
                        Auditing_Session_ID = session.SessionId,
                        AuditorId = session.AuditorId,
                        AuditorName = auditor != null ? auditor.Username : "",
                        CenterEnvironment = session.CenterEnvironment,
                        CommentCategory = session.CommentCategory,
                        Conducted = (bool)session.Conducted,
                        ConnectionQuality = session.ConnectionQuality,
                        Current_Chapter = session.CurrentChapter,
                        Depi_Logo_Flag = (bool)session.DepiLogoAdded,
                        HardwareProficiency = session.HardwareProficiency,
                        InitiativeClear = (bool)session.InitiativeClear,
                        InstructorEncouragement = session.InstructorEncouragement,
                        InstructorName = session.InstructorName,
                        Instructor_ID = session.InstructorId,
                        Lab_Flag = (bool)session.LabFlag,
                        MaterialDelivered = (bool)session.MaterialDelivered,
                        MaterialIsClear = (bool)session.MaterialIsClear,
                        PrevLinks = (bool)session.PrevLinks,
                        Remarks = session.Remarks,
                        SessionType = session.SessionType,
                        Study_Group_ID = int.Parse(session.StudyGroupId),
                        Test_Flag = (bool)session.TestFlag,
                        TimeForQuestions = session.TimeForQuestions,
                        UnderstoodExamples = session.UnderstoodExamples,
                        UnderstoonExplaination = session.UnderstoonExplaination,
                        VideoQuality = session.VideoQuality,
                        VoiceQuality = session.VoiceQuality,
                        Date = (DateTime)session.SessionDateTimeStart,
                        AttendanceType = session.AttendanceType,
                        LastModified = session.LastModified
                    };
                    var attendanceList = session.AuditingSessionAttendances;

                    //auditSession.StudentsAttendedList.AddRange(session.AuditingSessionAttendances.
                    //    Select(c => new AttendanceViewDTO { NameAr = c.StudentName, NameEn = c.StudentName, Present = (bool)c.presense, StudentID = (int)c.StudentId}).ToList());
                    foreach (var item in attendanceList)
                    {
                        auditSession.StudentsAttendedList.Add(new AttendanceViewDTO
                        {
                            NameAr = item.StudentName,
                            NameEn = item.StudentName,
                            Present = item.presense,
                            StudentID = item.StudentId
                        });
                    }

                    sessionsList.Add(auditSession);
                }
            }

            ;


            result.Data = sessionsList.OrderByDescending(x => x.Date);

            return result;
        }

        public async Task<CommonResponse<AuditSessionViewDTO>> GetSingleAuditingSession(int sessionID)
        {
            var result = new CommonResponse<AuditSessionViewDTO>();



            var auditSession = await _auditingSessionRepository.getSessionBySessionID(sessionID);

            if (auditSession == null)
            {
                result.Errors.Add(new Common.Error { Message = "Error: Unable to find session id : " + sessionID });
                return result;
            }

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(int.Parse(auditSession.StudyGroupId));

            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "VIEW: Group with ID: " + auditSession.StudyGroupId + " not found!" });
                return result;
            }

            //map from DB to DTO
            Auth auditor = new Auth();


            auditor = await _authRepository.GetAuditorById(auditSession.AuditorId);
            var auditSessionView = new AuditSessionViewDTO
            {
                RoundCode = studyGroup.RoundCode,
                ACCondition = auditSession.ACCondition,
                Assignment_Session_ID = auditSession.AssignmentSessionID ?? 0,
                Auditing_Session_ID = auditSession.SessionId,
                AuditorId = auditSession.AuditorId,
                AuditorName = auditor != null ? auditor.Username : "",
                CenterEnvironment = auditSession.CenterEnvironment,
                CommentCategory = auditSession.CommentCategory,
                Conducted = (bool)auditSession.Conducted,
                ConnectionQuality = auditSession.ConnectionQuality,
                Current_Chapter = auditSession.CurrentChapter,
                Depi_Logo_Flag = (bool)auditSession.DepiLogoAdded,
                HardwareProficiency = auditSession.HardwareProficiency,
                InitiativeClear = (bool)auditSession.InitiativeClear,
                InstructorEncouragement = auditSession.InstructorEncouragement,
                InstructorName = auditSession.InstructorName,
                Instructor_ID = auditSession.InstructorId,
                Lab_Flag = (bool)auditSession.LabFlag,
                MaterialDelivered = (bool)auditSession.MaterialDelivered,
                MaterialIsClear = (bool)auditSession.MaterialIsClear,
                PrevLinks = (bool)auditSession.PrevLinks,
                Remarks = auditSession.Remarks,
                SessionType = auditSession.SessionType,
                Study_Group_ID = int.Parse(auditSession.StudyGroupId),
                Test_Flag = (bool)auditSession.TestFlag,
                TimeForQuestions = auditSession.TimeForQuestions,
                UnderstoodExamples = auditSession.UnderstoodExamples,
                UnderstoonExplaination = auditSession.UnderstoonExplaination,
                VideoQuality = auditSession.VideoQuality,
                VoiceQuality = auditSession.VoiceQuality,
                Date = (DateTime)auditSession.SessionDateTimeStart,
                AttendanceType = auditSession.AttendanceType,
                LastModified = auditSession.LastModified
            };
            var attendanceList = auditSession.AuditingSessionAttendances;

            //auditSession.StudentsAttendedList.AddRange(session.AuditingSessionAttendances.
            //    Select(c => new AttendanceViewDTO { NameAr = c.StudentName, NameEn = c.StudentName, Present = (bool)c.presense, StudentID = (int)c.StudentId}).ToList());
            foreach (var item in attendanceList)
            {
                auditSessionView.StudentsAttendedList.Add(new AttendanceViewDTO
                {
                    NameAr = item.StudentName,
                    NameEn = item.StudentName,
                    Present = item.presense,
                    StudentID = item.StudentId
                });
            }

            result.Data = auditSessionView;

            return result;
        }

        public async Task<CommonResponse<AuditSessionViewDTO>> EditAuditingReport(AuditSessionDTO model)
        {
            var result = new CommonResponse<AuditSessionViewDTO>();

            var studyGroup = await _studyGroupRepository.getStudyGroupByIntID(model.Study_Group_ID);
            
            if (studyGroup == null)
            {
                result.Errors.Add(new Error { Message = "EDIT: Group with ID: " + model.Study_Group_ID + " not found!" });
                return result;
            }

            var auditSession = await _auditingSessionRepository.getSessionBySessionID(model.Auditing_Session_ID);

            if (auditSession == null )
            {
                result.Errors.Add(new Common.Error { Message = "Error: Unable to find session: " + model.Auditing_Session_ID });
                return result;
            }

            auditSession.AuditorId = model.AuditorId ?? auditSession.AuditorId;
            auditSession.StudyGroupId = model.Study_Group_ID.ToString() ?? auditSession.StudyGroupId;
            auditSession.Conducted = model.Conducted ?? auditSession.Conducted;
            auditSession.MaterialDelivered = model.MaterialDelivered ?? auditSession.MaterialDelivered;
            auditSession.LabFlag = model.Lab_Flag ?? auditSession.LabFlag;
            auditSession.TestFlag = model.Test_Flag ?? auditSession.TestFlag;
            auditSession.DepiLogoAdded = model.Depi_Logo_Flag ?? auditSession.DepiLogoAdded;
            auditSession.CurrentChapter = model.Current_Chapter ?? auditSession.CurrentChapter;
            auditSession.InstructorId = model.Instructor_ID ?? auditSession.InstructorId;
            auditSession.InstructorName = model.OtherInstructorName ?? auditSession.InstructorName;
            auditSession.ConnectionQuality = model.ConnectionQuality ?? auditSession.ConnectionQuality;
            auditSession.VoiceQuality = model.VoiceQuality ?? auditSession.VoiceQuality;
            auditSession.VideoQuality = model.VideoQuality ?? auditSession.VideoQuality;
            auditSession.Remarks = model.Remarks ?? auditSession.Remarks;
            
            auditSession.HardwareProficiency = model.HardwareProficiency ?? auditSession.HardwareProficiency;
            auditSession.UnderstoodExamples = model.UnderstoodExamples ?? auditSession.UnderstoodExamples;
            auditSession.UnderstoonExplaination = model.UnderstoonExplaination ?? auditSession.UnderstoonExplaination;
            auditSession.TimeForQuestions = model.TimeForQuestions ?? auditSession.TimeForQuestions;
            auditSession.InstructorEncouragement = model.InstructorEncouragement ?? auditSession.InstructorEncouragement;
            auditSession.MaterialIsClear = model.MaterialIsClear ?? auditSession.MaterialIsClear;
            auditSession.ACCondition = model.ACCondition ?? auditSession.ACCondition;
            auditSession.CenterEnvironment = model.CenterEnvironment ?? auditSession.CenterEnvironment;
            auditSession.InitiativeClear = model.InitiativeClear ?? auditSession.InitiativeClear;
            auditSession.PrevLinks = model.PrevLinks ?? auditSession.PrevLinks;
            auditSession.CommentCategory = model.CommentCategory ?? auditSession.CommentCategory;
            auditSession.SessionType = model.SessionType ?? auditSession.SessionType;
            auditSession.AttendanceType = model.AttendanceType ?? auditSession.AttendanceType;

            if (model.StudentsAttendedList != null && model.StudentsAttendedList.Count > 0)
            {
                if (auditSession.AuditingSessionAttendances != null || auditSession.AuditingSessionAttendances.Count() > 0)
                {
                    _auditingSessionAttendanceRepository.RemoveAll(auditSession.AuditingSessionAttendances.ToList());
                    ////var diff = auditSession.AuditingSessionAttendances.Where()
                    //foreach (var student in auditSession.AuditingSessionAttendances)
                    //{
                    //    var modelAttendance = model.StudentsAttendedList
                    //        .Where(c => c.StudentID == student.StudentId).FirstOrDefault();
                    //    student.presense = modelAttendance != null ? modelAttendance.Present: student.presense;
                    //}

                    foreach (var sAttendance in model.StudentsAttendedList)
                    {
                        //check if student exists in attendance list
                        var exists = auditSession.AuditingSessionAttendances.Where(c => c.StudentId == sAttendance.StudentID).FirstOrDefault();
                        
                        // in case of exists then overwrite present status
                        if (exists != null) 
                        {
                            exists.presense = sAttendance.Present;
                        }
                        else // in case don't exists (new student) add new attendance and add it to audting session
                        {
                            var newAttendee = new AuditingSessionAttendance
                            {
                                presense = sAttendance.Present,
                                StudentId = sAttendance.StudentID,
                                SendDate = auditSession.LastModified,
                                SessionId = auditSession.SessionId,
                                
                            };
                            var studentModel = await _studentRepository.getStudentByIntID(sAttendance.StudentID);
                            newAttendee.StudentName = studentModel != null ? (studentModel.NameEn != null ? studentModel.NameEn: studentModel.NameAr): "";
                            auditSession.AuditingSessionAttendances.Add(newAttendee);
                        }
                    }
                }
            }

            auditSession.LastModified = DateTime.Now;

            _auditingSessionRepository.Save();
            return await GetSingleAuditingSession(auditSession.SessionId);

        }

        #endregion
    }
}
