using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class JobProfileRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public JobProfileRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<JobProfile>> getAllJobProfiles()
        {
            var res = await _context.JobProfiles.ToListAsync();
            return res;
        }
    }
}
