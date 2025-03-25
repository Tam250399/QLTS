import axios from "axios";
import { environment } from "../environments/environment";
import {
  LyDoTangDat,
  MucDichTS,
  Phuong,
  quocgia,
  Thongtinchung,
  Tinh,
} from "../validateform/thongtinchung";
const API_URL = `${environment.apiUrl}`;

export async function GetDMQuocGia(): Promise<quocgia[]> {
  try {
    const response = await axios.get(`${API_URL}/DanhMuc/quocGia`);

    console.log("responses", response.data.Data.Results);
    const restponses = response.data.Data.Results;
    return restponses;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetDMTinhTP(quocGiaId: number): Promise<Tinh[]> {
  try {
    const response = await axios.get(
      `${API_URL}/DanhMuc/tinhthanhpho?quocGiaId=${quocGiaId}`
    );
    console.log("response", response.data.Data.Results);
    const restponses = response.data.Data.Results;
    return restponses;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetDMDuoiTinh(maTinh: string): Promise<Phuong[]> {
  try {
    const response = await axios.get(
      `${API_URL}/DanhMuc/diaBanDuoiTinhTP?maCha=${maTinh}`
    );
    console.log("dưới tỉnh", response.data.Data.Results);
    return response.data.Data.Results;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetDMMucDichTS(
  loaiHinhTaiSanId: number
): Promise<MucDichTS[]> {
  try {
    const response = await axios.get(
      `${API_URL}/DanhMuc/mucDichSuDung?loaiHinhTaiSanId=${loaiHinhTaiSanId}`
    );
    console.log("response", response.data.Data.Results);
    return response.data.Data.Results;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}

export async function GetDMLyDoTangDat(
  loaiHinhTaiSanId: number
): Promise<LyDoTangDat[]> {
  try {
    const response = await axios.get(
      `${API_URL}/DanhMuc/lyDoTangGiam?loaiHinhTaiSanId=${loaiHinhTaiSanId}`
    );
    console.log("response", response.data.Data.Results);
    return response.data.Data.Results;
  } catch (error) {
    console.error("Không lấy được dữ liệu:", error);
    throw error;
  }
}
export async function PostThongTinTaiSan(data: Thongtinchung): Promise<void> {
  try {
    const response = await axios.post(`${API_URL}/TaiSan`, data);
    return response.data;
    // console.log("aaa", response);
    // if (response.data.Success) {
    //   console.log("Gửi dữ liệu thành công:", response.data);
    // } else {
    //   throw new Error(response.data.message || "Gửi dữ liệu thất bại");
    // }
  } catch (error) {
    console.error("Lỗi khi gửi dữ liệu:", error);
    throw error;
  }
}
