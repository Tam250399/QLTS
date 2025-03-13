using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.BaoCaoDienTu;

namespace GS.Data.Mapping.BaoCaoDienTus
{
    public partial class BaoCaoDienTuMap : GSEntityTypeConfiguration<BaoCaoDienTu>
    {
        public override void Configure(EntityTypeBuilder<BaoCaoDienTu> builder)
        {
            builder.ToTable("BC_DIEN_TU");
            builder.HasKey(c => c.ID);
            builder.Ignore(c => c.TenTrangThai);
            builder.HasOne(t => t.donvi)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_ID);
            base.Configure(builder);
        }
    }
}