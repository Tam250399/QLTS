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
    public partial class MucDichSuDungMap : GSEntityTypeConfiguration<MucDichSuDung>
    {
        public override void Configure(EntityTypeBuilder<MucDichSuDung> builder)
        {
            builder.ToTable("DM_MUC_DICH_SU_DUNG");
            builder.HasKey(c => c.ID);
            builder.Ignore(c => c.LoaiHinhTaiSan);
            base.Configure(builder);
        }
    }
}
