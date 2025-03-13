//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.NghiepVu;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GS.Data.Mapping.NghiepVu
{
    public partial class YeuCauChiTietMap : GSEntityTypeConfiguration<YeuCauChiTiet>
    {
        public override void Configure(EntityTypeBuilder<YeuCauChiTiet> builder)
        {
            builder.ToTable("NV_YEU_CAU_CHI_TIET");
            builder.HasKey(c => c.YEU_CAU_ID);

            builder.HasOne(t => t.yeucau)
               .WithMany()
               .HasForeignKey(t => t.YEU_CAU_ID);
            builder.HasOne(t => t.hinhthucmuasam)
               .WithMany()
               .HasForeignKey(t => t.HINH_THUC_MUA_SAM_ID);
            builder.HasOne(t => t.mucdichsudung)
               .WithMany()
               .HasForeignKey(t => t.MUC_DICH_SU_DUNG_ID);
            builder.HasOne(t => t.donvinhandieuchuyen)
              .WithMany()
              .HasForeignKey(t => t.DON_VI_NHAN_DIEU_CHUYEN_ID);

            builder.Ignore(c => c.ID);
            builder.Ignore(c => c.HTSD_QUAN_LY_NHA_NUOC);
            builder.Ignore(c => c.HTSD_HDSN_KINH_DOANH_KHONG);
            builder.Ignore(c => c.HTSD_HDSN_KINH_DOANH);
            builder.Ignore(c => c.HTSD_CHO_THUE);
            builder.Ignore(c => c.HTSD_LIEN_DOANH);
            builder.Ignore(c => c.HTSD_CHO_THUE);
            builder.Ignore(c => c.HTSD_SU_DUNG_HON_HOP);
            builder.Ignore(c => c.HTSD_SU_DUNG_KHAC);

            builder.Ignore(c => c.HS_CNQSD_SO);
            builder.Ignore(c => c.HS_CNQSD_NGAY);
            builder.Ignore(c => c.HS_QUYET_DINH_GIAO_SO);
            builder.Ignore(c => c.HS_QUYET_DINH_GIAO_NGAY);
            builder.Ignore(c => c.HS_CHUYEN_NHUONG_SD_SO);
            builder.Ignore(c => c.HS_CHUYEN_NHUONG_SD_NGAY);
            builder.Ignore(c => c.HS_QUYET_DINH_CHO_THUE_SO);
            builder.Ignore(c => c.HS_QUYET_DINH_CHO_THUE_NGAY);
            builder.Ignore(c => c.HS_HOP_DONG_CHO_THUE_SO);
            builder.Ignore(c => c.HS_HOP_DONG_CHO_THUE_NGAY);
            builder.Ignore(c => c.HS_KHAC);
            builder.Ignore(c => c.HS_QUYET_DINH_BAN_GIAO);
            builder.Ignore(c => c.HS_QUYET_DINH_BAN_GIAO_NGAY);
            builder.Ignore(c => c.HS_BIEN_BAN_NGHIEM_THU);
            builder.Ignore(c => c.HS_BIEN_BAN_NGHIEM_THU_NGAY);
            builder.Ignore(c => c.HS_PHAP_LY_KHAC);
            builder.Ignore(c => c.HS_PHAP_LY_KHAC_NGAY);

            builder.Ignore(c => c.NGAY_BAN_THANH_LY);
            builder.Ignore(c => c.TAI_SAN_TRUOC_DIEU_CHUYEN_ID);
            builder.Ignore(c => c.DAT_GIA_TRI_QUYEN_SD_DAT);
            builder.Ignore(c => c.KH_TY_LE_NGUYEN_GIA_KHAU_HAO);
			builder.Ignore(c => c.OTO_SO_CAU_XE);
			base.Configure(builder);
        }
    }
}
