using EducationAPI.Context;
using EducationAPI.Domain;
using EducationAPI.DTO;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAPI.Repositories
{
    public class StudentRepository
    {
        StudentDBContext _context;
        public StudentRepository() {
            _context = new StudentDBContext();
        }

        public async Task<Trainee> getStudentByIntID(int studentID)
        {
            var result = await _context.Trainees.Where(e=> e.TraineeIntId == studentID).FirstOrDefaultAsync();
            return result;
        }

        public async Task<Trainee> getStudentByEmail(string email)
        {
            var result = await _context.Trainees.Where(e => e.Email == email).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Trainee>> getStudentByGroup(int groupID)
        {
            var result = await _context.Trainees.Where(e => e.GroupIntID == groupID).ToListAsync();
            return result;
        }

        public async Task<Trainee> getStudentByMobile(string mobile)
        {
            var result = await _context.Trainees.Where(e => e.Mobile == mobile).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<Trainee>> getAllStudent()
        {
            var result = await _context.Trainees.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Trainee>> getListOfStudents(List<int> IDs)
        {
            var result = await _context.Trainees.Where(d=> IDs.Contains(d.TraineeIntId)).ToListAsync();
            return result;
        }

        public async Task<string> Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "success";
        }

        public void Add(Trainee trainee)
        {
            _context.Add(trainee);
        }

    }
}
