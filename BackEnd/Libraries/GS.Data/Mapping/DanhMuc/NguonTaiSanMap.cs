//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class NguonTaiSanMap : GSEntityTypeConfiguration<NguonTaiSan>
    {
        public override void Configure(EntityTypeBuilder<NguonTaiSan> builder)
        {
            builder.ToTable("DM_NGUON_TAI_SAN");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
