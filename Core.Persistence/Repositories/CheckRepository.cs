namespace Core.Persistence.Repositories;
public class CheckRepository : BaseRepo<Check>, ICheckRepository
{
    public CheckRepository(DbContext db) : base(db)
    {
    }
}
