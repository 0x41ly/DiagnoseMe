
namespace Core.Domain.Entities
{
    public class ChatRoom : BaseEntity
    {
        public ChatRoom(){
        Chats = new HashSet<Chat>();
        }
        public Guid DoctorId {get; set;}
        public Guid PatientId {get; set;}

        public virtual Doctor Doctor {get; set;} = new();
        public virtual Patient Patient {get; set;} = new();
        public virtual ICollection<Chat> Chats {get; set;} 
    }
}