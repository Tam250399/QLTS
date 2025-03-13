//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.CCDC;

namespace GS.Data.Mapping.CCDC
{
    public partial class XuatNhapMap: GSEntityTypeConfiguration<XuatNhap>
	{
     public override void Configure(EntityTypeBuilder<XuatNhap> builder)
        { 
            builder.ToTable("CCDC_XUAT_NHAP");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
