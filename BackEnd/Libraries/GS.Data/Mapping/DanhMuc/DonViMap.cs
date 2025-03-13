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
    public partial class DonViMap : GSEntityTypeConfiguration<DonVi>
    {
        public override void Configure(EntityTypeBuilder<DonVi> builder)
        {
            builder.ToTable("DM_DON_VI");
            builder.HasKey(c => c.ID);
            builder.HasOne(m => m.DonViCha)
                    .WithMany()
                    .HasForeignKey(m => m.PARENT_ID);
            builder.HasOne(m => m.LoaiDonVi)
                 .WithMany()
                 .HasForeignKey(m => m.LOAI_DON_VI_ID);
            base.Configure(builder);
        }
    }
}
