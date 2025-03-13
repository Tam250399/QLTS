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
    public partial class YeuCauNhatKyMap : GSEntityTypeConfiguration<YeuCauNhatKy>
    {
        public override void Configure(EntityTypeBuilder<YeuCauNhatKy> builder)
        {
            builder.ToTable("NV_YEU_CAU_NHAT_KY");
            builder.HasKey(c => c.ID);

            //builder.HasOne(t => t.yeucau)
            //.WithMany()
            //.HasForeignKey(t => t.YEU_CAU_ID);

            builder.HasOne(t => t.nguoidung)
            .WithMany()
            .HasForeignKey(t => t.NGUOI_TAO_ID);

            builder.HasOne(t => t.donvi)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_ID);

            builder.HasOne(t => t.donvibophan)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_BO_PHAN_ID);

            builder.Ignore(c => c.LoaiNhatKy);
            base.Configure(builder);
        }
    }
}
