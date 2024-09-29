using EducationAPI.Context;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EducationAPI.Repositories
{
    public class AuthRepository
    {
        StudentDBContext _context = new StudentDBContext();
        public AuthRepository()
        {
            _context = new StudentDBContext();
        }

        //public async Task<Auth> getAuditor(string username, string password)
        //{
        //    var result = await _context.Auths.Where(c => c.Username.ToLower() == username.ToLower() && c.Password
        //    == password).FirstOrDefaultAsync(); //null check  && c.Date.Value.Date  == date.Date

        //    return result;
        //}
        public async Task<Auth> GetAuditorById(int id)
        {

            var res = await _context.Auths.Where(c => c.Id == id).FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<Auth>> getAllAuditors()
        {
            var result = await _context.Auths.Where(c=>c.Role == 4).OrderBy(c => c.Id).ToListAsync();
            return result;
        }
    }
}
