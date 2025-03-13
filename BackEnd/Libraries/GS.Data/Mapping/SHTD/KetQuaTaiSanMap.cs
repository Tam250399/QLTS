using GS.Core.Domain.SHTD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.SHTD
{
    public partial class KetQuaTaiSanMap : GSEntityTypeConfiguration<KetQuaTaiSan>
    {
        public override void Configure(EntityTypeBuilder<KetQuaTaiSan> builder)
        {
            builder.ToTable("SHTD_KET_QUA_TAI_SAN");
            builder.HasKey(c => c.ID);
            builder.HasOne(c => c.xulyketqua).WithMany().HasForeignKey(c => c.XU_LY_KET_QUA_ID);
            builder.HasOne(c => c.taisantd).WithMany().HasForeignKey(c => c.TAI_SAN_TD_ID);
            builder.HasOne(c => c.taisantdxuly).WithMany().HasForeignKey(c => c.TAI_SAN_TD_XU_LY_ID);
            base.Configure(builder);
        }
    }
}
