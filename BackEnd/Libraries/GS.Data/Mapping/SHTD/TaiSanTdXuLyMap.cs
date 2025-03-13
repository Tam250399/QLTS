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
    public partial class TaiSanTdXuLyMap: GSEntityTypeConfiguration<TaiSanTdXuLy>
	{
     public override void Configure(EntityTypeBuilder<TaiSanTdXuLy> builder)
        { 
            builder.ToTable("SHTD_TAI_SAN_TD_XU_LY");
            builder.HasKey(c =>c.ID);
            builder.HasOne(c => c.xuly).WithMany().HasForeignKey(c => c.XU_LY_ID);
            builder.HasOne(c => c.taisantd).WithMany().HasForeignKey(c => c.TAI_SAN_ID);
            builder.HasOne(c => c.hinhthucxuly).WithMany().HasForeignKey(c => c.HINH_THUC_XU_LY_ID);
            base.Configure(builder);
        }	
	}
}
