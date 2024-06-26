using EducationAPI.Common;
using EducationAPI.Context;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Enums;
using EducationAPI.Models;
using EducationAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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

        AuditorRoundCodeAssignmentRepository assignmentRepository = new AuditorRoundCodeAssignmentRepository();
        StudyGroupRepository studyGroupRepository = new StudyGroupRepository();
        AuditorRepository auditorRepository = new AuditorRepository();
        AuditingSessionRepository auditingSessionRepository = new AuditingSessionRepository();

        #region Auditing Session
        public async Task<CommonResponse<AuditingSessionCriteraDTO>> getAuditingCritera(string roundCode, int Audtor_ID)
        {
            var response = new CommonResponse<AuditingSessionCriteraDTO>();
            var criteria = new AuditingSessionCriteraDTO();

            try
            {
                var studyGroup = await studyGroupRepository.getStudyGroupByID(roundCode);
                if (studyGroup == null)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Round Code not found" });
                    return response;
                }

                var roundcodeAssignment = await assignmentRepository.getAssignmentByRoundCode(roundCode);
                if (roundcodeAssignment == null)
                {
                    response.Errors.Add(new Common.Error { Message = "Error: Round Code not found" });
                    return response;
                }

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
            
            var roundcodeList = await assignmentRepository.getAuditorAssignment(AuditorID, date); //edit to use only date and state
            foreach (var item in roundcodeList)
            {
                auditorGroups.Add(new AuditorGroupsDTO
                {
                    Doneflag = (int)RoundCodeStates.Open,
                    RoundCode = item.StudyGroupRoundCode
                });
                
            }
            response.Data = auditorGroups;
            return response;
        }

        public async Task<CommonResponse<List<AuditorDTO>>> getAuditorList()
        {
            var response = new CommonResponse<List<AuditorDTO>>();
            List<AuditorDTO> auditors = new List<AuditorDTO>();

            var auditorsList = await auditorRepository.getAllAuditors();
            foreach (var item in auditorsList)
            {
                auditors.Add(new AuditorDTO
                {
                    AuditorID = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn
                });
            }

            response.Data = auditors;
            return response;
        }

        public async Task<CommonResponse<string>> SaveAuditSession (AuditSessionSaveModel model)
        {
            var response = new CommonResponse<string>();


            //Retrieve and Validate
            var auditor = await auditorRepository.GetAuditorById(model.AuditorId);
            if (auditor == null)
            {
                response.Errors.Add(new Error { Message = "Error: Auditor not found, please check ID" });
                return response;
            }
            var studyGroup = await studyGroupRepository.getStudyGroupByID(model.RoundCode);
            if (studyGroup == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }
            //get study group session
            var assignedSession = await assignmentRepository.getAssignmentByID(model.Auditing_Session_ID);


            AuditingSession auditingSession = new AuditingSession
            {
                AttendanceType = "Online", //change later
                //Auditor = auditor,
                AuditorId = model.AuditorId,
                AuditorName = !auditor.NameEn.IsNullOrEmpty() ? auditor.NameEn :auditor.NameAr,
                
                //Course = studyGroup.CourseId, //retrieve name later
                //Instructor = studyGroup.Instructor,
                //InstructorId = (int)studyGroup.InstructorId,
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

            response.Data = await auditingSessionRepository.SaveSession(auditingSession);

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

            
            response.Data = StudyGroups;
            return response;
        }



        public async Task<CommonResponse<string>> EditAuditAssignment(EditAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            List<AuditorGroupsDTO> auditorGroups = new List<AuditorGroupsDTO>();


            return response;
        }

        public async Task<CommonResponse<string>> DeleteAuditAssignment(EditAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            var roundCodeAssign = await assignmentRepository.getAssignmentByRoundCode(model.RoundCode);

            if (roundCodeAssign == null)
            {
                response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                return response;
            }

            response.Data = assignmentRepository.DeleteAssignmentByRoundCode(roundCodeAssign);


            return response;
        }

        public async Task<CommonResponse<string>> AddAuditAssignment(AddAssignmentModel model)
        {
            var response = new CommonResponse<string>();
            List<AuditorGroupsDTO> auditorGroups = new List<AuditorGroupsDTO>();


            return response;
        }

        #endregion

    }
}
