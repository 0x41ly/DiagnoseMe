using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Persistence.Context.Configurations;

public class ChatConfigration : BaseConfiguration<Chat>
{
    public override void Configure(EntityTypeBuilder<Chat> builder=null!)
    {
        builder.ToTable("Chats");
        base.Configure(builder);
        builder.Property(c => c.Message).IsRequired();
        builder.Property(c => c.SenderId).IsRequired().HasMaxLength(255);
        builder.Property(c => c.RecieverId).IsRequired().HasMaxLength(255);
        builder.Property(c => c.RecieverId).IsRequired();
        builder.Property(c => c.SenderId).IsRequired();
        builder.Property(c => c.IsDelivered).IsRequired();
        builder.Property(c => c.IsSeen).IsRequired();
        builder.Property(c => c.MessageType).IsRequired().HasMaxLength(10);

        builder.Property(c => c.MessageType).IsRequired().HasMaxLength(10);
        builder.HasOne(ch => ch.Reciever).WithMany(rcv => rcv.RecievedChats).HasForeignKey(ch => ch.RecieverId);
        builder.HasOne(ch => ch.Sender).WithMany(rcv => rcv.SentChats).HasForeignKey(ch => ch.SenderId);
        builder.HasOne(ch => ch.ChatRoom).WithMany(c => c.Chats).HasForeignKey(ch => ch.ChatRoomId);
        
    }
}