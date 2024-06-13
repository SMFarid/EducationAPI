using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(int auditor_id, DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c=> c.AuditorId == auditor_id).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }
    }
}
