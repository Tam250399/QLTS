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
    public partial class TaiSanKiemKeTaiSanMap: GSEntityTypeConfiguration<TaiSanKiemKeTaiSan>
	{
     public override void Configure(EntityTypeBuilder<TaiSanKiemKeTaiSan> builder)
        { 
            builder.ToTable("TS_TAI_SAN_KIEM_KE_TAI_SAN");
            builder.HasKey(c => c.ID);
            builder.HasOne(c => c.taiSan)
                .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);
            
            base.Configure(builder);
        }	
	}
}
