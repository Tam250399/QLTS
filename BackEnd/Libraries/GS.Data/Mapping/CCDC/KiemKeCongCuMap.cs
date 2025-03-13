//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class KiemKeCongCuMap: GSEntityTypeConfiguration<KiemKeCongCu>
	{
     public override void Configure(EntityTypeBuilder<KiemKeCongCu> builder)
        { 
            builder.ToTable("CCDC_KIEM_KE_CONG_CU");
            builder.HasKey(c => c.ID);

            builder.HasOne(m => m.CongCu)
                    .WithMany()
                    .HasForeignKey(m => m.CONG_CU_ID);

            builder.HasOne(m => m.XuatNhap)
                    .WithMany()
                    .HasForeignKey(m => m.XUAT_NHAP_ID);

            builder.HasOne(m => m.KiemKe)
                    .WithMany()
                    .HasForeignKey(m => m.KIEM_KE_ID);

            base.Configure(builder);
        }	
	}
}
