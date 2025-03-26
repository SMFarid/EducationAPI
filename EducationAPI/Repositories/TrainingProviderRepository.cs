using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class TrainingProviderRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public TrainingProviderRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<TrainingProvider>> getAllProviders()
        {
            var res = await _context.TrainingProviders.ToListAsync();
            return res;
        }

        public async Task<TrainingProvider> getProviderByID(int providerID)
        {
            var res = await _context.TrainingProviders.Where(c => c.ProviderIntId == providerID).FirstOrDefaultAsync();
            return res;
        }
    }
}
