using Microservice.Consumer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservice.Consumer.Infra.Data.Mappings
{
    public class EntryMap : BaseMap<Entry>
    {
        public override void Configure(EntityTypeBuilder<Entry> builder)
        {
            base.Configure(builder);

            builder.ToTable("TAB_ENTRY");

            builder.Property(x => x.Id)
                .HasColumnName("ID")
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(x => x.UserName)
                .HasColumnName("USER_NM")
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.Moment)
                .HasColumnName("ENTRY_DT")
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(x => x.Value)
                .HasColumnName("ENTRY_VL")
                .HasColumnType("DECIMAL(10,2)")
                .IsRequired();

            builder.Property(x => x.Type)
                .HasColumnName("ENTRY_TP")
                .HasColumnType("CHAR")
                .IsRequired();

            builder.Property(x => x.AccountDescription)
                .HasColumnName("ACCOUNT_DS")
                .HasColumnType("VARCHAR(20)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("ENTRY_DS")
                .HasColumnType("VARCHAR(100)");
        }
    }
}
