//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 5/6/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.KT;

namespace GS.Data.Mapping.KT
{
    public partial class KhauHaoTaiSanMap: GSEntityTypeConfiguration<KhauHaoTaiSan>
	{
     public override void Configure(EntityTypeBuilder<KhauHaoTaiSan> builder)
        { 
            builder.ToTable("KT_KHAU_HAO_TAI_SAN");
            builder.HasKey(c => c.ID);
            builder.HasOne(t => t.TaiSan)
            .WithMany()
            .HasForeignKey(t => t.TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
