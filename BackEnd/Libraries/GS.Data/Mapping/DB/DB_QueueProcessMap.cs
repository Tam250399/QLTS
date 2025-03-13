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
    public partial class DB_QueueProcessMap: GSEntityTypeConfiguration<DB_QueueProcess>
	{
     public override void Configure(EntityTypeBuilder<DB_QueueProcess> builder)
        { 
            builder.ToTable("DB_QUEUE_PROCESS");
            builder.HasKey(c => c.ID);
            builder.Property(p => p.DATA_INPUT).HasColumnType("CLOB");
            builder.Property(p => p.API_RESPONSE).HasColumnType("CLOB");
            base.Configure(builder);
        }	
	}
}
