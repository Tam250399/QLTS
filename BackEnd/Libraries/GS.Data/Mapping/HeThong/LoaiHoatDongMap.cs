using GS.Core.Domain.HeThong;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.HeThong
{
    public partial class LoaiHoatDongMap : GSEntityTypeConfiguration<LoaiHoatDong>
    {
        public override void Configure(EntityTypeBuilder<LoaiHoatDong> builder)
        {
            builder.ToTable("QL_LOAI_HOAT_DONG");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
