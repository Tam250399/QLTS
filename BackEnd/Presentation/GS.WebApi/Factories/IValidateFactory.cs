using GS.Core.Domain.Common;
using GS.WebApi.Models.DanhMuc;
using GS.WebApi.Models.TaiSan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Factories
{
    public interface IValidateFactory
    {
        bool CheckTonTaiDonVi(decimal DonViId = 0, string MaDonVi = null, string MaDVQHNS=null);
        List<string> CheckTonTaiTaiSan(List<string> ListMaTaiSan);
        bool CheckTonTaiKhaiThac(decimal? Id);
        bool CheckTonTaiLoaiTaiSan(decimal? LoaiTaiSanId);
        bool CheckTonTaiDonViBoPhan(decimal? DonViBoPhanId);
        bool CheckTonTaiQuocGia(decimal? QuocGiaId);
        List<string> CheckThongTinTaiSanChiTiet(TaiSanDBModel TaiSan = null, decimal? LoaiTaiSanId = null, decimal? LoaiTaiSanDonViId = null);
        bool CheckTonTaiLyDoBienDong(decimal? LyDoBienDongId);
        //bool CheckTonTaiMucDichSuDung()
        bool CheckTonTaiDiaBan(decimal? DiaBanId);
        bool CheckDonViNhanDieuChuyen(string MaDonViNhanDieuChuyen);
        bool CheckTonTaiMucDichSuDung(decimal? MucDichSuDungId);
        List<string> CheckBienDongTheoTaiSan(TaiSanDBModel taiSanDB);
        bool CheckTonTaiTaiSan(string MaTaiSan);
        List<string> CheckBienDong(BienDongDBModel model);
        bool CheckTonTaiBienDong(string guid);
        string CheckTonTaiLoaiTaiSanDonViCha(decimal IdCha);
        bool CheckTonTaiCheDoHaoMon(decimal CheDoHaoMonId);
        string CheckLoaiTaisanForNhanTaiSan(decimal? LoaiTaiSanId, decimal? LoaiTaiSanDonViId);
        bool CheckTonTaiTaiSanDongBo(string MaDb);
        List<string> CheckChiTietHaoMonTaiSan(HaoMonDBModel haoMonDBModel, bool isCheckMaTaiSan = true);
        List<string> CheckChiTietKhauHaoTaiSan(KhauHaoDBModel khauHaoDBModel, bool isCheckMaTaiSan = true);
        string CheckDuAn(DuAnModel model);
        string CheckLoaiLyDoBienDong(LoaiLyDoBienDongModel model);
        string CheckChiTietLyDoBienDong(LyDoBienDongModel model);
        bool CheckPhuongAnXuLy(decimal? PhuongAnXuLyId);
        List<string> CheckChiTietKhauHaoInTaiSan(KhauHaoInTaiSanDBModel khauHaoDBModel);
        List<string> CheckKhauHaoTaiSan(KhauHaoTaiSanDBModel khauHaoDBModel);
        List<string> CheckChiTietHaoMonInTaiSan(HaoMonInTaiSanDBModel haoMonDBModel);
        List<string> CheckHaoMonTaiSan(HaoMonTaiSanDBModel haoMonDBModel);
        bool CheckListDonViBaoCao(List<decimal> lstDonVi);
    }
}
