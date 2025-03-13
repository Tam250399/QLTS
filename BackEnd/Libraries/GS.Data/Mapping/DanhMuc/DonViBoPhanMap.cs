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
    public partial class DonViBoPhanMap : GSEntityTypeConfiguration<DonViBoPhan>
    {
        public override void Configure(EntityTypeBuilder<DonViBoPhan> builder)
        {
            builder.ToTable("DM_DON_VI_BO_PHAN");
            builder.HasKey(c => c.ID);

            builder.HasOne(c => c.DonViBoPhanCha)
                .WithMany()
                .HasForeignKey(c => c.PARENT_ID);
            builder.HasOne(c => c.DON_VI)
                .WithMany()
                .HasForeignKey(c => c.DON_VI_ID);

            base.Configure(builder);
        }
    }
}
