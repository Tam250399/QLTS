//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 14/1/2020
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
using GS.Core.Domain.CCDC;

namespace GS.Services.CCDC
{
    public partial class XuatNhapService : IXuatNhapService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<XuatNhap> _itemRepository;
        private readonly IWorkContext _workContext;
        private readonly IRepository<NhapXuatCongCu> _nhapXuatCongCuRepository;
        private readonly IRepository<CongCu> _congCuRepository;
        #endregion

        #region Ctor

        public XuatNhapService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IWorkContext workContext,
            IRepository<XuatNhap> itemRepository,
            IRepository<NhapXuatCongCu> nhapXuatCongCuRepository,
            IRepository<CongCu> congCuRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._nhapXuatCongCuRepository = nhapXuatCongCuRepository;
            this._congCuRepository = congCuRepository;
        }

        #endregion

        #region Methods
        public virtual IList<XuatNhap> GetAllXuatNhaps()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }
        public virtual XuatNhap GetXuatNhapByGuid(Guid GUID)
        {
            var query = _itemRepository.Table.Where(c => c.GUID == GUID);
            return query.FirstOrDefault();
        }
        public virtual IPagedList<XuatNhap> SearchXuatNhaps(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, int LoaiCongCu = 0, DateTime? NgayNhapTu = null, DateTime? NgayNhapDen = null, string SoChungTu = null)
        {

            var query = (from xn in _itemRepository.Table.Where(c => c.IS_XUAT == false && c.DON_VI_ID == _workContext.CurrentDonVi.ID)
                         join xncc in _nhapXuatCongCuRepository.Table on
xn.ID equals xncc.NHAP_XUAT_ID
                         select (xncc.XuatNhap)).Distinct();
            if (isPhanBo)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID != null && c.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.PHAN_BO);
            }
            else
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == null);
            }
            if (Keysearch != null)
            {
                var keyup = Keysearch.ToUpper();
                var congcu = _congCuRepository.Table.Where(c => (c.MA.ToUpper()).Contains(keyup)).Select(c => c.ID).ToList();
                var xuatnhapcongcu = _nhapXuatCongCuRepository.Table.Where(c => congcu.Contains(c.CONG_CU_ID)).Select(c => c.NHAP_XUAT_ID).ToList();
                query = query.Where(c => (c.MA.ToUpper()).Contains(keyup) || (c.TEN.ToUpper()).Contains(keyup) || xuatnhapcongcu.Contains(c.ID));
            }
            if (LoaiCongCu > 0)
            {
                var xuatnhapcongcu = _nhapXuatCongCuRepository.Table.Where(c => c.CongCu.NHOM_CONG_CU_ID == LoaiCongCu).Select(c => c.NHAP_XUAT_ID).ToList();
                query = query.Where(c => xuatnhapcongcu.Contains(c.ID));
            }
            if (NgayNhapTu != null)
            {
                query = query.Where(c => c.NGAY_XUAT_NHAP >= NgayNhapTu);
            }
            if (NgayNhapDen != null)
            {
                query = query.Where(c => c.NGAY_XUAT_NHAP <= NgayNhapDen);
            }
            if (SoChungTu != null)
            {
                var xuatnhapcongcu = _nhapXuatCongCuRepository.Table.Where(c => c.CHUNG_TU_SO.ToUpper().Contains(SoChungTu.ToUpper())).Select(c => c.NHAP_XUAT_ID).ToList();
                query = query.Where(c => xuatnhapcongcu.Contains(c.ID));
            }
            return new PagedList<XuatNhap>(query.OrderByDescending(c => c.NGAY_TAO), pageIndex, pageSize);
        }
        public virtual IPagedList<NhapXuatCongCu> SearchXuatNhapPhanBos(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal LoaiCongCu = 0, Decimal DonViBoPhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {

            var query = from xn in _itemRepository.Table.Where(c => c.IS_XUAT == false && c.DON_VI_ID == _workContext.CurrentDonVi.ID && c.DON_VI_BO_PHAN_ID != null && c.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.PHAN_BO)
                        join xncc in _nhapXuatCongCuRepository.Table on
                         xn.ID equals xncc.NHAP_XUAT_ID
                        where xncc.XuatNhap != null
                        orderby xn.ID
                        select xncc;
            if (Keysearch != null)
            {
                Keysearch = Keysearch.ToUpper();
                var congcu = _congCuRepository.Table.Where(c => c.MA.ToUpper().Contains(Keysearch) || c.TEN.ToUpper().Contains(Keysearch)).Select(c => c.ID).ToList();
                var xn = _itemRepository.Table.Where(c => c.MA.ToUpper().Contains(Keysearch) || c.TEN.ToUpper().Contains(Keysearch)).Select(c => c.ID).ToList();
                query = query.Where(c => xn.Contains(c.NHAP_XUAT_ID) || congcu.Contains(c.CONG_CU_ID));
            }
            if (LoaiCongCu > 0)
            {
                var congcu = _congCuRepository.Table.Where(c => c.NHOM_CONG_CU_ID == LoaiCongCu).Select(c => c.ID).ToList();
                query = query.Where(c => congcu.Contains(c.CONG_CU_ID));
            }
            if (DonViBoPhanId > 0)
            {
                query = query.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            }
            if (TuNgay != null)
            {
                query = query.Where(c => c.XuatNhap.NGAY_XUAT_NHAP >= TuNgay);
            }
            if (DenNgay != null)
            {
                query = query.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= DenNgay);
            }
            return new PagedList<NhapXuatCongCu>(query.OrderByDescending(c => c.XuatNhap.NGAY_XUAT_NHAP).ThenBy(c => c.CongCu.MA), pageIndex, pageSize);
        }

        public virtual XuatNhap GetNhapKhoByCongCuId(Decimal CongCuId)
        {
            var query = from x in _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID)
                        join m in _nhapXuatCongCuRepository.Table on x.ID equals m.NHAP_XUAT_ID
                        where m.CONG_CU_ID == CongCuId && x.DON_VI_BO_PHAN_ID == null && x.IS_XUAT == false
                        select x;

            return query.FirstOrDefault();
        }

        public virtual XuatNhap GetXuatNhapById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<XuatNhap> GetXuatNhapByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual Decimal? GetValueIdMax()
        {
            //var idMax = _itemRepository.Table.OrderByDescending(c => c.ID).Select(c => c.ID).FirstOrDefault();
            var idMax = _itemRepository.Table.DefaultIfEmpty().Max(c => c.ID);
            return idMax;
        }

        public virtual String GetMaLienQuan(decimal? CongCuId)
        {
            var query = from x in _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID)
                        join m in _nhapXuatCongCuRepository.Table on x.ID equals m.NHAP_XUAT_ID
                        where m.CONG_CU_ID == CongCuId
                        select x;

            return query.OrderBy(c => c.NGAY_TAO).FirstOrDefault().MA_LIEN_QUAN;
        }

        public virtual Int32 GetCountCongCuByXuatNhapId(decimal? XuatNhapId = 0)
        {
            var query = from x in _itemRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID)
                        join m in _nhapXuatCongCuRepository.Table on x.ID equals m.NHAP_XUAT_ID
                        where m.NHAP_XUAT_ID == XuatNhapId
                        select x;

            return query.Count();
        }

        public virtual IList<XuatNhap> GetXuatNhaps(bool isXuat, Decimal fromId = 0, Decimal BoPhanId = 0, bool isKho = false, String maLienQuan = null, Decimal LoaiXuatNhap = 0, Boolean IsPhanBoFirst = false, decimal FromXuatNhap = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_XUAT == isXuat && c.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (fromId > 0)
            {
                query = query.Where(c => c.FROM_XUAT_NHAP_ID == fromId);
            }
            if (BoPhanId > 0)
            {
                query = query.Where(c => c.DON_VI_BO_PHAN_ID == BoPhanId);
            }
            if (IsPhanBoFirst)
            {
                query = query.Where(c => c.IS_PHAN_BO_FIRST == IsPhanBoFirst);
            }
            if (isKho)
            {
                query = query.Where(c => c.FROM_XUAT_NHAP_ID == null);
            }
            if (FromXuatNhap>0)
            {
                query = query.Where(c => c.FROM_XUAT_NHAP_ID == FromXuatNhap);
            }
            if (maLienQuan != null)
            {
                query = query.Where(c => c.MA_LIEN_QUAN == maLienQuan);
            }
            if (LoaiXuatNhap > 0)
            {
                query = query.Where(c => c.LOAI_XUAT_NHAP_ID == LoaiXuatNhap);
            }

            return query.OrderByDescending(c => c.NGAY_TAO).ToList();
        }
        public virtual XuatNhap GetXuatNhapForDieuChuyen(decimal FromXuatNhap = 0, bool isXuat = false, decimal LoaiXuatNhap = 0)
        {
            var query = _itemRepository.Table.Where(c => c.IS_XUAT == isXuat);
            if (FromXuatNhap > 0)
            {
                query = query.Where(c => c.FROM_XUAT_NHAP_ID == FromXuatNhap);
            }
            if (LoaiXuatNhap > 0)
            {
                query = query.Where(c => c.LOAI_XUAT_NHAP_ID == LoaiXuatNhap);
            }
            return query.FirstOrDefault();
        }

        public virtual IPagedList<XuatNhap> SearchLuanChuyen(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal LoaiCongCu = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            var query = _itemRepository.Table.Where(c => c.IS_XUAT == false && c.DON_VI_ID == _workContext.CurrentDonVi.ID && c.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.LUAN_CHUYEN);
            if (Keysearch != null)
            {
                query = from x in query
                        join m in _nhapXuatCongCuRepository.Table on x.ID equals m.NHAP_XUAT_ID
                        join c in _congCuRepository.Table on m.CONG_CU_ID equals c.ID
                        where c.MA.ToLower().Contains(Keysearch.ToLower()) || c.TEN.ToLower().Contains(Keysearch.ToLower())
                        select x;
            }
            if (LoaiCongCu > 0)
            {
                var listcongcu = (from m in _nhapXuatCongCuRepository.Table
                                  join c in _congCuRepository.Table on m.CONG_CU_ID equals c.ID
                                  where c.NHOM_CONG_CU_ID == LoaiCongCu
                                  select m.NHAP_XUAT_ID).ToList();
                query = query.Where(c => listcongcu.Contains(c.ID));
            }
            if (TuNgay != null)
            {
                query = query.Where(c => c.NGAY_XUAT_NHAP >= TuNgay);
            }
            if (DenNgay != null)
            {
                query = query.Where(c => c.NGAY_XUAT_NHAP <= DenNgay);
            }
            return new PagedList<XuatNhap>(query.OrderByDescending(c => c.NGAY_XUAT_NHAP), pageIndex, pageSize);
        }

        public virtual XuatNhap GetXuatNhap(String Ma = null, decimal? From_xuat_nhap_id = null)
        {
            var query = _itemRepository.Table;
            if (Ma != null)
            {
                query = query.Where(c => c.MA == Ma);
            }
            if (From_xuat_nhap_id != null)
            {
                query = query.Where(c => c.FROM_XUAT_NHAP_ID == From_xuat_nhap_id);
            }

            return query.OrderBy(c=>c.ID).FirstOrDefault();
        }
        public virtual IList<XuatNhap> GetXuatNhapsByMaLienQuan(String maLienQuan = null)
        {
            if (string.IsNullOrEmpty(maLienQuan))
            {
                return new List<XuatNhap>();
            }
            return _itemRepository.Table.Where(c => c.MA_LIEN_QUAN == maLienQuan).ToList();
        }
        public virtual IPagedList<XuatNhap> SearchDieuChuyen(Decimal LoaiDieuChuyen, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            query = from x in query
                    join y in _itemRepository.Table on x.FROM_XUAT_NHAP_ID equals y.ID
                    where y.FROM_XUAT_NHAP_ID != null && y.DON_VI_ID == _workContext.CurrentDonVi.ID && x.LOAI_XUAT_NHAP_ID == LoaiDieuChuyen
                    select x;
            if (Keysearch != null)
                query = from x in query
                        join m in _nhapXuatCongCuRepository.Table on x.ID equals m.NHAP_XUAT_ID
                        join c in _congCuRepository.Table on m.CONG_CU_ID equals c.ID
                        where c.MA.Contains(Keysearch) || c.TEN.Contains(Keysearch)
                        select x;

            return new PagedList<XuatNhap>(query.OrderByDescending(c => c.NGAY_TAO), pageIndex, pageSize);
        }

        public virtual void InsertXuatNhap(XuatNhap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_TAO = DateTime.Now;
            entity.NGUOI_DUNG_ID = _workContext.CurrentCustomer.ID;
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateXuatNhap(XuatNhap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.NGAY_CAP_NHAP = DateTime.Now;
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteXuatNhap(XuatNhap entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteXuatNhaps(IList<XuatNhap> entitys)
        {
            if (entitys == null)
                throw new ArgumentNullException(nameof(entitys));
            _itemRepository.Delete(entitys);
        }
        #endregion
    }
}

