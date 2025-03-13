//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.SHTD;

namespace GS.Data.Mapping.SHTD
{
    public partial class TaiSanTdMap: GSEntityTypeConfiguration<TaiSanTd>
	{
     public override void Configure(EntityTypeBuilder<TaiSanTd> builder)
        { 
            builder.ToTable("SHTD_TAI_SAN_TD");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.quyetdinh)
               .WithMany()
               .HasForeignKey(t => t.QUYET_DINH_TICH_THU_ID);
            builder.HasOne(t => t.loaitaisan)
              .WithMany()
              .HasForeignKey(t => t.LOAI_TAI_SAN_ID);
            //builder.Ignore(t => t.quyetdinh);
            base.Configure(builder);
        }	
	}
}
