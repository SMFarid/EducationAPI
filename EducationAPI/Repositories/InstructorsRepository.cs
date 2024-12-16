using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class InstructorsRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public InstructorsRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<Instructor>> getAllInstructors()
        {
            var res = await _context.Instructors.ToListAsync();
            return res;
        }
    }
}
