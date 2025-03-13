//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 22/3/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DB;

namespace GS.Data.Mapping.DB
{
    public partial class LogsDongBoCsdlqgMap: GSEntityTypeConfiguration<LogsDongBoCsdlqg>
	{
     public override void Configure(EntityTypeBuilder<LogsDongBoCsdlqg> builder)
        { 
            builder.ToTable("DB_LOGS_DONG_BO_CSDLQG");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
