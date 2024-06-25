namespace EducationAPI.DTO
{
    public class EditAssignmentModel
    {
        public int AssignmentID { get; set; }
        public string? RoundCode { get; set; }
        public int? AuditorID { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? Conducted {  get; set; }
    }
}
