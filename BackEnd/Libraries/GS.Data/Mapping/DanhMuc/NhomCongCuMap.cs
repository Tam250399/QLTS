//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/1/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class NhomCongCuMap: GSEntityTypeConfiguration<NhomCongCu>
	{
     public override void Configure(EntityTypeBuilder<NhomCongCu> builder)
        { 
            builder.ToTable("DM_NHOM_CONG_CU");
            builder.HasKey(c => c.ID);
			builder.HasOne(m => m.DON_VI_ENTITY)
				   .WithMany()
				   .HasForeignKey(m => m.DON_VI_ID);
			base.Configure(builder);
        }	
	}
}
