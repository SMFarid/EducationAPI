using EducationAPI.Context;
using EducationAPI.Models;

namespace EducationAPI.Repositories
{
    public class AuditingSessionRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditingSessionRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<string> SaveSession(AuditingSession session)
        {
            try
            {
                _context.AuditingSessions.Add(session);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }

        
    }
}
