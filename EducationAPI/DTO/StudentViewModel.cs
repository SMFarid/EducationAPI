using System.ComponentModel.DataAnnotations;

namespace EducationAPI.DTO
{
    public class StudentViewModel
    {
        public int StudentID { get; set; }
        public string? SocialID { get; set; }
        public string? NameEn { get; set; }
        public string? NameAr { get; set; }
        public int? TrackID { get; set; }
        public string RoundCode { get; set; }
        public int GroupIntID { get; set; }
        public string? StudyGovernorate { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        //public string? Nationality { get; set; }
        public string? Email { get; set; }
        //public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Status { get; set; }
        public bool? Active {  get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedUser { get; set; }
    }
}
