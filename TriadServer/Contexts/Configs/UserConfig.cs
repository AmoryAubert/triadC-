using TriadServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TriadServer.Contexts.Configs
{
    internal class UserConfig : ConfigBase<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(e => e.GUID)
                .IsRequired()
                .HasMaxLength(38);
            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(384);
            builder.Property(e => e.PasswordSalt)
                .IsRequired()
                .HasMaxLength(384);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.RefreshToken)
                .HasMaxLength(384);
            builder.Property(e => e.TokenCreated);
            builder.Property(e => e.TokenExpires);
        }
    }
}
    
