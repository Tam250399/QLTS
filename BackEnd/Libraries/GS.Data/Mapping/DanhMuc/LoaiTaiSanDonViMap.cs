//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/3/2020
//----------------------------------------------------------------------------------------------------------------------
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GS.Core.Domain.DanhMuc;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class LoaiTaiSanDonViMap: GSEntityTypeConfiguration<LoaiTaiSanDonVi>
	{
     public override void Configure(EntityTypeBuilder<LoaiTaiSanDonVi> builder)
        { 
            builder.ToTable("DM_LOAI_TAI_SAN_DON_VI");
            builder.HasKey(c => c.ID);
            builder.HasOne(c => c.DonVi).WithMany().HasForeignKey(c => c.DON_VI_ID);
            builder.HasOne(c => c.CheDoHaoMon).WithMany().HasForeignKey(c => c.CHE_DO_HAO_MON_ID);
            builder.HasOne(c => c.LoaiTaiSan).WithMany().HasForeignKey(c => c.LOAI_TAI_SAN_ID);
            base.Configure(builder);
        }	
	}
}
