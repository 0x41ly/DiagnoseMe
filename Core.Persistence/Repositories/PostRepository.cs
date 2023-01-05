namespace Core.Persistence.Repositories;
public class PostRepository : BaseRepo<Post>, IPostRepository
{
    public PostRepository(DbContext db) : base(db)
    {
    }
}
