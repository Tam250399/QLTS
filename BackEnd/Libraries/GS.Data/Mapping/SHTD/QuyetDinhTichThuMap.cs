//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.SHTD;

namespace GS.Data.Mapping.SHTD
{
    public partial class QuyetDinhTichThuMap : GSEntityTypeConfiguration<QuyetDinhTichThu>
    {
        public override void Configure(EntityTypeBuilder<QuyetDinhTichThu> builder)
        {
            builder.ToTable("SHTD_QUYET_DINH_TICH_THU");
            builder.HasKey(c => c.ID);
            builder.HasOne(m => m.DonVi).WithMany().HasForeignKey(m => m.DON_VI_ID);
            builder.HasOne(m => m.NguonGocTaiSan).WithMany().HasForeignKey(m => m.NGUON_GOC_ID);
            builder.HasOne(m => m.BoNganhTinh).WithMany().HasForeignKey(m => m.CO_QUAN_BAN_HANH_ID);
            base.Configure(builder);
        }
    }
}
