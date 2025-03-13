using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class NguoiDungDonViMap : GSEntityTypeConfiguration<NguoiDungDonViMapping>
    {
        public override void Configure(EntityTypeBuilder<NguoiDungDonViMapping> builder)
        {
            builder.ToTable("QL_NGUOI_DUNG_DON_VI");
            builder.HasKey(c => new { c.NGUOI_DUNG_ID, c.DON_VI_ID });
            builder.Property(mapping => mapping.NGUOI_DUNG_ID).HasColumnName("NGUOI_DUNG_ID");
            builder.Property(mapping => mapping.DON_VI_ID).HasColumnName("DON_VI_ID");

            builder.HasOne(m => m.nguoiDung)
                .WithMany()
                .HasForeignKey(m => m.NGUOI_DUNG_ID);
            builder.HasOne(m => m.donvi)
                .WithMany()
                .HasForeignKey(m => m.DON_VI_ID);

            builder.Ignore(mapping => mapping.ID);

            base.Configure(builder);
        }
    }
}
