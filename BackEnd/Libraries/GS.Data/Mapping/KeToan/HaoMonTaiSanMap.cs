//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/5/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.KT;

namespace GS.Data.Mapping.KT
{
    public partial class HaoMonTaiSanMap: GSEntityTypeConfiguration<HaoMonTaiSan>
	{
     public override void Configure(EntityTypeBuilder<HaoMonTaiSan> builder)
        { 
            builder.ToTable("KT_HAO_MON_TAI_SAN");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.TaiSan)
            .WithMany()
            .HasForeignKey(t => t.TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
