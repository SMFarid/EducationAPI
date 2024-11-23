using EducationAPI.Common;
using EducationAPI.Context;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class AuditingSessionAttendanceRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditingSessionAttendanceRepository()
        {
            _context = new StudentDBContext();
        }


        public async Task<IEnumerable<AuditingSessionAttendance>> getAttendanceBySessionID(int sessionID)
        {
            return await _context.AuditingSessionAttendances.Where(e => e.SessionId == sessionID).ToListAsync();
        }

    }
}
