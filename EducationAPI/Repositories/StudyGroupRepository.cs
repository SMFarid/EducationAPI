using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class StudyGroupRepository
    {
        StudentDBContext _context;
        public StudyGroupRepository() {
            _context = new StudentDBContext();
        }

        public async Task<StudyGroup> getStudyGroupByID (string ID)
        {
            var result = await _context.StudyGroups.Where(c => c.RoundCode == ID)
                .Include(c => c.Instructor)
                .Include(c => c.Trainees)
                .Include(c => c.TrackIntNavigation)
                .FirstOrDefaultAsync();
            //var result = await _context.StudyGroups.ToListAsync();
            //return result.First();
            return result;
        }

    }
}
