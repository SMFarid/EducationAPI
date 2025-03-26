using EducationAPI.Common;
using EducationAPI.Context;
using EducationAPI.DTO;
using EducationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ManageAuditsController : ControllerBase
    {
        ManageAuditsService _service = new ManageAuditsService();
        // GET: api/<AuditingController>

        //Manage Students

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<CommonResponse<List<RoundCodeDTO>>> GetAllGroups()
        {
            return await _service.getAllRoundCodes();
        }

        [HttpGet]
        [Route("GetRoundCodeAudits")]
        public async Task<CommonResponse<IEnumerable<AuditSessionViewDTO>>> GetRoundCodeAudits(int StudyGroupId)
        {
            //try
            //{
                return await _service.GetRoundCodeAudits(StudyGroupId);
            //} catch (Exception ex)
            //{
            //    CommonResponse<IEnumerable<AuditSessionViewDTO>> response = new CommonResponse<IEnumerable<AuditSessionViewDTO>>();
            //    response.Errors.Add(new Error { Message = ex.Message });
            //    return response;
            //}
        }

        ///Manage Study Groups
        ///
        [HttpPost]
        [Route("EditAuditingReport")]
        public async Task<CommonResponse<AuditSessionViewDTO>> EditAuditingReport(AuditSessionDTO model)
        {
            return await _service.EditAuditingReport(model);
        }

        [HttpPost]
        [Route("AddAuditingReport")]
        public async Task<CommonResponse<AuditSessionViewDTO>> AddAuditingReport(AddAuditSessionDTO model)
        {
            return await _service.AddAuditingReport(model);
        }

        [HttpGet]
        [Route("GetCriteria")]
        public async Task<CommonResponse<AuditingCriteriaModel>> GetAuditCriteria(int groupIntID)
        {
            return await _service.GetAuditingCriteria(groupIntID);
        }

    }
}
