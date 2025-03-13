//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.TaiSans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.TaiSans
{
    public partial class TaiSanMap : GSEntityTypeConfiguration<TaiSan>
    {
        public override void Configure(EntityTypeBuilder<TaiSan> builder)
        {
            builder.ToTable("TS_TAI_SAN");
            builder.HasKey(c => c.ID);


            builder.HasOne(t => t.loaitaisan)
            .WithMany()
            .HasForeignKey(t => t.LOAI_TAI_SAN_ID);

            builder.HasOne(t => t.loaitaisandonvi)
            .WithMany()
            .HasForeignKey(t => t.LOAI_TAI_SAN_DON_VI_ID);

            builder.HasOne(t => t.nguoidung)
            .WithMany()
            .HasForeignKey(t => t.NGUOI_TAO_ID);

            builder.HasOne(t => t.donvi)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_ID);

            builder.HasOne(t => t.lydobiendong)
            .WithMany()
            .HasForeignKey(t => t.LY_DO_BIEN_DONG_ID);

            builder.HasOne(t => t.nuocsanxuat)
            .WithMany()
            .HasForeignKey(t => t.NUOC_SAN_XUAT_ID);
            builder.HasOne(t => t.donvibophan)
            .WithMany()
            .HasForeignKey(t => t.DON_VI_BO_PHAN_ID);


            builder.Ignore(c => c.TrangThaiTaiSan);
            builder.Ignore(c => c.enumLoaiHinhTaiSan);
            builder.Ignore(c => c.SO_LUONG_HAO_MON_TAI_SAN);
            base.Configure(builder);
        }
    }
}
