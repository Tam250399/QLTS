import axios from "axios";
import { environment } from "../environments/environment";
const API_URL = `${environment.apiUrl}`;

interface quocgia {
  ma: string;
  ten: string;
  id: number;
}
export default async function GetDMQuocGia(): Promise<quocgia[]> {
  // const httpOptions = getNewAccessToken();
  try {
    const response = await axios.get(`${API_URL}/DanhMuc/quocGia`);
    console.log("response", response.data.data.results);

    return response.data.data.results;
  } catch (error) {
    console.error("Error fetching data:", error);
    throw error;
  }
}
