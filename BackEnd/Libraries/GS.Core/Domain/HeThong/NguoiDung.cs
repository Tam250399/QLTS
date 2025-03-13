//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 29/5/2019
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Core.Domain.HeThong
{
    public enum NguoiDungStatusID
    {
        choduyet = 0,
        /// <summary>
        /// Bình thường
        /// </summary>
        ok = 1,
        /// <summary>
        /// Khóa
        /// </summary>
        Lock = 2,
        /// <summary>
        /// Xóa
        /// </summary>
        Delete = 3
    }
    public enum NguoiDungKetQuaDangNhap
    {
        /// <summary>
        /// Login successful
        /// </summary>
        Successful = 1,

        /// <summary>
        /// Customer does not exist (email or username)
        /// </summary>
        CustomerNotExist = 2,

        /// <summary>
        /// Wrong password
        /// </summary>
        WrongPassword = 3,

        /// <summary>
        /// Account have not been activated
        /// </summary>
        NotActive = 4,

        /// <summary>
        /// Customer has been deleted 
        /// </summary>
        Deleted = 5,

        /// <summary>
        /// Customer not registered 
        /// </summary>
        NotRegistered = 6,

        /// <summary>
        /// Locked out
        /// </summary>
        LockedOut = 7
    }
    public partial class NguoiDung : BaseEntity
    {
        private IList<VaiTro> _vaiTros;
        private ICollection<VaiTroNguoiDungMapping> _vaiTroNguoiDungMappings;
        public NguoiDung()
        {
            this.GUID = new Guid();
        }
        public Guid GUID { get; set; }
        public String TEN_DANG_NHAP { get; set; }
        public String MAT_KHAU { get; set; }
        public String TEN_DAY_DU { get; set; }
        public String EMAIL { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public int TRANG_THAI_ID { get; set; }
        public String TMP_MAT_KHAU { get; set; }
        public NguoiDungStatusID nguoiDungStatusID
        {
            get => (NguoiDungStatusID)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
        public DateTime? NGAY_TRUY_CAP { get; set; }
        public String IP_TRUY_CAP { get; set; }
        public String GHI_CHU { get; set; }
        public bool IS_QUAN_TRI { get; set; }
        public String UNG_DUNG { get; set; }
        public String MOBILE { get; set; }
        public String MAT_KHAU_KEY { get; set; }
        public String MA_CAN_BO { get; set; }
        public decimal? CURRENT_DON_VI_ID { get; set; }
        public String PASSWORD_HASH { get; set; }
        public virtual ICollection<VaiTroNguoiDungMapping> vaiTroNguoiDungMappings
        {
            get => _vaiTroNguoiDungMappings ?? (_vaiTroNguoiDungMappings = new List<VaiTroNguoiDungMapping>());
            protected set => _vaiTroNguoiDungMappings = value;
        }
        public virtual IList<VaiTro> VaiTro
        {
            get => _vaiTros ?? (_vaiTros = vaiTroNguoiDungMappings.Select(mapping => mapping.vaiTro).ToList());
        }
        public String DB_ID { get; set; }
        public decimal? NGUON_ID { get; set; }
        public decimal? NGUOI_TAO { get; set; }
        public virtual ICollection<NguoiDungDonViMapping> NguoiDungDonViMappings { get; set; }
    }
    public partial class NguoiDungCache : BaseCacheEntity
    {        
        public Guid GUID { get; set; }
        public String TEN_DANG_NHAP { get; set; }
        public String MAT_KHAU { get; set; }
        public String TEN_DAY_DU { get; set; }
        public String EMAIL { get; set; }
        public DateTime NGAY_TAO { get; set; }
        public int TRANG_THAI_ID { get; set; }
        public NguoiDungStatusID nguoiDungStatusID
        {
            get => (NguoiDungStatusID)TRANG_THAI_ID;
            set => TRANG_THAI_ID = (int)value;
        }
        public DateTime? NGAY_TRUY_CAP { get; set; }
        public String IP_TRUY_CAP { get; set; }
        public String GHI_CHU { get; set; }
        public bool IS_QUAN_TRI { get; set; }
        public String UNG_DUNG { get; set; }
        public String MOBILE { get; set; }
        public String MAT_KHAU_KEY { get; set; }
        public String MA_CAN_BO { get; set; }
        public String PASSWORD_HASH { get; set; }
        
    }
   
}



