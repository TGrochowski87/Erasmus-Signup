namespace UserApi.Models
{
    public class User
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string? MiddleNames { get; set; }
        public string LastName { get; set; }
        public char Sex{ get; set; }
        public string TitlesBefore { get; set; }
        public string TitlesAfter { get; set; }
        public int StudentStatus { get; set; }
        public int StaffStatus { get; set; }
        public string Email { get; set; }
        public string PhotoUtl_50x50 { get; set; }
        public string PhotoUtl_400x500 { get; set; }
        public string? StudentNumber { get; set; }

        public User(long id, string firstName, string middleNames, string lastName, char sex, string titlesBefore, string titlesAfter, int studentStatus, int staffStatus, string email, string photoUrl_50x50, string photoUrl_400x500, string? studentNumber)
        {
            Id = id;
            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
            Sex = sex;
            TitlesBefore = titlesBefore;
            TitlesAfter = titlesAfter;
            StudentStatus = studentStatus;
            StaffStatus = staffStatus;
            Email = email;
            PhotoUtl_50x50 = photoUrl_50x50;
            PhotoUtl_400x500 = photoUrl_400x500;
            StudentNumber = studentNumber;
        }
    }
}
