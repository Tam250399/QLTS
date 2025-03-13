//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class LyDoBienDongMap : GSEntityTypeConfiguration<LyDoBienDong>
    {
        public override void Configure(EntityTypeBuilder<LyDoBienDong> builder)
        {
            builder.ToTable("DM_LY_DO_BIEN_DONG");
            builder.HasKey(c => c.ID);

            builder.Ignore(c => c.loaiLyDoTangGiam);
            //builder.Ignore(c => c.LoaiHinhTaiSan);
            builder.HasOne(c => c.LoaiLyDoBienDong)
                .WithMany()
                .HasForeignKey(c => c.LOAI_LY_DO_BIEN_DONG_ID);
            base.Configure(builder);
        }
    }
}
