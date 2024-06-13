using EducationAPI.Context;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class AuditorRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditorRepository() {
            _context = new StudentDBContext();
        }

        public async Task<Auditor> GetAuditorById(int id)
        {
            var res = await _context.Auditors.Where(c => c.Id == id).FirstOrDefaultAsync();
            return res;
        }
    }
}
