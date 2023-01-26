namespace MedicalBlog.Persistence.Repositories;

public class UserRepository : BaseRepo<User>, IUserRepository
{
    public UserRepository(DbContext db) : base(db)
    {
    }
}
