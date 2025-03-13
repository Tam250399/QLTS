//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanKiemKeHoiDongMap: GSEntityTypeConfiguration<TaiSanKiemKeHoiDong>
	{
     public override void Configure(EntityTypeBuilder<TaiSanKiemKeHoiDong> builder)
        { 
            builder.ToTable("TS_TAI_SAN_KIEM_KE_HOI_DONG");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
