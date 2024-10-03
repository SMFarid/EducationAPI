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

        public async Task<IEnumerable<DailyAuditorsAttendance>> getLoggedIn()
        {
            TimeSpan startTime = new TimeSpan(17, 30, 0);
            TimeSpan endTime = new TimeSpan(18, 0, 0);
            var result = await _context.DailyAuditorsAttendances
                .Where(c => c.LoginTime.Value.Date == DateTime.Now.Date)
                .Where(c => c.LoginTime.Value.TimeOfDay >= startTime && c.LoginTime.Value.TimeOfDay <= endTime)
                .ToListAsync();
            return result;
        }

    }
}
