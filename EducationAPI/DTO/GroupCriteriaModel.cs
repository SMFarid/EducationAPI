using EducationAPI.Common;

namespace EducationAPI.DTO
{
    public class GroupCriteriaModel
    {
        public GroupCriteriaModel()
        {
            Tracks = new List<CommonDTO>();
            Providers = new List<CommonDTO>();
        }

        public List<CommonDTO> Tracks { get; set; }
        public List<CommonDTO> Providers { get; set; }
    }
}
