﻿namespace EducationAPI.DTO
{
    public class RoundCodeAssignmentDTO
    {
        public string RoundCode { get; set; }
        public int Status { get; set; }
        public int? AssignmentID { get; set; }
        public int? AuditorID { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? Conducted {  get; set; }
    }
}
