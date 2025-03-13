//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.NghiepVu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.NghiepVu
{
    public partial class YeuCauMap : GSEntityTypeConfiguration<YeuCau>
    {
        public override void Configure(EntityTypeBuilder<YeuCau> builder)
        {
            builder.ToTable("NV_YEU_CAU");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.loaitaisan)
            .WithMany()
            .HasForeignKey(t => t.LOAI_TAI_SAN_ID);

            builder.HasOne(t => t.taisan)
            .WithMany(ts => ts.YeuCaus)
            .HasForeignKey(t => t.TAI_SAN_ID);

            builder.HasOne(t => t.nguoidung)
            .WithMany()
            .HasForeignKey(t => t.NGUOI_TAO_ID);

            builder.HasOne(t => t.donvi)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_ID);

            builder.HasOne(t => t.lydobiendong)
            .WithMany()
            .HasForeignKey(t => t.LY_DO_BIEN_DONG_ID);
            builder.HasOne(t => t.donvibophan)
           .WithMany()
           .HasForeignKey(t => t.DON_VI_BO_PHAN_ID);

            builder.Ignore(c => c.TrangThaiYeuCau);
            base.Configure(builder);
        }
    }
}
