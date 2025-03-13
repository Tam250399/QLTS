//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 3/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DM;

namespace GS.Data.Mapping.DM
{
    public partial class LoaiLyDoBienDongMap: GSEntityTypeConfiguration<LoaiLyDoBienDong>
	{
     public override void Configure(EntityTypeBuilder<LoaiLyDoBienDong> builder)
        { 
            builder.ToTable("DM_LOAI_LY_DO_BIEN_DONG");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
