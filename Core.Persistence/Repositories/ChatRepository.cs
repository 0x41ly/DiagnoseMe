namespace Core.Persistence.Repositories;
public class ChatRepository : BaseRepo<Chat>, IChatRepository
{
    public ChatRepository(DbContext db) : base(db)
    {
    }
}
