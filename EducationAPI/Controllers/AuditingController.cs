﻿using EducationAPI.Context;
using EducationAPI.DTO;
using EducationAPI.Models;
using EducationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EducationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditingController : ControllerBase
    {
        AuditingService auditingService = new AuditingService();
        // GET: api/<AuditingController>
        [HttpGet]
        [Route("GetCriteria")]
        public async Task<CommonResponse<AuditingSessionCriteraDTO>> GetCritera(string roundcode)
        {
            return await auditingService.getAuditingCritera(roundcode);
        }

        [HttpGet]
        [Route("GetAuditorGroups")]
        public async Task<CommonResponse<List<AuditorGroupsDTO>>> GetAudtorGroups(int auditor_ID)
        {
            return await auditingService.getAuditorGroups(auditor_ID, DateTime.Now);
        }

        // POST api/<AuditingController>
        [HttpPost]
        [Route("SaveAuditingReport")]
        public void SaveAudit(string value)
        {
        }
    }
}