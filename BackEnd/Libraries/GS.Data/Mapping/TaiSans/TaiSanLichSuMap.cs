using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanLichSuMap : GSEntityTypeConfiguration<TaiSanLichSu>
    {
        public override void Configure(EntityTypeBuilder<TaiSanLichSu> builder)
        {
            builder.ToTable("TS_TAI_SAN_LICH_SU");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
