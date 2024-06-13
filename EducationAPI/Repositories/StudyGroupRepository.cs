using EducationAPI.Context;
using EducationAPI.Domain;
using EducationAPI.DTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
                //.Include(c => c.Instructor)
                .Include(c => c.Trainees)
                //.Include(c => c.TrackIntNavigation)
                .FirstOrDefaultAsync();
            //var result = await _context.StudyGroups.ToListAsync();
            //return result.First();
            return result;
        }

        public async Task<AuditingSessionCriteraDTO> getStudyGroupByID_(string ID)
        {
            var result = await _context.StudyGroups.Where(c => c.RoundCode == ID)
                //.Include(c => c.Instructor)
                //.Include(c => c.Trainees)
                //.Include(c => c.TrackIntNavigation)
                .FirstOrDefaultAsync();
            //var result = await _context.StudyGroups.ToListAsync();
            //return result.First();
            

            //var r = result.Select(d=> d.GroupIntId).ToList();
            var temp = _context.Trainees.Where(c => c.GroupIntID == result.GroupIntId).ToList();
            //result.Trainees = temp;
            AuditingSessionCriteraDTO auditingSession = new AuditingSessionCriteraDTO();
            auditingSession.instructor = result.Instructor != null ? result.Instructor.InstructorIntId.ToString() : "";
            auditingSession.SessionType = "Online";
            auditingSession.NumberRegistered = (int)result.NumberOfStudents;
            auditingSession.students = temp.Select(c => new StudentDTO { Id = c.TraineeIntId, NameAr = c.NameAr, NameEN = c.NameEn }).ToList();
            return auditingSession;
        }

    }
}
