namespace Core.Domain.Entities;

public class Doctor : BaseEntity{

    public Doctor(){
        CommentAgreements= new HashSet<CommentAgreement>();
        DoctorRates = new HashSet<DoctorRate>();
        Patients = new HashSet<Patient>();
        User = new ApplicationUser();
        Comments = new HashSet<Comment>();
        Clinic = new();
        PatientDoctors = new HashSet<PatientDoctor>();
        MedicalRecords = new HashSet<MedicalRecord>();
        Posts = new HashSet<Post>();
        PostStateSuggestions = new HashSet<PostStateSuggestion>();
        ChatRooms = new HashSet<ChatRoom>();
        Answers = new HashSet<Answer>();
        AnswerAgreements = new HashSet<AnswerAgreement>();
        Bookings = new HashSet<Booking>();


    }
    public string UserId {get; set;} = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;
    public bool IsLicenseVerified {get; set;}
    public Guid ClinicId { get; set; }
    public virtual ICollection<Post> Posts {get; set;} 
    public virtual ICollection<PostStateSuggestion> PostStateSuggestions {get; set;} 
    public virtual ICollection<CommentAgreement> CommentAgreements {get; set;}
    public virtual ICollection<DoctorRate> DoctorRates {get; set;}
    public virtual ApplicationUser User {get; set;}
    public virtual ICollection<Patient> Patients {get; set;}
    public virtual ICollection<Comment> Comments {get; set;}
    public virtual ICollection<PatientDoctor> PatientDoctors {get; set;}
    public virtual ICollection<MedicalRecord> MedicalRecords {get; set;}
    public virtual ICollection<ChatRoom> ChatRooms {get; set;}
    public virtual ICollection<Answer> Answers {get; set;}
    public virtual ICollection<AnswerAgreement> AnswerAgreements {get; set;}
    public virtual ICollection<Booking> Bookings {get; set;}
    public virtual Clinic Clinic {get; set;}


}