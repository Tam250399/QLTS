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
using GS.Core.Domain.NghiepVu;
using GS.Core.Domain.TaiSans;
using GS.Data;
using GS.Services.BienDongs;
using GS.Services.NghiepVu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.TaiSans
{
	public partial class TaiSanNguonVonService : ITaiSanNguonVonService
	{
		#region Fields

		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<TaiSanNguonVon> _itemRepository;
		private readonly IYeuCauService _yeuCauService;
		private readonly IBienDongService _bienDongService;

		#endregion Fields

		#region Ctor

		public TaiSanNguonVonService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<TaiSanNguonVon> itemRepository,
			IYeuCauService yeuCauService,
			IBienDongService bienDongService
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._itemRepository = itemRepository;
			this._yeuCauService = yeuCauService;
			this._bienDongService = bienDongService;
		}

		#endregion Ctor

		#region Methods

		public virtual IList<TaiSanNguonVon> GetTaiSanNguonVons(decimal? taisanId = 0, decimal? BienDongID = 0)
		{
			var query = _itemRepository.Table;
			if (taisanId > 0)
			{
				query = query.Where(c => c.TAI_SAN_ID == taisanId);
			}
			if (BienDongID > 0)
			{
				query = query.Where(c => c.BIEN_DONG_ID == BienDongID);
			}
			return query.ToList();
		}

		public virtual IPagedList<TaiSanNguonVon> SearchTaiSanNguonVons(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
		{
			var query = _itemRepository.Table;
			return new PagedList<TaiSanNguonVon>(query, pageIndex, pageSize); ;
		}

		public virtual TaiSanNguonVon GetTaiSanNguonVonById(decimal Id)
		{
			if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		}

		public virtual TaiSanNguonVon GetTaiSanNguonVonByTaiSanIdFirst(decimal TaiSanId, decimal NguonVonId)
		{
			return _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId && c.NGUON_VON_ID == NguonVonId).FirstOrDefault();
		}

		public virtual List<TaiSanNguonVon> GetTaiSanNguonVonByBienDongId(decimal IdBienDong)
		{
			var q = _itemRepository.Table;
			q = q.Where(p => p.BIEN_DONG_ID == IdBienDong);
			return q.ToList();
		}

		public virtual IList<TaiSanNguonVon> GetTaiSanNguonVonByIds(decimal[] Ids)
		{
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}

		public virtual void InsertTaiSanNguonVon(TaiSanNguonVon entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
			//event notification
			//_eventPublisher.EntityInserted(entity);
		}

		public virtual void InsertTaiSanNguonVons(List<TaiSanNguonVon> entities)
		{
			if (entities == null || entities.Count == 0)
				throw new ArgumentNullException(nameof(entities));
			_itemRepository.Insert(entities);
		}

		public virtual void UpdateTaiSanNguonVon(TaiSanNguonVon entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
			//event notification
			//_eventPublisher.EntityUpdated(entity);
		}

		public virtual void DeleteTaiSanNguonVon(TaiSanNguonVon entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}

		public virtual void DeleteTaiSanNguonVons(IList<TaiSanNguonVon> entities)
		{
			if (entities == null || entities.Count == 0)
				throw new ArgumentNullException(nameof(entities));
			_itemRepository.Delete(entities: entities);
		}

		public virtual void DeleteTaiSanNguonVonByTaiSanId(decimal TaiSanId)
		{
			var lstTaiSanNguonVon = _itemRepository.Table.Where(c => c.TAI_SAN_ID == TaiSanId).ToList();
			foreach (var item in lstTaiSanNguonVon)
			{
				_itemRepository.Delete(item);
			}
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}

		/// <summary>
		/// tạm thời chưa cần
		/// </summary>
		/// <param name="TaiSanId"></param>
		/// <returns></returns>
		public virtual IList<TaiSanNguonVon> GetAllSumNguonVonNumberOfTaiSan(decimal TaiSanId, DateTime? NgayBienDongDen = null)
		{
			var yeuCausTs =  _yeuCauService.GetYeuCaus(TaiSanId, TrangThaiId: (int)enumTRANG_THAI_YEU_CAU.CHO_DUYET).AsQueryable<YeuCau>();
			var bienDongTS = _bienDongService.GetBienDongsByTaiSanId(TaiSanId, NgayBienDongDen).Select(c => c.ID);
			if (NgayBienDongDen.HasValue)
				yeuCausTs = yeuCausTs.Where(p => p.NGAY_BIEN_DONG < NgayBienDongDen);
			var listNguonVonYeuCau = (yeuCausTs!=null&& yeuCausTs.Count()>0)? yeuCausTs.Where(p=>p.NGUON_VON_JSON!= null).Select(p => p.NGUON_VON_JSON.toEntities<NguonVonModelCore>()).SelectMany(p=>p).Select(p=> new TaiSanNguonVon(p,TaiSanId)).ToList(): new List<TaiSanNguonVon>();

			var query = _itemRepository.Table.Where(c => bienDongTS.Contains(c.BIEN_DONG_ID)).ToList();
			var list_taiSanNguonVon = query
				.Union(listNguonVonYeuCau)
				.GroupBy(p => p.NGUON_VON_ID)
				.Select(p => new TaiSanNguonVon { TAI_SAN_ID = TaiSanId, NGUON_VON_ID = p.Key, GIA_TRI= p.Sum(c => c.GIA_TRI) });
			return list_taiSanNguonVon.ToList();
		}

		#endregion Methods
	}
}