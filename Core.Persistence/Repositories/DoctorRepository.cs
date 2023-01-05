namespace Core.Persistence.Repositories;
public class DoctorRepository : BaseRepo<Doctor>, IDoctorRepository
{
    public DoctorRepository(DbContext db) : base(db)
    {
    }
}
