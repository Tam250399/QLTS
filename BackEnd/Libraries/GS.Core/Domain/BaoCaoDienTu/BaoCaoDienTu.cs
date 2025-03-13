using GS.Core.Domain.DanhMuc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GS.Core.Domain.BaoCaoDienTu
{
    
    public partial class BaoCaoDienTu : BaseEntity
    {
       
        public string TEN { get; set; }
        public DateTime? NGAY_BAO_CAO { get; set; }
        public Decimal? NGUOI_TAO_ID { get; set; }
        public Decimal? NGUOI_DUYET_ID { get; set; }
        public Decimal? TRANG_THAI_ID { get; set; }
        public Decimal? DON_VI_ID { get; set; }
        public enumTrangThaiBaoCao TenTrangThai
        {
            get => (enumTrangThaiBaoCao)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
        public string SO_VAN_BAN { get; set; }
        public string TINH_HINH_BAN_HANH_VAN_BAN { get; set; }
        public string THUC_TRANG { get; set; }
        public string TINH_HINH_TANG_GIAM { get; set; }
        public string CONG_TAC_CHI_DAO { get; set; }
        public string TINH_HINH_THUC_HIEN { get; set; }
        public string DANH_GIA_TICH_CUC { get; set; }
        public string DANH_GIA_TINH_HINH { get; set; }
        public string KET_QUA_KHAC { get; set; }
        public string KIEN_NGHI { get; set; }
        public string NOI_NHAN { get; set; }
        public string LY_DO_TU_CHOI { get; set; }
        public Decimal? FILE_ID { get; set; }
        public virtual DonVi donvi { get; set; }
    }
}
