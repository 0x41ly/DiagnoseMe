namespace Core.Persistence.Repositories;
public class PatientRepository : BaseRepo<Patient>, IPatientRepository
{
    public PatientRepository(DbContext db) : base(db)
    {
    }
}
