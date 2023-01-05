namespace Core.Persistence.Repositories;
public class PostStateSuggestionRepository : BaseRepo<PostStateSuggestion>, IPostStateSuggestionRepository
{
    public PostStateSuggestionRepository(DbContext db) : base(db)
    {
    }
}
