using EducationAPI.Context;
using EducationAPI.Models;
using System.Collections.Generic;

namespace EducationAPI.Repositories
{
    public class TrainingCenterRepository
    {
        //private StudentDBContext _dbContext { get return Context as StudentDBContext }
        StudentDBContext _context;
        public TrainingCenterRepository()
        {
            _context = new StudentDBContext();
        }

        public IEnumerable<TrainingCenter> getTrainingCenterByID(int id)
        {
            return null;
        }
    }
}
