namespace EducationAPI.DTO
{
    public class AttendanceViewDTO
    {
        public int StudentID {  get; set; }
        public bool Present { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
