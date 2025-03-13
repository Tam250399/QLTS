using DevExpress.Internal;
using DevExpress.XtraPivotGrid;
using GS.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.Core.Domain.Api.TaiSanDBApi
{
    public class Kho_DongBoTaiSan
    {
        public Kho_DongBoTaiSan()
        {
            data = new List<Kho_TaiSan_BienDong>();
            CapNhatIDBienDongs = new List<CapNhatIDBienDong>();
        }
        public List<Kho_TaiSan_BienDong> data { get; set; }
        [JsonIgnore]
        public List<CapNhatIDBienDong> CapNhatIDBienDongs { get; set; }
    }

    public class Kho_TaiSan_BienDong
    {
        public Kho_TaiSan_BienDong()
        {
            this.assetMutationAssetUsageStates = new List<Kho_assetMutationAssetUsageStates>();
            // this.vehicleCapacity = 0;
            this.syncDate = CommonHelper.toDateKhoString(DateTime.Now);
        }
        /// <summary>
        /// loại hình tài sản tsc
        /// </summary>
        public decimal? LOAI_HINH_TAI_SAN_ID { get; set; }
        /// <summary>
        /// Mã tài sản của kho
        /// </summary>
        public string assetCode { get; set; }
        /// <summary>
        /// Mã tài sản của phần mềm đồng bộ
        /// </summary>
        public string syncSourceAssetId { get; set; }
        /// <summary>
        /// Loại biến động
        /// </summary>
        public int assetMutationTypeId { get; set; }
        /// <summary>
        /// đơn vị đồng bộ
        /// </summary>
        public int syncUnitId { get; set; }
        /// <summary>
        /// Ngày biến động
        /// </summary>
        public string mutationDate { get; set; }
        /// <summary>
        ///  lý do tăng
        /// </summary>
        public int mutationCauseId { get; set; }
        /// <summary>
        /// Lý do giảm
        /// </summary>       
        #region Biến động điều chuyển  
        /// <summary>
        /// ID đơn vị điều chuyển
        /// </summary>
        public int? transferUnitId { get; set; }
        /// <summary>
        /// Mã đơn vị điều chuyển
        /// </summary>
        public string transferUnitCode { get; set; }
        /// <summary>
        /// tên đơn vị ngoài ngành điều chuyển
        /// </summary>
        public string transferUnitName { get; set; }
        #endregion
        #region Kiểm kê
        /// <summary>
        /// Mã biên bản kiểm kê
        /// </summary>
        public string inventoryCode { get; set; }
        /// <summary>
        /// Ngày kiểm kê
        /// </summary>
        public string inventoryDate { get; set; }
        #endregion
        /// <summary>
        /// tài khoản đông bộ
        /// </summary>
        public long? syncUserId { get; set; }
        /// <summary>
        /// Ngày đồng bộ
        /// </summary>
        public string syncDate { get; set; }
        /// <summary>
        /// Mã tài sản cũ
        /// </summary>
        public string syncSourceId { get; set; }
        /// <summary>
        /// tên tài sản
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// nhóm tài sản kho
        /// </summary>
        public int assetTypeId { get; set; }
        /// <summary>
        /// Nhoms tài sản to nhất của kho
        /// </summary>
       // public long assetCategoryId { get; set; }
        /// <summary>
        /// đơn vị quản lý- đơn vị tổng hợp
        /// </summary>
        public int unitId { get; set; }
        /// <summary>
        /// Id dự án
        /// </summary>
        public long? projectId { get; set; }
        /// <summary>
        /// đơn vị sử dụng
        /// </summary>
        public string unitName { get; set; }
        /// <summary>
        /// Ngày duyệt
        /// </summary>
        public string approvedDate { get; set; }
        /// <summary>
        /// Người duyệt
        /// </summary>
        public string approverName { get; set; }
        /// <summary>
        /// nguồn gốc tài sản
        /// </summary>
        public long? assetSourceId { get; set; }
        /// <summary>
        /// Ngày sử dụng
        /// </summary>
        public string dateOfUse { get; set; }
        /// <summary>
        /// Id đơn vị sử dụng
        /// </summary>
        public int? departmentId { get; set; }
        /// <summary>
        /// mục đích sử dụng
        /// </summary>
        public long? usagePurposeId { get; set; }
        #region Khấu hao, hao mòn
        /// <summary>
        /// tỷ lệ hao mòn
        /// </summary>
        public float? amortizationRate { get; set; }
        /// <summary>
        /// Hao mòn lũy kế
        /// </summary>
        public Double? amortizationAccumulatedValue { get; set; }
        /// <summary>
        /// Giá trị tính hao mòn hàng năm
        /// </summary>
        public Double? amortizationValue { get; set; }
        #endregion


        /// <summary>
        /// Nguyên giá
        /// </summary>
        public long originalValue { get; set; }
        /// <summary>
        /// Nguyên giá tăng/giảm
        /// </summary>
        public long? originalValueIncreasement { get; set; }
        /// <summary>
        /// Nguyên giá trước tăng/giảm
        /// </summary>
        public long? originalValueOld { get; set; }
        /// <summary>
        /// Giá trị còn lại
        /// </summary>
        public Double residualValue { get; set; }
        /// <summary>
        /// Giá trị còn lại trước khi tăng/giảm
        /// </summary>
        public Double residualValueOld { get; set; }
        /// <summary>
        /// Giá mua/tiếp nhận
        /// </summary>
        public long? purchaseValue { get; set; }
        /// <summary>
        /// Hình thức mua sắm
        /// </summary>
        public string procurementForm { get; set; }
        /// <summary>
        /// Chế độ hạch toán
        /// 1: chỉ tính hao mòn
        /// 2: chỉ tính khấu hao
        /// 3: cả khấu hao và hao mòn
        /// </summary>
        public long? accountingStandard { get; set; }
        /// <summary>
        /// Thông số kỹ thuật
        /// </summary>
        public string specifications { get; set; }
        #region Nguồn vốn
        /// <summary>
        /// hoạt động sự nghiệp
        /// </summary>
        public long originalValueSourceBusiness { get; set; }
        /// <summary>
        /// Nguồn vay
        /// </summary>
        public long originalValueSourceBorrow { get; set; }
        /// <summary>
        /// Nguồn ODA
        /// </summary>
        public long originalValueSourceOda { get; set; }
        /// <summary>
        /// Nguồn khác
        /// </summary>
        public long originalValueSourceOther { get; set; }
        /// <summary>
        /// NGuồn ngân sách nhà nước
        /// </summary>
        public long originalValueSourceStateBudget { get; set; }
        /// <summary>
        /// Nguồn sự nghiệp tăng/giảm
        /// </summary>
        public long? originalValueSourceBusinessIncreasement { get; set; }
        /// <summary>
        /// Nguồn vay tăng/giảm
        /// </summary>
        public long? originalValueSourceBorrowIncreasement { get; set; }
        /// <summary>
        /// Nguồn ODA tăng/giảm
        /// </summary>
        public long? originalValueSourceOdaIncreasement { get; set; }
        /// <summary>
        /// Nguồn khác tăng/giảm
        /// </summary>
        public long? originalValueSourceOtherIncreasement { get; set; }
        /// <summary>
        /// Nguồn NSNN tăng/giảm
        /// </summary>
        public long? originalValueSourceStateBudgetIncreasement { get; set; }
        /// <summary>
        /// Nguồn ngân sách cũ
        /// </summary>
        public long? originalValueSourceStateBudgetOld { get; set; }
        /// <summary>
        /// Nguồn ODA trước khi tăng/giảm
        /// </summary>
        public long? originalValueSourceOdaOld { get; set; }
        /// <summary>
        /// Nguồn sự nghiệp trước khi tăng/giảm
        /// </summary>
        public long? originalValueSourceBusinessOld { get; set; }
        /// <summary>
        /// Nguồn vay  trước khi tăng/giảm
        /// </summary>
        public long? originalValueSourceBorrowOld { get; set; }
        /// <summary>
        /// Nguồn khác trước khi tăng/giảm
        /// </summary>
        public long? originalValueSourceOtherOld { get; set; }
        #endregion
        #region Đất
        /// <summary>
        /// giá trị quyền sử dụng đất == nguyên giá
        /// </summary>
        public long? landUseRightValue { get; set; }
        /// <summary>
        ///diện tích
        /// </summary>
        public double? landArea { get; set; }
        /// <summary>
        /// Diện tích đất tăng/giảm
        /// </summary>
        public Double? landAreaIncreasement { get; set; }
        /// <summary>
        /// Diện tích đất trước khi tăng/giảm
        /// </summary>
        public Double? landAreaOld { get; set; }
        /// <summary>
        /// Mã địa bàn
        /// </summary>
        public int? landRegionId { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string landAddress { get; set; }
        /// <summary>
        /// Id quốc gia
        /// </summary>
        public long? landCountryId { get; set; }
        
        #region hồ sơ giấy tờ đất
        /// <summary>
        /// Ngày quyết định giao đất
        /// </summary>
        public string landDocumentDateLandAllocate { get; set; }
        /// <summary>
        /// Ngày quyết định cho thuê đất
        /// </summary>
        public string landDocumentDateLandLease { get; set; }
        /// <summary>
        /// Ngày chứng nhận quyền sử dụng đất
        /// </summary>
        public string landDocumentDateLandUseRightDate { get; set; }
        /// <summary>
        /// Ngày ký biên bản xác định giá trị quyền sử dụng đất
        /// </summary>
        public string landDocumentDateLandUseRightEvaluation { get; set; }
        /// <summary>
        /// Ngày ký hợp đồng chuyển nhượng quyền sử dụng đất
        /// </summary>
        public string landDocumentDateLandUseRightTransfer { get; set; }
        /// <summary>
        /// Số quyết định giao đất
        /// </summary>
        public string landDocumentLandAllocate { get; set; }
        /// <summary>
        /// Số quyết định cho thuê đất
        /// </summary>
        public string landDocumentLandLease { get; set; }
        /// <summary>
        /// Số giấy chứng nhận quyền sử dụng đất
        /// </summary>
        public string landDocumentLandUseRight { get; set; }
        /// <summary>
        /// Biên bản xác định giá trị quyền sử dụng đất
        /// </summary>
        public string landDocumentLandUseRightEvaluation { get; set; }
        /// <summary>
        /// Hợp đồng chuyển nhượng quyền sử dụng đất
        /// </summary>
        public string landDocumentLandUseRightTransfer { get; set; }
        //public string landDocumentLandLeaseContract { get; set; }//xóa
        //public string landDocumentDateLandLeaseContract { get; set; }//xóa

        #region thay đổi theo RnD_2020_Tài liệu mô tả service v4.1_20210111.doc
        /// <summary>
        /// //Ngày chứng từ pháp lý khác
        /// </summary>
        public DateTime? documentDateOther { get; set; }
        /// <summary>
        /// //Giá trên hóa đơn
        /// </summary>
        public long? documentValue { get; set; }
        /// <summary>
        /// //ID nhãn hiệu
        /// </summary>
        public int? brandId { get; set; }
        /// <summary>
        /// //Đơn vị nhận ủy thác quản lý
        /// </summary>
        public int? delegatingUnitId { get; set; }
        /// <summary>
        /// //Được miễn thuế
        /// </summary>
        public Boolean? isTaxExemption { get; set; }
        /// <summary>
        /// //Nguyên giá trích khấu hao
        /// </summary>
        public long? originalValueDepreciation { get; set; }
        /// <summary>
        /// //Đối tác (đơn vị được thuê quản lý, nhà cung cấp, khách hàng ...)
        /// </summary>
        public int? partnerId { get; set; }
        /// <summary>
        /// //ID hình thức mua sắm
        /// </summary>
        public int? procurementFormId { get; set; }
        /// <summary>
        /// // ID dòng sản phẩm
        /// </summary>
        public int? productLineId { get; set; }
        /// <summary>
        /// //Dòng sản phẩm
        /// </summary>
        public String productLineName { get; set; }
        /// <summary>
        /// // ID tuyến đường (đối với tài sản HTGTĐB)
        /// </summary>
        public int? routeId { get; set; }
        /// <summary>
        /// // Số thuế được miễn
        /// </summary>
        public long? taxExamptionValue { get; set; }
        /// <summary>
        /// //Ngày chứng nhận đăng ký xe
        /// </summary>
        public DateTime? vehicleRegistrationDocumentDate { get; set; }
        /// <summary>
        /// // Chức danh phục vụ ID
        /// </summary>
        public int? vehicleUserTitleId { get; set; }
        /// <summary>
        /// //Số hợp đồng cho thuê đất
        /// </summary>
        public String documentLeaseContract { get; set; }
        /// <summary>
        /// //Ngày hợp đồng cho thuê đất
        /// </summary>
        public String documentDateLeaseContract { get; set; }
        /// <summary>
        /// //Hình thức xử lý là bán / thanh lý
        /// </summary>
        public Boolean? assetHandlingFormSale { get; set; }
        /// <summary>
        /// //Số tiền thu được từ bán / thanh lý
        /// </summary>
        public long? assetHandlingFormValueObtained { get; set; }
        /// <summary>
        /// //Hình thức xử lý tài sản (khi giảm)
        /// </summary>
        public Decimal? assetHandlingFormId { get; set; }

        #endregion
        #endregion
        #endregion
        #region Nhà
        /// <summary>
        /// Diện tích sàn
        /// </summary>
        public string houseLandCode { get; set; }
        /// <summary>
        /// Số tầng nhà
        /// </summary>
        public long? houseNumberOfFloor { get; set; }
        /// <summary>
        /// Diện tích xây dựng
        /// </summary>
        public double? houseAreaBuilding { get; set; }
        /// <summary>
        /// Diện tích xây dựng tăng giảm
        /// </summary>
        public double? houseAreaBuildingIncreasement { get; set; }
        /// <summary>
        /// Diện tích xây dựng trước khi tăng giảm
        /// </summary>
        public double? houseAreaBuildingOld { get; set; }
        /// <summary>
        /// diện tích sàn
        /// </summary>
        public double? houseAreaFloor { get; set; }
        /// <summary>
        /// Địa chỉ nhà
        /// </summary>
        public string houseAddress { get; set; }
        /// <summary>
        /// Năm xây dựng
        /// </summary>
        public int? houseBuiltYear { get; set; }
        /// <summary>
        /// Diện tích sàn tăng/giảm
        /// </summary>
        public Double? houseAreaFloorIncreasement { get; set; }
        /// <summary>
        /// Diện tích sàn trước tăng/giảm
        /// </summary>
        public Double? houseAreaFloorOld { get; set; }
        //hồ sơ giấy tờ nhà
        /// <summary>
        /// Biên bản nghiệm thu
        /// </summary>
        public string documentAcceptance { get; set; }
        /// <summary>
        /// Ngày nghiệm thu
        /// </summary>
        public string documentDateAcceptance { get; set; }
        /// <summary>
        /// Ngày bàn giao
        /// </summary>
        public string documentDateDelivery { get; set; }
        /// <summary>
        /// Ngày ký hóa đơn/chứng từ
        /// </summary>
        public string documentDateReceipt { get; set; }
        /// <summary>
        /// Ngày quyết định
        /// </summary>
        public string documentDecisionDate { get; set; }
        /// <summary>
        /// Số quyết định
        /// </summary>
        public string documentDecisionNumber { get; set; }
        /// <summary>
        /// Biên bản bàn giao
        /// </summary>
        public string documentDelivery { get; set; }
        /// <summary>
        /// Chứng từ pháp lý khác
        /// </summary>
        public string documentOther { get; set; }
        /// <summary>
        /// Hóa đơn/chứng từ
        /// </summary>
        public string documentReceipt { get; set; }
        #endregion
        #region phương tiện vận tải
        /// <summary>
        /// Số khung
        /// </summary>
        public string vehicleChassisNumber { get; set; }
        /// <summary>
        /// Số máy
        /// </summary>
        public string vehicleEngineNumber { get; set; }
        /// <summary>
        /// Biển kiểm soát
        /// </summary>
        public string vehicleRegistrationPlateNumber { get; set; }
        /// <summary>
        /// số chỗ ngồi
        /// </summary>
        public long? vehicleSize { get; set; }
        /// <summary>
        /// Tải trọng
        /// </summary>
        public double? vehicleCapacity { get; set; }
        /// <summary>
        /// Công xuất
        /// </summary>
        public double? enginePower { get; set; }
        /// <summary>
        /// Dung tích xi lanh
        /// </summary>
        public string motorCylinder { get; set; }
        /// <summary>
        /// chức danh
        /// </summary>
        public string vehicleUserTitle { get; set; }
        /// <summary>
        /// Chức danh phục vụ khác
        /// </summary>
        public string vehicleUserTitleOther { get; set; }
        /// <summary>
        /// ID chức danh phục vụ khác
        /// </summary>
        public int? vehicleUserTitleOtherId { get; set; }
        /// <summary>
        /// Nước sản xuất
        /// </summary>
        public string countryOfOrigin { get; set; }
        /// <summary>
        /// Nhãn hiệu
        /// </summary>
        public string brandName { get; set; }
        /// <summary>
        ///  số cầu xe
        /// </summary>
        public int? vehicleNumberOfWheelDrives { get; set; }
        /// <summary>
        /// giấy chứng nhận đăng ký xe.
        /// </summary>
        public string vehicleRegistrationDocumentNumber { get; set; }
        /// <summary>
        /// cơ quan cấp đăng ký
        /// </summary>
        public string vehicleRegistrationIssuedAuthority { get; set; }
        #endregion
        #region Tài sản khác
        /// <summary>
        /// Số seri
        /// </summary>
        public string serialNumber { get; set; }
        /// <summary>
        /// Nhà cung cấp
        /// </summary>
        public string supplier { get; set; }
        /// <summary>
        /// Dung tích/thể tích
        /// </summary>
        public float? volume { get; set; }
        /// <summary>
        /// Chiều dài (m)
        /// </summary>
        public float? length { get; set; }
        /// <summary>
        /// Năm sản xuất/ Năm sinh của cây
        /// </summary>
        public int? plantYear { get; set; }
        #endregion
        /// <summary>
        /// Tỷ lệ khấu hao
        /// </summary>
        // public float depreciationRate { get; set; }
        /// <summary>
        /// Ngày nhập
        /// </summary>
        public string inputDate { get; set; }
        /// <summary>
        /// Lý do thay đổi
        /// </summary>
        public string changeReason { get; set; }
        /// <summary>
        /// Ghi chú
        /// </summary>
        public string notes { get; set; }
        /// <summary>
        /// Dung tích/thể tích tăng/giảm
        /// </summary>
        public double? volumeIncreasement { get; set; }
        /// <summary>
        /// Dung tích/thể tích cũ
        /// </summary>
        public double? volumeOld { get; set; }
        /// <summary>
        /// Chiều dài tăng/giảm (m)
        /// </summary>
        public double? lengthIncreasement { get; set; }
        /// <summary>
        /// Chiều dài cũ (m)
        /// </summary>
        public double? lengthOld { get; set; }
        /// <summary>
        /// Danh sách hiện trạng sử dụng
        /// </summary>
        public List<Kho_assetMutationAssetUsageStates> assetMutationAssetUsageStates { get; set; }
    }
    public class Kho_assetMutationAssetUsageStates
    {
        /// <summary>
        /// ID hiện trạng sử dụng
        /// </summary>
        public int? usageStateId { get; set; }
        /// <summary>
        /// Giá trị hiện trạng sử dụng
        /// </summary>
        public double? usageValue { get; set; }
    }
    public class Kho_MaTaiSan
    {
        public string assetCode { get; set; }
        public string syncSourceId { get; set; }
    }
    public class CapNhatIDBienDong
    {
        public decimal TaiSanId { get; set; }
        public decimal BienDongId { get; set; }
        public List<Decimal> ListBienDongId { get; set; }
    }
    public class Kho_DongBoBaoCao
    {
        public decimal ID { get; set; }
        public string MA_BAO_CAO { get; set; }
        public string MA_BAO_CAO_KHO { get; set; }
        public string DATA_JSON { get; set; }
    }
}
