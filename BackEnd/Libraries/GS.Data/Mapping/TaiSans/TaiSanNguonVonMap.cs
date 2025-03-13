//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanNguonVonMap : GSEntityTypeConfiguration<TaiSanNguonVon>
    {
        public override void Configure(EntityTypeBuilder<TaiSanNguonVon> builder)
        {
            builder.ToTable("TS_TAI_SAN_NGUON_VON");
            builder.HasKey(c => new { c.TAI_SAN_ID, c.NGUON_VON_ID, c.BIEN_DONG_ID });
            builder.Property(mapping => mapping.NGUON_VON_ID).HasColumnName("NGUON_VON_ID");
            builder.Property(mapping => mapping.TAI_SAN_ID).HasColumnName("TAI_SAN_ID");
            builder.Property(mapping => mapping.BIEN_DONG_ID).HasColumnName("BIEN_DONG_ID");

            builder.HasOne(m => m.taisan)
                .WithMany()
                .HasForeignKey(m => m.TAI_SAN_ID);
            builder.HasOne(m => m.nguonvon)
                .WithMany()
                .HasForeignKey(m => m.NGUON_VON_ID);
            builder.HasOne(m => m.biendong)
               .WithMany()
               .HasForeignKey(m => m.BIEN_DONG_ID);
            builder.Ignore(c => c.ID);
            base.Configure(builder);
        }
    }
}
