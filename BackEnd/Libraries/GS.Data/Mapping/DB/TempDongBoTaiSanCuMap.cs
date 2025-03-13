using GS.Core.Domain.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.DB
{
   public class TempDongBoTaiSanCuMap : GSEntityTypeConfiguration<TempDongBoTaiSanCu>
    {
        public override void Configure(EntityTypeBuilder<TempDongBoTaiSanCu> builder)
        {
            builder.ToTable("TEMP_DONGBO_DULIEU_TAISAN_CU");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
