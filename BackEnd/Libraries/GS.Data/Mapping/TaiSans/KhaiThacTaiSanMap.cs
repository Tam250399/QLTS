//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/7/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class KhaiThacTaiSanMap: GSEntityTypeConfiguration<KhaiThacTaiSan>
	{
     public override void Configure(EntityTypeBuilder<KhaiThacTaiSan> builder)
        { 
            builder.ToTable("TS_KHAI_THAC_TAI_SAN");
            //builder.HasKey(c => c.ID);
            builder.HasKey(c => c.ID);
            builder.Property(mapping => mapping.KHAI_THAC_ID).HasColumnName("KHAI_THAC_ID");
            builder.HasOne(m => m.khaiThac)
                .WithMany()
                .HasForeignKey(m => m.KHAI_THAC_ID);
            builder.Property(mapping => mapping.TAI_SAN_ID).HasColumnName("TAI_SAN_ID");
            builder.HasOne(m => m.taiSan)
                .WithMany()
                .HasForeignKey(m => m.TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
