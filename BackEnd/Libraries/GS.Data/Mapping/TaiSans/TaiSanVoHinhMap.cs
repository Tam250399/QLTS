//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 16/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class TaiSanVoHinhMap : GSEntityTypeConfiguration<TaiSanVoHinh>
    {
        public override void Configure(EntityTypeBuilder<TaiSanVoHinh> builder)
        {
            builder.ToTable("TS_TAI_SAN_VO_HINH");
            builder.HasKey(c => c.TAI_SAN_ID);
            builder.HasOne(t => t.taisan)
               .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);

            builder.Ignore(c => c.ID);
            base.Configure(builder);
        }
    }
}
