//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 21/7/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class MuaSamChiTietMap: GSEntityTypeConfiguration<MuaSamChiTiet>
	{
     public override void Configure(EntityTypeBuilder<MuaSamChiTiet> builder)
        { 
            builder.ToTable("TS_MUA_SAM_CHI_TIET");
            //builder.HasKey(c => c.ID);
            builder.HasKey(c => new { c.ID });
            builder.Property(mapping => mapping.MUA_SAM_ID).HasColumnName("MUA_SAM_ID");
            builder.HasOne(m => m.muaSam)
                .WithMany()
                .HasForeignKey(m => m.MUA_SAM_ID);
            base.Configure(builder);
        }	
	}
}
