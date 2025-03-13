using FluentValidation.Attributes;
using GS.Web.Framework.Models;
using GS.Web.Validators.SHTD;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Web.Models.SHTD
{
    [Validator(typeof(KetQuaTaiSanValidator))]
    public class KetQuaTaiSanModel: BaseGSEntityModel
    {
        public KetQuaTaiSanModel()
        {
            Guid = Guid.NewGuid();
            DDLTaiSanTD = new List<SelectListItem>();
        }
        public Decimal? TAI_SAN_TD_ID { get; set; }
        public Decimal? XU_LY_KET_QUA_ID { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_LUONG { get; set; }
        [UIHint("InputAddon")]
        public Decimal? SO_TIEN { get; set; }
        public Decimal? TAI_SAN_TD_XU_LY_ID { get; set; }
        public string TaiSanTen { get; set; }
        public string LoaiTaiSanTen { get; set; }
        public string PhuongAnXuLyTen { get; set; }
        [UIHint("InputAddon")]
        public Decimal SoLuongCoTheXuLy { get; set; }
        public List<SelectListItem> DDLTaiSanTD { get; set; }
        public Guid Guid { get; set; }


    }
    public partial class KetQuaTaiSanSearchModel : BaseSearchModel
    {
        public KetQuaTaiSanSearchModel()
        {
            Is_Create = false;
        }
        public string KeySearch { get; set; }
        public Decimal? XuLyKetQuaId { get; set; }
        public Boolean Is_Create { get; set; }
    }
    public partial class KetQuaTaiSanListModel : BasePagedListModel<KetQuaTaiSanModel>
    {

    }
}
