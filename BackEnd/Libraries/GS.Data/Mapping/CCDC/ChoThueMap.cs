//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 11/2/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class ChoThueMap: GSEntityTypeConfiguration<ChoThue>
	{
     public override void Configure(EntityTypeBuilder<ChoThue> builder)
        { 
            builder.ToTable("CCDC_CHO_THUE");
            builder.HasKey(c => c.ID);

            builder.HasOne(m => m.CongCu)
                    .WithMany()
                    .HasForeignKey(m => m.CONG_CU_ID);

            base.Configure(builder);
        }	
	}
}
