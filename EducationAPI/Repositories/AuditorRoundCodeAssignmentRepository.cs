using Azure;
using EducationAPI.Context;
using EducationAPI.Domain;
using EducationAPI.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var result = await _context.AuditorRoundCodeAssignments.Where(c=> c.Date == date && c.Conducted!= (int)RoundCodeStates.Done).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date

            return result;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAssignmentsByDate(DateTime date)
        {
            //var 
            try
            {
                var result = await _context.AuditorRoundCodeAssignments.Where(c => c.Date.Date == date.Date).ToListAsync(); //null check  && c.Date.Value.Date  == date.Date
                return result;
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAssignmentsByTimeFrame(DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            //var 
            try
            {
                var result = await _context.AuditorRoundCodeAssignments
                    .Where(x => x.AuditorId == null || x.AuditorId == 0)
                    .Where(c => c.Date.Date == DateTime.Now.Date)
                    .Where(c => c.Date.TimeOfDay >= startTime && c.Date.TimeOfDay <= endTime)
                    .ToListAsync(); 
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return null;
        }

        public async Task<IEnumerable<AuditorRoundCodeAssignment>> getAuditorAssignment(int AuditorID, DateTime date)
        {
            try
            {
                var result = await _context.AuditorRoundCodeAssignments.Where(e => e.Date.Date == date.Date)//e.Conducted != (int)RoundCodeStates.Done 
                .Where(e=> e.AuditorId == AuditorID)
                .ToListAsync();

                return result;
            } catch (Exception ex)
            {
                Console.Out.WriteLine( ex.InnerException.ToString());
            }
            return null;
        }

        public async Task<AuditorRoundCodeAssignment> getAssignmentByRoundCode(string roundCode)
        {
            var result = await _context.AuditorRoundCodeAssignments
                .Where(c => c.StudyGroupRoundCode == roundCode && c.Date.Date == DateTime.Now.Date)
                //.Include(c => c.auditingSession)
                .FirstOrDefaultAsync();

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
                return ex.ToString();
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
