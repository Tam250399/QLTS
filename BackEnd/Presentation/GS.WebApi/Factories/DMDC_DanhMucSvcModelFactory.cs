using GS.Core;
using GS.Core.Domain.Common;
using GS.Core.Domain.DMDC;
using GS.Core.Domain.HeThong;
using GS.Services.DMDC;
using GS.Services.HeThong;
using GS.WebApi.Infrastructure.Mapper.Extensions;
using GS.WebApi.Models.DMDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GS.WebApi.Factories
{
    public class DMDC_DanhMucSvcModelFactory : IDMDC_DanhMucSvcModelFactory
    {
        #region Ctor
        private readonly IHoatDongService _hoatDongService;
        private readonly IDMDC_DiaBanService _dMDC_DiaBanService;
        private readonly IDMDC_QuocGiaService _dMDC_QuocGiaService;
        private readonly IDMDC_DonViNganSachService _dMDC_DonViNganSachService;
        public DMDC_DanhMucSvcModelFactory(
            IHoatDongService hoatDongService,
            IDMDC_DiaBanService dMDC_DiaBanService,
            IDMDC_QuocGiaService dMDC_QuocGiaService,
            IDMDC_DonViNganSachService dMDC_DonViNganSachService
         )
        {
            this._hoatDongService = hoatDongService;
            this._dMDC_DiaBanService = dMDC_DiaBanService;
            this._dMDC_QuocGiaService = dMDC_QuocGiaService;
            this._dMDC_DonViNganSachService = dMDC_DonViNganSachService;
        }
        #endregion
        #region Method
        #region DiaBan
        public MessageReturn DMDC(HttpWebRequest webRequest)
        {
            return MessageReturn.CreateSuccessMessage("", webRequest.Headers);
        }
        public MessageReturn InsertOrUpdateDMDCDiaBan(DMDC_DiaBanModel model)
        {
            List<DMDC_DiaBanModel> ListError = new List<DMDC_DiaBanModel>();
            List<DMDC_DiaBanModel> ListSucc = new List<DMDC_DiaBanModel>();
            if (string.IsNullOrEmpty(model.MA_DB))
            {
                model.Error = "MA_DB null";
                ListError.Add(model);
            }
            if (string.IsNullOrEmpty(model.MA_T))
            {
                model.Error = "MA_T null";
                ListError.Add(model);
            }

            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                ListError.Add(model);
            }

            if (string.IsNullOrEmpty(model.VAN_BAN_BH))
            {
                model.Error = "VB_BAN_BH null";
                ListError.Add(model);
            }

            if (model.LOAI < 1)
            {
                model.Error = "LOAI null";
                ListError.Add(model);
            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                decimal? ID_CHA = null;
                decimal? QUOC_GIA_ID = null;
                if (model.ID_CHA > 0)
                {
                    var PARENT = _dMDC_DiaBanService.GetDMDC_DiaBanById(model.ID_CHA.Value);
                    if (PARENT != null)
                    {
                        ID_CHA = PARENT.ID;
                        model.MA_CHA = PARENT.MA_CHA;
                    }
                    /*else
                    {
                        //model.Error = "ID_CHA not exist";
                        return new MessageReturn(MessageReturn.ERROR_CODE, "ID_CHA not exist", new List<DMDC_DiaBanModel>() { model });
                    }*/
                }
                if (model.MA_DB != null)
                {
                    var entity = _dMDC_DiaBanService.GetDiaBanByMa(model.MA_DB);
                    if (entity != null)
                    {
                        entity.MA_V = model.MA_V;
                        entity.MA_T = model.MA_T;
                        entity.MA_H = model.MA_H;
                        entity.MA_X = model.MA_X;
                        entity.TEN = model.TEN;
                        entity.HIEU_LUC = model.HIEU_LUC;
                        //entity.MO_TA = model.MO_TA;
                        entity.MA_CHA = model.MA_CHA;
                        entity.ID_CHA = ID_CHA;
                        //entity.TRANG_THAI_ID = model.TRANG_THAI_ID;
                        entity.LOAI = model.LOAI;
                        entity.QUOC_GIA_ID = QUOC_GIA_ID;
                        _dMDC_DiaBanService.UpdateDMDC_DiaBan(entity);
                    }
                    else
                    {

                        entity = model.ToEntity<DMDC_DiaBan>();
                        entity.NGAY_HL = model.NGAY_HL.toDateSys("dd/MM/yyyy");
                        entity.NGAY_KT = model.NGAY_KT.toDateSys("dd/MM/yyyy");
                        entity.NGAY_SD = model.NGAY_SD.toDateSys("dd/MM/yyyy");
                        entity.NGAY_TAO = model.NGAY_TAO.toDateSys("dd/MM/yyyy");
                        entity.NGAY_VB = model.NGAY_VB.toDateSys("dd/MM/yyyy");
                        _dMDC_DiaBanService.InsertDMDC_DiaBan(entity);
                    }
                    //else
                    //{

                    //    DMDC_DiaBan entity = model.ToEntity<DMDC_DiaBan>();
                    //    entity.ID = 0;
                    //    entity.ID_CHA = ID_CHA;
                    //    entity.QUOC_GIA_ID = QUOC_GIA_ID;
                    //    _dMDC_DiaBanService.InsertDMDC_DiaBan(entity);
                    //    model.ID = (long)entity.ID;
                    //}
                    // return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<DMDC_DiaBanModel>() { model });
                }
                ListSucc.Add(model);
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }
        public List<MessageReturnDMDC> InsertOrUpdateListDMDC_DiaBan(List<DMDC_DiaBanModel> ListModel)
        {
            // List<DMDC_DiaBanModel> ListSucc = new List<DMDC_DiaBanModel>();
            List<MessageReturnDMDC> ListMessage = new List<MessageReturnDMDC>();
            foreach (var item in ListModel)
            {
                if (string.IsNullOrEmpty(item.MA_DB))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{(ListModel.IndexOf(item))}: MA_DB null"));
                    continue;
                }
                if (string.IsNullOrEmpty(item.MA_T))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: MA_T null"));
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: TEN null"));
                }
                if (item.LOAI == 0)
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: LOAI null"));
                }
                if (string.IsNullOrEmpty(item.VAN_BAN_BH))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: VAN_BAN_BH null"));
                }
                if (item.ID_CHA > 0)
                {
                    DMDC_DiaBan diaBanCha = _dMDC_DiaBanService.GetDMDC_DiaBanById(item.ID_CHA.Value);
                    if (diaBanCha == null)
                    {
                        ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: ID_CHA not exist"));
                    }
                }
                DMDC_DiaBan diaban = _dMDC_DiaBanService.GetDiaBanByMa(item.MA_DB);
                if (diaban == null)
                {
                    diaban = item.ToEntity<DMDC_DiaBan>();
                    diaban.NGAY_HL = item.NGAY_HL.toDateSys("dd/MM/yyyy");
                    diaban.NGAY_KT = item.NGAY_KT.toDateSys("dd/MM/yyyy");
                    diaban.NGAY_SD = item.NGAY_SD.toDateSys("dd/MM/yyyy");
                    diaban.NGAY_TAO = item.NGAY_TAO.toDateSys("dd/MM/yyyy");
                    diaban.NGAY_VB = item.NGAY_VB.toDateSys("dd/MM/yyyy");
                    _dMDC_DiaBanService.InsertDMDC_DiaBan(diaban);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Địa bàn", item.ID, "DMDC_DiaBan", item);
                }
                else
                {
                    diaban = item.ToEntity(diaban);
                    _dMDC_DiaBanService.UpdateDMDC_DiaBan(diaban);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Địa bàn", item.ID, "DMDC_DiaBan", item);
                }
                //ListMessage.Add(MessageReturnDMDC.CreateSuccessMessage($"{item.MA_DB}: Done"));

            }
            return ListMessage;

        }
        #endregion
        #region QuocGia
        public MessageReturn InsertOrUpdateDMDCQuocGia(DMDC_QuocGiaModel model)
        {
            List<DMDC_QuocGiaModel> ListError = new List<DMDC_QuocGiaModel>();
            List<DMDC_QuocGiaModel> ListSucc = new List<DMDC_QuocGiaModel>();
            if (model.ID == 0)
            {
                model.Error = "ID null";
                ListError.Add(model);
            }
            if (string.IsNullOrEmpty(model.MA))
            {
                model.Error = "MA null";
                ListError.Add(model);
            }
            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                ListError.Add(model);
            }
            if (string.IsNullOrEmpty(model.TEN_TA))
            {
                model.Error = "TEN_TA null";
                ListError.Add(model);
            }
            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                DMDC_QuocGia quocGia = _dMDC_QuocGiaService.GetDMDC_QuocGiaByMa(model.MA);
                if (quocGia == null)
                {
                    quocGia = model.ToEntity<DMDC_QuocGia>();
                    _dMDC_QuocGiaService.InsertDMDC_QuocGia(quocGia);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Quốc gia", model.ID, "DMDC_QuocGia", model);
                }
                else
                {
                    quocGia = model.ToEntity(quocGia);
                    quocGia.NGAY_HL = model.NGAY_HL.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_KT = model.NGAY_KT.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_SD = model.NGAY_SD.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_TAO = model.NGAY_TAO.toDateSys("dd/MM/yyyy").GetValueOrDefault(DateTime.Now);
                    quocGia.NGAY_VB = model.NGAY_VB.toDateSys("dd/MM/yyyy");
                    _dMDC_QuocGiaService.UpdateDMDC_QuocGia(quocGia);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Quốc gia", model.ID, "DMDC_QuocGia", model);
                }
                ListSucc.Add(model);
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_CODE,
                    Message = "Success done"
                };
            }
        }

        public List<MessageReturnDMDC> InsertOrUpdateListDMDCQuocGia(List<DMDC_QuocGiaModel> ListModel)
        {
            List<MessageReturnDMDC> ListMessage = new List<MessageReturnDMDC>();
            foreach (var item in ListModel)
            {

                if (string.IsNullOrEmpty(item.MA))
                {
                    item.Error = "MA null";
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{(ListModel.IndexOf(item))}: MA null"));
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    item.Error = "TEN null";
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA}: TEN null"));
                }
                if (string.IsNullOrEmpty(item.TEN_TA))
                {
                    item.Error = "TEN_TA null";
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA}: TEN_TA null"));
                }
                DMDC_QuocGia quocGia = _dMDC_QuocGiaService.GetDMDC_QuocGiaByMa(item.MA);
                if (quocGia == null)
                {
                    quocGia = item.ToEntity<DMDC_QuocGia>();
                    _dMDC_QuocGiaService.InsertDMDC_QuocGia(quocGia);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Quốc gia", item.ID, "DMDC_QuocGia", item);
                }
                else
                {
                    quocGia = item.ToEntity(quocGia);
                    quocGia.NGAY_HL = item.NGAY_HL.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_KT = item.NGAY_KT.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_SD = item.NGAY_SD.toDateSys("dd/MM/yyyy");
                    quocGia.NGAY_TAO = item.NGAY_TAO.toDateSys("dd/MM/yyyy").GetValueOrDefault(DateTime.Now);
                    quocGia.NGAY_VB = item.NGAY_VB.toDateSys("dd/MM/yyyy");
                    _dMDC_QuocGiaService.UpdateDMDC_QuocGia(quocGia);
                    // _hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Quốc gia", item.ID, "DMDC_QuocGia", item);
                }
               // ListMessage.Add(MessageReturnDMDC.CreateSuccessMessage($"{item.MA}: done"));

            }
            return ListMessage;
        }
        #endregion
        #region DonViNganSach

        public MessageReturn InsertOrUpdateDMDCDonViNganSach(DMDC_DonViNganSachModel model)
        {
            List<DMDC_DonViNganSachModel> ListError = new List<DMDC_DonViNganSachModel>();
            List<DMDC_DonViNganSachModel> ListSucc = new List<DMDC_DonViNganSachModel>();
            if (string.IsNullOrEmpty(model.MA_DB))
            {
                model.Error = "MA_DB null";
                ListError.Add(model);
            }
            if (string.IsNullOrEmpty(model.MA_T))
            {
                model.Error = "MA_T null";
                ListError.Add(model);
            }

            if (string.IsNullOrEmpty(model.TEN))
            {
                model.Error = "TEN null";
                ListError.Add(model);
            }

            if (model.LOAI < 1)
            {
                model.Error = "LOAI null";
                ListError.Add(model);
            }

            if (string.IsNullOrEmpty(model.VAN_BAN_BH))
            {
                model.Error = "VB_BAN_BH null";
                ListError.Add(model);
            }

            if (model.NGAY_HL == null)
            {
                model.Error = "NGAY_HL null";
                ListError.Add(model);
            }

            if (ListError.Count > 0)
            {
                return new MessageReturn()
                {
                    Code = MessageReturn.SUCCESS_PARTIAL_CODE,
                    Message = $"Total {ListSucc.Count} success - Total {ListError.Count} error",
                    ObjectInfo = ListError
                };
            }
            else
            {
                if (model.MA_DB != null)
                {
                    var entity = _dMDC_DonViNganSachService.GetDMDC_DonViNganSachByMa(model.MA_DB);
                    if (entity != null)
                    {
                        entity.MA_V = model.MA_V;
                        entity.MA_T = model.MA_T;
                        entity.MA_H = model.MA_H;
                        entity.MA_X = model.MA_X;
                        entity.TEN = model.TEN;
                        entity.NGAY_HL = model.NGAY_HL.toDateSys("dd/MM/yyyy");
                        entity.HIEU_LUC = model.HIEU_LUC;
                        entity.LOAI = model.LOAI;
                        _dMDC_DonViNganSachService.UpdateDMDC_DonViNganSach(entity);
                    }
                    else
                    {
                        entity = model.ToEntity<DMDC_DonViNganSach>();
                        entity.NGAY_HL = model.NGAY_HL.toDateSys("dd/MM/yyyy");
                        entity.NGAY_KT = model.NGAY_KT.toDateSys("dd/MM/yyyy");
                        entity.NGAY_SD = model.NGAY_SD.toDateSys("dd/MM/yyyy");
                        entity.NGAY_TAO = model.NGAY_TAO.toDateSys("dd/MM/yyyy");
                        entity.NGAY_VB = model.NGAY_VB.toDateSys("dd/MM/yyyy");
                        _dMDC_DonViNganSachService.InsertDMDC_DonViNganSach(entity);

                    }
                }

                else
                {

                    DMDC_DonViNganSach entity = model.ToEntity<DMDC_DonViNganSach>();
                    entity.ID = 0;
                    //entity.ID_CHA = ID_CHA;
                    //entity.QUOC_GIA_ID = QUOC_GIA_ID;
                    _dMDC_DonViNganSachService.InsertDMDC_DonViNganSach(entity);
                    model.ID = (long)entity.ID;
                }
            }
            ListSucc.Add(model);
            return new MessageReturn()
            {
                Code = MessageReturn.SUCCESS_CODE,
                Message = "Success done"
            };
        }
        //decimal? ID_CHA = null;
        //decimal? QUOC_GIA_ID = null;
        //if (model.ID_CHA > 0)
        //{
        //    var PARENT = _dMDC_DonViNganSachService.GetDMDC_DonViNganSachById(model.ID);
        //    if (PARENT != null)
        //    {
        //        ID_CHA = PARENT.ID;
        //        model.MA_CHA = PARENT.MA_CHA;
        //    }
        //    else
        //    {
        //        model.Error = "ID_CHA not exist";
        //        return new MessageReturn(MessageReturn.ERROR_CODE, "ID_CHA not exist", new List<DMDC_DiaBanModel>() { model });
        //    }
        //}
        //if (model.QUOC_GIA_ID > 0)
        //{
        //    DMDC_QuocGia QuocGia = _dMDC_QuocGiaService.GetDMDC_QuocGiaById(model.QUOC_GIA_ID.Value);
        //    if (QuocGia != null)
        //        QUOC_GIA_ID = QuocGia.ID;
        //    else
        //    {
        //        model.Error = "QUOC_GIA_ID not exist";
        //        return new MessageReturn(MessageReturn.ERROR_CODE, "QUOC_GIA_ID not exist", new List<DMDC_DiaBanModel>() { model });
        //    }
        //}

        //return new MessageReturn(MessageReturn.SUCCESS_CODE, "Success done", new List<DMDC_DonViNganSachModel>() { model });
        public List<MessageReturnDMDC> InsertOrUpdateListDMDC_DonViNganSach(List<DMDC_DonViNganSachModel> ListModel)
        {
            List<MessageReturnDMDC> ListMessage = new List<MessageReturnDMDC>();
            foreach (var item in ListModel)
            {

                if (string.IsNullOrEmpty(item.MA_DB))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{(ListModel.IndexOf(item))}: MA_DB null"));
                    continue;
                }

                if (string.IsNullOrEmpty(item.MA_T))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: MA_T null"));
                }
                if (string.IsNullOrEmpty(item.TEN))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: TEN null"));
                }
                if (item.LOAI == 0)
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: LOAI null"));
                }
                if (string.IsNullOrEmpty(item.VAN_BAN_BH))
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: VAN_BAN_BH null"));
                }
                if (item.NGAY_HL == null)
                {
                    ListMessage.Add(MessageReturnDMDC.CreateErrorMessage($"{item.MA_DB}: NGAY_HL null"));
                }
                DMDC_DonViNganSach donViNganSach = _dMDC_DonViNganSachService.GetDMDC_DonViNganSachByMa(item.MA_DB);
                if (donViNganSach == null)
                {
                    donViNganSach = item.ToEntity<DMDC_DonViNganSach>();
                    _dMDC_DonViNganSachService.InsertDMDC_DonViNganSach(donViNganSach);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.TaoMoi, "Thêm mới danh mục dùng chung Đơn vị Ngân sách", item.ID, "DMDC_DonViNganSach", item);
                }
                else
                {
                    donViNganSach = item.ToEntity(donViNganSach);
                    donViNganSach.NGAY_HL = item.NGAY_HL.toDateSys("dd/MM/yyyy");
                    donViNganSach.NGAY_KT = item.NGAY_KT.toDateSys("dd/MM/yyyy");
                    donViNganSach.NGAY_SD = item.NGAY_SD.toDateSys("dd/MM/yyyy");
                    donViNganSach.NGAY_TAO = item.NGAY_TAO.toDateSys("dd/MM/yyyy");
                    donViNganSach.NGAY_VB = item.NGAY_VB.toDateSys("dd/MM/yyyy");
                    _dMDC_DonViNganSachService.UpdateDMDC_DonViNganSach(donViNganSach);
                    //_hoatDongService.InsertHoatDong(currentUser, enumHoatDong.CapNhat, "Cập nhật danh mục dùng chung Đơn vị Ngân sách", item.ID, "DMDC_DonViNganSach", item);
                }
                //ListMessage.Add(MessageReturnDMDC.CreateSuccessMessage($"{item.MA_DB}: done"));

            }
            return ListMessage;
        }
        #endregion

        public MessageReturn TransferData(DMDC_TransferData model)
        {
            return new MessageReturn()
            {
                Code = MessageReturn.SUCCESS_CODE,
                Message = "Success done"
            };
        }
        #endregion
    }
}
