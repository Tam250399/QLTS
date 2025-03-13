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
    public partial class TaiSanOtoMap : GSEntityTypeConfiguration<TaiSanOto>
    {
        public override void Configure(EntityTypeBuilder<TaiSanOto> builder)
        {
            builder.ToTable("TS_TAI_SAN_OTO");
            builder.HasKey(c => c.TAI_SAN_ID);
            builder.HasOne(t => t.chucDanh)
               .WithMany()
               .HasForeignKey(t => t.CHUC_DANH_ID);
            builder.HasOne(t => t.taisan)
               .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);
            builder.HasOne(t => t.nhanxe)
              .WithMany()
              .HasForeignKey(t => t.NHAN_XE_ID);

            builder.Ignore(c => c.ID);
            base.Configure(builder);
        }
    }
}
