//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class BaoCaoMap: GSEntityTypeConfiguration<BaoCao>
	{
     public override void Configure(EntityTypeBuilder<BaoCao> builder)
        { 
            builder.ToTable("DM_BAO_CAO");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
