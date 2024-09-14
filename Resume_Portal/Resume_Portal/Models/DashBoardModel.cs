namespace Resume_Portal.Models
{
    public class DashBoardModel
    {
        public List<TblUser> Users { get; set; } // Assuming tbl_User is your entity for users
        public TblUser UserData { get; set; }
        public TblResume Resumes { get; set; }
        public List<TblDesignation> Designations { get; set; }
        public List<TblResume> Resumesdata { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResumes { get; set; }
    }
}
