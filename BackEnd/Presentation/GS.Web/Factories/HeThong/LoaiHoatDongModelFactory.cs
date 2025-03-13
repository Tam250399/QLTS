using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using System;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public class LoaiHoatDongModelFactory : ILoaiHoatDongModelFactory
    {
        #region Fields
        private readonly IHoatDongService _hoatDongService;
        #endregion
        #region Ctor
        public LoaiHoatDongModelFactory(
            IHoatDongService hoatDongService
            )
        {
            this._hoatDongService = hoatDongService;
        }
        #endregion
        #region Methods
        public bool CheckTrungMa(string Ma, decimal id = 0)
        {
            var obj = _hoatDongService.GetAllLoaiHoatDong().Where(c => c.TU_KHOA_HE_THONG.Equals(Ma.ToLower())).FirstOrDefault();
            if (id == 0)
            {
                if (obj != null)
                {
                    return false;
                    //return View(model);
                }
            }
            else
            {
                if (obj != null && obj.ID != id)
                {
                    return false;
                    //return View(model);
                }
            }
            return true;
        }

        public void PrepareLoaiHoatDong(LoaiHoatDongModel model, LoaiHoatDong item)
        {
            item.TU_KHOA_HE_THONG = model.TU_KHOA_HE_THONG;
            item.TEN = model.TEN;
            item.TINH_KHA_DUNG = model.TINH_KHA_DUNG;
        }

        public LoaiHoatDongListModel PrepareLoaiHoatDongListModel(LoaiHoatDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _hoatDongService.SearchLoaiHoatDongs(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, keywordSystem: searchModel.TU_KHOA_HE_THONG, name: searchModel.TEN);

            //prepare list model
            var model = new LoaiHoatDongListModel
            {
                //fill in model values from the entity
                //Data = items.Select(c => c.ToModel<NhanHienThiModel>()),
                Data = items.Select(c => c.ToModel<LoaiHoatDongModel>()),
                Total = items.TotalCount
            };
            return model;
        }

        public LoaiHoatDongModel PrepareLoaiHoatDongModel(LoaiHoatDongModel model, LoaiHoatDong item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = model ?? item.ToModel<LoaiHoatDongModel>();
            }
            //more

            return model;
        }



        public LoaiHoatDongSearchModel PrepareLoaiHoatDongSearchModel(LoaiHoatDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            searchModel.SetGridPageSize();
            return searchModel;
        }
        #endregion
    }
}
