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
    public partial class XuLyMap: GSEntityTypeConfiguration<XuLy>
	{
     public override void Configure(EntityTypeBuilder<XuLy> builder)
        { 
            builder.ToTable("SHTD_XU_LY");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
