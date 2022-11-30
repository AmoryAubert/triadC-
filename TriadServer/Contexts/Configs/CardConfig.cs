using TriadServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriadServer.Contexts.Configs
{
    internal class CardConfig : ConfigBase<Card>
    {
        public override void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable("Cards");
            builder.Property(e => e.Id)
                .IsRequired();
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
            builder.Property(e => e.Stars)
                .IsRequired();
            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(25);
            builder.Property(e => e.Img)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(e => e.Top)
                .IsRequired();
            builder.Property(e => e.Right)
                .IsRequired();
            builder.Property(e => e.Bottom)
                .IsRequired();
            builder.Property(e => e.Left)
                .IsRequired();
            builder.Property(e => e.SellPrice)
                .IsRequired();
        }
    }
}