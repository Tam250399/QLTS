//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 7/12/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.SHTD;

namespace GS.Data.Mapping.SHTD
{
    public partial class NhatKyTaiSanToanDanMap: GSEntityTypeConfiguration<NhatKyTaiSanToanDan>
	{
     public override void Configure(EntityTypeBuilder<NhatKyTaiSanToanDan> builder)
        { 
            builder.ToTable("SHTD_NHAT_KY");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
