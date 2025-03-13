//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.HeThong;

namespace GS.Data.Mapping.HeThong
{
    public partial class WidgetMap: GSEntityTypeConfiguration<Widget>
	{
     public override void Configure(EntityTypeBuilder<Widget> builder)
        { 
            builder.ToTable("QL_WIDGET");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
