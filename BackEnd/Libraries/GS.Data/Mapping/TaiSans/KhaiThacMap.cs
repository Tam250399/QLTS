//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class KhaiThacMap: GSEntityTypeConfiguration<KhaiThac>
	{
     public override void Configure(EntityTypeBuilder<KhaiThac> builder)
        { 
            builder.ToTable("TS_KHAI_THAC");
            builder.HasKey(c => c.ID);            

            //builder.Ignore(mapping => mapping.ID);
            base.Configure(builder);
        }	
	}
}
