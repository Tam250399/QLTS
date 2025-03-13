using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.BaoCaoDoiChieus;

namespace GS.Data.Mapping.BaoCaoDoiChieus
{
    public partial class BaoCaoDoiChieuMap : GSEntityTypeConfiguration<BaoCaoDoiChieu>
    {
        public override void Configure(EntityTypeBuilder<BaoCaoDoiChieu> builder)
        {
            builder.ToTable("BC_BAO_CAO_DOI_CHIEU");
            builder.HasKey(c => c.ID);
            builder.Ignore(c => c.PhanMem);
            builder.Ignore(c => c.PhanBaoCao);
            base.Configure(builder);
        }
    }
}