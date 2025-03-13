//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class NguonGocTaiSanMap: GSEntityTypeConfiguration<NguonGocTaiSan>
	{
     public override void Configure(EntityTypeBuilder<NguonGocTaiSan> builder)
        { 
            builder.ToTable("DM_NGUON_GOC_TAI_SAN");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
