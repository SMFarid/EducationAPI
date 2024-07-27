using EducationAPI.Context;
using EducationAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace EducationAPI.Repositories
{
    public class AuditorRoundCodeAssignmentRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuditorRoundCodeAssignmentRepository()
        {
            _context = new StudentDBContext();
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c=> c.Date == date && c.Conducted!= null).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAssignmentsByDate(DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.Date.Date == date.Date).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(int AuditorID, DateTime date)
        {
            var result = await _context.AuditorRoundCodeAssignments.ToListAsync(); //null check  && c.Date.Value.Date  == date.Date Where(c => c.AuditorId == AuditorID)

            return result;
        }

        public async Task<AuditorRoundCodeAssignment> getAssignmentByRoundCode(string roundCode)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.StudyGroupRoundCode == roundCode).FirstOrDefaultAsync();

            return result;
        }

        public string DeleteAssignmentByRoundCode(AuditorRoundCodeAssignment assignment)
        {
            try
            {
                var result = _context.Remove(assignment);
            }
            catch(Exception ex)
            {
                return ex.InnerException.ToString();
            }
            return "Success";
        }

        public string Save()
        {
            try
            {
                var result = _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.InnerException.ToString();
            }
            return "Success";
        }

        public string Add(AuditorRoundCodeAssignment codeAssignment)
        {
            try
            {
                _context.AuditorRoundCodeAssignments.Add(codeAssignment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            return "Success";
        }

        public async Task<AuditorRoundCodeAssignment> getAssignmentByID(int sessionID)
        {
            var result = await _context.AuditorRoundCodeAssignments.Where(c => c.AssignmentSessionID == sessionID).FirstOrDefaultAsync(); 
            return result;
        }
    }
}
