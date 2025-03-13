//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.DanhMuc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.DanhMuc
{
    public partial class LoaiTaiSanMap : GSEntityTypeConfiguration<LoaiTaiSan>
    {
        public override void Configure(EntityTypeBuilder<LoaiTaiSan> builder)
        {
            builder.ToTable("DM_LOAI_TAI_SAN");
            builder.HasKey(c => c.ID);
            
            builder.HasOne(t => t.CheDoHaoMon)
              .WithMany()
              .HasForeignKey(t => t.CHE_DO_HAO_MON_ID);
            builder.HasOne(t => t.LoaiTaiSanCha)
             .WithMany(t => t.ListLoaiTaiSanCon)
             .HasForeignKey(t => t.PARENT_ID);
			builder.Ignore(c => c.LoaiHinhTaiSan);
            base.Configure(builder);
        }
    }
}
