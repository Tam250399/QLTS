using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class QuyenVaiTroMap : GSEntityTypeConfiguration<QuyenVaiTroMapping>
    {
        public override void Configure(EntityTypeBuilder<QuyenVaiTroMapping> builder)
        {
            builder.ToTable("QL_QUYEN_VAI_TRO");
            builder.HasKey(c => new { c.VAI_TRO_ID, c.QUYEN_ID });
            builder.Property(mapping => mapping.QUYEN_ID).HasColumnName("QUYEN_ID");
            builder.Property(mapping => mapping.VAI_TRO_ID).HasColumnName("VAI_TRO_ID");

            builder.HasOne(m => m.quyen)
                .WithMany()
                .HasForeignKey(m => m.QUYEN_ID);

            builder.HasOne(m => m.vaiTro)
                .WithMany()
                .HasForeignKey(m => m.VAI_TRO_ID);

            builder.Ignore(mapping => mapping.ID);

            base.Configure(builder);
        }
    }
}
