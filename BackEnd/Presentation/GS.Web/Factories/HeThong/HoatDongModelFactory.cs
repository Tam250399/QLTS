using GS.Core.Domain.HeThong;
using GS.Services.HeThong;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using GS.Web.Models.HeThong;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.Web.Factories.HeThong
{
    public partial class HoatDongModelFactory : IHoatDongModelFactory
    {
        #region Fields

        //private readonly IQuanTriCoSoModelFactory _quantriModelFactory;
        private readonly IHoatDongService _hoatDongService;
        private readonly INguoiDungService _nguoiDungService;

        #endregion

        #region Ctor
        public HoatDongModelFactory(
            IHoatDongService HoatDongService,
            INguoiDungService NguoiDungService
            )
        {
            this._hoatDongService = HoatDongService;
            this._nguoiDungService = NguoiDungService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public HoatDongSearchModel PrepareHoatDongSearchModel(HoatDongSearchModel searchModel)
        {
            searchModel.KIEU_HOAT_DONG_List = this.PrepareSelectListLoaiHoatDong();
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            searchModel.SetGridPageSize();
            return searchModel;
        }

        /// <summary>
        /// Get list activities
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public HoatDongListModel PrepareHoatDongListModel(HoatDongSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _hoatDongService.SearchActivities(pageIndex: searchModel.Page - 1, pageSize: searchModel.PageSize, KeySearch: searchModel.DIA_CHI_MAY, TEN_DANG_NHAP: searchModel.TEN_DANG_NHAP, TEN_DOI_TUONG: searchModel.TEN_DOI_TUONG, KIEU_HOAT_DONG: searchModel.KIEU_HOAT_DONG, FromDate: searchModel.FromDate, ToDate: searchModel.ToDate);

            //prepare list model
            var model = new HoatDongListModel
            {
                Data = items.Select(c =>
                {
                    var activity = c.ToModel<HoatDongModel>();
                    if (activity.NGUOI_DUNG_ID != null)
                    {
                        var user = _nguoiDungService.GetNguoiDungById(activity.NGUOI_DUNG_ID);
                        activity.TEN_DANG_NHAP = user.TEN_DANG_NHAP;
                        activity.TEN_DAY_DU = user.TEN_DAY_DU;
                    }
                    return activity;
                }).ToList(),
                Total = items.TotalCount
            };
            return model;
        }


        /// <summary>
        /// bind entity to model
        /// </summary>
        /// <param name="model"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public HoatDongModel PrepareHoatDongModel(HoatDongModel model, HoatDong item)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = model ?? item.ToModel<HoatDongModel>();
                //model.DU_LIEU = JValue.Parse(model.DU_LIEU).ToString(Formatting.Indented);
            }
            //more

            return model;
        }

        /// <summary>
        /// Get list activities types
        /// </summary>
        /// <returns></returns>
        public IList<SelectListItem> PrepareSelectListLoaiHoatDong()
        {
            IList<SelectListItem> selectList = _hoatDongService.GetAllLoaiHoatDong().Select(c => new SelectListItem()
            {
                Text = c.TEN.ToString(),
                Value = c.ID.ToString(),
            }).ToList();
            selectList.Insert(0, new SelectListItem
            {
                Text = "--Chọn loại hoạt động--",
                Value = "0"
            });
            return selectList;
        }
        #endregion
    }
}
