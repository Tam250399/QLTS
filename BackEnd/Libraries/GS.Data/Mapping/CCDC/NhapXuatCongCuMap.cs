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
    public partial class NhapXuatCongCuMap: GSEntityTypeConfiguration<NhapXuatCongCu>
	{
     public override void Configure(EntityTypeBuilder<NhapXuatCongCu> builder)
        { 
            builder.ToTable("CCDC_NHAP_XUAT_CONG_CU");
            builder.HasKey(c => c.ID);

            builder.HasOne(m => m.XuatNhap)
                    .WithMany()
                    .HasForeignKey(m => m.NHAP_XUAT_ID);

            builder.HasOne(m => m.CongCu)
                    .WithMany()
                    .HasForeignKey(m => m.CONG_CU_ID);

            builder.Ignore(c => c.SoLuongCoThePhanBo);

            base.Configure(builder);
        }	
	}
}
