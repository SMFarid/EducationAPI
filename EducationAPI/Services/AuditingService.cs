using EducationAPI.Context;
using EducationAPI.DTO;
using EducationAPI.Models;
using EducationAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace EducationAPI.Services
{
    public class AuditingService
    {

        AuditorRoundCodeAssignmentRepository assignmentRepository = new AuditorRoundCodeAssignmentRepository();
        StudyGroupRepository studyGroupRepository = new StudyGroupRepository();

        public async Task<CommonResponse<AuditingSessionCriteraDTO>> getAuditingCritera(string roundCode)
        {
            var response = new CommonResponse<AuditingSessionCriteraDTO>();
            var criteria = new AuditingSessionCriteraDTO();

            try
            {
                var studyGroup = await studyGroupRepository.getStudyGroupByID(roundCode);
                if (studyGroup == null)
                {
                    response.Errors.Add(new Error { Message = "Error: Round Code not found" });
                    return response;
                }

                var students = studyGroup.Students.ToList();

                criteria.students = students.Select(c => new StudentDTO { Id = c.StudentIntId, NameAr = c.NameAr, NameEN = c.NameEn, }).ToList();

                criteria.CourseName = "";
                criteria.TrainingCenterName = studyGroup.ProviderId;
                criteria.instructor = studyGroup.Instructor != null ? studyGroup.Instructor.Id.ToString() :"";
                criteria.SessionType = "Online";
                criteria.NumberRegistered = (int)studyGroup.NumberOfStudents;
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
            
            var roundcodeList = await assignmentRepository.getAuditorAssignment(AuditorID, date);
            foreach (var item in roundcodeList)
            {
                auditorGroups.Add(new AuditorGroupsDTO
                {
                    Doneflag = false,
                    RoundCode = item.StudyGroupRoundCode
                });
                
            }
            response.Data = auditorGroups;
            return response;
        }

        //public async Task<CommonResponse<List<string>>> getAuditSessionCriteria(int AuditorID, string RoundCode)
        //{
        //    var response = new CommonResponse<List<String>>();

            

            

        //    return response;
        //}

    }
}
