using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.TaiSans
{
	public partial class KhaiThacChiTietMap : GSEntityTypeConfiguration<KhaiThacChiTiet>
	{
		public override void Configure(EntityTypeBuilder<KhaiThacChiTiet> builder)
		{
			builder.ToTable("TS_KHAI_THAC_CHI_TIET");
			builder.HasKey(c => c.ID);

			//builder.Ignore(mapping => mapping.ID);
			base.Configure(builder);
		}
	}
}
