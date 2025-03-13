//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class CongCuDonViMap: GSEntityTypeConfiguration<CongCuDonVi>
	{
     public override void Configure(EntityTypeBuilder<CongCuDonVi> builder)
        { 
            builder.ToTable("CCDC_CONG_CU_DON_VI");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
