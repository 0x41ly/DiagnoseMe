namespace Core.Persistence.Repositories;
public class ChatRoomRepository : BaseRepo<ChatRoom>, IChatRoomRepository
{
    public ChatRoomRepository(DbContext db) : base(db)
    {
    }
}
