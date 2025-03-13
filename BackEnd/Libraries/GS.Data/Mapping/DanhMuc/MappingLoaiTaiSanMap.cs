using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.DanhMuc
{
   public partial class MappingLoaiTaiSanMap: GSEntityTypeConfiguration<MappingLoaiTaiSan>
    {
        public override void Configure(EntityTypeBuilder<MappingLoaiTaiSan> builder)
        {
            builder.ToTable("DM_LOAI_TAI_SAN_MAP");
            builder.HasKey(c => c.ID);
            builder.Ignore(m => m.NewLoaiTaiSan);
            builder.Ignore(m => m.OldLoaiTaiSan);
            builder.HasOne(m => m.OldLoaiTaiSan)
                .WithMany()
                .HasForeignKey(m => m.OLD_LOAI_TAI_SAN_ID);
            builder.HasOne(m => m.NewLoaiTaiSan)
                .WithMany()
                .HasForeignKey(m => m.NEW_LOAI_TAI_SAN_ID);
            base.Configure(builder);
        }
    }
}
