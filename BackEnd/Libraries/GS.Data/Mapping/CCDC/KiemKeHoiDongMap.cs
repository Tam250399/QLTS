//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class KiemKeHoiDongMap: GSEntityTypeConfiguration<KiemKeHoiDong>
	{
     public override void Configure(EntityTypeBuilder<KiemKeHoiDong> builder)
        { 
            builder.ToTable("CCDC_KIEM_KE_HOI_DONG");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
