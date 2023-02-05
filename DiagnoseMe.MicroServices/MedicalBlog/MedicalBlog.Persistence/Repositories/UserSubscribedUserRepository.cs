namespace MedicalBlog.Persistence.Repositories;

public class UserSubscribedUserRepository : BaseRepo<UserSubscribedUser>, IUserSubscribedUserRepository
{
    public UserSubscribedUserRepository(DbContext db) : base(db)
    {
    }
}