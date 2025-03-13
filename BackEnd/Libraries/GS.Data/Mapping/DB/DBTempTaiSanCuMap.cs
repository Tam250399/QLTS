//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DB;

namespace GS.Data.Mapping.DB
{
    public partial class DBTempTaiSanCuMap: GSEntityTypeConfiguration<DBTempTaiSanCu>
	{
     public override void Configure(EntityTypeBuilder<DBTempTaiSanCu> builder)
        { 
            builder.ToTable("DB_TEMP_TAI_SAN_CU");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
