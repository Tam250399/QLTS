//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 6/10/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DMDC;

namespace GS.Data.Mapping.DMDC
{
    public partial class DMDC_ToChucNganSachMap: GSEntityTypeConfiguration<DMDC_ToChucNganSach>
	{
     public override void Configure(EntityTypeBuilder<DMDC_ToChucNganSach> builder)
        { 
            builder.ToTable("DMDC_TO_CHUC_NGAN_SACH");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
