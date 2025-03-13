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
    public partial class QuocGiaMap : GSEntityTypeConfiguration<QuocGia>
    {
        public override void Configure(EntityTypeBuilder<QuocGia> builder)
        {
            builder.ToTable("DM_QUOC_GIA");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
