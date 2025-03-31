import { BoPhanSuDung, KhuonVienDat } from "../validateform/thongtinnha";
import { requestAuth } from "../config/axiosConfig";

export async function GetListBoPhanSD(param: any): Promise<BoPhanSuDung[]> {
  try {
    const response = await requestAuth({
      url: `/danhmuc/donViBoPhan`,
      method: "GET",
      params: param,
    });
    const restponses = response.Data.Results;
    return restponses;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetListChonDats(data: any): Promise<KhuonVienDat[]> {
  try {
    const response = await requestAuth({
      url: `/taisan/getTaiSanDatsWithDonVi`,
      method: "POST",
      data: data,
    });
    const restponses = response.Data.Results;
    return restponses;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}
