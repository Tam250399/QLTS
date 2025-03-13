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
    public partial class DongXeMap : GSEntityTypeConfiguration<DongXe>
    {
        public override void Configure(EntityTypeBuilder<DongXe> builder)
        {
            builder.ToTable("DM_DONG_XE");
            builder.HasKey(c => c.ID);

            builder.HasOne(m => m.NhanXe)
                    .WithMany()
                    .HasForeignKey(m => m.NHAN_XE_ID);

            base.Configure(builder);
        }
    }
}
