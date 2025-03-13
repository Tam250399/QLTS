//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Core.Domain.DM;
using GS.Data;
using GS.Services.DM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
    public partial class LyDoBienDongService : ILyDoBienDongService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IWorkContext _workContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<LyDoBienDong> _itemRepository;
        private readonly IRepository<LoaiDonVi> _loaiDonViRepository;
        private readonly IRepository<DonVi> _donViRepository;
        private readonly ILoaiLyDoBienDongService _loaiLyDoBienDongService;
        #endregion

        #region Ctor

        public LyDoBienDongService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<LyDoBienDong> itemRepository,
            IWorkContext workContext,
            IRepository<LoaiDonVi> loaiDonViRepository,
            IRepository<DonVi> donViRepository,
            ILoaiLyDoBienDongService loaiLyDoBienDongService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._workContext = workContext;
            this._loaiDonViRepository = loaiDonViRepository;
            this._donViRepository = donViRepository;
            this._loaiLyDoBienDongService = loaiLyDoBienDongService;
        }

        #endregion

        #region Methods
        public virtual IList<LyDoBienDong> GetTable()
        {
            return _staticCacheManager.Get("GET_ALL_LY_DO_BIEN_DONG", () =>
            {
                return _itemRepository.Table.OrderBy(c => c.MA).ToList();
            });
        }
        public virtual IList<LyDoBienDong> GetAllLyDoBienDongs()
        {
            return GetTable();
        }
        public virtual IList<LyDoBienDong> GetAllLyDoBienDongsChuaDb()
        {
            return _itemRepository.Table.Where(c => c.DB_ID == null).ToList();
        }
        public virtual IList<LyDoBienDong> GetLyDoBienDongs(decimal? LoaiHinhTaiSanId = 0, decimal? loailydoId = 0)
        {
            var query = GetTable().AsQueryable<LyDoBienDong>();

            if (LoaiHinhTaiSanId > 0)
            {
                //var strloai_hinh_tai_san = LoaiHinhTaiSanId.ToString();
                //query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloai_hinh_tai_san)).OrderByDescending(c => c.LOAI_HINH_TAI_SAN_ID);


                var strloaiHinhTSId = "," + LoaiHinhTaiSanId + ",";
                var strloaiHinhTSId1 = "[" + LoaiHinhTaiSanId;
                var strloaiHinhTSId2 = LoaiHinhTaiSanId + "]";
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId1.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId2.ToString()));
            }
            if (loailydoId > 0 && loailydoId != 12)
            {
                query = query.Where(c => c.LOAI_LY_DO_ID == loailydoId);
            }
            if (_workContext.CurrentDonVi != null)
            {
                var currentDV = _donViRepository.GetById(_workContext.CurrentDonVi.ID);
                if (currentDV != null)
                {
                    string str = $",{currentDV.LOAI_DON_VI_ID},";
                    query = query.Where(c => c.LOAI_DON_VI == null || c.LOAI_DON_VI.Contains(str));
                }
            }

            return query.ToList();
        }
        public bool CheckMaLyDoBienDong(decimal id = 0, string ma = null)
        {
            return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id);
        }

        public virtual IPagedList<LyDoBienDong> SearchLyDoBienDongs(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, Decimal? loaiHinhTSId = -1, Decimal? loaiLyDoId = 0, string strLoaiHinhTSIds = null)
        {
            var query = GetTable().AsQueryable<LyDoBienDong>();

            if (!String.IsNullOrEmpty(Keysearch))
            {
                query = query.Where(c => c.TEN.ToLower().Contains(Keysearch.ToLower()) || c.MA.ToLower().Contains(Keysearch.ToLower()));
            }
            if (loaiHinhTSId > 0)
            {
                //var strloaiHinhTSId = "-" + loaiHinhTSId + "-";
                //query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(loaiHinhTSId.ToString()));
                //query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || JsonConvert.DeserializeObject<List<int>>(c.LOAI_HINH_TAI_SAN_AP_DUNG_ID).Contains((int)loaiHinhTSId));
                //var a = query.ToList();

                var strloaiHinhTSId = "," + loaiHinhTSId + ",";
                var strloaiHinhTSId1 = "[" + loaiHinhTSId;
                var strloaiHinhTSId2 = loaiHinhTSId + "]";
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId1.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId2.ToString()));
            }
            if (loaiLyDoId > 0)
            {
                query = query.Where(c => c.LOAI_LY_DO_ID == loaiLyDoId);
            }

            query = query.OrderBy(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID).ThenBy(c => c.TRUONG_SAP_XEP).ThenBy(c => c.MA);
            return new PagedList<LyDoBienDong>(query, pageIndex, pageSize);
        }

        public virtual LyDoBienDong GetLyDoBienDongById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }

        public virtual IList<LyDoBienDong> GetLyDoBienDongByIds(decimal[] Ids)
        {
            var query = GetTable();
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertLyDoBienDong(LyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void InsertLyDoBienDong(List<LyDoBienDong> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Insert(entities);
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateLyDoBienDong(LyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Update(entity);
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void UpdateLyDoBienDong(List<LyDoBienDong> entities)
        {
            if (entities == null || (entities != null && entities.Count == 0))
                throw new ArgumentNullException(nameof(entities));
            _itemRepository.Update(entities);
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteLyDoBienDong(LyDoBienDong entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            _staticCacheManager.Remove("GET_ALL_LY_DO_BIEN_DONG");
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
        public virtual LyDoBienDong GetLyDoBienDongByMa(string Ma, int loai_hinh_tai_san = 0)
        {
            var query = GetTable().AsQueryable<LyDoBienDong>();
            if (!string.IsNullOrEmpty(Ma))
                query = query.Where(c => c.MA == Ma);
            if (loai_hinh_tai_san > 0)
            {
                //var strloai_hinh_tai_san = loai_hinh_tai_san.ToString();
                //query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloai_hinh_tai_san)).OrderByDescending(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID);

                string strloaiHinhTSId = "," + loai_hinh_tai_san + ",";
                string strloaiHinhTSId1 = "[" + loai_hinh_tai_san;
                string strloaiHinhTSId2 = loai_hinh_tai_san + "]";
                query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId1.ToString())
                                                                                || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId2.ToString()));
            }
            return query.FirstOrDefault();
        }
        /// <summary>
        /// lấy lý do biến động theo tên và loại hình tài sản
        /// </summary>
        /// <param name="ten"></param>
        /// <param name="loai_hinh_tai_san"></param>
        /// <returns></returns>
        public virtual LyDoBienDong GetLyDoBienDongByTEN_LOAI_HINH_TS(string ten, int loai_hinh_tai_san)
        {
            if (string.IsNullOrEmpty(ten))
                return null;
            var query = GetTable().Where(c => c.TEN == ten);
            //var strloai_hinh_tai_san = loai_hinh_tai_san.ToString();
            //query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloai_hinh_tai_san)).OrderByDescending(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID);

            string strloaiHinhTSId = "," + loai_hinh_tai_san + ",";
            string strloaiHinhTSId1 = "[" + loai_hinh_tai_san + ",";
            string strloaiHinhTSId2 = "," + loai_hinh_tai_san + "]";
            query = query.Where(c => c.LOAI_HINH_TAI_SAN_AP_DUNG_ID == null || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId.ToString())
                                                                            || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId1.ToString())
                                                                            || c.LOAI_HINH_TAI_SAN_AP_DUNG_ID.Contains(strloaiHinhTSId2.ToString()));
            return query.OrderByDescending(c => c.LOAI_HINH_TAI_SAN_ID).FirstOrDefault();
        }
        public virtual List<LyDoBienDong> GetLyDoBienDongByLoaiLyDoBienDong(string MA_LOAI_LY_DO_BIEN_DONG)
        {
            var loaiLDBD = _loaiLyDoBienDongService.GetLoaiLyDoBienDongByMa(ma: MA_LOAI_LY_DO_BIEN_DONG);
            if (loaiLDBD != null)
            {
                var list_loaiLyDoBD = _loaiLyDoBienDongService.GetIQueryableLoaiLyDoBienDongByParent(loaiLDBD.ID, loaiLDBD.TREE_NODE);
                if (list_loaiLyDoBD != null && list_loaiLyDoBD.Count() > 0)
                {
                    var query = from ldBD in _itemRepository.Table
                                join loai_ldBD in list_loaiLyDoBD
                                on ldBD.LOAI_LY_DO_BIEN_DONG_ID equals loai_ldBD.ID
                                select ldBD;
                    return query.ToList();
                }
            }
            return new List<LyDoBienDong>();
        }
        public virtual decimal? GetIdLyDoBienDongByMa(string MA_LY_DO_BIEN_DONG)
        {
            var lyDoBienDong = _itemRepository.Table.FirstOrDefault(p => p.MA == MA_LY_DO_BIEN_DONG);
            if (lyDoBienDong != null)
                return lyDoBienDong.ID;
            else return null;
        }
        #endregion
    }
}

