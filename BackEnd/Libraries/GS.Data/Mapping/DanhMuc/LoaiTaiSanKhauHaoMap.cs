//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 4/6/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class LoaiTaiSanKhauHaoMap: GSEntityTypeConfiguration<LoaiTaiSanKhauHao>
	{
     public override void Configure(EntityTypeBuilder<LoaiTaiSanKhauHao> builder)
        { 
            builder.ToTable("DM_LOAI_TAI_SAN_KHAU_HAO");
            builder.Ignore(c => c.ID);
            builder.HasKey(c => new { c.DON_VI_ID, c.LOAI_TAI_SAN_ID });
            base.Configure(builder);
        }	
	}
}
