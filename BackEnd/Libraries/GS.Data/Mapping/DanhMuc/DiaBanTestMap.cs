//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 10/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class DiaBanTestMap: GSEntityTypeConfiguration<DiaBanTest>
	{
     public override void Configure(EntityTypeBuilder<DiaBanTest> builder)
        { 
            builder.ToTable("DM_DIA_BAN_TEST");
            builder.HasKey(c => c.ID);
            builder.HasOne(c => c.QuocGia).WithMany().HasForeignKey(c => c.QUOC_GIA_ID);
            builder.HasOne(c => c.DiaBanTestCha).WithMany().HasForeignKey(c => c.PARENT_ID);
            base.Configure(builder);
        }	
	}
}
