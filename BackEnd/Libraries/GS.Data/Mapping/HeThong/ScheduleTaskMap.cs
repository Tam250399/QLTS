//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/6/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.HeThong;

namespace GS.Data.Mapping.HeThong
{
    public partial class ScheduleTaskMap: GSEntityTypeConfiguration<ScheduleTask>
	{
     public override void Configure(EntityTypeBuilder<ScheduleTask> builder)
        { 
            builder.ToTable("QL_SCHEDULE_TASK");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
            
        }	
	}
}
