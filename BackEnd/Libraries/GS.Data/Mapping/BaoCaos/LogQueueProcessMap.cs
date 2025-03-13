//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/6/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.BaoCaos;

namespace GS.Data.Mapping.BaoCaos
{
    public partial class LogQueueProcessMap: GSEntityTypeConfiguration<LogQueueProcess>
	{
     public override void Configure(EntityTypeBuilder<LogQueueProcess> builder)
        { 
            builder.ToTable("BC_LOG_QUEUE_PROCESS");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
            
        }	
	}
}
