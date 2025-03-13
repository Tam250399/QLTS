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
    public partial class HienTrangMap : GSEntityTypeConfiguration<HienTrang>
    {
        public override void Configure(EntityTypeBuilder<HienTrang> builder)
        {
            builder.ToTable("DM_HIEN_TRANG");
            builder.HasKey(c => c.ID);
            builder.Ignore(c => c.LoaiHinhTS);
            builder.Ignore(c => c.KieuDuLieu);
            base.Configure(builder);
        }
    }
}
