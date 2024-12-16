using EducationAPI.Common;

namespace EducationAPI.DTO
{
    public class AuditingCriteriaModel
    {
        public AuditingCriteriaModel()
        {
            Instructors = new List<CommonDTO>();
            Auditors = new List<CommonDTO>();
            Students = new List<CommonDTO>();
        }

        public List<CommonDTO> Instructors { get; set; }
        public List<CommonDTO> Auditors { get; set; }
        public List<CommonDTO> Students { get; set; }
    }
}
