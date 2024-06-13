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
                _context.Add(session);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }
    }
}
