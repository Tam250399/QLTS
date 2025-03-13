//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class SuaChuaBaoDuongMap: GSEntityTypeConfiguration<SuaChuaBaoDuong>
	{
     public override void Configure(EntityTypeBuilder<SuaChuaBaoDuong> builder)
        { 
            builder.ToTable("CCDC_SUA_CHUA_BAO_DUONG");
            builder.HasKey(c => c.ID);

            builder.HasOne(m => m.CongCu)
                    .WithMany()
                    .HasForeignKey(m => m.CONG_CU_ID);
            builder.HasOne(m => m.XuatNhap)
                    .WithMany()
                    .HasForeignKey(m => m.NHAP_XUAT_ID);

            base.Configure(builder);
        }	
	}
}
