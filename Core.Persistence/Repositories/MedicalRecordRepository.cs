namespace Core.Persistence.Repositories;
public class MedicalRecordRepository : BaseRepo<MedicalRecord>, IMedicalRecordRepository
{
    public MedicalRecordRepository(DbContext db) : base(db)
    {
    }
}
