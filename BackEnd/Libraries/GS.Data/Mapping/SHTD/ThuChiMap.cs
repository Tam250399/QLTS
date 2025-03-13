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
    public partial class ThuChiMap: GSEntityTypeConfiguration<ThuChi>
	{
     public override void Configure(EntityTypeBuilder<ThuChi> builder)
        { 
            builder.ToTable("SHTD_THU_CHI");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.ketQua)
              .WithMany()
              .HasForeignKey(t => t.KET_QUA_ID);
            builder.HasOne(t => t.xuLy)
            .WithMany()
            .HasForeignKey(t => t.XU_LY_ID);
            base.Configure(builder);
        }	
	}
}
