using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class ProviderStudyGroupRepository
    {
        StudentDBContext _context;
        public ProviderStudyGroupRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<ProviderStudyGroup> getProviderbyStudyGroup(int ID)
        {

            var result =  await _context.ProviderStudyGroups.Where(c => c.StudyGroupIntId == ID)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
