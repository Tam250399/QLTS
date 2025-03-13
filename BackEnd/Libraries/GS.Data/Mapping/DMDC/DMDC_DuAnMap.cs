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
    public partial class DMDC_DuAnMap: GSEntityTypeConfiguration<DMDC_DuAn>
	{
     public override void Configure(EntityTypeBuilder<DMDC_DuAn> builder)
        { 
            builder.ToTable("DMDC_DU_AN");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
