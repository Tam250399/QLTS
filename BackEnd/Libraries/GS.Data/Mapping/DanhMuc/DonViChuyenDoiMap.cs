//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 25/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class DonViChuyenDoiMap: GSEntityTypeConfiguration<DonViChuyenDoi>
	{
     public override void Configure(EntityTypeBuilder<DonViChuyenDoi> builder)
        { 
            builder.ToTable("DM_DON_VI_CHUYEN_DOI");
            builder.HasKey(c => c.ID);
            //builder.HasOne(c => c.DON_VI).WithMany().HasForeignKey(c => c.DON_VI_ID);
            base.Configure(builder);
        }	
	}
}
