using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class VaiTroNguoiDungMap : GSEntityTypeConfiguration<VaiTroNguoiDungMapping>
    {
        public override void Configure(EntityTypeBuilder<VaiTroNguoiDungMapping> builder)
        {
            builder.ToTable("QL_VAI_TRO_NGUOI_DUNG");
            builder.HasKey(c => new { c.VAI_TRO_ID, c.NGUOI_DUNG_ID });
            builder.Property(mapping => mapping.NGUOI_DUNG_ID).HasColumnName("NGUOI_DUNG_ID");
            builder.Property(mapping => mapping.VAI_TRO_ID).HasColumnName("VAI_TRO_ID");

            builder.HasOne(m => m.nguoiDung)
                .WithMany(m => m.vaiTroNguoiDungMappings)
                .HasForeignKey(m => m.NGUOI_DUNG_ID);

            builder.HasOne(m => m.vaiTro)
                .WithMany()
                .HasForeignKey(m => m.VAI_TRO_ID);

            builder.Ignore(mapping => mapping.ID);

            base.Configure(builder);
        }
    }
}
