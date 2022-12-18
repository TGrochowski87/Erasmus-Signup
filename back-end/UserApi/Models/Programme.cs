namespace UserApi.Models
{
/*
{[
  {
    "id": "71417",
    "programme": {
      "id": "W04-ISTP-000P-OSMW3",
      "description": {
        "pl": "informatyka stosowana, drugiego stopnia, stacjonarne",
        "en": "Applied Computer Science, second-level studies, full-time studies"
      }
    },
    "status": "active",
    "stages": [
      {
        "id": "305622",
        "code": "2M-IST-ZTI",
        "description": {
          "pl": "2 semestr, informatyka stosowana, zastosowania specjalistycznych technologii informatycznych",
          "en": "2 semester, Applied Computer Science, Application of modern information technologies"
        }
      }
    ]
  }
]}
*/

    public class Programme
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        Programme(int id, string code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }
    }
}
/*
attendance Attendance lists
blobbox Storing binary data
calendar University calendar
cards Data on users' ID cards
courses Info on courses
credits Study credits info
csgroups User-defined groups
emrex Student mobility
facperms Per-faculty permissions
feedback Feedback reports
fileshare Sharing files
geo Geographical data
groups Accessing group info
guide University guide

instaddr Institutional addresse
*/

/* GOOD

fac Information on faculties
grades Accessing grades info
oauth OAuth Authorization
progs
users
*/