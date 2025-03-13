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
    public partial class TaiSanDatMap : GSEntityTypeConfiguration<TaiSanDat>
    {
        public override void Configure(EntityTypeBuilder<TaiSanDat> builder)
        {
            builder.ToTable("TS_TAI_SAN_DAT");
            builder.HasKey(c => c.TAI_SAN_ID);
            builder.HasOne(t => t.taisan)
               .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);
            builder.HasOne(t => t.diaban)
               .WithMany()
               .HasForeignKey(t => t.DIA_BAN_ID);

            builder.Ignore(c => c.ID);
            base.Configure(builder);
        }
    }
}
