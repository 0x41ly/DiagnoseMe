namespace Core.Persistence.Repositories;
public class BookingRepository : BaseRepo<Booking>, IBookingRepository
{
    public BookingRepository(DbContext db) : base(db)
    {
    }
}
