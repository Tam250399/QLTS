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
    public partial class KetQuaMap: GSEntityTypeConfiguration<KetQua>
	{
     public override void Configure(EntityTypeBuilder<KetQua> builder)
        { 
            builder.ToTable("SHTD_KET_QUA");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.taiSanTdXuLy)
              .WithMany()
              .HasForeignKey(t => t.TAI_SAN_TD_XU_LY_ID);
            builder.HasOne(t => t.donVi)
              .WithMany()
              .HasForeignKey(t => t.DON_VI_CHUYEN_ID);
            base.Configure(builder);
        }	
	}
}
