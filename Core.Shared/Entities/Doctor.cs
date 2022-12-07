namespace Core.Shared.Entities;

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


    }
    public string UserId {get; set;} = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string License { get; set; } = string.Empty;
    public string ProfilPic {get; set;} = string.Empty;
    public Guid ClinicId { get; set; }
    public virtual ICollection<CommentAgreement> CommentAgreements {get; set;}
    public virtual ICollection<DoctorRate> DoctorRates {get; set;}
    public virtual ApplicationUser User {get; set;}
    public virtual ICollection<Patient> Patients {get; set;}
    public virtual ICollection<Comment> Comments {get; set;}
    public virtual ICollection<PatientDoctor> PatientDoctors {get; set;}
    public virtual ICollection<MedicalRecord> MedicalRecords {get; set;}
    public virtual Clinic Clinic {get; set;}


}