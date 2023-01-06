

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class ChatRoomConfigration : BaseConfiguration<ChatRoom>
{
        public override void Configure(EntityTypeBuilder<ChatRoom> builder=null!)
    {
        builder.ToTable("ChatRooms");
        base.Configure(builder);
        builder.Property(c => c.DoctorId).IsRequired();
        builder.Property(c => c.PatientId).IsRequired();
        builder.HasOne(c => c.Doctor).WithMany(D => D.ChatRooms).HasForeignKey(c => c.DoctorId);
        builder.HasOne(c => c.Patient).WithMany(P => P.ChatRooms).HasForeignKey(c => c.PatientId);
    }
}
