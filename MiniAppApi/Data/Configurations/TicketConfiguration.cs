using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniAppApi.Models;

namespace MiniAppApi.Data.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Type)
                   .IsRequired()
                   .HasMaxLength(50);
        builder.Property(t => t.Price).HasColumnType("decimal(18,2)").IsRequired();
        builder.HasOne(t => t.Event)
               .WithMany(e => e.Tickets)
               .HasForeignKey(t => t.EventId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
