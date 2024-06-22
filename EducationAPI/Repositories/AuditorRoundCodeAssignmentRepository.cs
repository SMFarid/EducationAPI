using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAPI.Repositories
{
    public class AuditorRoundCodeAssignmentRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditorRoundCodeAssignmentRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c=> c.Date == date && c.Conducted!= null).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(int AuditorID, DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.AuditorId == AuditorID).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<AuditorRoundCodeAssignment> getAssignmentByRoundCode(string roundCode)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.StudyGroupRoundCode == roundCode).FirstOrDefaultAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<AuditorRoundCodeAssignment> getAssignmentByID(int sessionID)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.AuditingSessionId == sessionID).FirstOrDefaultAsync(); 
            return result;
        }
    }
}
