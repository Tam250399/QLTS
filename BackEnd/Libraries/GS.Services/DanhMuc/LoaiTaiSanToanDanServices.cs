using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Domain.CauHinh;
using GS.Core.Domain.DanhMuc;
using GS.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Services.DanhMuc
{
	public partial class LoaiTaiSanToanDanService : ILoaiTaiSanToanDanService
	{
		#region Fields
		private readonly CauHinhChung _cauhinhChung;
		private readonly ICacheManager _cacheManager;
		private readonly IDataProvider _dataProvider;
		private readonly IDbContext _dbContext;
		private readonly IStaticCacheManager _staticCacheManager;
		private readonly IRepository<LoaiTaiSan> _itemRepository;
		private readonly IMappingLoaiTaiSanService _mappingLoaiTaiSanService;
		#endregion
		#region Ctor
		public LoaiTaiSanToanDanService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,
			IStaticCacheManager staticCacheManager,
			IRepository<LoaiTaiSan> itemRepository,
			IMappingLoaiTaiSanService mappingLoaiTaiSanService
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;
			this._itemRepository = itemRepository;
			this._mappingLoaiTaiSanService = mappingLoaiTaiSanService;
		}

		#endregion

		#region Methods

		public virtual IList<LoaiTaiSan> GetAllLoaiTaiSans(decimal CheDoHaoMon = 0)
		{
			var query = _itemRepository.Table;
			if (CheDoHaoMon > 0)
			{
				query = query.Where(m => m.CHE_DO_HAO_MON_ID == CheDoHaoMon);
			}
			return query.OrderBy(c => c.TREE_NODE).ToList();
		}
		public virtual IList<LoaiTaiSan> GetAllLoaiTaiSansChuaDb()
		{
			var valexclude = new decimal?[] { (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_KHAC, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_DAT, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_NHA, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_OTO, (decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_TOAN_DAN_TAI_SAN_KHAC };
			var query = _itemRepository.Table.Where(c => c.DB_ID == null && !valexclude.Contains(c.LOAI_HINH_TAI_SAN_ID));
			return query.OrderBy(c => c.TREE_NODE).ToList();
		}
		public virtual IList<LoaiTaiSan> GetAllLoaiTaiSanToanDansChuaDb()
		{
			var valexclude = new decimal?[] {   (decimal)enumLOAI_HINH_TAI_SAN.DAT,
												(decimal)enumLOAI_HINH_TAI_SAN.NHA,
												(decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_VAT_KIEN_TRUC,
												(decimal)enumLOAI_HINH_TAI_SAN.OTO,
												(decimal)enumLOAI_HINH_TAI_SAN.PHUONG_TIEN_KHAC,
												(decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_MAY_MOC_THIET_BI,
												(decimal)enumLOAI_HINH_TAI_SAN.TAI_SAN_CAY_LAU_NAM_SVLV,
												(decimal)enumLOAI_HINH_TAI_SAN.HUU_HINH_KHAC,
												(decimal)enumLOAI_HINH_TAI_SAN.VO_HINH,
												(decimal)enumLOAI_HINH_TAI_SAN.DAC_THU
			};
			var query = _itemRepository.Table.Where(c => c.DB_ID == null && !valexclude.Contains(c.LOAI_HINH_TAI_SAN_ID));
			return query.OrderBy(c => c.TREE_NODE).ToList();
		}
		public virtual IList<LoaiTaiSan> GetLoaiTaiSans(decimal LoaiHinhTaiSanId = 0, string TreeNode = null, decimal cheDoId = 0, List<decimal> ListLoaiHinhTaiSanId = null)
		{
			var query = _itemRepository.Table;
			if (!String.IsNullOrEmpty(TreeNode))
			{
				query = query.Where(c => c.TREE_NODE.Contains(TreeNode));
			}
			if (LoaiHinhTaiSanId > 0)
			{
				query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSanId);
			}
			if (cheDoId > 0)
			{
				query = query.Where(c => c.CHE_DO_HAO_MON_ID == cheDoId);
			}
			if (ListLoaiHinhTaiSanId != null && ListLoaiHinhTaiSanId.Count() > 0)
			{
				query = query.Where(c => ListLoaiHinhTaiSanId.Contains((decimal)c.LOAI_HINH_TAI_SAN_ID));
			}

			return query.ToList();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="Keysearch"></param>
		/// <param name="ParentId"></param>
		/// <param name="ChedoId"></param>
		/// <param name="loaiHinhTaiSanId"></param>
		/// <param name="id">nếu không có key search thì lấy lts id này. nếu có key search thì tìm trong con loại tài sản này</param>
		/// <param name="isHasNoChildrend"></param>
		/// <returns></returns>
		public virtual IPagedList<LoaiTaiSan> SearchLoaiTaiSans(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0, decimal? id = 0, bool isHasNoChildrend = false)
		{
			var query = _itemRepository.Table;
			if (!String.IsNullOrEmpty(Keysearch))
			{
				Keysearch = Keysearch.ToUpper();
				query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
			}
			if (ParentId > 0)
			{
				query = query.Where(c => c.PARENT_ID == ParentId);
			}
			else if (id > 0)
			{
				if (!string.IsNullOrEmpty(Keysearch))
				{
					var ltsTreeNode = _itemRepository.GetById(id)?.TREE_NODE;
					query = query.Where(c => c.TREE_NODE.Contains(ltsTreeNode + "-"));
				}
				else
					query = query.Where(c => c.ID == id);
			}
			else if (String.IsNullOrEmpty(Keysearch))
				query = query.Where(c => c.PARENT_ID == null);

			if (ChedoId > 0)
			{
				query = query.Where(c => c.CHE_DO_HAO_MON_ID == ChedoId);
			}
			if (loaiHinhTaiSanId > 0)
			{
				query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId);
			}
			//nếu chỉ lấp cấp thấp nhất. không còn con
			if (isHasNoChildrend)
			{
				query = query.Where(c => c.ListLoaiTaiSanCon.Count == 0);
			}
			query = query.OrderBy(c => c.MA);
			return new PagedList<LoaiTaiSan>(query, pageIndex, pageSize);
		}
		/// <summary>
		/// search tất cả tài sản. Trong trường hợp không có keySeach thì lấy tất cả loại tài sản
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="Keysearch"></param>
		/// <param name="ParentId"></param>
		/// <param name="ChedoId"></param>
		/// <param name="loaiHinhTaiSanId"></param>
		/// <param name="id">lấy tất cả con của loại tài sản có id này</param>
		/// <param name="isHasNoChildrend">chỉ lấy ra những loại tài sản không có con</param>
		/// <returns></returns>
		public virtual IPagedList<LoaiTaiSan> SearchAllLoaiTaiSan(int pageIndex = 0, int pageSize = int.MaxValue, string Keysearch = null, decimal? ParentId = 0, decimal? ChedoId = 0, decimal? loaiHinhTaiSanId = 0, decimal? id = 0, bool isHasNoChildrend = false)
		{
			var query = _itemRepository.Table;
			if (!String.IsNullOrEmpty(Keysearch))
			{
				Keysearch = Keysearch.ToUpper();
				query = query.Where(c => c.TEN.ToUpper().Contains(Keysearch) || c.MA.ToUpper().Contains(Keysearch));
			}
			if (ParentId > 0)
			{
				query = query.Where(c => c.PARENT_ID == ParentId);
			}
			else if (id > 0)
			{
				var ltsTreeNode = _itemRepository.GetById(id)?.TREE_NODE;
				query = query.Where(c => c.TREE_NODE.Contains(ltsTreeNode + "-"));
			}
			if (ChedoId > 0)
			{
				query = query.Where(c => c.CHE_DO_HAO_MON_ID == ChedoId);
			}
			if (loaiHinhTaiSanId > 0)
			{
				query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTaiSanId);
			}
			//nếu chỉ lấp cấp thấp nhất. không còn con
			if (isHasNoChildrend)
			{
				query = query.Where(c => c.ListLoaiTaiSanCon.Count == 0);
			}
			query = query.OrderBy(c => c.MA);
			return new PagedList<LoaiTaiSan>(query, pageIndex, pageSize);
		}
		public int GetCountSub(decimal? ParentId = 0)
		{
			var query = _itemRepository.Table.Where(c => c.PARENT_ID == ParentId);
			return query.Count();
		}
		public virtual LoaiTaiSan GetLoaiTaiSanById(decimal Id)
		{
			if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		}
		public virtual LoaiTaiSan GetLoaiTaiSanByMa(string ma, String cheDoHaoMonId = enumCheDoHaoMon_ThongTu.CDHM_TT45)
		{

			if (String.IsNullOrEmpty(ma))
				return null;
			return _itemRepository.Table.Where(c => c.MA == ma && c.CheDoHaoMon.MA_CHE_DO == cheDoHaoMonId).FirstOrDefault();
		}

		public virtual IList<LoaiTaiSan> GetLoaiTaiSanByIds(decimal[] Ids)
		{
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}

		public virtual void InsertLoaiTaiSan(LoaiTaiSan entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
			if (entity.PARENT_ID == null)
			{
				//entity.TREE_NODE = entity.ID.ToString();
				var parent = GetLoaiTaiSanById(entity.ID);
				entity.TREE_NODE = CommonHelper.GenTreeNode(entity.ID.ToString(), entity.ID, parent.TREE_LEVEL + 1);
				entity.TREE_LEVEL = 1;
			}
			GenTreeNode(entity);
			_staticCacheManager.Remove("GET_ALL_LOAI_TAI_SAN");
			//event notification
			//_eventPublisher.EntityInserted(entity);
		}
		public virtual void UpdateLoaiTaiSan(LoaiTaiSan entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
			GenTreeNode(entity);
			_staticCacheManager.Remove("GET_ALL_LOAI_TAI_SAN");
			//event notification
			//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void InsertLoaiTaiSan(List<LoaiTaiSan> entities)
		{
			if (entities == null || (entities != null && entities.Count == 0))
				throw new ArgumentNullException(nameof(entities));
			_itemRepository.Insert(entities);
			UpdateLoaiTaiSan(entities);
			_staticCacheManager.Remove("GET_ALL_LOAI_TAI_SAN");

			//event notification
			//_eventPublisher.EntityInserted(entity);
		}
		public virtual void UpdateLoaiTaiSan(List<LoaiTaiSan> entities)
		{
			if (entities == null || (entities != null && entities.Count == 0))
				throw new ArgumentNullException(nameof(entities));
			foreach (var entity in entities)
				if (entity.PARENT_ID == null)
				{
					//entity.TREE_NODE = entity.ID.ToString();
					var parent = GetLoaiTaiSanById(entity.ID);
					entity.TREE_NODE = CommonHelper.GenTreeNode(entity.ID.ToString(), entity.ID, parent.TREE_LEVEL + 1);
					entity.TREE_LEVEL = 1;
				}
			_itemRepository.Update(entities);
			_staticCacheManager.Remove("GET_ALL_LOAI_TAI_SAN");
			//event notification
			//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void DeleteLoaiTaiSan(LoaiTaiSan entity)
		{
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			_staticCacheManager.Remove("GET_ALL_LOAI_TAI_SAN");
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		}
		public virtual IList<LoaiTaiSan> GetForInputSearch(string KeySearch = null, decimal? loaiHinhTSId = 0, decimal? cheDoId = 0)
		{
			var query = _itemRepository.Table;
			if (!String.IsNullOrEmpty(KeySearch))
			{
				query = query.Where(c => c.TEN.Contains(KeySearch) || c.MA.Contains(KeySearch));
			}
			if (loaiHinhTSId > 0)
			{
				query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == loaiHinhTSId);
			}
			if (cheDoId > 0)
			{
				query = query.Where(c => c.CHE_DO_HAO_MON_ID == cheDoId);
			}

			return query.Take(10).ToList();
		}
		public IList<LoaiTaiSan> GetListLoaiTaiSanTreeNodeByRoot(decimal? cheDoHaoMonId = 0, decimal? LoaiHinhTaiSanId = 0, List<decimal> ListLoaiHinhTaiSanId = null, decimal? ParentLoaiTaiSanId = null, List<decimal> ListIgnoreLTS = null)
		{
			var query = _itemRepository.Table;
			if (cheDoHaoMonId > 0)
			{
				query = query.Where(c => c.CHE_DO_HAO_MON_ID == cheDoHaoMonId)
				 .OrderBy(c => c.TREE_NODE);
			}
			if (LoaiHinhTaiSanId > 0)
			{
				query = query.Where(c => c.LOAI_HINH_TAI_SAN_ID == LoaiHinhTaiSanId);
			}
			if (ListLoaiHinhTaiSanId != null && ListLoaiHinhTaiSanId.Count() > 0)
			{
				query = query.Where(c => ListLoaiHinhTaiSanId.Contains((decimal)c.LOAI_HINH_TAI_SAN_ID));
			}
			if (ListIgnoreLTS != null && ListIgnoreLTS.Count() > 0)
			{
				query = query.Where(c => !ListIgnoreLTS.Contains((decimal)c.LOAI_HINH_TAI_SAN_ID));
			}
			if (ParentLoaiTaiSanId > 0)
			{
				var ltsParent = _itemRepository.Table.FirstOrDefault(p => p.ID == ParentLoaiTaiSanId);
				if (ltsParent != null)
					query = query.Where(c => c.TREE_NODE.Contains(ltsParent.TREE_NODE + "-"));
			}
			return query.OrderBy(p => p.LOAI_HINH_TAI_SAN_ID).ThenBy(p => p.TREE_NODE).ToList<LoaiTaiSan>();
		}
		public void GenTreeNode(LoaiTaiSan entity)
		{
			if (entity.PARENT_ID > 0)
			{
				var parent = GetLoaiTaiSanById(entity.PARENT_ID ?? 0);
				entity.TREE_NODE = CommonHelper.GenTreeNode(parent.TREE_NODE, entity.ID, parent.TREE_LEVEL + 1);
				entity.TREE_LEVEL = parent.TREE_LEVEL + 1;
				_itemRepository.Update(entity);
			}
			else
			{
				entity.TREE_LEVEL = 1;
				entity.TREE_NODE = CommonHelper.GenTreeNode(null, entity.ID, null);
				_itemRepository.Update(entity);
			}
		}
		public bool CheckMaLoaiTaiSan(decimal? id = 0, string ma = null, decimal? CheDoHaoMonId = 0)
		{
			return _itemRepository.Table.Any(c => c.MA == ma && c.ID != id && c.CHE_DO_HAO_MON_ID == CheDoHaoMonId);
		}
		public virtual LoaiTaiSan GetLoaiTaiSanByTen(string Ten)
		{
			if (String.IsNullOrEmpty(Ten))
				return null;
			return _itemRepository.Table.Where(c => c.TEN.Equals(Ten) && c.CHE_DO_HAO_MON_ID == 2).FirstOrDefault();
		}
		public LoaiTaiSan GetLoaiTaiSanByIdDb(decimal IdDb = 0)
		{
			if (IdDb == 0)
				return null;
			return _itemRepository.Table.Where(m => m.DB_ID == IdDb).FirstOrDefault();
		}

		public virtual IList<LoaiTaiSan> GetLoaiTaiSanByTreeLevel(decimal TreeLevel = 0)
		{
			if (TreeLevel == 0)
				return null;
			var query = _itemRepository.Table;
			return query.Where(m => m.TREE_LEVEL == TreeLevel).ToList();
		}
		public virtual List<string> GetTenLoaiTaiSan(List<decimal> ids)
		{
			if (ids != null || ids.Count > 0)
				return _itemRepository.Table.Where(p => ids.Contains(p.ID)).Select(p => p.TEN).ToList();
			return null;
		}

		#endregion
		#region Cache
		const string key = "DM_LOAI_TAI_SAN";
		public virtual IList<LoaiTaiSan> GetTable()
		{
			return _staticCacheManager.Get(key, () =>
			{
				return _itemRepository.Table.ToList();
			});
		}
		public virtual LoaiTaiSan GetCacheLoaiTaiSanByMa(string ma)
		{
			if (String.IsNullOrEmpty(ma))
				return null;
			return GetTable().Where(c => c.MA == ma && c.CHE_DO_HAO_MON_ID == 2).FirstOrDefault();// chế độ hao mòn 162
		}
		public virtual LoaiTaiSan GetCacheLoaiTaiSanByTen(string Ten)
		{
			if (String.IsNullOrEmpty(Ten))
				return null;
			return GetTable().Where(c => c.TEN.Equals(Ten) && c.CHE_DO_HAO_MON_ID == 2).FirstOrDefault();
		}
		#endregion

	}
}
