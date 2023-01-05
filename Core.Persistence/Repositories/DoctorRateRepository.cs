namespace Core.Persistence.Repositories;
public class DoctorRateRepository : BaseRepo<DoctorRate>, IDoctorRateRepository
{
    public DoctorRateRepository(DbContext db) : base(db)
    {
    }
}
