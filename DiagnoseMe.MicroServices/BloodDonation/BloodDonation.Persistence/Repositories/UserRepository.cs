using BloodDonation.Application.Common.Interfaces.Persistence;

namespace BloodDonation.Persistence.Repositories;

public class UserRepository : BaseRepo<User>, IUserRepository
{
    public UserRepository(DbContext db) : base(db)
    {
    }
}
