//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 28/5/2021
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.TaiSans;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanKhauHaoMap: GSEntityTypeConfiguration<TaiSanKhauHao>
	{
     public override void Configure(EntityTypeBuilder<TaiSanKhauHao> builder)
        { 
            builder.ToTable("TS_TAI_SAN_KHAU_HAO");
            builder.HasKey(c => c.ID);            
            base.Configure(builder);
        }	
	}
}
