//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.ThuocTinhs;

namespace GS.Data.Mapping.ThuocTinhs
{
    public partial class ThuocTinhTaiSanMap: GSEntityTypeConfiguration<ThuocTinhTaiSan>
	{
     public override void Configure(EntityTypeBuilder<ThuocTinhTaiSan> builder)
        { 
            builder.ToTable("TT_THUOC_TINH_TAI_SAN");
            builder.HasKey(c => new { c.THUOC_TINH_ID, c.LOAI_HINH_TAI_SAN_ID,c.LOAI_TAI_SAN_ID });
            builder.Property(mapping => mapping.THUOC_TINH_ID).HasColumnName("THUOC_TINH_ID");
            builder.Property(mapping => mapping.LOAI_HINH_TAI_SAN_ID).HasColumnName("LOAI_HINH_TAI_SAN_ID");
            builder.Property(mapping => mapping.LOAI_TAI_SAN_ID).HasColumnName("LOAI_TAI_SAN_ID");
            builder.HasOne(t => t.ThuocTinh)
               .WithMany()
               .HasForeignKey(t => t.THUOC_TINH_ID);
            builder.Ignore(mapping => mapping.ID);
            base.Configure(builder);
        }	
	}
}
