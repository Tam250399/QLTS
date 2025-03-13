//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/11/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class DeNghiXuLyTaiSanMap : GSEntityTypeConfiguration<DeNghiXuLyTaiSan>
    {
        public override void Configure(EntityTypeBuilder<DeNghiXuLyTaiSan> builder)
        {
            builder.ToTable("TS_DE_NGHI_XU_LY_TAI_SAN");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
