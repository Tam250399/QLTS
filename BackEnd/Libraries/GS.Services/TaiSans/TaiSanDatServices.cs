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
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.DanhMuc;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.TaiSans
{
    public partial class TaiSanDatService : ITaiSanDatService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<TaiSanDat> _itemRepository;
        private readonly IDiaBanService _diaBanService;
        #endregion

        #region Ctor

        public TaiSanDatService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<TaiSanDat> itemRepository, 
            IDiaBanService diaBanService
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
            this._diaBanService = diaBanService;
        }

        #endregion

        #region Methods
        public virtual IList<TaiSanDat> GetAllTaiSanDats()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<TaiSanDat> SearchTaiSanDats(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<TaiSanDat>(query, pageIndex, pageSize); ;
        }

        public virtual TaiSanDat GetTaiSanDatByTaiSanId(decimal TaiSanId)
        {
            if (TaiSanId == 0)
                return null;
            return _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId).FirstOrDefault();
        }
        public virtual TaiSanDat GetTaiSanDatById(decimal Id)
        {
            if (Id == 0)
                return null;
            return _itemRepository.GetById(Id);
        }
        /// <summary>
        /// Lấy tài sản đất theo mã tài sản, đơn vị đồng bộ
        /// </summary>
        /// <param name="maTS"></param>
        /// <param name="donViId"></param>
        /// <returns></returns>
        public virtual TaiSanDat GetTaiSanDatByMaTSAndDonVi(string maTS = null, decimal? donViId = null)
        {
            var query = _itemRepository.Table;
            if (!string.IsNullOrEmpty(maTS))
                query = query.Where(c => c.taisan.MA == maTS);
            if (donViId.HasValue)
                query = query.Where(c => c.taisan.DON_VI_ID == donViId);
            return query.FirstOrDefault();
        }
        public virtual IList<TaiSanDat> GetTaiSanDatByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertTaiSanDat(TaiSanDat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateTaiSanDat(TaiSanDat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.DIA_BAN_ID == 0)
                entity.DIA_BAN_ID = null;
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteTaiSanDat(TaiSanDat entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }

		public TaiSanDat CheckDiaChiTaiSanDat(string diaChi, decimal? diaBanId = 0, decimal? donViId = 0)
		{
            var query = _itemRepository.Table.Where(c => c.DIA_BAN_ID == diaBanId);
            if (!string.IsNullOrEmpty(diaChi))
            {
                query = query.Where(c => c.DIA_CHI.ToLower() == diaChi.ToLower());
            }
            if (donViId > 0)
			{
                query = query.Where(c => c.taisan.DON_VI_ID == donViId);
			}
            return query.FirstOrDefault();
		}
        public void GetDiaBanInfoByMaDiaBan(decimal? DiaBanId = 0, TaiSanDat item = null)
        {
            if (DiaBanId > 0 && item != null)
            {
                var diaban = _diaBanService.GetDiaBanById(DiaBanId ?? 0);
                switch (diaban.LOAI_DIA_BAN_ID)
                {
                    case (int)enumLOAI_DIABAN.XA:
                        var diaBanCha = _diaBanService.GetDiaBanById(diaban.PARENT_ID ?? 0);
                        var diaBanChaParentId = diaBanCha != null ? (decimal?)diaBanCha.PARENT_ID : null;
                        item.XA_ID = (int)diaban.ID;
                        item.HUYEN_ID = (int)diaban.PARENT_ID;
                        item.TINH_ID = (int)diaBanChaParentId;
                        break;
                    case (int)enumLOAI_DIABAN.HUYEN:

                        item.HUYEN_ID = (int)diaban.ID;
                        item.TINH_ID = (int)diaban.PARENT_ID;
                        break;
                    case (int)enumLOAI_DIABAN.TINH:
                        item.TINH_ID = (int)diaban.ID;
                        break;
                }
            }
        }
        #endregion
    }
}

