using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class TrackRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public TrackRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<Track>> getAllTracks()
        {
            var res = await _context.Tracks.ToListAsync();
            return res;
        }
    }
}
