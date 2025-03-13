//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanNhaMap : GSEntityTypeConfiguration<TaiSanNha>
    {
        public override void Configure(EntityTypeBuilder<TaiSanNha> builder)
        {
            builder.ToTable("TS_TAI_SAN_NHA");
            builder.HasKey(c => c.TAI_SAN_ID);
            builder.HasOne(t => t.taisan)
               .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);

            builder.Ignore(c => c.ID);
            base.Configure(builder);
        }
    }
}
