//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 20/5/2020
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
using Castle.Components.DictionaryAdapter;

namespace GS.Services.HeThong
{
	public partial class VaiTroWidgetService:IVaiTroWidgetService
	{				
		 #region Fields
			private readonly CauHinhChung _cauhinhChung;
			private readonly ICacheManager _cacheManager;
			private readonly IDataProvider _dataProvider;
			private readonly IDbContext _dbContext;           
			private readonly IStaticCacheManager _staticCacheManager;
			private readonly IRepository<VaiTroWidget> _itemRepository;
			private readonly IRepository<VaiTroNguoiDungMapping> _vaiTroNguoiDungMappingRepository;
			private readonly IRepository<Widget> _widgetRepository;
			private readonly IRepository<NguoiDung> _nguoiDungRepository;
		 #endregion
		 
		 #region Ctor

		public VaiTroWidgetService(CauHinhChung cauhinhChung,
			ICacheManager cacheManager,
			IDataProvider dataProvider,
			IDbContext dbContext,            
			IStaticCacheManager staticCacheManager,            
			IRepository<VaiTroWidget> itemRepository,
			IRepository<Widget> widgetRepository,
			IRepository<NguoiDung> nguoiDungRepository,
			IRepository<VaiTroNguoiDungMapping> vaiTroNguoiDungMappingRepository
			)
		{
			this._cauhinhChung = cauhinhChung;
			this._cacheManager = cacheManager;
			this._dataProvider = dataProvider;
			this._dbContext = dbContext;
			this._staticCacheManager = staticCacheManager;    
			this._itemRepository = itemRepository;
			this._vaiTroNguoiDungMappingRepository = vaiTroNguoiDungMappingRepository;
			this._nguoiDungRepository = nguoiDungRepository;
			this._widgetRepository = widgetRepository;
		}

		#endregion
		
		#region Methods
		public virtual IList<VaiTroWidget> GetAllVaiTroWidgets(){
			var query = _itemRepository.Table;
			 return query.ToList();
		 }

		public virtual bool AuthorizeWidget(decimal userId, string partialViewName)
		{
			var qNguoiDung = _nguoiDungRepository.GetById(userId);
			var listVaiTroNguoiDung = qNguoiDung.VaiTro.Select(c => c.ID).ToList();
			if (string.IsNullOrEmpty(partialViewName))
			{
				return false;
			}
			else
			{
				var widget = _widgetRepository.Table.Where(c => c.PARTIAL_VIEW_NAME == partialViewName).FirstOrDefault();
				var q = _itemRepository.Table.Where(c => c.WIDGET_ID == widget.ID);
				if (q == null)
				{
					return false;
				}
				else
				{
					var listVaiTroWidget = q.Select(c => c.VAI_TRO_ID).ToList();
					foreach (var vaitro in listVaiTroNguoiDung)
					{
						if (listVaiTroWidget.Contains(vaitro))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public virtual IPagedList <VaiTroWidget> SearchVaiTroWidgets(int pageIndex = 0, int pageSize = int.MaxValue,string Keysearch = null){
			 var query = _itemRepository.Table;
			 return new PagedList<VaiTroWidget>(query, pageIndex, pageSize);
		 }    
		
		public virtual VaiTroWidget GetVaiTroWidgetById(decimal Id){
			  if (Id == 0)
				return null;
			return _itemRepository.GetById(Id);
		 }

		public virtual IList<VaiTroWidget> GetVaiTroWidgetsByVaiTro(decimal vaiTroId)
		{
			if(vaiTroId == 0)
			{
				return null;
			}
			var list = _itemRepository.Table;
			return list.Where(c => c.VAI_TRO_ID == vaiTroId).ToList();
		}

		public virtual IList<VaiTroWidget> GetVaiTroWidgetsByWidget(decimal widgetId)
		{
			if(widgetId == 0)
			{
				return null;
			}
			var list = _itemRepository.Table;
			return list.Where(c => c.WIDGET_ID == widgetId).ToList();
		}

		public virtual IList<VaiTroWidget> GetVaiTroWidgetByIds(decimal[] Ids){
			var query = _itemRepository.Table;
			return query.Where(c => Ids.Contains(c.ID)).ToList();
		}
		
		public virtual void InsertVaiTroWidget(VaiTroWidget entity){
			if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Insert(entity);
			//event notification
			//_eventPublisher.EntityInserted(entity);
			
		}
		public virtual void UpdateVaiTroWidget(VaiTroWidget entity){
			 if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Update(entity);
			//event notification
			//_eventPublisher.EntityUpdated(entity);            
		}
		public virtual void DeleteVaiTroWidget(VaiTroWidget entity){
			 if (entity == null)
				throw new ArgumentNullException(nameof(entity));
			_itemRepository.Delete(entity);
			//event notification
			//_eventPublisher.EntityDeleted(entity);
		} 
		
		#endregion	
	 }
}		

