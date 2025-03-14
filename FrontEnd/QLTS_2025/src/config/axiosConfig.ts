import axios from "axios";
import { IBaseRequest } from "./Interfaces";

const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE,
  timeout: 5000,
  headers: {
    "Content-Type": "application/json",
  },
});

export function cleanUndefinedFields<T extends Record<string, unknown>>(
  data: T
): T {
  return Object.entries(data)
    .filter(([, value]) => value !== undefined)
    .reduce((obj, [key, value]) => {
      (obj as Record<string, unknown>)[key] = value;
      return obj;
    }, {} as T);
}

export function cleanParams<T extends Record<string, unknown>>(params: T): T {
  return Object.entries(params).reduce((acc, [key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
          (acc as Record<string, unknown>)[key] = value;
      }
      return acc;
  }, {} as T);
}

export async function requestNoAuth<T extends Record<string, unknown>>(
  config: IBaseRequest<T & Record<string, unknown>>
) {
  try {
    if (config.params && typeof config.params === "object") {
      config.params = cleanUndefinedFields(config.params);
    }
    const response = await axiosInstance(config)
      .then((res) => res.data)
      .catch((error) => {
        if (error.response) {
          return { error: error.response.data.message };
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log(error.message);
        }
      });
    return response;
  } catch (error) {
    console.log(error);
  }
}

export async function requestAuth<T>(config: IBaseRequest<T>) {
  try {
    const access_token = localStorage.getItem("access_token");

    if (config.params && typeof config.params === "object") {
        config.params = cleanParams(config.params as Record<string, unknown>) as T;
    }

    const response = await axiosInstance({
      ...config,
      headers: {
        Authorization: `Bearer ${access_token}`,
      },
    })
      .then((res) => res.data)
      .catch((error) => {
        if (error.response) {
          return { error: error.response.data.message };
        } else if (error.request) {
          console.log(error.request);
        } else {
          console.log(error.message);
        }
      });
    return response;
  } catch (error) {
    console.log(error);
  }
}
