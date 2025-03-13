using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class DonViLichSuMap : GSEntityTypeConfiguration<DonViLichSu>
    {
        public override void Configure(EntityTypeBuilder<DonViLichSu> builder)
        {
            builder.ToTable("DM_DON_VI_LICH_SU");
            builder.HasKey(c => c.ID);
            //builder.HasOne(c => c.DON_VI).WithMany().HasForeignKey(c => c.DON_VI_ID);
            base.Configure(builder);
        }
    }
}
