//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 18/6/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.HeThong;

namespace GS.Data.Mapping.HeThong
{
    public partial class DinhMucMap: GSEntityTypeConfiguration<DinhMuc>
	{
     public override void Configure(EntityTypeBuilder<DinhMuc> builder)
        { 
            builder.ToTable("QL_DINH_MUC");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
            
        }	
	}
}
