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
    public partial class TaiSanKiemKeMap: GSEntityTypeConfiguration<TaiSanKiemKe>
	{
     public override void Configure(EntityTypeBuilder<TaiSanKiemKe> builder)
        { 
            builder.ToTable("TS_TAI_SAN_KIEM_KE");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
