using EducationAPI.Common;
using EducationAPI.Context;
using EducationAPI.DTO;
using EducationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuditingController : ControllerBase
    {
        AuditingService auditingService = new AuditingService();
        // GET: api/<AuditingController>
        [HttpGet]
        
        [Route("GetCriteria")]
        public async Task<CommonResponse<AuditingSessionCriteraDTO>> GetCritera(string roundcode, int Auditor_ID)
        {
            return await auditingService.getAuditingCritera(roundcode, Auditor_ID);
        }

        [HttpGet]
        [Route("GetAuditorGroups")]
        public async Task<CommonResponse<List<AuditorGroupsDTO>>> GetAuditorGroups(int auditor_ID)
        {
            return await auditingService.getAuditorGroups(auditor_ID, DateTime.Now);
        }

        // POST api/<AuditingController>
        [HttpPost]
        [Route("SaveAuditingReport")]
        public async Task<CommonResponse<string>> SaveAudit(AuditSessionSaveModel model)
        {
            var result = await auditingService.SaveAuditSession(model);
            return result;
        }

        [HttpGet]
        [Route("GetRoundCodesForEdit")]
        public async Task<CommonResponse<List<RoundCodeAssignmentDTO>>> GetRoundCodesForEdit()
        {
            return await auditingService.getRoundCodes();
        }

        [HttpGet]
        [Route("GetAuditorsList")]
        public async Task<CommonResponse<List<AuditorDTO>>> GetAuditorsList(int user_id)
        {
            return await auditingService.getAuditorList(user_id);
        }

        [HttpPost]
        [Route("EditRoundCodeAssignment")]
        public async Task<CommonResponse<string>> EditRoundCodeAssignment(EditAssignmentModel model)
        {
            var result = await auditingService.EditAuditAssignment(model);
            return result;
        }

        [HttpPost]
        [Route("AddRoundCodeAssignment")]
        public async Task<CommonResponse<string>> AddRoundCodeAssignment(AddAssignmentModel model)
        {
            var result = await auditingService.AddAuditAssignment(model);
            return result;
        }

        [HttpPost]
        [Route("DeleteRoundCodeAssignment")]
        public async Task<CommonResponse<string>> DeleteRoundCodeAssignment(EditAssignmentModel model)
        {
            var result = await auditingService.EditAuditAssignment(model);
            return result;
        }
    }
}
