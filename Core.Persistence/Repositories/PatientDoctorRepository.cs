namespace Core.Persistence.Repositories;
public class PatientDoctorRepository : BaseRepo<PatientDoctor>, IPatientDoctorRepository
{
    public PatientDoctorRepository(DbContext db) : base(db)
    {
    }
}
