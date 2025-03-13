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
    public partial class DoiTacMap : GSEntityTypeConfiguration<DoiTac>
    {
        public override void Configure(EntityTypeBuilder<DoiTac> builder)
        {
            builder.ToTable("DM_DOI_TAC");
            builder.HasKey(c => c.ID);


            builder.HasOne(t => t.donvi)
              .WithMany()
              .HasForeignKey(t => t.DON_VI_ID);
            builder.Ignore(c => c.LoaiDoiTac_enum);
            base.Configure(builder);
        }
    }
}
