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
    public partial class LoaiDonViMap : GSEntityTypeConfiguration<LoaiDonVi>
    {
        public override void Configure(EntityTypeBuilder<LoaiDonVi> builder)
        {
            builder.ToTable("DM_LOAI_DON_VI");
            builder.HasKey(c => c.ID);
            base.Configure(builder);
        }
    }
}
