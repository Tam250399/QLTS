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
    public partial class NhapXuatCongCuService : INhapXuatCongCuService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<NhapXuatCongCu> _itemRepository;
        private readonly IRepository<XuatNhap> _xuatNhapRepository;
        private readonly IRepository<CongCu> _congCuRepository;
        private readonly IWorkContext _workContext;
        #endregion

        #region Ctor

        public NhapXuatCongCuService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<NhapXuatCongCu> itemRepository,
            IRepository<XuatNhap> xuatNhapRepository,
            IRepository<CongCu> congCuRepository,
            IWorkContext workContext
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._xuatNhapRepository = xuatNhapRepository;
            this._congCuRepository = congCuRepository;
            this._workContext = workContext;
        }

        #endregion

        #region Methods
        public virtual IList<NhapXuatCongCu> GetAllNhapXuatCongCus()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<NhapXuatCongCu> SearchNhapXuatCongCus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, bool isPhanBo = false, Decimal NhapXuatId = 0)
        {
            var query = _itemRepository.Table.Where(c => c.XuatNhap.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (isPhanBo)
            {
                query = query.Where(c => c.XuatNhap.FROM_XUAT_NHAP_ID == null && c.XuatNhap.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.NHAP_KHO);
            }
            if (NhapXuatId > 0)
            {
                query = query.Where(c => c.NHAP_XUAT_ID == NhapXuatId);
            }
            if (Keysearch != null)
            {
                var congcu = _congCuRepository.Table.Where(c => c.MA.Contains(Keysearch)).Select(c => c.ID).ToList();
                query = query.Where(c => c.XuatNhap.TEN.ToLower().Contains(Keysearch.ToLower()) || c.XuatNhap.MA.ToLower().Contains(Keysearch.ToLower()) || congcu.Contains(c.CONG_CU_ID));
            }

            return new PagedList<NhapXuatCongCu>(query, pageIndex, pageSize); ;
        }

        public virtual NhapXuatCongCu GetNhapXuatCongCuById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual NhapXuatCongCu GetNhapXuatCongCu(decimal CongCuId = 0, decimal NhapXuatId = 0)
        {
            var query = _itemRepository.Table;
            if (CongCuId > 0 && NhapXuatId > 0)
            {
                query = query.Where(c => c.CONG_CU_ID == CongCuId && c.NHAP_XUAT_ID == NhapXuatId);
            }

            return query.FirstOrDefault();
        }

        public virtual IList<NhapXuatCongCu> GetNhapXuatCongCuByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual Decimal GetSoLuongDaXuat(Decimal mapId, Boolean IsPhanBo = false)
        {
            var item = _itemRepository.GetById(mapId);
            var listXuatNhap = _xuatNhapRepository.Table.Where(c => c.MA_LIEN_QUAN == item.XuatNhap.MA_LIEN_QUAN && c.IS_XUAT == true);
            if (IsPhanBo)
            {
                listXuatNhap = listXuatNhap.Where(c => c.LOAI_XUAT_NHAP_ID == (int)enumLoaiXuatNhap.PHAN_BO);
            }
            var query = _itemRepository.Table.Where(c => c.CONG_CU_ID == item.CONG_CU_ID && listXuatNhap.Select(q => q.ID).Contains(c.NHAP_XUAT_ID) && c.DON_GIA == item.DON_GIA );
            return (Decimal)query.Sum(c => c.SO_LUONG);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CongCuId"></param>
        /// <param name="nhapOrXuat">0: null; 1: xuat; 2: nhap</param>
        /// <returns></returns>
        public virtual IList<NhapXuatCongCu> GetNhapXuatCongCus(decimal? CongCuId = 0, int nhapOrXuat = 0, bool isPhanBo = false, decimal? NhapXuatId = 0, decimal[] ListNhapXuatId = null, decimal TrangThai = 0, decimal DonGia = 0, decimal NhapKhoId = 0)
        {
            var query = _itemRepository.Table;
            if (CongCuId > 0)
            {
                query = query.Where(c => c.CONG_CU_ID == CongCuId);
            }
            if (NhapKhoId > 0)
            {
                query = query.Where(c => c.NHAP_KHO_ID == NhapKhoId);
            }
            if (TrangThai > 0)
            {
                query = query.Where(c => c.TRANG_THAI_ID == TrangThai);
            }
            if (DonGia > 0)
            {
                query = query.Where(c => c.DON_GIA == DonGia);
            }
            if (NhapXuatId > 0)
            {
                query = query.Where(c => c.NHAP_XUAT_ID == NhapXuatId);
            }
            if (nhapOrXuat > 0)
            {
                if (nhapOrXuat == 1)
                {
                    query = from m in query
                            join x in _xuatNhapRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID) on m.NHAP_XUAT_ID equals x.ID
                            where x.IS_XUAT == true
                            select m;
                }
                if (nhapOrXuat == 2)
                {
                    query = from m in query
                            join x in _xuatNhapRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID) on m.NHAP_XUAT_ID equals x.ID
                            where x.IS_XUAT == false
                            select m;
                }
            }
            if (isPhanBo)
            {
                query = from m in query
                        join x in _xuatNhapRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID) on m.NHAP_XUAT_ID equals x.ID
                        where x.FROM_XUAT_NHAP_ID == null && x.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.PHAN_BO
                        select m;
            }
            if (ListNhapXuatId != null)
                query = query.Where(c => ListNhapXuatId.Contains(c.NHAP_XUAT_ID));
            query = from m in query
                    join x in _xuatNhapRepository.Table.Where(c => c.DON_VI_ID == _workContext.CurrentDonVi.ID) on m.NHAP_XUAT_ID equals x.ID
                    orderby x.NGAY_TAO descending
                    select m;

            return query.ToList();
        }
        //
        public virtual IList<NhapXuatCongCu> GetNhapXuatCongCuByNhapXuatId(decimal NhapXuatId)
        {
            if (NhapXuatId == 0)
                return new List<NhapXuatCongCu>();
            return _itemRepository.Table.Where(c => c.NHAP_XUAT_ID == NhapXuatId).ToList();
        }
        public virtual IPagedList<NhapXuatCongCu> GetNhapXuatCongCusByBaoHanh(Decimal DonViBoPhanId, int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, string listCongCuDaChon = null, Decimal? DonViId = 0)
        {
            IList<NhapXuatCongCu> query = new List<NhapXuatCongCu>();
            var listCongCu = _congCuRepository.Table.Where(c => c.DON_VI_ID == DonViId);
            if (KeySearch != null)
            {
                listCongCu = listCongCu.Where(c => c.TEN.ToLower().Contains(KeySearch.ToLower()) || c.MA.ToLower().Contains(KeySearch.ToLower()));
            }

            foreach (var cc in listCongCu)
            {

                var listMap = _itemRepository.Table.Where(c => c.XuatNhap.DON_VI_ID == DonViId && c.XuatNhap.DON_VI_BO_PHAN_ID != null && c.CONG_CU_ID == cc.ID && c.XuatNhap.IS_XUAT == false);
                if (DonViBoPhanId > 0)
                {
                    listMap = listMap.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
                }
                foreach (var map in listMap)
                {
                    var lst = _itemRepository.Table.Where(c => c.CONG_CU_ID == cc.ID);
                    if (DonViBoPhanId > 0)
                    {
                        lst = lst.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
                    }
                    var countXuat = lst.Where(c => c.XuatNhap.IS_XUAT == true && c.XuatNhap.FROM_XUAT_NHAP_ID == map.NHAP_XUAT_ID && c.DON_GIA == map.DON_GIA && c.NHAP_KHO_ID == map.ID).Sum(c => c.SO_LUONG);
                    if (map.SO_LUONG > countXuat)
                    {
                        var item = new NhapXuatCongCu
                        {
                            ID = map.ID,
                            CONG_CU_ID = cc.ID,
                            SoLuongCoThePhanBo = (Decimal)map.SO_LUONG - (Decimal)countXuat,
                            DON_GIA = map.DON_GIA,
                            CongCu = cc,
                            NHAP_XUAT_ID = map.NHAP_XUAT_ID,
                            XuatNhap = map.XuatNhap
                        };
                        query.Add(item);
                    }
                }
            }

            if (listCongCuDaChon != null)
            {
                var arr = listCongCuDaChon.Split(',');
                query = query.Where(c => !arr.Contains(c.ID.ToString())).ToList();
            }
            return new PagedList<NhapXuatCongCu>(query.OrderByDescending(c => c.XuatNhap.NGAY_XUAT_NHAP).ThenBy(c => c.XuatNhap.DON_VI_BO_PHAN_ID).ToList(), pageIndex, pageSize);
        }

        public virtual List<NhapXuatCongCu> GetMapForKiemKeCongCu(Decimal? DonViBoPhanId = 0, string KeySearch = null, DateTime? NgayKiemKe = null)
        {
            IList<NhapXuatCongCu> query = new List<NhapXuatCongCu>();
            var listNhap = _itemRepository.Table.Where(c => c.XuatNhap.DON_VI_ID == _workContext.CurrentDonVi.ID && c.XuatNhap.IS_XUAT == false && c.XuatNhap.DON_VI_BO_PHAN_ID != null);
            if (DonViBoPhanId > 0)
                listNhap = listNhap.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            if (NgayKiemKe != null)
                listNhap = listNhap.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= NgayKiemKe);
            foreach (var nhap in listNhap)
            {
                var daXuat = _itemRepository.Table.Where(c => c.XuatNhap.IS_XUAT == true && c.CONG_CU_ID == nhap.CONG_CU_ID && c.XuatNhap.FROM_XUAT_NHAP_ID == nhap.XuatNhap.ID && c.NHAP_KHO_ID == nhap.ID);
                if (NgayKiemKe != null)
                    daXuat = daXuat.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= NgayKiemKe);
                if (DonViBoPhanId > 0)
                    daXuat = daXuat.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
                if (daXuat.Sum(c => c.SO_LUONG) < nhap.SO_LUONG)
                {
                    var item = new NhapXuatCongCu
                    {
                        ID = nhap.ID,
                        CONG_CU_ID = nhap.CONG_CU_ID,
                        SoLuongCoThePhanBo = (Decimal)nhap.SO_LUONG - (Decimal)daXuat.Sum(c => c.SO_LUONG),
                        DON_GIA = nhap.DON_GIA,
                        CongCu = nhap.CongCu,
                        NHAP_XUAT_ID = nhap.NHAP_XUAT_ID,
                        XuatNhap = nhap.XuatNhap
                    };
                    query.Add(item);
                }
            }
            if (KeySearch != null)
            {
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(KeySearch.ToLower()) || c.CongCu.MA.ToLower().Contains(KeySearch.ToLower())).ToList();
            }

            return query.ToList();
        }

        public virtual NhapXuatCongCu GetSoLuongDangCoByMapId(Decimal MapId)
        {
            var map = _itemRepository.GetById(MapId);
            if (map != null)
            {
                if (map.XuatNhap.IS_XUAT == true)
                {
                    var nhap = _xuatNhapRepository.GetById(map.XuatNhap.FROM_XUAT_NHAP_ID);
                    if (nhap != null)
                    {
                        var CCDCTang = _itemRepository.Table.Where(c => c.NHAP_XUAT_ID == nhap.ID && c.CONG_CU_ID == map.CONG_CU_ID && c.DON_GIA == map.DON_GIA).Select(c => c.SO_LUONG).FirstOrDefault();
                        var listXNGiam = _xuatNhapRepository.Table.Where(c => c.FROM_XUAT_NHAP_ID == nhap.ID && c.IS_XUAT == true).Select(c => c.ID).ToList();
                        var CCDCGiam = _itemRepository.Table.Where(c => listXNGiam.Contains(c.NHAP_XUAT_ID) && c.CONG_CU_ID == map.CONG_CU_ID && c.DON_GIA == map.DON_GIA && c.TRANG_THAI_ID == map.TRANG_THAI_ID).Select(c => c.SO_LUONG).Sum();
                        map.SoLuongCoThePhanBo = (decimal)(CCDCTang - CCDCGiam);
                    }
                }
                else
                {
                    var listXNGiam = _xuatNhapRepository.Table.Where(c => c.FROM_XUAT_NHAP_ID == map.XuatNhap.ID && c.IS_XUAT == true).Select(c => c.ID).ToList();
                    var CCDCGiam = _itemRepository.Table.Where(c => listXNGiam.Contains(c.NHAP_XUAT_ID) && c.CONG_CU_ID == map.CONG_CU_ID && c.DON_GIA == map.DON_GIA && c.TRANG_THAI_ID == map.TRANG_THAI_ID).Select(c => c.SO_LUONG).Sum();
                    map.SoLuongCoThePhanBo = (decimal)(map.SO_LUONG - CCDCGiam);

                    // Fix lỗi số lượng có thể phân bổ (Do bên trên thừa c.TRANG_THAI_ID == map.TRANG_THAI_ID )
                    // tạo thêm điều kiện để tránh ảnh hưởng đến các chức năng khác - Hungnt
                    if (map.XuatNhap.DON_VI_BO_PHAN_ID == null)
                    {
                        var SLGiam = _itemRepository.Table.Where(c => c.XuatNhap.FROM_XUAT_NHAP_ID == map.XuatNhap.ID && c.XuatNhap.IS_XUAT == true && c.CONG_CU_ID == map.CONG_CU_ID && c.DON_GIA == map.DON_GIA)
                                                            .Select(c => c.SO_LUONG)
                                                            .Sum();
                        map.SoLuongCoThePhanBo = (decimal)(map.SO_LUONG - SLGiam);
                    }

                }
            }
          

            
            

            return map;
        }

        public virtual IList<NhapXuatCongCu> GetXuatByCongCu(Decimal CongCuId, Decimal NhapId, Decimal? BoPhanId = 0, bool isKho = false)
        {
            var xuatnhap = _xuatNhapRepository.Table.Where(c => c.FROM_XUAT_NHAP_ID == NhapId && c.IS_XUAT == true && c.DON_VI_ID == _workContext.CurrentDonVi.ID);
            if (BoPhanId > 0)
                xuatnhap = xuatnhap.Where(c => c.DON_VI_BO_PHAN_ID == BoPhanId);
            if (isKho)
                xuatnhap = xuatnhap.Where(c => c.DON_VI_BO_PHAN_ID == null);
            var query = from m in _itemRepository.Table
                        join x in xuatnhap on m.NHAP_XUAT_ID equals x.ID
                        where m.CONG_CU_ID == CongCuId
                        select m;

            return query.ToList();
        }

        public virtual IPagedList<NhapXuatCongCu> GetByKiemKeCongCu(int pageIndex = 0, int pageSize = int.MaxValue, string KeySearch = null, Decimal? BoPhanId = 0)
        {
            var query = _itemRepository.Table;
            var congcu = _congCuRepository.Table;
            var xuatnhap = _xuatNhapRepository.Table.Where(c => c.IS_XUAT == false);
            if (KeySearch != null)
                congcu = congcu.Where(c => c.MA.Contains(KeySearch) || c.TEN.Contains(KeySearch));
            query = from x in xuatnhap
                    join m in query on x.ID equals m.NHAP_XUAT_ID
                    join c in congcu on m.CONG_CU_ID equals c.ID
                    where x.IS_XUAT == false && c.DON_VI_ID == _workContext.CurrentDonVi.ID
                    select m;
            // điều kiện: số lượng nhập > số lượng xuất
            //query = from m in query
            //        join xBefore in xuatnhap.Where(c => c.IS_XUAT == true).GroupBy(c => c.FROM_XUAT_NHAP_ID) on m.NHAP_XUAT_ID equals xBefore.Key
            //        join mBefore in _itemRepository.Table on new { A = xBefore.ID, B = m.CONG_CU_ID } equals new { A = mBefore.NHAP_XUAT_ID, B = mBefore.CONG_CU_ID }
            //        where mBefore.g
            //        select m;
            if (BoPhanId > 0)
            {
                query = from m in query
                        join x in xuatnhap on m.NHAP_XUAT_ID equals x.ID
                        where x.DON_VI_BO_PHAN_ID == BoPhanId
                        select m;
            }
            return new PagedList<NhapXuatCongCu>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<NhapXuatCongCu> SearchDieuChuyen(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            var xuatnhap = from x in _xuatNhapRepository.Table
                           join y in _xuatNhapRepository.Table on x.FROM_XUAT_NHAP_ID equals y.ID
                           where y.FROM_XUAT_NHAP_ID != null && y.DON_VI_ID == _workContext.CurrentDonVi.ID
                           && x.LOAI_XUAT_NHAP_ID == (Decimal)enumLoaiXuatNhap.DIEU_CHUYEN
                           orderby x.NGAY_TAO descending
                           select x;
            if (Keysearch != null)
                query = from m in query
                        join x in xuatnhap on m.NHAP_XUAT_ID equals x.ID
                        join c in _congCuRepository.Table on m.CONG_CU_ID equals c.ID
                        where c.MA.Contains(Keysearch) || c.TEN.Contains(Keysearch)
                        select m;

            return new PagedList<NhapXuatCongCu>(query, pageIndex, pageSize);
        }

        public virtual IPagedList<NhapXuatCongCu> SearchPhanBo(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            IList<NhapXuatCongCu> query = new List<NhapXuatCongCu>();
            var listMap = from xncc in _itemRepository.Table join xn in _xuatNhapRepository.Table on xncc.NHAP_XUAT_ID equals xn.ID where xn.DON_VI_ID == _workContext.CurrentDonVi.ID && xn.IS_XUAT == false && xn.DON_VI_BO_PHAN_ID == null select xncc;
            foreach (var map in listMap)
            {
                var XuatKho = _xuatNhapRepository.Table.Where(c => c.IS_XUAT == true && c.LOAI_XUAT_NHAP_ID == (int)enumLoaiXuatNhap.PHAN_BO && c.FROM_XUAT_NHAP_ID == map.NHAP_XUAT_ID && c.DON_VI_ID == _workContext.CurrentDonVi.ID).Select(c => c.ID);
                var soLuongXuat = _itemRepository.Table.Where(c => XuatKho.Contains(c.NHAP_XUAT_ID) && c.ID != map.ID && c.CONG_CU_ID == map.CONG_CU_ID && c.DON_GIA == map.DON_GIA).Sum(c => c.SO_LUONG);
                if (map.SO_LUONG > soLuongXuat)
                {
                    map.SoLuongCoThePhanBo = (Decimal)map.SO_LUONG - (Decimal)soLuongXuat;
                    query.Add(map);
                }
            }
            if (Keysearch != null)
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower())).ToList();
            return new PagedList<NhapXuatCongCu>(query.OrderByDescending(c => c.XuatNhap.NGAY_XUAT_NHAP).ThenBy(c => c.CongCu.MA).ToList(), pageIndex, pageSize);
        }

        public virtual IPagedList<NhapXuatCongCu> SeaechForGiamDieuChuyen(Decimal LoaiDieuChuyen, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal LoaiCongCu = 0, Decimal DonViBoPhanId = 0, DateTime? TuNgay = null, DateTime? DenNgay = null)
        {
            // list loại xuất nhập
            var listDieuChuyen = new List<decimal>() { (decimal)enumLoaiXuatNhap.DIEU_CHUYEN, (decimal)enumLoaiXuatNhap.DIEU_CHUYEN_NGOAI, (decimal)enumLoaiXuatNhap.GIAM_BAN, (decimal)enumLoaiXuatNhap.GIAM_KHAC, (decimal)enumLoaiXuatNhap.GIAM_TIEU_HUY, (decimal)enumLoaiXuatNhap.GIAM_HONG_MAT,
                                                       (decimal)enumLoaiXuatNhap.THANH_LY, (decimal)enumLoaiXuatNhap.BI_THU_HOI, (decimal)enumLoaiXuatNhap.GIAM_MAT_HUY_HOAI };

            var query = _itemRepository.Table.Where(c => c.XuatNhap.IS_XUAT == true && c.XuatNhap.FROM_XUAT_NHAP_ID != null && c.XuatNhap.DON_VI_ID == _workContext.CurrentDonVi.ID && listDieuChuyen.Contains((decimal)c.XuatNhap.LOAI_XUAT_NHAP_ID));
            if (Keysearch != null)
            {
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower()));
            }
            if (LoaiDieuChuyen > 0)
            {
                query = query.Where(c => c.XuatNhap.LOAI_XUAT_NHAP_ID == LoaiDieuChuyen);
            }
            if (DonViBoPhanId > 0)
            {
                query = query.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
            }
            if (LoaiCongCu > 0)
            {
                query = query.Where(c => c.CongCu.NHOM_CONG_CU_ID == LoaiCongCu);
            }
            if (TuNgay != null)
            {
                query = query.Where(c => c.XuatNhap.NGAY_XUAT_NHAP >= TuNgay);
            }
            if (DenNgay != null)
            {
                query = query.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= DenNgay);
            }
            return new PagedList<NhapXuatCongCu>(query.OrderByDescending(c => c.XuatNhap.NGAY_TAO).ThenBy(c => c.CongCu.MA), pageIndex, pageSize);
        }
        public virtual IPagedList<NhapXuatCongCu> SearchForGiamKhac(Decimal LoaiDieuChuyen, int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var giams = new List<int> { (int)enumLoaiXuatNhap.GIAM_BAN, (int)enumLoaiXuatNhap.GIAM_KHAC, (int)enumLoaiXuatNhap.GIAM_TIEU_HUY, (int)enumLoaiXuatNhap.THANH_LY, (int)enumLoaiXuatNhap.BI_THU_HOI, (int)enumLoaiXuatNhap.GIAM_MAT_HUY_HOAI };
            var query = _itemRepository.Table.Where(c => c.XuatNhap.FROM_XUAT_NHAP_ID != null && c.XuatNhap.DON_VI_ID == _workContext.CurrentDonVi.ID && c.XuatNhap.IS_XUAT == true && giams.Contains((int)c.XuatNhap.LOAI_XUAT_NHAP_ID));
            if (Keysearch != null)
                query = query.Where(c => c.CongCu.TEN.ToLower().Contains(Keysearch.ToLower()) || c.CongCu.MA.ToLower().Contains(Keysearch.ToLower()));
            if (LoaiDieuChuyen != 0)
            {
                query = query.Where(c => c.XuatNhap.LOAI_XUAT_NHAP_ID == LoaiDieuChuyen);
            }
            return new PagedList<NhapXuatCongCu>(query.OrderBy(c => c.XuatNhap.NGAY_TAO), pageIndex, pageSize);
        }
        public virtual void InsertNhapXuatCongCu(NhapXuatCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateNhapXuatCongCu(NhapXuatCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteNhapXuatCongCu(NhapXuatCongCu entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual void DeleteNhapXuatCongCus(IList<NhapXuatCongCu> entitys)
        {
            if (entitys == null)
                throw new ArgumentNullException(nameof(entitys));
            _itemRepository.Delete(entitys);
        }
        #endregion
        #region read only
        /// <summary>
        /// Lấy NhapXuatCongCu theo mã CCDC đồng bộ
        /// </summary>
        /// <param name="lstMaCCDC_DB"></param>
        /// <returns></returns>
        public virtual List<NhapXuatCongCu> GetReadOnlyNhapXuatCongCusByMaCCDC_DB(List<string> lstMaCCDC_DB, decimal? DonViBoPhanId, DateTime? ngayKiemKe = null)
        {
            if (lstMaCCDC_DB == null || lstMaCCDC_DB.Count == 0)
                return null;
            if (DonViBoPhanId.GetValueOrDefault(0) == 0)
                return null;
            List<NhapXuatCongCu> query = new List<NhapXuatCongCu>();
            var listNhap = _itemRepository.Table.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId && lstMaCCDC_DB.Contains(c.CongCu.MA_DB));
            if (ngayKiemKe != null)
                listNhap = listNhap.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= ngayKiemKe);
            foreach (var nhap in listNhap)
            {
                var daXuat = _itemRepository.Table.Where(c => c.XuatNhap.IS_XUAT == true && c.CONG_CU_ID == nhap.CONG_CU_ID && c.XuatNhap.FROM_XUAT_NHAP_ID == nhap.XuatNhap.ID && c.NHAP_KHO_ID == nhap.ID);
                if (ngayKiemKe != null)
                    daXuat = daXuat.Where(c => c.XuatNhap.NGAY_XUAT_NHAP <= ngayKiemKe);
                if (DonViBoPhanId > 0)
                    daXuat = daXuat.Where(c => c.XuatNhap.DON_VI_BO_PHAN_ID == DonViBoPhanId);
                if (daXuat != null && daXuat.Sum(c => c.SO_LUONG) < nhap.SO_LUONG)
                {
                    var item = new NhapXuatCongCu
                    {
                        ID = nhap.ID,
                        CONG_CU_ID = nhap.CONG_CU_ID,
                        SoLuongCoThePhanBo = (Decimal)nhap.SO_LUONG - (Decimal)daXuat.Sum(c => c.SO_LUONG),
                        DON_GIA = nhap.DON_GIA,
                        CongCu = nhap.CongCu,
                        NHAP_XUAT_ID = nhap.NHAP_XUAT_ID,
                        XuatNhap = nhap.XuatNhap
                    };
                    query.Add(item);
                }
            }
            return query.ToList();
        }
        #endregion
    }
}
