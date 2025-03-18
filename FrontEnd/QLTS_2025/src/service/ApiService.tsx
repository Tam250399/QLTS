import axios, { AxiosResponse } from "axios";
import { environment } from "../environments/environment";

const API_URL = `${environment.apiUrl}`;
// Cấu hình axios
const apiClient = axios.create({
  baseURL: API_URL,
  timeout: 10000,
  headers: {
    "Content-Type": "application/json",
  },
});

// Interface cho dữ liệu (có thể tùy chỉnh theo API của bạn)
interface ApiResponse<T> {
  data: T;
  status: number;
  message?: string;
}

// API
class ApiService {
  // GET
  async get<T>(endpoint: string, params?: object): Promise<ApiResponse<T>> {
    try {
      const response: AxiosResponse<T> = await apiClient.get(endpoint, {
        params,
      });
      return {
        data: response.data,
        status: response.status,
      };
    } catch (error) {
      throw this.handleError(error);
    }
  }

  // POST
  async post<T>(endpoint: string, data: object): Promise<ApiResponse<T>> {
    try {
      const response: AxiosResponse<T> = await apiClient.post(endpoint, data);
      return {
        data: response.data,
        status: response.status,
      };
    } catch (error) {
      throw this.handleError(error);
    }
  }

  // PUT
  async put<T>(endpoint: string, data: object): Promise<ApiResponse<T>> {
    try {
      const response: AxiosResponse<T> = await apiClient.put(endpoint, data);
      return {
        data: response.data,
        status: response.status,
      };
    } catch (error) {
      throw this.handleError(error);
    }
  }

  // DELETE
  async delete<T>(endpoint: string): Promise<ApiResponse<T>> {
    try {
      const response: AxiosResponse<T> = await apiClient.delete(endpoint);
      return {
        data: response.data,
        status: response.status,
      };
    } catch (error) {
      throw this.handleError(error);
    }
  }

  // Xử lý lỗi
  private handleError(error: any) {
    if (axios.isAxiosError(error)) {
      return new Error(error.response?.data?.message || "API request failed");
    }
    return new Error("An unexpected error occurred");
  }
}

export default new ApiService();
