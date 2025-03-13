//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.NghiepVu;
using GS.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.NghiepVu
{
    public partial class YeuCauChiTietService : IYeuCauChiTietService
    {
        #region Fields
        private readonly CauHinhChung _cauhinhChung;
        private readonly ICacheManager _cacheManager;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;
        private readonly IStaticCacheManager _staticCacheManager;
        private readonly IRepository<YeuCauChiTiet> _itemRepository;
        #endregion

        #region Ctor

        public YeuCauChiTietService(CauHinhChung cauhinhChung,
            ICacheManager cacheManager,
            IDataProvider dataProvider,
            IDbContext dbContext,
            IStaticCacheManager staticCacheManager,
            IRepository<YeuCauChiTiet> itemRepository
            )
        {
            this._cauhinhChung = cauhinhChung;
            this._cacheManager = cacheManager;
            this._dataProvider = dataProvider;
            this._dbContext = dbContext;
            this._staticCacheManager = staticCacheManager;
            this._itemRepository = itemRepository;
        }

        #endregion

        #region Methods
        public virtual IList<YeuCauChiTiet> GetAllYeuCauChiTiets()
        {
            var query = _itemRepository.Table;
            return query.ToList();
        }

        public virtual IPagedList<YeuCauChiTiet> SearchYeuCauChiTiets(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
        {
            var query = _itemRepository.Table;
            return new PagedList<YeuCauChiTiet>(query, pageIndex, pageSize); ;
        }

        public virtual YeuCauChiTiet GetYeuCauChiTietById(decimal Id)
        {
            if (Id == 0)
                return null;
			var res = _itemRepository.GetById(Id);
			if (res!= null)
				GetRowFromDataJsonToEntity(res.DATA_JSON, res);
			return res;
		}
        public virtual YeuCauChiTiet GetYeuCauChiTietByYeuCauId(decimal YeuCauId)
        {
            var query = _itemRepository.Table;
            if (YeuCauId > 0)
			{
				var res = query.Where(c => c.YEU_CAU_ID == YeuCauId).FirstOrDefault();
				if (res!= null)
					GetRowFromDataJsonToEntity(res.DATA_JSON, res);
				return res;
			}
			else
                return null;
        }
        public virtual IList<YeuCauChiTiet> GetYeuCauChiTietByIds(decimal[] Ids)
        {
            var query = _itemRepository.Table;
            return query.Where(c => Ids.Contains(c.ID)).ToList();
        }

        public virtual void InsertYeuCauChiTiet(YeuCauChiTiet entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.DATA_JSON = null;
            entity.DATA_JSON = entity.toStringJson();
            _itemRepository.Insert(entity);
            //event notification
            //_eventPublisher.EntityInserted(entity);

        }
        public virtual void UpdateYeuCauChiTiet(YeuCauChiTiet entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var entity_json = new YeuCauChiTiet(entity);
            entity_json.DATA_JSON = null;
            entity.DATA_JSON = entity_json.toStringJson();
            _itemRepository.Update(entity);
            //event notification
            //_eventPublisher.EntityUpdated(entity);            
        }
        public virtual void DeleteYeuCauChiTiet(YeuCauChiTiet entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _itemRepository.Delete(entity);
            //event notification
            //_eventPublisher.EntityDeleted(entity);
        }
		public virtual IList<YeuCauChiTiet> GetYeuCauChiTietByYeuCauIds(IList<decimal> YeuCauIds)
		{
			var query = _itemRepository.Table;
			if (YeuCauIds!= null && YeuCauIds.Count>0)
				return query.Where(c => YeuCauIds.Contains( c.YEU_CAU_ID) ).ToList();
			else
				return null;
		}
		private void GetRowFromDataJsonToEntity(string dataJson, YeuCauChiTiet entity)
		{
			if (!string.IsNullOrEmpty(dataJson) && entity!= null)
			{
				var ycct_json = dataJson.toEntity<YeuCauChiTiet>();
				if (ycct_json!= null)
				{
					entity.HS_CNQSD_SO = ycct_json.HS_CNQSD_SO;
					entity.HS_CNQSD_NGAY = ycct_json.HS_CNQSD_NGAY;
					entity.HS_QUYET_DINH_GIAO_SO = ycct_json.HS_QUYET_DINH_GIAO_SO;
					entity.HS_QUYET_DINH_GIAO_NGAY = ycct_json.HS_QUYET_DINH_GIAO_NGAY;
					entity.HS_CHUYEN_NHUONG_SD_SO = ycct_json.HS_CHUYEN_NHUONG_SD_SO;
					entity.HS_CHUYEN_NHUONG_SD_NGAY = ycct_json.HS_CHUYEN_NHUONG_SD_NGAY;
					entity.HS_QUYET_DINH_CHO_THUE_SO = ycct_json.HS_QUYET_DINH_CHO_THUE_SO;
					entity.HS_QUYET_DINH_CHO_THUE_NGAY = ycct_json.HS_QUYET_DINH_CHO_THUE_NGAY;
					entity.HS_KHAC = ycct_json.HS_KHAC;
					entity.HS_QUYET_DINH_BAN_GIAO = ycct_json.HS_QUYET_DINH_BAN_GIAO;
					entity.HS_QUYET_DINH_BAN_GIAO_NGAY = ycct_json.HS_QUYET_DINH_BAN_GIAO_NGAY;
					entity.HS_BIEN_BAN_NGHIEM_THU = ycct_json.HS_BIEN_BAN_NGHIEM_THU;
					entity.HS_BIEN_BAN_NGHIEM_THU_NGAY = ycct_json.HS_BIEN_BAN_NGHIEM_THU_NGAY;
					entity.HS_PHAP_LY_KHAC = ycct_json.HS_PHAP_LY_KHAC;
					entity.HS_PHAP_LY_KHAC_NGAY = ycct_json.HS_PHAP_LY_KHAC_NGAY;
					entity.HS_HOP_DONG_CHO_THUE_SO = ycct_json.HS_HOP_DONG_CHO_THUE_SO;
					entity.HS_HOP_DONG_CHO_THUE_NGAY = ycct_json.HS_HOP_DONG_CHO_THUE_NGAY;
                    //
                    entity.NGAY_BAN_THANH_LY = ycct_json.NGAY_BAN_THANH_LY;
                    entity.TAI_SAN_TRUOC_DIEU_CHUYEN_ID = ycct_json.TAI_SAN_TRUOC_DIEU_CHUYEN_ID;
                    entity.KH_TY_LE_NGUYEN_GIA_KHAU_HAO = ycct_json.KH_TY_LE_NGUYEN_GIA_KHAU_HAO;
                    //
                    entity.DAT_GIA_TRI_QUYEN_SD_DAT = ycct_json.DAT_GIA_TRI_QUYEN_SD_DAT;
                    entity.OTO_SO_CAU_XE = ycct_json.OTO_SO_CAU_XE;

                }
			}
		}
		#endregion
	}
}

