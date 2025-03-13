//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 24/2/2020
//----------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using GS.Core;
using GS.Core.Caching;
using GS.Core.Data;
using GS.Core.Data.Extensions;
using GS.Core.Domain.Common;
using GS.Core.Domain.HeThong;
using GS.Core.Domain.ThuocTinhs;
using GS.Core.Infrastructure;
using GS.Data;
using GS.Services.Common;
using GS.Services.HeThong;
using GS.Services.ThuocTinhs;
using GS.Web.Framework.Extensions;
using GS.Web.Areas.Admin.Infrastructure.Mapper.Extensions;

using GS.Web.Models.ThuocTinh;
using GS.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using GS.Web.Factories.DanhMuc;
using GS.Services.DanhMuc;

namespace GS.Web.Factories.ThuocTinhs
{
    public class ThuocTinhModelFactory:IThuocTinhModelFactory
	{				
         #region Fields    		
            private readonly ICacheManager _cacheManager;            
            private readonly IWorkContext _workContext;
            private readonly IStaticCacheManager _staticCacheManager;
            private readonly IThuocTinhService _itemService;
            private readonly ILoaiTaiSanModelFactory _loaiTaiSanModelFactory;
            private readonly INguoiDungService _nguoiDungService;
            private readonly IDonViService _donViService;
            private readonly IThuocTinhTaiSanService _thuocTinhTaiSanService;
            private readonly IDonViModelFactory _donViModelFactory;
        #endregion

        #region Ctor

        public ThuocTinhModelFactory(
            ICacheManager cacheManager,            
            IWorkContext workContext,
            IStaticCacheManager staticCacheManager,            
            IThuocTinhService itemService,
            ILoaiTaiSanModelFactory loaiTaiSanModelFactory,
            INguoiDungService nguoiDungService,
            IDonViService donViService,
            IThuocTinhTaiSanService thuocTinhTaiSanService,
            IDonViModelFactory donViModelFactory
            )
        {           
            this._cacheManager = cacheManager;           
            this._workContext = workContext;
            this._staticCacheManager = staticCacheManager; 
            this._itemService = itemService;
            this._loaiTaiSanModelFactory = loaiTaiSanModelFactory;
            this._nguoiDungService = nguoiDungService;
            this._donViService = donViService;
            this._thuocTinhTaiSanService = thuocTinhTaiSanService;
            this._donViModelFactory = donViModelFactory;
        }
        #endregion
        
        #region ThuocTinh
        public ThuocTinhSearchModel PrepareThuocTinhSearchModel(ThuocTinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));
            
            searchModel.SetGridPageSize();            
            return searchModel;
        }
        public modelThuocTinh PreparemodelThuocTinh(modelThuocTinh model)
        {
            //xét stt ban đầu
            if (model.stt == 0)
            {
                model.stt = 1;
            }
            //tự tăng
            else
            {
                model.stt += 1;
            }
            model.ddlLoaiDuLieu = ddlLoaiDuLieu(is_obj:true);
            return model;
        }
        public ThuocTinhListModel PrepareThuocTinhListModel(ThuocTinhSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            //get items
            var items = _itemService.SearchThuocTinhs(Keysearch:searchModel.KeySearch, pageIndex:searchModel.Page - 1, pageSize:searchModel.PageSize);
            //var x = ListModelChiTiet((int)items.FirstOrDefault().ID);
            //prepare list model
            var model = new ThuocTinhListModel
            {
                //fill in model values from the entity
                Data = items.Select(c => PrepareThuocTinhModel(new ThuocTinhModel(),c)),
                Total = items.TotalCount
            };
            return model;
        }
        public ThuocTinhModel PrepareThuocTinhModel(ThuocTinhModel model, ThuocTinh item, bool excludeProperties = false)
        {
            if (item != null)
            {
                //fill in model values from the entity
                model = item.ToModel<ThuocTinhModel>();
                var tt = (item.CAU_HINH.toEntity<ThuocTinhEntity>());
                model.LoaiDuLieu = tt != null ? tt.TYPE : " ";
                if (item.NGUOI_TAO_ID != null)
                {
                    var nguoidung = _nguoiDungService.GetNguoiDungById(item.NGUOI_TAO_ID);
                    if (nguoidung != null)
                    {
                        model.TenNguoiTao = nguoidung.TEN_DAY_DU;
                    }
                }
                if (item.DON_VI_ID != null)
                {
                    var donvi = _donViService.GetDonViById((decimal)item.DON_VI_ID);
                    if (donvi != null)
                    {
                        model.TenDonVi = donvi.TEN;
                    }
                }

            }
            //more
            model.ddlLoaiDuLieu = ddlLoaiDuLieu();
            model.ddlLoaiTaiSanId = _loaiTaiSanModelFactory.PrepareSelectListLoaiTaiSan(isAddFirst: true).ToList();
            return model;
        }
       public void PrepareThuocTinh(ThuocTinhModel model, ThuocTinh item)
        {
		item.MA = model.MA;
		item.TEN = model.TEN;
		item.CAU_HINH = model.CAU_HINH;
		item.NGAY_TAO = model.NGAY_TAO;
		item.NGUOI_TAO_ID = model.NGUOI_TAO_ID;
		item.DON_VI_ID = model.DON_VI_ID;
            
        }
        public List<SelectListItem> ddlLoaiDuLieu(bool is_obj=false)
        {
            var ddl = new List<SelectListItem>();
            if(is_obj==true)
            {
                ddl = new enumLoaiDuLieuCauHinh().ToSelectList(valuesToExclude: new int[] { (int)enumLoaiDuLieuCauHinh.OBJ }).ToList();
            }
            else
            {
                ddl = new enumLoaiDuLieuCauHinh().ToSelectList().ToList();
            }
            return ddl;
        }
        public modelThuocTinh PreparemodelThuocTinhByThuocTinh(ThuocTinh ttData, int LoaiHinhTaiSan = 0, int LoaiTaiSan = 0,int? Sapxep = null )
        {
            var model = new modelThuocTinh();
            if (ttData != null)
            {
                var item = ttData.CAU_HINH.toEntity<ThuocTinhEntity>();
                if (item != null)
                {
                    model = PreparemodelThuocTinhByThuocTinhEntity(model, item);
                }
                // gán thêm
                if (Sapxep != null)
                {
                    model.SapXep = (int)Sapxep;
                }
                else
                {
                    model.ThuocTinhId = (int)ttData.ID;
                    var ttTaiSan = _thuocTinhTaiSanService.GetThuocTinhTaiSanByLoaiTaiSanIdAndLoaiHinhTaiSanAndThuocTinhId(LoaiTaiSan: LoaiTaiSan, LoaiHinhTaiSan: LoaiHinhTaiSan, ThuocTinhId: (int)ttData.ID);
                    if (ttTaiSan != null)
                    {
                        model.SapXep = (int)ttTaiSan.SAP_XEP;
                    }
                }
            }
            return model;
        }
        public modelThuocTinh PreparemodelThuocTinhByThuocTinhEntity(modelThuocTinh model,ThuocTinhEntity item)
        {
            if (item != null)
            {
                model = item.ToModel<modelThuocTinh>();
                model.LoaiDuLieuId = model.TYPE.ToLoaiIntDuLieu();
                //chuyển đổi value theo dạng dữ liệu
                PrepareValueOfmodelThuocTinh(model);
                //nếu là obj thì chuyển đổi thuộc tính con của nó
                if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ)
                {
                    if (model.THUOC_TINH != null && model.THUOC_TINH.Count() > 0)
                    {
                        foreach (var tt in model.THUOC_TINH)
                        {
                            tt.LoaiDuLieuId = tt.TYPE.ToLoaiIntDuLieu();
                            tt.LoaiDuLieuIdParrent = model.LoaiDuLieuId;
                            tt.GuidParrent = model.GUID;
                            PrepareValueOfmodelThuocTinh(tt);
                            if(tt.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL)
                            {
                                tt.OPTION.Insert(0, new SelectListItem
                                {
                                    Value = "0",
                                    Text = "--Chọn--"
                                });
                            }
                            //set selected
                            if (tt.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL)
                            {
                                var selected = tt.OPTION.Where(c => c.Value == tt.VALUE).FirstOrDefault();
                                if (selected != null) selected.Selected = true;
                            }
                            else if (tt.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.MS)
                            {
                                var selected = tt.OPTION.Where(c => tt.ValueListInt.Contains(c.Value.ToNumberInt32())).ToList();
                                if (selected != null && selected.Count() > 0) selected.Select(c => c.Selected = true);
                            }
                        }
                    }
                }
            }
            return model;
        }
        public void PrepareValueOfmodelThuocTinh(modelThuocTinh model)
        {
            //dạng số
            if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.NB || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.RD)
            {               
               
                model.ValueSo = model.VALUE.ToNumber();
            }
            //dạng list số
            else if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.MS || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB)
            {
                if (model.VALUE != null)
                {
                    model.ValueListInt = model.VALUE.Split(',').Select(c => c.ToNumberInt32()).ToList();
                }
            }
            //dạng ngày
            else if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DT)
            {
                if (model.VALUE != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(model.VALUE, out dt))
                    {
                        model.ValueNgayThang = dt;
                    };
                }
            }
            //dạng string
            else
            {
                model.ValueChuoi = model.VALUE;
            };
        }
        public List<ThuocTinhEntity> ToThuocTinhEntites (List<modelThuocTinh> listmodel)
        {
            var list = new List<ThuocTinhEntity>();
            if (listmodel.Count() > 0)
            {
                foreach(var model in listmodel)
                {
                    model.TYPE = model.LoaiDuLieuId.ToLoaiStringDuLieu();
                    if(model.FIELD!=null && model.FIELD != "")
                    {
                        model.FIELD = model.FIELD.Substring(model.FIELD.IndexOf('('), model.FIELD.LastIndexOf(')'));
                    }
                    //sửa lại value nếu là lại checkbox
                    if(model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB)
                    {
                        if (model.VALUE != null)
                        {
                            if (model.VALUE.Substring(model.VALUE.Length - 1) == ",")
                            {
                                model.VALUE = model.VALUE.Remove(model.VALUE.Length - 1);
                            }
                        }
                    }
                    //lấy DDL
                    if(model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.MS || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.RD)
                    {
                        var tt = _itemService.GetAllThuocTinhs(CauHinh: model.GUID.ToString()).FirstOrDefault();
                        if (tt != null)
                        {
                            var ett = tt.CAU_HINH.toEntity<ThuocTinhEntity>();
                            //nếu là ett là đối tượng obj thì cần tìm trong phần con của nó
                            if (ett.GUID != model.GUID)
                            {
                                var ettcon = ett.THUOC_TINH.Where(c => c.GUID == model.GUID).FirstOrDefault();
                                if (ettcon != null)
                                {
                                    model.OPTION = ettcon.OPTION;
                                }
                            }
                            else
                            {
                                model.OPTION = ett.OPTION;
                            }
                        }

                    }
                };
                //nếu là kiểu obj ta tìm con của nó
                var listobj = listmodel.Where(c => c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ).ToList();
                if (listobj != null && listobj.Count() > 0)
                {
                    foreach (var model in listobj)
                    {
                        model.THUOC_TINH = new List<modelThuocTinh>();
                        if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ)
                        {
                            var listcon = listmodel.Where(c => c.GuidParrent == model.GUID).ToList();
                            if (listcon != null && listcon.Count() > 0)
                            {
                                foreach (var con in listcon)
                                {                                  
                                    model.THUOC_TINH.Add(con);
                                }
                            }
                        }
                    }
                }
                //xóa hết con
                var listloaibo = listmodel.Where(c => !c.GuidParrent.Equals(Guid.Empty)).ToList();
                if(listloaibo!=null && listloaibo.Count() > 0)
                {
                    foreach(var model in listloaibo)
                    {
                        listmodel.Remove(model);
                    }
                }
                //chuyển đổi model=> entity
                list = listmodel.Select(c => c.ToEntity<ThuocTinhEntity>()).ToList();
            }
            var js = list.toStringJson();
            return list;
        }
        public List<modelThuocTinh> ToThuocTinhModel(List<modelThuocTinh> listmodel)
        {
            var list = new List<modelThuocTinh>();
            if (listmodel.Count() > 0)
            {
                foreach (var model in listmodel)
                {
                    model.TYPE = model.LoaiDuLieuId.ToLoaiStringDuLieu();
                    if (model.FIELD != null && model.FIELD != "" && model.FIELD.LastIndexOf(')') > 0 )
                    {                        
                        model.FIELD = model.FIELD.Substring(model.FIELD.IndexOf('(')+1, model.FIELD.LastIndexOf(')')-1);
                    }
                    //sửa lại value nếu là lại checkbox
                    if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB)
                    {
                        if (model.VALUE != null)
                        {
                            if (model.VALUE.Substring(model.VALUE.Length - 1) == ",")
                            {
                                model.VALUE = model.VALUE.Remove(model.VALUE.Length - 1);
                            }
                        }
                    }
                    //lấy DDL
                    if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.MS || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB || model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.RD)
                    {
                        var tt = _itemService.GetAllThuocTinhs(CauHinh: model.GUID.ToString()).FirstOrDefault();
                        if (tt != null)
                        {
                            var ett = tt.CAU_HINH.toEntity<ThuocTinhEntity>();
                            //nếu là ett là đối tượng obj thì cần tìm trong phần con của nó
                            if (ett.GUID != model.GUID)
                            {
                                var ettcon = ett.THUOC_TINH.Where(c => c.GUID == model.GUID).FirstOrDefault();
                                if (ettcon != null)
                                {
                                    model.OPTION = ettcon.OPTION;
                                }
                            }
                            else
                            {
                                model.OPTION = ett.OPTION;
                            }
                        }

                    }
                };
                //nếu là kiểu obj ta tìm con của nó
                var listobj = listmodel.Where(c => c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ).ToList();
                if (listobj != null && listobj.Count() > 0)
                {
                    foreach (var model in listobj)
                    {
                        model.THUOC_TINH = new List<modelThuocTinh>();
                        if (model.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ)
                        {
                            var listcon = listmodel.Where(c => c.GuidParrent == model.GUID).ToList();
                            if (listcon != null && listcon.Count() > 0)
                            {
                                foreach (var con in listcon)
                                {
                                    model.THUOC_TINH.Add(con);
                                }
                            }
                        }
                    }
                }
                //xóa hết con
                var listloaibo = listmodel.Where(c => !c.GuidParrent.Equals(Guid.Empty)).ToList();
                if (listloaibo != null && listloaibo.Count() > 0)
                {
                    foreach (var model in listloaibo)
                    {
                        listmodel.Remove(model);
                    }
                }
                list = listmodel;
            }
            return list;
        }
        //lấy dữ liệu từ view chuyển về dạng modelThuocTinh
        public List<ThuocTinhEntity> ChuyenDoi(List<modelThuocTinh> listmodel)
        {
            var list = new List<ThuocTinhEntity>();
            if (listmodel != null && listmodel.Count()>0)
            {
                
                //check thằng tổng
                var modelTT = listmodel.Where(c => c.GuidParrent == new Guid()).FirstOrDefault();
                if (modelTT != null)
                {
                    //loại bỏ thằng tổng
                    listmodel.Remove(modelTT);
                    modelTT.TYPE = modelTT.LoaiDuLieuId.ToLoaiStringDuLieu();
                    if (modelTT.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.OBJ)
                    {

                        modelTT.THUOC_TINH = PhanLoaiAndGopmodelThuocTinh(listmodel);
                        list.Add(modelTT.ToEntity<ThuocTinhEntity>());
                    }
                    else
                    {
                        var model = PhanLoaiAndGopmodelThuocTinh(listmodel).FirstOrDefault();
                        if (model != null)
                        {
                            modelTT.OPTION = model.OPTION;
                            list.Add(modelTT.ToEntity<ThuocTinhEntity>());
                        }
                    }
                }
            }
            return list;
        }
        public List<modelThuocTinh> PhanLoaiAndGopmodelThuocTinh(List<modelThuocTinh> listmodel)
        {
            var listcon = new List<modelThuocTinh>();
            //loại number,string,date
            var loai1 = listmodel.Where(c => c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.NB || c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.ST || c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DT);
            if (loai1 != null && loai1.Count() > 0)
            {
                foreach (var item in loai1)
                {
                    item.TYPE = item.LoaiDuLieuId.ToLoaiStringDuLieu();
                    item.NAME = item.NameTT;
                    item.VALUE = item.ValueTT;
                    listcon.Add(item);
                }
            }
            //loại RD
            var loai2 = listmodel.Where(c => c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.RD && c.stt == 1);
            if (loai2 != null && loai2.Count() > 0)
            {
                foreach (var item in loai2)
                {
                    item.TYPE = item.LoaiDuLieuId.ToLoaiStringDuLieu();
                    item.OPTION = new List<SelectListItem>();
                    //vì radio button sẽ có 2 giá trị mặc định nên tìm giá trị 2
                    var item1 = listmodel.Where(c => c.GUID == item.GUID && c.ValueTT != item.ValueTT).FirstOrDefault();
                    if (item1 != null)
                    {
                        if (item1.Is_Default == true)
                        {
                            item.VALUE = item1.ValueTT;
                        }
                        item.OPTION.Insert(0, new SelectListItem { Value = item1.ValueTT, Text = item1.NameTT });
                    }
                    item.OPTION.Insert(0, new SelectListItem { Value = item.ValueTT, Text = item.NameTT });
                    if (item.Is_Default == true)
                    {
                        item.VALUE = item.ValueTT;
                    }
                    listcon.Add(item);
                }
            }
            //loại nhiều giá trị
            var loai3 = listmodel.Where(c => (c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.DDL || c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.MS || c.LoaiDuLieuId == (int)enumLoaiDuLieuCauHinh.CB) && c.stt ==1 );
            if (loai3 != null && loai3.Count() > 0)
            {
                foreach (var item in loai3)
                {
                    //list giá trị default
                    var listdefault = new List<int>();
                    item.TYPE = item.LoaiDuLieuId.ToLoaiStringDuLieu();
                    item.OPTION = new List<SelectListItem>();
                    //tìm giá trị còn lại
                    var listitemCL = listmodel.Where(c => c.GuidParrent == item.GUID).OrderByDescending(c=>c.stt).ToList();
                    if (listitemCL != null && listitemCL.Count() > 0)
                    {
                        foreach (var item1 in listitemCL)
                        {
                            if (item1.Is_Default == true)
                            {
                                if (int.TryParse(item1.ValueTT, out int DE))
                                {
                                    listdefault.Add(DE);
                                };
                            }
                            item.OPTION.Insert(0, new SelectListItem { Value = item1.ValueTT, Text = item1.NameTT });
                        }
                    }
                    if (item.Is_Default == true)
                    {
                        if (int.TryParse(item.ValueTT, out int DE))
                        {
                            listdefault.Add(DE);
                        };
                    }
                    item.VALUE = string.Join(',',listdefault);
                    item.OPTION.Insert(0, new SelectListItem { Value = item.ValueTT, Text = item.NameTT });
                    listcon.Add(item);
                }
            }
            return listcon;
        }
        #endregion	
     }
}		

