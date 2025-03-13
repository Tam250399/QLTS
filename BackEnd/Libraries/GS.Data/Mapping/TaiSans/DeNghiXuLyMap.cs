//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class DeNghiXuLyMap: GSEntityTypeConfiguration<DeNghiXuLy>
	{
     public override void Configure(EntityTypeBuilder<DeNghiXuLy> builder)
        { 
            builder.ToTable("TS_DE_NGHI_XU_LY");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
