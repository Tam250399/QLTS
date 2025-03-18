import axios from "axios";
import { environment } from "../environments/environment";
import { quocgia, Tinh } from "../validateform/thongtinchung";
const API_URL = `${environment.apiUrl}`;

export async function GetDMQuocGia(): Promise<quocgia[]> {
  // const httpOptions = getNewAccessToken();
  try {
    const response = await axios.get(`${API_URL}/DanhMuc/quocGia`);
    console.log("response", response.data.data.results);
    return response.data.data.results;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetDMTinhTP(quocGiaId: number): Promise<Tinh[]> {
  // const httpOptions = getNewAccessToken();
  try {
    const response = await axios.get(
      `${API_URL}/DanhMuc/tinhthanhpho?quocGiaId=${quocGiaId}`
    );
    console.log("response", response.data.data.results);
    return response.data.data.results;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}
