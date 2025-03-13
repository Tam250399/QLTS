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
    public partial class DuAnMap : GSEntityTypeConfiguration<DuAn>
    {
        public override void Configure(EntityTypeBuilder<DuAn> builder)
        {
            builder.ToTable("DM_DU_AN");
            builder.HasKey(c => c.ID);
            builder.HasOne(m => m.donVi)
                    .WithMany()
                    .HasForeignKey(m => m.DON_VI_ID);
            base.Configure(builder);
        }
    }
}
