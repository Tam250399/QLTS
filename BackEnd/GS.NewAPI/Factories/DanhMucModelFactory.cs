using DevExpress.DataProcessing;
using GS.Core.Domain.Common;
using GS.Core.Domain.DanhMuc;
using GS.NewAPI.Infrastructure.Mapper.Extensions;
using GS.NewAPI.Models.DanhMuc;
using GS.Services.DanhMuc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GS.NewAPI.Factories
{
    public class DanhMucModelFactory: IDanhMucModelFactory
    {
        #region Ctor
        private readonly IQuocGiaService _quocGiaService;
       
        public DanhMucModelFactory(
            IQuocGiaService quocGiaService           
            )
        {
            this._quocGiaService = quocGiaService;
           
        }
        #endregion
        #region
        #region quốc gia
        public IList<QuocGiaModel> GetAllQuocGias()
        {
            var query = _quocGiaService.GetAllQuocGias();
            query.ForEach(x => x.MO_TA = "111");
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        public IList<QuocGiaModel> SearchQuocGiasByName(string tenQuocGia)
        {
            var query = _quocGiaService.SearchQuocGias(Keysearch: tenQuocGia);
            return query.Select(m => m.ToModel<QuocGiaModel>()).ToList();
        }
        //public MessageReturn UpdateQuocGia(QuocGiaModel model, NguoiDung currentUser)
        //{
        //    if (string.IsNullOrEmpty(model.TEN))
        //    {
        //        model.Error = "TEN null";
        //        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "TEN null", new List<QuocGiaModel>() { model });
        //    }
        //    if (model.DB_ID == null)
        //    {
        //        model.Error = "DB_ID null";
        //        return new MessageReturn(MessageReturn.NOT_FOUND_CODE, "DB_ID null", new List<QuocGiaModel>() { model });
        //    }
        //    else
        //    {
        //        QuocGia quocGia = new QuocGia();
        //        if (model.ID == 0)
        //        {
        //            quocGia = model.ToEntity<QuocGia>();
        //            quocGia.ID = 0;
        //            //quocGia.MA = null;
        //            _quocGiaService.InsertQuocGia(quocGia);
        //            _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", model);
        //            model.ID = (long)quocGia.ID;
        //            return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
        //        }
        //        else
        //        {
        //            quocGia = _quocGiaService.GetQuocGiaDB(ID: model.ID);
        //            if (quocGia != null)// cập nhật
        //            {
        //                quocGia.TEN = model.TEN;
        //                quocGia.MO_TA = model.MO_TA;
        //                _quocGiaService.UpdateQuocGia(quocGia);
        //                _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", model);
        //                return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<QuocGiaModel>() { model });
        //            }
        //            else
        //            {
        //                model.Error = "ID not exist";
        //                return new MessageReturn(MessageReturn.ERROR_CODE, "ID not exist", new List<QuocGiaModel>() { model });
        //            }
        //        }
        //    }

        //}
        //public MessageReturn UpDateListQuocGia(List<QuocGiaModel> ListQuocGiaModel, NguoiDung currentUser)
        //{
        //    if (currentUser == null)
        //    {
        //        currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
        //    }
        //    // lọc các quốc gia không đủ điều kiện           
        //    int TotalErr = 0;
        //    int TotalSuc = 0;
        //    List<QuocGia> LstAdd = new List<QuocGia>();
        //    List<QuocGia> LstEdit = new List<QuocGia>();
        //    List<QuocGia> quocGias = new List<QuocGia>();
        //    foreach (var model in ListQuocGiaModel)
        //    {
        //        if (model.DB_ID == null)
        //        {
        //            model.Error = "DB_ID null";
        //            TotalErr++;
        //            continue;
        //        }
        //        if (string.IsNullOrEmpty(model.TEN))
        //        {
        //            model.Error = "TEN null";
        //            TotalErr++;
        //            continue;
        //        }
        //        if (model.ID > 0)
        //        {
        //            var entity = _quocGiaService.GetQuocGiaById(model.ID);
        //            if (entity == null)
        //            {
        //                model.Error = "ID not exist";
        //                continue;
        //            }
        //            else
        //            {
        //                //entity = model.ToEntity<QuocGia>();
        //                entity.TEN = model.TEN;
        //                entity.MA = model.MA;
        //                entity.DB_ID = model.DB_ID;
        //                LstEdit.Add(entity);
        //            }
        //        }
        //        else
        //        {
        //            var entity = model.ToEntity<QuocGia>();
        //            entity.ID = 0;
        //            LstAdd.Add(entity);
        //        }
        //    }
        //    if (LstAdd.Count > 0)
        //    {
        //        _quocGiaService.InsertListQuocGia(LstAdd);
        //        if (currentUser == null)
        //        {
        //            currentUser = _nguoiDungService.GetNguoiDungByUsername("admin");
        //        }
        //        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới quốc gia", 0, "QuocGia", LstAdd);
        //        quocGias.AddRange(LstAdd);
        //    }
        //    if (LstEdit.Count > 0)
        //    {
        //        _quocGiaService.UpdateListQuocGia(LstEdit);
        //        _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật quốc gia", 0, "QuocGia", LstEdit);
        //        quocGias.AddRange(LstEdit);
        //    }
        //    foreach (var item in ListQuocGiaModel)
        //    {
        //        var quocgia = quocGias.Where(m => m.ID > 0 && m.DB_ID == item.DB_ID).FirstOrDefault();
        //        if (quocgia == null)
        //            continue;
        //        item.ID = (long)quocgia.ID;
        //    }
        //    if (TotalErr > 0)
        //    {
        //        return new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_PARTIAL_CODE,
        //            Message = $"Total {quocGias.Count} success - Total {TotalErr} error",
        //            ObjectInfo = ListQuocGiaModel
        //        };
        //    }
        //    else
        //    {
        //        return new MessageReturn()
        //        {
        //            Code = MessageReturn.SUCCESS_CODE,
        //            ObjectInfo = quocGias,
        //            Message = "Success done"
        //        };
        //    }
        //}
        public MessageReturn DeleteQuocGia(decimal ID = 0)
        {
            QuocGia quocGia = _quocGiaService.GetQuocGiaById(Id: ID);
            try
            {
                if (quocGia.DB_ID == null)
                {
                    return MessageReturn.CreateErrorMessage("ID not exist");
                }
                quocGia.DB_ID = null;
                _quocGiaService.UpdateQuocGia(quocGia);
                return MessageReturn.CreateSuccessMessage("Success done");
            }
            catch (Exception ex)
            {
                return MessageReturn.CreateErrorMessage("ID invalid");
            }

        }
        #endregion
        #endregion
    }
}
