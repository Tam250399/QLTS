using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GS.WebApi.Models.ConsumingApi.TaiSanToanDan
{
	public class DuLieuDongBoQuyetDinhTichThu
	{
		public DuLieuDongBoQuyetDinhTichThu()
		{
			this.data = new List<Kho_QuyetDinhTichThu>();
		}

		public List<Kho_QuyetDinhTichThu> data { get; set; }
	}
	public class Kho_QuyetDinhTichThu
	{
		public Kho_QuyetDinhTichThu()
		{
			decision = new confiscateDecision();
			assets = new List<nationalPublicAssets>();
		}
		public confiscateDecision decision { get; set; }
		public IList<nationalPublicAssets> assets { get; set; }
	}
	public class confiscateDecision
	{
		public string syncId { get; set; }
		public string decisionNumber { get; set; }
		public string decisionDate { get; set; }
		public string notes { get; set; }
		public string createdDate { get; set; }
		public string creator { get; set; }
		public int? unitId { get; set; }
		public int? issuedUnitId { get; set; }
		public int decisionType { get; set; }
		public int assetSourceId { get; set; }
		public string decider { get; set; }
		public string decisionName { get; set; }
		public long? id { get; set; }
	}
	public class nationalPublicAssets
	{
		public string syncId { get; set; }
		public int? decisionId { get; set; }
		public string assetName { get; set; }
        public string assetTypeName { get; set; }
        public int assetTypeGroupId { get; set; }
		public int? assetTypeId { get; set; }
		public double? assetValue { get; set; }
		public double? calculationUnitValue { get; set; }
		public string calculationUnitType { get; set; }
		public long? houseLandId { get; set; }
		public string houseAddress { get; set; }
		public int houseBuiltYear { get; set; }
		public double? houseAreaBuilding { get; set; }
		public long? brandId { get; set; }
		public long? productLineId { get; set; }
		public string vehicleRegistrationPlateNumber { get; set; }
		public int? vehicleSize { get; set; }
		public double? vehicleCapacity { get; set; }
		public int? vehicleNumberOfWheelDrives { get; set; }
		public string notes { get; set; }
		public long? id { get; set; }
	}
	public class Kho_MaDongBo
	{
		public confiscateDecision decision { get; set; }
		public nationalPublicAssets nationalPublicAssets { get; set; }
		public decimal? syncId { get; set; }
		public string id { get; set; }
		public object[] idList { get; set; }
	}
}
