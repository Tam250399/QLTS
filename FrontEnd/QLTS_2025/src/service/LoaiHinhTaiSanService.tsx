import axios from "axios";
import { environment } from "../environments/environment";
import { LoaiHinhTSId } from "../validateform/loaihinhtaisanid";

const API_URL = `${environment.apiUrl}`;

export async function GetListIdLoaiHinhTS(): Promise<LoaiHinhTSId[]> {
  try {
    const response = await axios.get(`${API_URL}/LoaiTaiSans/tsCha`);
    const restponses = response.data.Data.Results;
    return restponses;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}
