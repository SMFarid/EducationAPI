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
    public class ManagementController : ControllerBase
    {
        DatatManagementService _managementService = new DatatManagementService();
        // GET: api/<AuditingController>

        //Manage Students

        [HttpPost]
        [Route("EditStudent")]
        public async Task<CommonResponse<StudentViewModel>> EditStudent(StudentEditModel model)
        {
            return await _managementService.EditStudent(model);
        }

        [HttpGet]
        [Route("GetStudentDetails")]
        public async Task<CommonResponse<StudentViewModel>> GetStudentDetails(int studentID)
        {
            return await _managementService.GetStudent(studentID);
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<CommonResponse<List<StudentDTO>>> GetAllStudents()
        {
            return await _managementService.getStudentsList();
        }

        ///Manage Study Groups
        ///
        [HttpPost]
        [Route("EditGroup")]
        public async Task<CommonResponse<ViewStudyGroupDTO>> EditGroup(EditStudyGroupDTO model)
        {
            return await _managementService.EditGroup(model);
        }

        [HttpGet]
        [Route("GetGroupDetails")]
        public async Task<CommonResponse<ViewStudyGroupDTO>> GetGroupDetails(int groupIntID)
        {
            return await _managementService.GetGroupDetails(groupIntID);
        }

        [HttpGet]
        [Route("GetGroupCriteria")]
        public async Task<CommonResponse<GroupCriteriaModel>> GetGroupCriteria()
        {
            return await _managementService.GetGroupCriteria();
        }

        [HttpGet]
        [Route("GetAllGroups")]
        public async Task<CommonResponse<List<RoundCodeDTO>>> GetAllGroups()
        {
            return await _managementService.getAllRoundCodes();
        }

    }
}
