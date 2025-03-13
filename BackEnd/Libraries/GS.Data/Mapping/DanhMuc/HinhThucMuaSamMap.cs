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
    public partial class HinhThucMuaSamMap : GSEntityTypeConfiguration<HinhThucMuaSam>
    {
        public override void Configure(EntityTypeBuilder<HinhThucMuaSam> builder)
        {
            builder.ToTable("DM_HINH_THUC_MUA_SAM");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
