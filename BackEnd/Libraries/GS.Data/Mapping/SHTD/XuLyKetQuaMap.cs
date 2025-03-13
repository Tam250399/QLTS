using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.SHTD;

namespace GS.Data.Mapping.SHTD
{
    public partial class XuLyKetQuaMap : GSEntityTypeConfiguration<XuLyKetQua>
    {
        public override void Configure(EntityTypeBuilder<XuLyKetQua> builder)
        {
            builder.ToTable("SHTD_XU_LY_KET_QUA");
            builder.HasKey(c => c.ID);
            //builder.HasOne(m => m.xuly).WithMany().HasForeignKey(m => m.XU_LY_ID);
            builder.HasOne(m => m.xulyTD).WithMany().HasForeignKey(m => m.XU_LY_ID);
            base.Configure(builder);
        }
    }
}
