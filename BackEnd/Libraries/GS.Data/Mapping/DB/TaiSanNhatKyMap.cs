//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DB;

namespace GS.Data.Mapping.DB
{
    public partial class TaiSanNhatKyMap: GSEntityTypeConfiguration<TaiSanNhatKy>
	{
     public override void Configure(EntityTypeBuilder<TaiSanNhatKy> builder)
        { 
            builder.ToTable("DB_TAI_SAN_NHAT_KY");
            builder.HasKey(c => c.ID);
            builder.HasOne(c => c.Taisan).WithMany().HasForeignKey(m => m.TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
