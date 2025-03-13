//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0
// Template create : GS
// Create date     : 4/3/2020
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
	public partial class PhuongAnXuLyMap : GSEntityTypeConfiguration<PhuongAnXuLy>
	{
		public override void Configure(EntityTypeBuilder<PhuongAnXuLy> builder)
		{
			builder.ToTable("DM_PHUONG_AN_XU_LY");
			builder.HasKey(c => c.ID);
			base.Configure(builder);
		}
	}
}