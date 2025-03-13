using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class HoatDongMap : GSEntityTypeConfiguration<HoatDong>
    {
        public override void Configure(EntityTypeBuilder<HoatDong> builder)
        {
            builder.ToTable("QL_HOAT_DONG");
            builder.HasKey(c => c.ID);
            builder.HasOne(m => m.loaihoatdong)
                .WithMany()
                .HasForeignKey(m => m.LOAI_HOAT_DONG_ID);
            builder.HasOne(m => m.nguoidung)
                .WithMany()
                .HasForeignKey(m => m.NGUOI_DUNG_ID);
            builder.Property(c => c.DU_LIEU).HasColumnType("CLOB");
            base.Configure(builder);
        }
    }
}
