using EducationAPI.Context;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class DailyAuditorsAttendanceRepository
    {

        StudentDBContext _context = new StudentDBContext();
        public DailyAuditorsAttendanceRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<DailyAuditorsAttendance>> getLoggedIn(TimeSpan startTime, TimeSpan endTime)
        {
            
            var result = await _context.DailyAuditorsAttendances
                .Include(c => c.Auditor)
                .Where(c => c.LoginTime.Value.Date == DateTime.Now.Date)
                .Where(c => c.LoginTime.Value.TimeOfDay >= startTime && c.LoginTime.Value.TimeOfDay <= endTime)
                .Where(c => c.Auditor.Id == 4) //change to enum
                .ToListAsync();
            return result;
        }

    }
}
