//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class GiamHongmatMap: GSEntityTypeConfiguration<GiamHongmat>
	{
     public override void Configure(EntityTypeBuilder<GiamHongmat> builder)
        { 
            builder.ToTable("CCDC_GIAM_HONGMAT");

            builder.HasOne(m => m.CongCu)
                    .WithMany()
                    .HasForeignKey(m => m.CONG_CU_ID);
            builder.HasOne(m => m.XuatNhap)
                    .WithMany()
                    .HasForeignKey(m => m.NHAP_XUAT_ID);

            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
