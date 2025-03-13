//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class MuaSamMap: GSEntityTypeConfiguration<MuaSam>
	{
     public override void Configure(EntityTypeBuilder<MuaSam> builder)
        { 
            builder.ToTable("TS_MUA_SAM");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
