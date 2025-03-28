import { BoPhanSuDung } from "../validateform/thongtinnha";
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
