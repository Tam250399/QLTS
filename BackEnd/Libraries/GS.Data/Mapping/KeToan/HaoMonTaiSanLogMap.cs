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
    public partial class HaoMonTaiSanLogMap: GSEntityTypeConfiguration<HaoMonTaiSanLog>
	{
     public override void Configure(EntityTypeBuilder<HaoMonTaiSanLog> builder)
        { 
            builder.ToTable("KT_HAO_MON_TAI_SAN_LOG");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
