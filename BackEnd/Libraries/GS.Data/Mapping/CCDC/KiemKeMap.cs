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
    public partial class KiemKeMap: GSEntityTypeConfiguration<KiemKe>
	{
     public override void Configure(EntityTypeBuilder<KiemKe> builder)
        { 
            builder.ToTable("CCDC_KIEM_KE");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
