using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Data;

namespace GS.Services.DanhMuc
{
	public class DonViLichSuService : IDonViLichSuService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<DonViLichSu> _itemRepository;
		#endregion

		#region Ctor

		public DonViLichSuService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<DonViLichSu> itemRepository
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
		public void DeleteDonViLichSu(DonViLichSu entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
		}

		public DonViLichSu GetDonViLichSuById(decimal Id)
		{
			if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		}

		public IList<DonViLichSu> GetDonViLichSuByIds(decimal[] newsIds)
		{
			var query = _itemRepository.Table;
			return query.Where(c => newsIds.Contains(c.ID)).ToList();
		}

		public IList<DonViLichSu> GetDonViLichSus()
		{
			return _itemRepository.Table.ToList();
		}

		public void InsertDonViLichSu(DonViLichSu entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
		}

		public void InsertDonViLichSu(List<DonViLichSu> entities)
		{
			if (entities == null || entities.Count == 0)
				throw new ArgumentNullException(nameof(entities));
			_itemRepository.Insert(entities);
		}

		public IPagedList<DonViLichSu> SearchDonViLichSus(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null)
		{
			var query = _itemRepository.Table;
			if (!String.IsNullOrEmpty(Keysearch))
			{
				Keysearch = Keysearch.ToUpper();
				query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch));
			}
			return new PagedList<DonViLichSu>(query, pageIndex, pageSize); ;
		}

		public void UpdateDonViLichSu(DonViLichSu entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
		}

		public void UpdateDonViLichSu(List<DonViLichSu> entities)
		{
			if (entities == null || entities.Count==0)
				throw new ArgumentNullException(nameof(entities));
			_itemRepository.Update(entities);
		}
	}
}
