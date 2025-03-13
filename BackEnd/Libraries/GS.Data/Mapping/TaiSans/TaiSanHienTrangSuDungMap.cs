using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.TaiSans
{
	public partial class TaiSanHienTrangSuDungMap : GSEntityTypeConfiguration<TaiSanHienTrangSuDung>
	{
		public override void Configure(EntityTypeBuilder<TaiSanHienTrangSuDung> builder)
		{
			builder.ToTable("TS_TAI_SAN_HIEN_TRANG_SU_DUNG");
			builder.HasKey(c => c.ID);
			builder.HasOne(t => t.TaiSan)
			   .WithMany()
			   .HasForeignKey(t => t.TAI_SAN_ID);
			builder.HasOne(t => t.BienDong)
			   .WithMany()
			   .HasForeignKey(t => t.BIEN_DONG_ID);
            builder.HasOne(t => t.HienTrang)
               .WithMany()
               .HasForeignKey(t => t.HIEN_TRANG_ID);
            base.Configure(builder);
		}
	}
}