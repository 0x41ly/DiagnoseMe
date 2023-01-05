namespace Core.Persistence.Repositories;
public class MedicineRepository : BaseRepo<Medicine>, IMedicineRepository
{
    public MedicineRepository(DbContext db) : base(db)
    {
    }
}
