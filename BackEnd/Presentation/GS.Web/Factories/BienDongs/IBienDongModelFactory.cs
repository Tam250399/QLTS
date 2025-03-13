//----------------------------------------------------------------------------------------------------------------------
// Create by       : GS template 1.0 
// Template create : GS
// Create date     : 13/12/2019
//----------------------------------------------------------------------------------------------------------------------
using GS.Core.Domain.BienDongs;
using GS.Web.Models.BienDongs;
using GS.Web.Models.ImportTaiSan;
using GS.Web.Models.NghiepVu;
using System.Collections.Generic;

namespace GS.Web.Factories.BienDongs
{
    public partial interface IBienDongModelFactory
    {
        #region BienDong

        BienDongSearchModel PrepareBienDongSearchModel(BienDongSearchModel searchModel);

        BienDongListModel PrepareBienDongListModel(BienDongSearchModel searchModel);

        BienDongModel PrepareBienDongModel(BienDongModel model, BienDong item, bool excludeProperties = false);

        void PrepareBienDong(BienDongModel model, BienDong item);
        void PrepareBienDongFromBDTDTT(BienDong item, BienDong bienDongTDTT = null);
        void DeleteBienDong(BienDong bienDong, BienDongChiTiet bienDongChiTiet = null, bool isChotHaoMOn = true);
        /// <summary>
        /// Tính giá trị còn lại của tài sản.<br></br>
        /// Được sử dụng chốt lại giá trị tài sản khi duyệt biến động
        /// </summary>
        /// <param name="bienDong"></param>
        /// <param name="bienDongChiTiet"></param>
        void TinhGiaTriConLaiBienDong(BienDong bienDong, BienDongChiTiet bienDongChiTiet);
        BienDongListModel PrepareForListBienDongSaiHienTrang(BienDongSearchModel searchModel);
        YeuCauChiTietModel PrepareForBienDongSaiHienTrangModel(decimal BienDongId);
        BienDongModel InsertToBienDongGiamTS(ImportBdGiamTaiSanModel item, BienDongModel model);
        List<string> ValidateListImportBdGiamTaiSan(ImportBdGiamTaiSanModel model);
        BienDongChiTietModel InsertToBienDongChiTiet(ImportBdGiamTaiSanModel item, BienDongChiTietModel model, BienDongModel bd);
        BienDong InsertToBdTangGiamNguyenGia(ImportBdTangGiamNguyenGiaModel item, BienDongModel model);
        BienDongChiTietModel InsertToBdTangGiamNguyenGiaChiTiet(ImportBdTangGiamNguyenGiaModel item, BienDongChiTietModel model, BienDongModel bd);
        List<string> ValidateBdTangGiamNguyenGia(ImportBdTangGiamNguyenGiaModel model);
        void InsertTaiSanNguonVonFromBienDong(ImportBdTangGiamNguyenGiaModel item, BienDongModel bd);
        #endregion
    }
}
