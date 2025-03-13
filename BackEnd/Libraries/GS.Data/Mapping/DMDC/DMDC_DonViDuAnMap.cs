//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DMDC;

namespace GS.Data.Mapping.DMDC
{
    public partial class DMDC_DonViDuAnMap: GSEntityTypeConfiguration<DMDC_DonViDuAn>
	{
     public override void Configure(EntityTypeBuilder<DMDC_DonViDuAn> builder)
        { 
            builder.ToTable("DMDC_DON_VI_DU_AN");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
