//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanChoThueMap: GSEntityTypeConfiguration<TaiSanChoThue>
	{
     public override void Configure(EntityTypeBuilder<TaiSanChoThue> builder)
        { 
            builder.ToTable("TS_TAI_SAN_CHO_THUE");
            builder.HasKey(c => c.ID);

            builder.HasOne(t => t.nguoidung)
               .WithMany()
               .HasForeignKey(t => t.NGUOI_TAO_ID);

            builder.HasOne(t => t.doitac)
               .WithMany()
               .HasForeignKey(t => t.DOI_TAC_ID);
            builder.HasOne(t => t.taisan)
               .WithMany()
               .HasForeignKey(t => t.TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
