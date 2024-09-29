using EducationAPI.Common;
using EducationAPI.Context;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class AuditingSessionRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditingSessionRepository()
        {
            _context = new StudentDBContext();
        }


        public async Task<AuditingSession> getSessionByAssignmentID(int assignmentID)
        {
            return await _context.AuditingSessions.Where(e => e.AssignmentSessionID == assignmentID).FirstOrDefaultAsync();
        }

        public async Task<AuditingSession> getSessionBySessionID(int sessionID)
        {
            return await _context.AuditingSessions.Where(e => e.SessionId == sessionID).FirstOrDefaultAsync();
        }

        public async Task<string> Add(AuditingSession session)
        {
            try
            {
                _context.AuditingSessions.Add(session);
                //await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
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

        public async Task<CommonResponse<string>> Save()
        {
            var response  = new CommonResponse<string>();
            try
            {
                //_context.AuditingSessions.Add(session);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Errors.Add(new Error { Message = ex.InnerException.ToString() });
                return response;
            }
            return response;
        }


    }
}
