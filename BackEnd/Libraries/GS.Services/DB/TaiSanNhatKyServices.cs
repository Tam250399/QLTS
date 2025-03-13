//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 15/5/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Data;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.CauHinh;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Core.Domain.DB;
using GS.Core.Domain.TaiSans;
using OfficeOpenXml.FormulaParsing.ExpressionGraph.FunctionCompilers;
using GS.Core.Domain.SHTD;
using GS.Services.DanhMuc;
using GS.Core.Domain.BienDongs;
using GS.Core.Domain.DanhMuc;

namespace GS.Services.DB
{
    public partial class TaiSanNhatKyService : ITaiSanNhatKyService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanNhatKy> _itemRepository;
        private readonly IRepository<TaiSan> _taiSanRepository;
        private readonly IRepository<QuyetDinhTichThu> _qDTTRepository;
        private readonly IRepository<BienDong> _bienDongRepository;
        private readonly IDonViService _donViService;
        #endregion

        #region Ctor

        public TaiSanNhatKyService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanNhatKy> itemRepository,
            IRepository<TaiSan> taiSanRepository,
            IRepository<QuyetDinhTichThu> qDTTRepository,
            IRepository<BienDong> bienDongRepository,
            IDonViService donViService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._taiSanRepository = taiSanRepository;
            this._qDTTRepository = qDTTRepository;
            this._donViService = donViService;
            this._bienDongRepository = bienDongRepository;
        }

        #endregion

        #region Methods
        public virtual IList<TaiSanNhatKy> GetAllTaiSanNhatKys(decimal? trangThai = null, List<decimal> trangThais = null, DateTime? ngayCapNhatTu = null, DateTime? ngayCapNhatDen = null, DateTime? ngayDongBo = null)
        {
            var query = _itemRepository.Table;
            if (trangThais != null)
                query = query.Where(c => trangThais.Contains(c.TRANG_THAI_ID.GetValueOrDefault(-1)));
            else if (trangThai != null)
                query = query.Where(c => c.TRANG_THAI_ID == trangThai);
            if (ngayCapNhatTu.HasValue)
                query = query.Where(c => c.NGAY_CAP_NHAT.HasValue && DateTime.Compare(c.NGAY_CAP_NHAT.Value, ngayCapNhatTu.Value) >= 0);
            if (ngayCapNhatDen.HasValue)
                query = query.Where(c => c.NGAY_CAP_NHAT.HasValue && DateTime.Compare(c.NGAY_CAP_NHAT.Value, ngayCapNhatDen.Value) <= 0);
            if (ngayDongBo.HasValue)
                query = query.Where(c => c.NGAY_DONG_BO.HasValue && DateTime.Compare(c.NGAY_DONG_BO.Value, ngayDongBo.Value) == 0);
            return query.ToList();
        }
        public virtual IList<TaiSanNhatKy> GetTaiSanNhatKys(int? trangThai = null, int taiSanID = 0, Boolean? isTaiSanToanDan = false, int? QuyetDinhID = 0)
        {
            var query = _itemRepository.Table;
            if (trangThai != null)
                query = query.Where(c => c.TRANG_THAI_ID == trangThai);
            if (taiSanID != 0)
                query = query.Where(c => c.TAI_SAN_ID == taiSanID);
            if (isTaiSanToanDan != null)
                query = query.Where(c => c.IS_TAI_SAN_TOAN_DAN == isTaiSanToanDan);
            if (QuyetDinhID != 0)
                query = query.Where(c => c.QUYET_DINH_TICH_THU_ID == QuyetDinhID);
            return query.ToList();
        }
        public virtual IPagedList<TaiSanNhatKy> SearchTaiSanNhatKys(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? TrangThai = null, string MaTaiSan = null, string MaTaiSanDB = null, decimal? LoaiHinhTaiSan = null, DateTime? NgayDB = null, decimal? DonViId = null, bool? isTaiSanToanDan = null)
        {
            var query = _itemRepository.Table;
            if (TrangThai.GetValueOrDefault(0) >= 0)
                query = query.Where(c => c.TRANG_THAI_ID == TrangThai);
            if (LoaiHinhTaiSan.GetValueOrDefault(0) != 0)
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSan);
            if (!string.IsNullOrEmpty(MaTaiSan))
                query = query.Where(c => c.Taisan.MA == MaTaiSan);
            if (!string.IsNullOrEmpty(MaTaiSanDB))
                query = query.Where(c => c.Taisan.MA_DB == MaTaiSanDB);
            if (NgayDB != null)
                query = query.Where(c => c.NGAY_DONG_BO == NgayDB);
            if (isTaiSanToanDan != null)
                query = query.Where(c => c.IS_TAI_SAN_TOAN_DAN == isTaiSanToanDan);
            if (DonViId != null)
            {
                GS.Core.Domain.DanhMuc.DonVi donVi = _donViService.GetDonViById(DonViId.GetValueOrDefault(0));
                if (donVi != null)
                    query = query.Where(c => c.Taisan.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE));
            }
            // lấy các tài sản có biến động
            query = query.Where(m => _bienDongRepository.Table.Where(c => c.TAI_SAN_ID == m.TAI_SAN_ID).Count() > 0);
            return new PagedList<TaiSanNhatKy>(query.OrderByDescending(c => c.NGAY_DONG_BO), pageIndex, pageSize); ;
        }

        public virtual TaiSanNhatKy GetTaiSanNhatKyById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<TaiSanNhatKy> GetTaiSanNhatKyByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanNhatKy(TaiSanNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanNhatKy(TaiSanNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateTaiSanNhatKy(List<TaiSanNhatKy> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            entities = entities.Select(c => { c.NGAY_CAP_NHAT = DateTime.Now; return c; }).ToList();
            _itemRepository.Update(entities);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanNhatKy(TaiSanNhatKy entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        /// <summary>
        /// get nhat ky cuoi cung cua tai san
        /// </summary>
        /// <param name="idTaiSan"></param>
        /// <returns></returns>
        public virtual TaiSanNhatKy GetByTaiSanId(decimal idTaiSan)
        {
            if (idTaiSan == 0)
                return null;
            return _itemRepository.Table.Where(p => p.TAI_SAN_ID == idTaiSan).FirstOrDefault();
        }
        public virtual TaiSanNhatKy GetByTaiSanTdId(decimal idTaiSanTd)
        {
            if (idTaiSanTd == 0)
                return null;
            return _itemRepository.Table.Where(p => p.TAI_SAN_ID == idTaiSanTd && p.IS_TAI_SAN_TOAN_DAN == true).FirstOrDefault();
        }
        public virtual TaiSanNhatKy GetByMaTaiSan(string maTaiSan)
        {
            if (string.IsNullOrEmpty(maTaiSan))
                return null;
            TaiSan ts = _taiSanRepository.Table.Where(c => c.MA == maTaiSan).FirstOrDefault();
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == (ts != null ? ts.ID : 0)).FirstOrDefault();
        }

        public virtual void UpdateTrangThaiTaiSanNhatKy(decimal idTaiSan)
        {
            var taiSanNhatKy = GetByTaiSanId(idTaiSan);
            decimal? TrangThai = null;
            if (taiSanNhatKy != null)
            {
                if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO)
                {
                    TrangThai = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                }
                else
                {
                    TrangThai = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                }
            }
            else
            {
                TrangThai = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
            }

            var ts = _taiSanRepository.GetById(idTaiSan);
            decimal loaiHinhTS = 0;
            if (ts != null)
                loaiHinhTS = ts.LOAI_HINH_TAI_SAN_ID ?? 0;
            taiSanNhatKy = new TaiSanNhatKy
            {
                ID = 0,
                TAI_SAN_ID = idTaiSan,
                LOAI_HINH_TAI_SAN_ID = loaiHinhTS,
                NGAY_CAP_NHAT = DateTime.Now,
                NGAY_DONG_BO = DateTime.Now,
                TRANG_THAI_ID = TrangThai,

                IS_TAI_SAN_TOAN_DAN = false
            };
            // update cac nhat ky truoc do                    
            UpdateTaiSanNhatKy(taiSanNhatKy);

        }
        public virtual IList<TaiSanNhatKy> GetTaiSanToaDanDB()
        {
            var query = _itemRepository.Table;
            return query.Where(m => m.QUYET_DINH_TICH_THU_ID != null && (m.IS_TAI_SAN_TOAN_DAN ?? false) && m.TRANG_THAI_ID != (int)enumTrangThaiTaiSanNhatKy.DA_DONG_BO).ToList();
        }
        public virtual IList<TaiSanNhatKy> GetAllNhatKyByTaiSanId(decimal idTaiSan)
        {
            if (idTaiSan == 0)
                return null;
            return _itemRepository.Table.Where(p => p.TAI_SAN_ID == idTaiSan).ToList();
        }
        public virtual TaiSanNhatKy GetTaiSanNhatKyByQuyetDinhTichThu(string SoQuyetDinh, DateTime? NgayQuyetDinh, string MaDonVi)
        {
            if (string.IsNullOrEmpty(SoQuyetDinh) || NgayQuyetDinh == null || string.IsNullOrEmpty(MaDonVi))
                return null;
            return null;
        }
        /// <summary>
        /// get quyết định tịch thu có trạng thái mới nhất
        /// </summary>
        /// <param name="QuyetDinhId"></param>
        /// <returns></returns>
        public virtual TaiSanNhatKy GetTaiSanNhatKyByQuyetDinhTichThu(decimal QuyetDinhId)
        {
            if (QuyetDinhId == 0)
                return null;
            return _itemRepository.Table.Where(p => p.QUYET_DINH_TICH_THU_ID == QuyetDinhId).FirstOrDefault();
        }
        public virtual void UpdateQuyetDinhTichThuNhatKY(decimal QuyetDinhId)
        {
            var taiSanNhatKy = GetTaiSanNhatKyByQuyetDinhTichThu(QuyetDinhId);
            decimal? TrangThai = null;
            if (taiSanNhatKy != null)
            {
                if (taiSanNhatKy.TRANG_THAI_ID == (int)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO)
                {
                    TrangThai = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
                }
                else
                {
                    TrangThai = (int)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI;
                }
            }
            else
            {
                TrangThai = (decimal)enumTrangThaiTaiSanNhatKy.CHUA_DONG_BO;
            }

            var qd = _qDTTRepository.GetById(QuyetDinhId);
            taiSanNhatKy = new TaiSanNhatKy
            {
                ID = 0,
                QUYET_DINH_TICH_THU_ID = QuyetDinhId,
                IS_TAI_SAN_TOAN_DAN = true,
                NGAY_CAP_NHAT = DateTime.Now,
                NGAY_DONG_BO = DateTime.Now,
                TRANG_THAI_ID = TrangThai,

            };
            // update cac nhat ky truoc do
            var ListNhatKy = _itemRepository.Table.Where(m => m.IS_TAI_SAN_TOAN_DAN == true && m.QUYET_DINH_TICH_THU_ID == QuyetDinhId);
            foreach (var item in ListNhatKy)
            {

                item.NGAY_CAP_NHAT = DateTime.Now;
                UpdateTaiSanNhatKy(item);
            }
            InsertTaiSanNhatKy(taiSanNhatKy);
        }
        public virtual string GenArrBienDongId(string StringArrBDID = null, List<decimal> idDel = null, List<decimal> idAdd = null)
        {
            if (string.IsNullOrEmpty(StringArrBDID) && idAdd != null && idAdd.Count > 0)
            {
                return string.Join(',', idAdd);
            }
            else if (!string.IsNullOrEmpty(StringArrBDID))
            {
                try
                {
                    List<decimal> lst = StringArrBDID.Split(',').Select(c => Convert.ToDecimal(c)).ToList();
                    if (idAdd != null && idAdd.Count > 0)
                        lst = lst.Concat(idAdd).Distinct().ToList();
                    if (idDel != null && idDel.Count > 0)
                        foreach (decimal idel in idDel)
                        {
                            if (lst.Contains(idel))
                                lst.Remove(idel);
                        }
                    return string.Join(',', lst);
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }
        public List<TaiSanNhatKy> GetTaiSanCanDongBo(decimal donViID = 0, int TakeNumber = 1000)
        {

            var query = _itemRepository.Table.Where(c => c.BD_IDS != null && c.Taisan.MA_DB != null && (c.Taisan.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.DKTS40 || c.Taisan.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.QLTSNN || c.Taisan.NGUON_TAI_SAN_ID == (decimal)enumNguonTaiSan.QLTSC) && c.TRANG_THAI_ID == (decimal)enumTrangThaiTaiSanNhatKy.CO_THAY_DOI);
            if (donViID > 0)
            {
                DonVi donVi = _donViService.GetDonViById(donViID);
                query = query.Where(c => c.Taisan.donvi.TREE_NODE.StartsWith(donVi.TREE_NODE));
            }

            return query.OrderBy(c => c.ID).Take(TakeNumber).ToList();
        }
        #endregion
    }
}

