//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 12/12/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DB;

namespace GS.Data.Mapping.DB
{
    public partial class DB_QueueProcessHistoryMap: GSEntityTypeConfiguration<DB_QueueProcessHistory>
	{
     public override void Configure(EntityTypeBuilder<DB_QueueProcessHistory> builder)
        { 
            builder.ToTable("DB_QUEUE_PROCESS_HISTORY");
            builder.HasKey(c => c.ID);
            builder.Property(p => p.RESPONSE).HasColumnType("CLOB");
            base.Configure(builder);
        }	
	}
}
