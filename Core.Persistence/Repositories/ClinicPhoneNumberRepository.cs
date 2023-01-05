namespace Core.Persistence.Repositories;
public class ClinicPhoneNumberRepository : BaseRepo<ClinicPhoneNumber>, IClinicPhoneNumberRepository
{
    public ClinicPhoneNumberRepository(DbContext db) : base(db)
    {
    }
}
