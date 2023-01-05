namespace Core.Persistence.Repositories;
public class ClinicRepository : BaseRepo<Clinic>, IClinicRepository
{
    public ClinicRepository(DbContext db) : base(db)
    {
    }
}
