using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class AuditorRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditorRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<Auditor> getAuditor(string username, string password)
        {
            var result = await _context.Auditors.Where(c => c.Username == username && c.Password == password).FirstOrDefaultAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }
        public async Task<Auditor> GetAuditorById(int id)
        {
            
            var res = await _context.Auditors.Where(c => c.Id == id).FirstOrDefaultAsync();
            return res;
        }
    }
}
