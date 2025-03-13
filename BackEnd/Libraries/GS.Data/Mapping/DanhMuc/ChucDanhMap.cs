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
    public partial class ChucDanhMap : GSEntityTypeConfiguration<ChucDanh>
    {
        public override void Configure(EntityTypeBuilder<ChucDanh> builder)
        {
            builder.ToTable("DM_CHUC_DANH");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
