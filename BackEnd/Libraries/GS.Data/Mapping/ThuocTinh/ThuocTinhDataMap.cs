//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.ThuocTinhs;

namespace GS.Data.Mapping.ThuocTinhs
{
    public partial class ThuocTinhDataMap: GSEntityTypeConfiguration<ThuocTinhData>
	{
     public override void Configure(EntityTypeBuilder<ThuocTinhData> builder)
        { 
            builder.ToTable("TT_THUOC_TINH_DATA");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }	
	}
}
