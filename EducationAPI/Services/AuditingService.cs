using EducationAPI.Common;
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
    public class AuditingService
    {

        AuditorRoundCodeAssignmentRepository _assignmentRepository = new AuditorRoundCodeAssignmentRepository();
        StudyGroupRepository _studyGroupRepository = new StudyGroupRepository();
        AuditorRepository _auditorRepository = new AuditorRepository();
        AuditingSessionRepository _auditingSessionRepository = new AuditingSessionRepository();

        #region Auditing Session
        public async Task<CommonResponse<AuditingSessionCriteraDTO>> getAuditingCritera(string roundCode, int Audtor_ID)
        {
            var response = new CommonResponse<AuditingSessionCriteraDTO>();
            var criteria = new AuditingSessionCriteraDTO();

            try
            {
                var studyGroup = await _studyGroupRepository.getStudyGroupByID(roundCode);
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

                roundcodeAssignment.AuditorId = Audtor_ID;
                roundcodeAssignment.Conducted = (int)RoundCodeStates.InProgress;
                var students = studyGroup.Trainees != null ? studyGroup.Trainees.ToList() : new List<Trainee>();

                criteria.Students = students.Select(c => new StudentDTO { Id = c.TraineeIntId, NameAr = c.NameAr, NameEN = c.NameEn }).OrderBy(c=> c.NameAr).ToList();

                criteria.CourseName = "";
                //criteria.TrainingCenterName = studyGroup.trainingProvider.NameEn;
                if (studyGroup.Instructor != null)
                {
                    criteria.Instructors.Add(
                        new InstructorDTO() { Id = studyGroup.Instructor.InstructorIntId, NameEN = studyGroup.Instructor.NameEn });
                }
                
                criteria.SessionType = "Online";
                criteria.NumberRegistered = studyGroup.NumberOfStudents != null ? (int)studyGroup.NumberOfStudents: 0;
                criteria.Study_Group_ID = studyGroup.GroupIntId;
                criteria.Auditing_Session_ID = roundcodeAssignment.AssignmentSessionID;
                criteria.MeetingLink = studyGroup.MeetingLink;
                var startTime = roundcodeAssignment.Date;

                if (startTime != null)
                {
                    criteria.StartTime = startTime;
                    criteria.EndTime = startTime.AddHours(3);
                }


                _assignmentRepository.Save();

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
            foreach (var item in roundcodeList)
            {
                auditorGroups.Add(new AuditorGroupsDTO
                {
                    Doneflag = (int)(item.Conducted != null ? item.Conducted : (int)RoundCodeStates.Open),
                    RoundCode = item.StudyGroupRoundCode,
                    SessionDateTime = item.Date.ToShortTimeString()
                });
                
            }
            response.Data = auditorGroups;
            return response;
        }

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
                    AuditorID = user.Id,
                    NameAr = user.NameAr != null ? user.NameAr: "",
                    NameEn = user.NameEn !=null ? user.NameEn: ""
                });

            if (user.Role == (int)UserRoles.Admin)
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

        public async Task<CommonResponse<string>> SaveAuditSession (AuditSessionSaveModel model)
        {
            var response = new CommonResponse<string>();


            //Retrieve and Validate
            var auditor = await _auditorRepository.GetAuditorById(model.AuditorId);
            if (auditor == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
                return response;
            }
            var studyGroup = await _studyGroupRepository.getStudyGroupByID(model.RoundCode);
            if (studyGroup == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }
            //get study group session
            var assignedSession = await _assignmentRepository.getAssignmentByID(model.Auditing_Session_ID);
            assignedSession.Conducted = (int)RoundCodeStates.Done;

            AuditingSession auditingSession = new AuditingSession
            {
                AttendanceType = "Online", //change later
                //Auditor = auditor,
                AuditorId = model.AuditorId,
                AuditorName = !auditor.NameEn.IsNullOrEmpty() ? auditor.NameEn :auditor.NameAr,
                
                //Course = studyGroup.CourseId, //retrieve name later
                //Instructor = studyGroup.Instructor,
                InstructorId = (int)studyGroup.InstructorId != null ? (int)studyGroup.InstructorId: 0,
                InstructorName = studyGroup.InstructorName,
                Conducted = model.Conducted,
                MaterialDelivered = model.MaterialDelivered,
                CurrentChapter = model.Current_Chapter,
                //SessionDateTimeStart = (DateTime)model.ReportStart,
                //SessionDateTimeClose = model.ReportEnd,
                DepiLogoAdded = model.Depi_Logo_Flag,
                LabFlag = model.Lab_Flag,
                TestFlag = model.Test_Flag,
                
                ConnectionQuality = model.ConnectionQuality.ToString(),
                VoiceQuality = model.VoiceQuality.ToString(),
                VideoQuality = model.VideoQuality.ToString(),
                StudyGroupId = studyGroup.GroupIntId.ToString(),
                
            };

            _assignmentRepository.Save();
            response.Data = await _auditingSessionRepository.SaveSession(auditingSession);

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
                        Conducted = code.Conducted
                    });
                }
            }

            var roundCodesList = await _studyGroupRepository.getAllGroups();

            foreach (var code in roundCodesList)
            {
                if (!StudyGroups.Select(c => c.RoundCode).ToList().Contains(code.RoundCode))
                {
                    StudyGroups.Add(new RoundCodeAssignmentDTO
                    {
                        RoundCode = code.RoundCode,
                        AssignmentID = 0,
                        Status = 0
                    });
                }
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

            roundCodeAssign.AuditorId = model.AuditorID;
            roundCodeAssign.Conducted = model.Conducted;
            roundCodeAssign.Date = (DateTime)model.AssignmentDate;

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
            AuditorRoundCodeAssignment auditorRoundCode = new AuditorRoundCodeAssignment
            {
                AuditorId = model.AuditorID,
                Conducted = 0,
                Date = model.AssignmentDate,
                StudyGroupRoundCode = model.RoundCode
            };

            response.Data = _assignmentRepository.Add(auditorRoundCode);


            return response;
        }

        #endregion

    }
}
