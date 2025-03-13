using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Data.Mapping.TaiSans
{
	public partial class ImportExcelTaiSanMap : GSEntityTypeConfiguration<ImportExcelTaiSan>
	{
		public override void Configure(EntityTypeBuilder<ImportExcelTaiSan> builder)
		{
			builder.ToTable("TS_IMPORT_TAI_SAN");
			builder.HasKey(c => c.ID);

			//builder.Ignore(mapping => mapping.ID);
			base.Configure(builder);
		}
	}
}
